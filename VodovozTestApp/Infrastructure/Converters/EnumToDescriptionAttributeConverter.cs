using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using VodovozTestApp.Models;

namespace VodovozTestApp.Infrastructure.Converters;

public class EnumToDescriptionAttributeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return GetDescription(Enum.Parse<Sex>(value.ToString()));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private string GetDescription<T>(T e) where T : IConvertible
    {
        if (e is not Enum) { return String.Empty; }

        Type type = e.GetType();
        Array values = System.Enum.GetValues(type);

        foreach (int val in values)
        {
            if (val == e.ToInt32(CultureInfo.InvariantCulture))
            {
                var memInfo = type.GetMember(type.GetEnumName(val));
                var descriptionAttribute = memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
            }
        }
        return null;
    }
}

