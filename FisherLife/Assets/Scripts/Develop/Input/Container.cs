using VContainer;
using VContainer.Unity;

public class Container : LifetimeScope
{

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<FisherLifeInputActions>(Lifetime.Singleton);
        builder.Register<InputActionMapSwitcher>(Lifetime.Singleton);
        builder.Register<TypingInputHandler>(Lifetime.Singleton);
    }
}
