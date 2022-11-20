using KliczekPomocniczek.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KliczekPomocniczek.QuickMenu
{
    /// <summary>
    /// Interaction logic for QuickMenuPage.xaml
    /// </summary>
    public partial class QuickMenuPage : Window
    {
        public static int numberOfIcons = 6;
        public QuickMenuPage()
        {
            InitializeComponent();
            CreateClipPlanes.Margin = point(0, numberOfIcons);
            DeleteClipPlanes.Margin = point(1, numberOfIcons);
            ObjectsCoordynates.Margin = point(2, numberOfIcons);
            SetPartWorkplane.Margin = point(3, numberOfIcons);
            Redraw.Margin = point(4, numberOfIcons);
            CSVfiles.Margin = point(5, numberOfIcons);
        }

        public static Thickness point(int n, int numberOfIcons)
        {
            int radius = 60;
            double windowHeight = 500 - 50;
            double windowWidth = 500 - 50;
            double angle = 2 * Math.PI / numberOfIcons;
            double x = radius * Math.Cos(angle * n);
            double y = radius * Math.Sin(angle * n);
            double one = 0.5 * windowWidth + x;
            double two = 0.5 * windowHeight - y;
            double three = 0.5 * windowWidth - x;
            double four = 0.5 * windowHeight + y;
            Thickness thickness = new Thickness(one, two, three, four); 
            return thickness;
        }

        private void DeleteClipPlanes_Click(object sender, RoutedEventArgs e)
        {
            clipPlanes.deleteClipPlanes();
            this.Hide();
        }

        private void CreateClipPlanes_Click(object sender, RoutedEventArgs e)
        {
            clipPlanes.createClipPlanes();
            this.Hide();
        }

        private void ObjectsCoordynates_Click(object sender, RoutedEventArgs e)
        {
            partCoordSyst.Draw();
            this.Hide();
        }

        private void SetPartWorkplane_Click(object sender, RoutedEventArgs e)
        {
            partCoordSyst.Set();
            this.Hide();
        }

        private void Redraw_Click(object sender, RoutedEventArgs e)
        {
            partCoordSyst.Redraw();
            this.Hide();
        }

        private void CSVfiles_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
