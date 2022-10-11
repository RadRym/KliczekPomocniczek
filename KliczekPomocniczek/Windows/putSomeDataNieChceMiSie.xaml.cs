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

namespace KliczekPomocniczek.Windows
{
    /// <summary>
    /// Interaction logic for putSomeDataNieChceMiSie.xaml
    /// </summary>
    public partial class putSomeDataNieChceMiSie : Window
    {
        public putSomeDataNieChceMiSie()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CSVlist.filesToCSV();
        }

        public void What_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        public void Where_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
