using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class AddEmployeeWindowViewModel : ViewModelBase
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IDepartmentRepository departmentRepository;
    private readonly IMapper mapper;

    public ObservableCollection<DepartmentModel> Departments { get;  } = new();

    EmployeeModel contextEmployee = new();
    public EmployeeModel ContextEmployee
    {
        get => contextEmployee;
        set
        {
            if (Set(ref contextEmployee, value))
            {
                Title = "Редактирование данных работника";
                ButtonActionText = "Обновить";
            }
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand LoadedCommand { get; }
    public ICommand ChangeSexCommand { get; }

    string title = "Добавление сотрудника";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    string buttonActionText = "Добавить";
    public string ButtonActionText
    {
        get => buttonActionText;
        set => Set(ref buttonActionText, value);
    }

    public bool CanSave
    {
        get
        {
            return !string.IsNullOrWhiteSpace(ContextEmployee.FirstName) &&
                !string.IsNullOrWhiteSpace(ContextEmployee.LastName) &&
                ContextEmployee.Department != null;
        }
    }

    public AddEmployeeWindowViewModel()
    {
        ChangeSexCommand = new LambdaCommand(ChangeSex);
        SaveCommand = new LambdaCommand(async e => await AddEmployee(e), e => CanSave);
        LoadedCommand = new LambdaCommand(async e => await Loaded());
        ContextEmployee.PropertyChanged += (o, e) => RaisePropertyChanged(nameof(CanSave));
    }

    private void ChangeSex(object obj)
    {
        ContextEmployee.Sex = Enum.Parse<Sex>(obj.ToString());
    }

    public AddEmployeeWindowViewModel(IEmployeeRepository employeeRepository, 
        IDepartmentRepository departmentRepository, 
        IMapper mapper) : this()
    {
        this.employeeRepository = employeeRepository;
        this.departmentRepository = departmentRepository;
        this.mapper = mapper;
    }

    private async Task Loaded()
    {
        var departments = mapper.Map<IEnumerable<DepartmentModel>>(await departmentRepository.GetAll());
        Departments.ClearAndAddRange(departments);

        if (ContextEmployee.Department != null)
        {
            ContextEmployee.Department = Departments.Single(d => d.DepartmentID == ContextEmployee.Department.DepartmentID);
        }
    }

    private async Task AddEmployee(object o)
    {
        var employee = mapper.Map<Employee>(ContextEmployee);   
        employee.department_id = ContextEmployee.Department.DepartmentID;
        await employeeRepository.AddOrUpdate(employee);
        (o as Window).DialogResult = true;
    }
}
