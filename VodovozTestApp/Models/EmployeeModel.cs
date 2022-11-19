using System;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.ViewModels;

namespace VodovozTestApp.Models;

public class EmployeeModel : ViewModelBase
{
    public event Action<EmployeeModel> OnDeleteClicked;
    public event Action<EmployeeModel> OnEditClicked;
    public event Action<EmployeeModel> OnShowDetailsClicked;

    public int EmployeeID { get; set; }
    public int? DepartmentID { get; set; }

    string firstName;
    public string FirstName
    {
        get => firstName;
        set
        {
            if (Set(ref firstName, value))
            {
                RaisePropertyChanged(nameof(FullDisplayName));
            }
        }
    }

    string lastName;
    public string LastName
    {
        get => lastName;
        set
        {
            if (Set(ref lastName, value))
            {
                RaisePropertyChanged(nameof(FullDisplayName));
            }
        }
    }

    string middleName;
    public string MiddleName
    {
        get => middleName;
        set
        {
            if(Set(ref middleName, value))
            {
                RaisePropertyChanged(nameof(FullDisplayName));
            }
        }
    }

    public string FullDisplayName
    {
        get => $"{FirstName} {LastName} {MiddleName ?? ""}".Trim();
    }

    DateTime? dateOfBirth;
    public DateTime? DateOfBirth
    {
        get => dateOfBirth;
        set
        {
            if(Set(ref dateOfBirth, value))
            {
                RaisePropertyChanged(nameof(Age));
            }
        }
    }

    Sex sex = Sex.Other;
    public Sex Sex
    {
        get => sex;
        set => Set(ref sex, value);
    }

    DepartmentModel? department;
    public DepartmentModel? Department
    {
        get => department;
        set => Set(ref department, value);
    }

    public int? Age
    {
        get
        {
            if (DateOfBirth.HasValue)
            {
                DateTime zero = new DateTime(1, 1, 1);
                TimeSpan span = DateTime.Now - DateOfBirth.Value;
                return (zero + span).Year - 1;
            }
            return null;
        }
    }

    public ICommand ShowDeleteDialogCommand { get; }
    public ICommand ShowEditWindowCommand { get; }
    public ICommand ShowDetailsWindowCommand { get; }

    public EmployeeModel()
    {
        ShowDeleteDialogCommand = new LambdaCommand(e => OnDeleteClicked?.Invoke(this));
        ShowEditWindowCommand = new LambdaCommand(e => OnEditClicked?.Invoke(this));
        ShowDetailsWindowCommand = new LambdaCommand(e => OnShowDetailsClicked?.Invoke(this));
    }
}
