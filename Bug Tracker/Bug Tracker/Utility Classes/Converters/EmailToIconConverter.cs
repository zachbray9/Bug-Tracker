using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bug_Tracker.Utility_Classes.Converters
{
    public class EmailToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEmail = (bool)value;
            if (isEmail)
            {
                return FontAwesomeIcon.EnvelopeOutline;
            }
            else
            {
                return FontAwesomeIcon.UserOutline;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
