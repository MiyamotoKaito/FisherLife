using Common;
using System.Collections.Generic;

namespace TypingModule
{
    /// <summary>
    /// 単語をレベル別に分けるUsecase
    /// </summary>
    public class WordSeparatorUsecase : IWordSeparatorUsecase
    {
        /// <summary>
        /// 与えられたテキストファイルをカンマ区切りで辞書に代入する
        /// </summary>
        /// <param name="text"></param>
        public Dictionary<uint, string[]> WordSeparate(string text)
        {
            var dictionaty = new Dictionary<uint, string[]>();

            var lines = text.Split("\n");
            foreach (var line in lines)
            {
                var num = line[0].ToString();//TryParse用にStringに変換
                if (!uint.TryParse(num, out uint resule))
                {
                    continue;
                }
                var words = line.Split(",", line[0]);// カンマとレベルで区切る

                dictionaty[resule] = words;
            }

            return dictionaty;
        }
    }
}
