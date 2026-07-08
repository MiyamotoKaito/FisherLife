using Cysharp.Threading.Tasks;

namespace ShopModule
{
    public class NextPanelItem : ItemBase
    {
        public override UniTask<bool> IsTrade()
        {
            throw new System.NotImplementedException();
        }

        public override UniTask Trade()
        {
            throw new System.NotImplementedException();
        }
    }
}
