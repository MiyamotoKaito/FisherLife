using Commons;
using InputModule;
using PlayerModule;
using StateMachine;
using TypingModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private TextAsset _textAsset;
        protected override void Configure(IContainerBuilder builder)
        {
            #region Stateの登録
            builder.Register<IWorldStateMachine, WorldStateMachine>(Lifetime.Singleton);
            #endregion
        }
    }
}