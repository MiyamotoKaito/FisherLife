using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// セーブデータの基底クラス
    /// </summary>
    public abstract class SaveBase
    {
        /// <summary>
        /// データを読み込み、値を取得する
        /// </summary>
        /// <returns></returns>
        public virtual UniTask ReadAsync()
        {
            if (PlayerPrefs.HasKey(_key))
            {
                var json = PlayerPrefs.GetString(_key);

                try
                {
                    //　値の上書き
                    JsonUtility.FromJsonOverwrite(_key, true);
                }
                catch(Exception ex)
                {
                    Debug.LogError($"セーブデータの読み込みに失敗しました。初期値を返します。{_key}\n{ex.Message}");
                }
            }

            return UniTask.CompletedTask;
        }
        /// <summary>
        ///     データを書き込み、値を保持する
        /// </summary>
        /// <returns></returns>
        public virtual UniTask WriteAsync()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_key, json);
            PlayerPrefs.Save();// 明示的にディスクへ反映(クラッシュしても消えないように)
            return UniTask.CompletedTask;
        }

        public virtual UniTask DeleteAsync()
        {
            PlayerPrefs.DeleteKey(_key);
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }
        /// <summary>PlayerPrefsの鍵。型名はクラスの名前</summary>
        protected virtual string _key => GetType().FullName;
    }
}
