using System.Collections.Generic;

namespace Common
{
    public interface IWordSeparatorUsecase
    {
        Dictionary<uint, string[]> WordSeparate(string text);
    }
}
