using System;
using System.Globalization;
using Vts.Gui.XF.Extensions;
using Xamarin.Forms;

namespace Vts.Gui.XF.Converters
{
    /// Converts a boolean to visibility value.
    public class ResourceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringLookup.GetLocalizedString((string) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}