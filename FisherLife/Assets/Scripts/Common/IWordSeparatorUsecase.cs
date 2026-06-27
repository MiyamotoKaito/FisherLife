using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common
{
    public interface IWordSeparatorUsecase
    {
        ValueTask<Dictionary<uint, string[]>> WordSeparate(string text);
    }
}
