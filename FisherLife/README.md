# FisherLife

**釣り × タイピング** の見下ろし型3Dゲーム（個人開発 / Unity 6）。
魚を釣り、食いついた瞬間に始まる**タイピングバトル**で単語を打ち切って魚を釣り上げる。釣った魚を売り、竿を買い替え・装備して、より強い魚を狙う——という「釣る→売る→強くする」のループを実装。

---

## 🎮 ゲーム内容

| フェーズ | 内容 |
|---|---|
| **移動** | 見下ろし視点でフィールドを歩き、釣り場・店に近づいてインタラクト |
| **釣り** | 釣り場でキャストするとスポットへ向いて投げ、竿レベルに応じた魚が水しぶきと共に出現 |
| **タイピングバトル** | 食いつくとタイピング開始。単語を1つ打ち切るごとに魚へ攻撃。制限時間内にHPを削れば **釣果**、失敗で **逃走** |
| **売買（店）** | 別シーンの店へ。釣った魚を売る／竿を買う。所持金・在庫・所持金不足を反映したUI |
| **装備** | 所持している竿を切り替え。装備竿が釣り・戦闘のパラメータに即反映 |

- 魚のスポーン確率：**90%が現在の竿レベル / 10%が1つ上のレベル**（上位が無ければ現レベルにフォールバック）
- タイピングの**出題単語レベルが魚のレベルに連動**（CSVからレベル別に読み込み）
- 釣った魚は**図鑑データ（種類ごとの所持数＋入手済みフラグ）**として保存

---

## 🛠 技術スタック

- **エンジン**: Unity 6 (6000.3.10f1) / **URP**
- **DI**: VContainer（`LifetimeScope` によるスコープ設計）
- **リアクティブ**: R3（`ReactiveProperty` / `Observable`）
- **非同期**: UniTask
- **入力**: Input System（`InputActionAsset` + 独自のアクションマップ・スタック管理）
- **演出**: DOTween（シーン遷移・数値アニメ）、Particle System / VFX、Cinemachine
- **設計**: MVP（Model-View-Presenter）＋ クリーンアーキテクチャ志向 ＋ **アセンブリ分割**

---

## 🧱 アーキテクチャ

### モジュール分割（asmdef × 11）
インターフェース・列挙型・データ契約を **`Commons`** に集約し、各機能モジュールは Commons のインターフェース越しに疎結合。

```
Commons      … 全モジュールが参照するインターフェース/enum/データ契約
Utility      … セーブシステム、シーン遷移、共通ヘルパー
StateMachine … ワールドステートマシン本体
InputModule  … InputActionMapのスタック管理、タイピング入力
PlayerModule … プレイヤー移動・インタラクト・装備インベントリ
FishModule   … 魚の生成/モデル、スポット、マスタデータ(SO)
FishingModule… 釣りフロー制御、釣果パネル、モードレジストリ
TypingModule … 出題・入力照合・攻撃通知
ShopModule   … 売買パネル、所持金
BattleModule … ダメージ計算・攻撃パイプライン・戦闘ユースケース
Container    … VContainerの各LifetimeScope（依存登録の集約点）
```

### 依存性注入（VContainer）
シーン・ライフサイクル単位でスコープを分離し、**生成/破棄をDIに委ねる**。

```
RootLifetimeScope (DontDestroyOnLoad)
 └ InGameLifetimeScope        … StateMachine / Battle / 魚マスタ / 装備 など共有
     ├ PlayerLifetimeScope    … 移動・装備
     ├ FishingLifetimeScope   … 釣り・魚生成
     ├ TypingLifetimeScope    … タイピング
     └ ShoppingLifetimeScope  … 売買
```

### ワールドステートマシン（スタック型）
`Moving / Fishing / Typing / Shopping / Equipment` などを**スタックで push/pop**。状態遷移と **InputActionMap のスタック**を連動させ、「今の状態でだけ有効な入力」を保証。`ChangeState`（積む）/ `BackState`（戻る）で釣り→タイピング→釣り→移動の入れ子遷移を管理。

### MVP
`Model`（状態・ロジック / R3で購読可能）— `View`（MonoBehaviour / 表示）— `Presenter`（購読して橋渡し）。
例：`FishModel`/`FishView`/`FishPresenter`、`MoneyModel`/`MoneyView`/`MoneyPresenter`、プレイヤー移動。

### セーブシステム
静的 `SaveSystem` が **PlayerPrefs + JsonUtility** を型キーでキャッシュしつつ読み書き。
セーブデータ：`MoneyData`（所持金）/ `FishCountData`（図鑑）/ `RodCountData`（所持竿・装備中の竿）。

---

## 📂 主なディレクトリ

```
Assets/Scripts/
├ Common/        インターフェース・enum（依存の中心）
├ Container/     LifetimeScope（DI登録）
├ StateMachine/  WorldStateMachine
├ Input/         入力（マップスタック・タイピング入力）
├ Player/        移動・インタラクト・装備インベントリ
├ Fishing/       釣りフロー・魚・スポット
├ Typing/        出題・照合・攻撃通知
├ Shop/          売買UI・所持金
├ Battle/        ダメージ計算・戦闘ユースケース
└ Utility/       セーブ・シーン遷移・共通
```

---

## 💡 設計上こだわった点

- **モジュール間はインターフェース依存**：具象を `Commons` のインターフェースに逃がし、機能追加時の影響範囲を局所化（例：タイピング以外の「釣りモード」も `IFishingModeController` で差し替え可能な形）。
- **状態と入力の一元管理**：ステート遷移に合わせてアクションマップをスタック制御し、状態ごとの入力有効/無効を宣言的に。
- **リアクティブなUI**：所持金・HPなどを `ReactiveProperty` にし、View は購読して自動更新（DOTweenで数値アニメ）。
- **非同期フローの明示化**：釣り→待機→戦闘→保存→復帰を UniTask で逐次記述し、`CancellationToken` で戦闘ごとに購読を確実に破棄（状態遷移中の不正な攻撃発火を防止）。

---

## ▶ 動作環境

- Unity **6000.3.10f1**（URP）
- 主要ライブラリ：VContainer / R3 / UniTask / DOTween / Input System / Cinemachine

> 個人開発。企画・設計・実装を担当。
