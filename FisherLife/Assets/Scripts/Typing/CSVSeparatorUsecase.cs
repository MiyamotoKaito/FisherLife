using System.Collections.Generic;
using System.Threading.Tasks;
using Commons;

namespace TypingModule
{
    /// <summary>
    ///     単語をレベル別に分けるユースケース。
    /// </summary>
    public class CSVSeparatorUsecase : IWordSeparatorUsecase
    {
        /// <summary>
        ///     与えられたテキストをカンマ区切りで解析し、レベルごとの単語配列へ変換する。
        /// </summary>
        public async ValueTask<Dictionary<uint, string[]>> WordSeparate(string text)
        {
            var dictionary = new Dictionary<uint, string[]>();

            // 改行で行に分割する。
            var lines = text.Split('\n', '\r');
            foreach (var line in lines)
            {
                var columns = line.Trim().Split(",");

                // 先頭列をレベルとして解析できない行は飛ばす。
                if (!uint.TryParse(columns[0], out uint level))
                {
                    continue;
                }

                // 単語列を読み取り、空でないものだけ集める。
                var words = new List<string>();
                for (int i = WORD_START_INDEX; i < columns.Length; i++)
                {
                    if (!string.IsNullOrEmpty(columns[i]))
                    {
                        words.Add(columns[i]);
                    }
                }

                dictionary[level] = words.ToArray();
            }

            return dictionary;
        }

        private const int WORD_START_INDEX = 2;
    }
}
