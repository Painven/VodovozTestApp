using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class DepartmentsListViewModel : ViewModelBase
{
    private readonly IDepartmentRepository departmentRepository;
    private readonly IMapper mapper;

    public ICommand LoadedCommand { get; }

    public ObservableCollection<DepartmentModel> Departments { get; set; } = new()
    {
        new DepartmentModel() { Name = "Тестовый отдел 1"},
        new DepartmentModel() { Name = "Тестовый отдел 2"},
        new DepartmentModel() { Name = "Тестовый отдел 3"},
        new DepartmentModel() { Name = "Тестовый отдел 4"},
        new DepartmentModel() { Name = "Тестовый отдел 5"},
    };

    public DepartmentsListViewModel()
    {
        LoadedCommand = new LambdaCommand(LoadDepartments);
    }
    public DepartmentsListViewModel(IDepartmentRepository departmentRepository, IMapper mapper) : this()
    {        
        this.departmentRepository = departmentRepository;
        this.mapper = mapper;
    }

    private async void LoadDepartments(object obj)
    {
        Departments.Clear();

        var departments = await departmentRepository.GetAll();
        departments.ForEach(d =>
        {
            Departments.Add(mapper.Map<DepartmentModel>(d));
        });
    }
}

