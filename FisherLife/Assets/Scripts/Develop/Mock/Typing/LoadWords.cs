using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// CSV形式のテキストアセットをレベルごとに分割するして辞書を作成するクラス
    /// </summary>
    public class LoadWords
    {
        public ReadOnlyDictionary<int, List<string>> WordDictionary => _readOnlyDictionary;

        private ReadOnlyDictionary<int, List<string>> _readOnlyDictionary;
        private Dictionary<int, List<string>> _wordDictionary = new();

        public LoadWords(TextAsset texts)
        {
            GenerateDictionary(texts);
        }

        /// <summary>
        /// 辞書作成
        /// </summary>
        /// <param name="texts"></param>
        private void GenerateDictionary(TextAsset texts)
        {
            var lines = texts.text.Split("\n");

            for (var i = 1; i < lines.Length; i++)
            {
                ParseWords(lines[i]);
            }

            _readOnlyDictionary = new ReadOnlyDictionary<int, List<string>>(_wordDictionary);
        }

        /// <summary>
        /// 与えられた文字列をレベル別の辞書に変換
        /// </summary>
        /// <param name="words"></param>
        private void ParseWords(string words)
        {
            var wordArray = words.Trim().Split(',');

            // 始めの要素はレベル(数字)なので型変換
            if (!int.TryParse(wordArray[0], out int level))
            {
                return;
            }

            // リストの初期化
            if (!_wordDictionary.ContainsKey(level))
            {
                _wordDictionary[level] = new List<string>();
            }

            // レベルに対応した文字を追加
            for (int i = 1; i < wordArray.Length; i++)
            {
                _wordDictionary[level].Add(wordArray[i]);
            }
        }
    }
}