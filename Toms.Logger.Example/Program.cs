using Toms.Logger.Attributes;

namespace Toms.Logger.Example;

internal class Program
{
    private static void Main(string[] args)
    {
        var conf = LogConfigBuilder.Default
            .SetDisplayLevel(LogLevel.Debug)
            .SetUseFullClassName(false)
            .SaveToFile(false)
            .Build();
        var l = new Logger(conf);
        var myInstance = new MyClass(l);
        myInstance.MyMethod();
        myInstance.MyMethodWithAttr();
        Console.WriteLine();
    }
}

class MyClass
{
    Logger Logger {get;}

    public MyClass(Logger logger)
    {
        Logger = logger;
    }

    public void MyMethod() {
        Logger.Debug("First message");
        Logger.Info("2nd text");
        Logger.Warning("This is a warning message");
        Logger.Error("Error message");
    }

    [LogCall]
    [LogExit]
    public void MyMethodWithAttr() {
        Logger.Info("Inside the method");
    }
}