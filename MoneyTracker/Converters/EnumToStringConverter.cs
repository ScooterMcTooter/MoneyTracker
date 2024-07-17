using MoneyTracker.ViewModels;
using System.Globalization;

namespace MoneyTracker.Converters;

public class EnumToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            return enumValue.ToString();
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            Type enumType = typeof(JobType);
            if (value.GetType() == enumType)
            {
                return (JobType)value;
            }

            enumType = typeof(JobLocation);
            if (value.GetType() == enumType)
            {
                return (JobLocation)value;
            }

            enumType = typeof(JobStatus);
            if (value.GetType() == enumType)
            {
                return (JobStatus)value;
            }

            enumType = typeof(JobHours);
            if (value.GetType() == enumType)
            {
                return (JobHours)value;
            }
        }
        return string.Empty;
    }
}

