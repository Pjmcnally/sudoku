using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.View.Converter
{
    internal class SolveDelayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> delayList = (List<int>)parameter;
            return delayList.FindIndex(x => x == (int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> delayList = (List<int>)parameter;
            int index = (int)(double)value;

            return delayList[index];
        }
    }
}
