using Cysharp.Threading.Tasks;

namespace BattleModule
{
    public class BattleReslutNotify
    {
        public BattleReslutNotify(BattleResultView battleResultView)
        {
            _battleResultView = battleResultView;
        }

        public async UniTask NotifyResult()
        {
            
        }
        private readonly BattleResultView _battleResultView;
    }
}
