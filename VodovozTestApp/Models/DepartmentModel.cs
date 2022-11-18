using System;
using System.Collections.Generic;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;

namespace VodovozTestApp.Models;

public class DepartmentModel
{
    public event Action<DepartmentModel> OnDeleteClicked;
    public event Action<DepartmentModel> OnEditClicked;
    public event Action<DepartmentModel> OnShowDetailsClicked;

    public int DepartmentID { get; set; }
    public string Name { get; set; }
    public EmployeeModel Leader { get; set; }
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
