# Toms Logger

This is a simple logging library that allows you to log messages with different levels of severity. It also supports logging method calls and exits, and saving logs to a file.

## Installation

You can install the Toms Logger library via NuGet:

```
PM> Install-Package Toms.Logger
```

## Usage

### Logger Class

The `Logger` class is the main class of the library. You can create an instance of it and use its methods to log messages.

```csharp
var logger = new Logger();
```

The `Logger` class has methods for logging messages at different levels of severity:

- `Debug`: Logs a debug message.
- `Info`: Logs an informational message.
- `Warning`: Logs a warning message.
- `Error`: Logs an error message.

```csharp
logger.Debug("This is a debug message");
logger.Info("This is an informational message");
logger.Warning("This is a warning message");
logger.Error("This is an error message");
```

You can also save the logs to a file:

```csharp
logger.SaveToFile("log.txt");
```

### LogCall and LogExit Attributes

The `LogCall` and `LogExit` attributes can be used to log method calls and exits. They are always `Debug`.

```csharp
[LogCall]
[LogExit]
public void MyMethodWithAttr() {
    logger.Info("Inside the method");
}
```

This will log a message both when the method is called and when it exits.

## Configuration

The `LogConfig` class is used to configure the logger. You can set the display level, whether to use full class names, and whether to save logs to a file.

```csharp
var conf = LogConfigBuilder.Default
    .SetDisplayLevel(LogLevel.Debug)
    .SetUseFullClassName(false)
    .SaveToFile(false)
    .Build();
```

## Example

Here's an example of how to use the logger:

```csharp
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

var conf = LogConfigBuilder.Default
    .SetDisplayLevel(LogLevel.Debug)
    .SetUseFullClassName(false)
    .SaveToFile(false)
    .Build();
var l = new Logger(conf);
var myInstance = new MyClass(l);
myInstance.MyMethod();
myInstance.MyMethodWithAttr();
```

This will log the messages from `MyMethod` and the calls and exits of `MyMethodWithAttr`.