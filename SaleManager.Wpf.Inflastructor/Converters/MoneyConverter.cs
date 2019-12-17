using SaleManager.Wpf.Inflastructor.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SaleManager.Wpf.Inflastructor.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Decimal money;
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            if (Decimal.TryParse(value.ToString(), out money))
            {
                return money.ToString("#,###", cul.NumberFormat);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var money = value.ToString().Replace(".", "");
                if (NumberHelpers.IsNumber(money))
                    return Decimal.Parse(money);
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
