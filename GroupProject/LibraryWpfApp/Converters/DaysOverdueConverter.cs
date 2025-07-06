using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryWpfApp.Converters
{
    public class DaysOverdueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 || !(values[0] is DateTime dueDate) || !(values[1] is string status) || !(values[2] is DateTime today))
            {
                return "";
            }

            if (status != "Borrowed")
            {
                return "";
            }

            TimeSpan daysOverdue = today.Date - dueDate.Date;
            if (daysOverdue.TotalDays > 0)
            {
                return $"{(int)daysOverdue.TotalDays} days";
            }
            return "0 days";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
