using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons
{
    public interface IWordSeparatorUsecase
    {
        ValueTask<Dictionary<uint, string[]>> WordSeparate(string text);
    }
}
