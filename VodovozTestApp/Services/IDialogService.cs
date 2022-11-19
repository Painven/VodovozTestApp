using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VodovozTestApp.Services;

public interface IDialogService
{
    void ShowInformation(string message, string? title = null);
    void ShowError(string message, string? title = null);
    bool ShowConfirmDialog(string message, string? title = null);
}

public class MessageBoxDialogService : IDialogService
{
    public bool ShowConfirmDialog(string message, string? title = null)
    {
        var result = MessageBox.Show(message, title ?? "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        return result == MessageBoxResult.OK;
    }

    public void ShowError(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void ShowInformation(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
