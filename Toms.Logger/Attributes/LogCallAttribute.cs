using AspectInjector.Broker;

namespace Toms.Logger.Attributes;

[Aspect(Scope.Global)]
[Injection(typeof(LogCallAttribute))]
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class LogCallAttribute : Attribute
{
    public LogCallAttribute()
    {
        
    }

    [Advice(Kind.Before)]
    public void LogBefore([Argument(Source.Name)] string name)
    {
        var entry = LogEntry.Debug(name, $"Calling {name}...", LogService.Instance.UseColors);
        LogService.Instance.Add(entry);
    }
}