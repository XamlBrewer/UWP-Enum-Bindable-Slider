using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XamlBrewer.Uwp.Controls
{
    class DoubleToEnumConverter : IValueConverter
    {
        private Type _enum;

        public DoubleToEnumConverter(Type type)
        {
            _enum = type;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //if (this._enum == null)
            //{
            //    return null; // e.g. at design time.
            //}

            return Enum.ToObject(_enum, (int)(double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value; // Never called
        }
    }
}
