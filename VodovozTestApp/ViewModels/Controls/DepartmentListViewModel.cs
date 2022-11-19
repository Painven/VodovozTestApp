using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class DepartmentsListViewModel : ViewModelBase
{
    private readonly IDepartmentRepository departmentRepository;
    private readonly IMapper mapper;
    private readonly IWindowService windowService;
    private readonly IDialogService dialogService;

    public ICommand LoadedCommand { get; }
    public ICommand AddNewDepartmentCommand { get; }

    public ObservableCollection<DepartmentModel> Departments { get; set; } = new()
    {
        new DepartmentModel() { Name = "Тестовый отдел 1"},
        new DepartmentModel() { Name = "Тестовый отдел 2"},
        new DepartmentModel() { Name = "Тестовый отдел 3"},
        new DepartmentModel() { Name = "Тестовый отдел 4"},
        new DepartmentModel() { Name = "Тестовый отдел 5"},
    };

    bool isLoadingInProgress;
    public bool IsLoadingInProgress
    {
        get => isLoadingInProgress;
        set => Set(ref isLoadingInProgress, value);
    }

    public DepartmentsListViewModel()
    {
        LoadedCommand = new LambdaCommand(async e => await LoadDepartments(null));
        AddNewDepartmentCommand = new LambdaCommand(e => windowService.ShowAddDepartmentWindow());
    }
    
    public DepartmentsListViewModel(IDepartmentRepository departmentRepository, 
        IMapper mapper, 
        IWindowService windowService,
        IDialogService dialogService) : this()
    {        
        this.departmentRepository = departmentRepository;
        this.mapper = mapper;
        this.windowService = windowService;
        this.dialogService = dialogService;
    }

    private async Task LoadDepartments(object obj)
    {
        Departments.Clear();
        IsLoadingInProgress = true;

        var departments = mapper.Map<IEnumerable<DepartmentModel>>(await departmentRepository.GetAll());

        foreach(var department in departments)
        {
            department.OnDeleteClicked += async (e) =>
            {
                if(dialogService.ShowConfirmDialog($"Подтвердите удаление '{e.Name}'"))
                {
                    Departments.Remove(e);
                    await departmentRepository.Delete(e.DepartmentID);                   
                }
            };
            department.OnEditClicked += (e) => windowService.ShowAddDepartmentWindow(e);
            department.OnShowDetailsClicked += (e) => System.Windows.MessageBox.Show($"OnShowDetailsClicked {e.Name}");
            Departments.Add(department);
        }

        IsLoadingInProgress = false;
    }
}

