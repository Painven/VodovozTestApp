using System.ComponentModel;

namespace VodovozTestApp.Models;

public enum Sex
{
    [Description("Не указан")]
    None,
    [Description("Муж.")]
    Male,
    [Description("Жен.")]
    Female,
    [Description("Др.")]
    Other
}
