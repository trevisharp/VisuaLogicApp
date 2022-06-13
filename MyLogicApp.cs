global using global::VisualLogic;
global using global::VisualLogic.Elements;

public class MyLogicApp : LogicApp
{
    private int size = 50;
    protected override DIBuilder DefineDependencyInjection(DIBuilder builder)
    {
        builder.AddMethod("logic")
            .AddInstance(new VisualArray(0, 1000, size));
        return builder;
    }

    protected override void LoadFromParams(params object[] args)
    {
        if (args.Length > 0)
            size = (int)args[0];
    }

    protected override void SetRunHooks()
    {
        AddRunHook("logic", HookType.OnAppStart);
    }
}