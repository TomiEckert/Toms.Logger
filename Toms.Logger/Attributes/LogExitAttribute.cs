using AspectInjector.Broker;

namespace Toms.Logger.Attributes;

[Aspect(Scope.Global)]
[Injection(typeof(LogExitAttribute))]
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class LogExitAttribute : Attribute
{
    public LogExitAttribute()
    {

    }

    [Advice(Kind.After)]
    public void LogAfter([Argument(Source.Name)] string name)
    {
        var entry = LogEntry.Debug(name, $"Exiting {name} successfully...", LogService.Instance.UseColors);
        LogService.Instance.Add(entry);
    }
}