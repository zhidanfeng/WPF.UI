using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace ZdfFlatUI.Utils
{
    public class PaletteHelper
    {

        public static void SetLightDarkTheme(bool IsDarkTheme)
        {
            var existingResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/WPF.UI;component\/Themes\/Theme\.)((Light)|(Dark))").Success);

            if (existingResourceDictionary == null)
            {
                //throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");
            }

            var source =
                $"pack://application:,,,/WPF.UI;component/Themes/Theme.{(IsDarkTheme ? "Dark" : "Light")}.xaml";
            var newResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);
        }
    }
}
