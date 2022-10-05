using System.Windows;
using System.Windows.Controls;

namespace KliczekPomocniczek.QuickMenu
{
    public class ButtonClass : Button
    {
        static ButtonClass()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonClass), new FrameworkPropertyMetadata(typeof(ButtonClass)));
        }
    }
}
