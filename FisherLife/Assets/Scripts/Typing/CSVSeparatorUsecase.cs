using Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypingModule
{
    /// <summary>
    /// 単語をレベル別に分けるUsecase
    /// </summary>
    public class CSVSeparatorUsecase : IWordSeparatorUsecase
    {
        /// <summary>
        /// 与えられたテキストファイルをカンマ区切りで辞書に代入する
        /// </summary>
        /// <param name="text"></param>
        public async ValueTask<Dictionary<uint, string[]>> WordSeparate(string text)
        {
            var dictionaty = new Dictionary<uint, string[]>();

            var lines = text.Split('\n', '\r');
            foreach (var line in lines)
            {
                var str = line.Trim().Split(",");
                var num = str[0].ToString();//TryParse用にStringに変換
                if (!uint.TryParse(num, out uint resule))
                {
                    continue;
                }
                var words = new List<string>();
                for (int i = 2; i < str.Length; i++)
                {
                    if (!string.IsNullOrEmpty(str[i])) 
                        words.Add(str[i]);
                }

                dictionaty[resule] = words.ToArray();
            }

            return dictionaty;
        }
    }
}
