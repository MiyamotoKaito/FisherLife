using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons
{
    /// <summary>
    ///     単語をレベル別に分割するユースケースのインターフェース。
    /// </summary>
    public interface IWordSeparatorUsecase
    {
        /// <summary>
        ///     テキストを解析し、レベルごとの単語配列へ変換する。
        /// </summary>
        ValueTask<Dictionary<uint, string[]>> WordSeparate(string text);
    }
}
