#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Utility;

namespace EditorTools
{
    /// <summary>
    ///     テスト用にセーブデータを削除するエディタメニュー。実機ビルドには含まれない。
    ///     Unity上部の「Debug/Save」から実行する。
    /// </summary>
    public static class SaveDebugMenu
    {
        [MenuItem("Debug/Save/Delete RodCountData (所持竿)")]
        private static void DeleteRod() => Delete<RodCountData>();

        [MenuItem("Debug/Save/Delete FishCountData (所持魚)")]
        private static void DeleteFish() => Delete<FishCountData>();

        [MenuItem("Debug/Save/Delete MoneyData (所持金)")]
        private static void DeleteMoney() => Delete<MoneyData>();

        [MenuItem("Debug/Save/Delete All Save (全消し)")]
        private static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("[SaveDebug] 全セーブを削除しました。");
        }

        // キーは型のFullName（SaveBase._key と同じ規則）。
        private static void Delete<T>()
        {
            var key = typeof(T).FullName;
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            Debug.Log($"[SaveDebug] {key} を削除しました。");
        }
    }
}
#endif
