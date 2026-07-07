using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// 実際にセーブデータをセーブ、ロードするクラス
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// セーブデータを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static UniTask<T> LoadAsync<T>() where T : SaveBase, new()
        {
            var type = typeof(T);

            lock (_lock)
            {
                if (_cache.TryGetValue(type, out var cache))
                {
                    return new((T)cache);
                }
            }

            return LoadCoreAsync<T>();
        }
        /// <summary>
        /// セーブデータがキャッシュが無かった場合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static async UniTask<T> LoadCoreAsync<T>() where T : SaveBase, new()
        {
            var data = new T();
            await data.ReadAsync();

            lock (_lock)
            {
                // awaitしている間に別の呼び出しが先に登録した場合の再チェック
                if (_cache.TryGetValue(typeof(T), out var existing))
                {
                    return (T)existing;
                }
                _cache[typeof(T)] = data;
            }

            return data;
        }
        /// <summary>
        /// データをセーブする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask SaveAsync<T>() where T : SaveBase, new()
        {
            var data = await LoadAsync<T>();
            await data.WriteAsync();
        }

        public static async UniTask SaveAll()
        {
            List<SaveBase> target;
            lock (_lock)
            {
                target = new List<SaveBase>(_cache.Values);
            }

            foreach (var data in target)
            {
                await data.WriteAsync();
            }
        }
        /// <summary>
        /// データを削除する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static UniTask DeleteAsync<T>() where T : SaveBase, new()
        {
            lock (_lock)
            {
                _cache.Remove(typeof(T));
            }

            // キーが型ごとに異なるので、新規インスタンスを通して削除する
            return new T().DeleteAsync();
        }
#if UNITY_EDITOR 
        [RuntimeInitializeOnLoadMethod]
        private static void ResetCache()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }
#endif
        private static readonly Dictionary<Type, SaveBase> _cache = new();
        private static readonly object _lock = new object();
    }
}
