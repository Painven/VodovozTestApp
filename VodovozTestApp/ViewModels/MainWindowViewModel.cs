using System;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;

namespace VodovozTestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    string title = "Тестовое приложение. Веселый Водовоз";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    public ICommand LoadedCommand { get; }

    public MainWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(e => Title += $" загружено в {DateTime.Now}");
    }
}
