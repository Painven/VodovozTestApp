using System;
using System.Windows.Input;

namespace VodovozTestApp.Infrastructure.Commands;

public class LambdaCommand : CommandBase
{
    private readonly Action<object> action;
    private readonly Func<object, bool> canExecute;
    private string? login;
    private Func<object, bool> p;

    public LambdaCommand(Action<object> action, Func<object, bool> canExecute = null)
    {
        this.action = action ?? throw new ArgumentNullException(nameof(action));
        this.canExecute = canExecute;
    }

    public LambdaCommand(string? login, Func<object, bool> p)
    {
        this.login = login;
        this.p = p;
    }

    public override bool CanExecute(object parameter)
    {
        return canExecute?.Invoke(parameter) ?? true;
    }

    public override void Execute(object parameter)
    {
        action(parameter);
    }
}
