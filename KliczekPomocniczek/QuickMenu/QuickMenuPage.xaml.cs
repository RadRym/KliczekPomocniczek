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
        public QuickMenuPage()
        {
            InitializeComponent();
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
            //putSomeDataNieChceMiSie.Show();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
