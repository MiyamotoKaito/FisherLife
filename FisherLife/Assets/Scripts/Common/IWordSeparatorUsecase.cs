using System.Collections.Generic;

namespace Common
{
    public interface IWordSeparatorUsecase
    {
        Dictionary<int, string[]> WordDictionary { get; }
        void WordSeparate();
    }
}
