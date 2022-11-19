using System;
using System.Collections.Generic;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.ViewModels;

namespace VodovozTestApp.Models;

public class DepartmentModel : ViewModelBase
{
    public event Action<DepartmentModel> OnDeleteClicked;
    public event Action<DepartmentModel> OnEditClicked;
    public event Action<DepartmentModel> OnShowDetailsClicked;

    public int DepartmentID { get; set; }
    public int? LeadID { get; set; }

    string name;
    public string Name
    {
        get => name;
        set => Set(ref name, value);
    }

    EmployeeModel? leader;
    public EmployeeModel? Leader
    {
        get => leader;
        set => Set(ref leader, value);
    }

    public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

    public ICommand ShowDeleteDialogCommand { get; }
    public ICommand ShowEditWindowCommand { get; }
    public ICommand ShowDetailsWindowCommand { get; }

    public DepartmentModel()
    {
        ShowDeleteDialogCommand = new LambdaCommand(e => OnDeleteClicked?.Invoke(this));
        ShowEditWindowCommand = new LambdaCommand(e => OnEditClicked?.Invoke(this));
        ShowDetailsWindowCommand = new LambdaCommand(e => OnShowDetailsClicked?.Invoke(this));
    }
}
