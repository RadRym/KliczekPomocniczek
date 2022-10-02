using KliczekPomocniczek.Skills;
using System.Windows;
using System.Windows.Media;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using TSMUI = Tekla.Structures.Model.UI;

namespace KliczekPomocniczek
{
    public partial class MainWindow : Window
    {
        public keyboardKeyListener listener;
        public static System.Windows.Input.Key keyChangeWeldPosition = System.Windows.Input.Key.Space;

        public MainWindow()
        {
            InitializeComponent();
            setComboColors.ItemsSource = typeof(Colors).GetProperties();
            setComboColors.Text = "-----Select-----";
            selectComboColors.ItemsSource = typeof(Colors).GetProperties();
            selectComboColors.Text = "-----Select-----";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listener = new keyboardKeyListener();
            listener.OnKeyPressed += Listener_OnKeyPressed;
            listener.HookKeyboard();
        }

        public void Listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            Model model = new Model();
            if (e.KeyPressed == keyChangeWeldPosition &&
                checkBox_WeldPosition.IsChecked == true &&
                model.GetConnectionStatus())
                changeWeldDirection.weldPositionEnum();
        }

        private void deleteClipPlanes_Click(object sender, RoutedEventArgs e)
        {
            clipPlanes.deleteClipPlanes();
        }

        private void createClipPlanes_Click(object sender, RoutedEventArgs e)
        {
            clipPlanes.createClipPlanes();
        }

        private void Ribs_Click(object sender, RoutedEventArgs e)
        {
            ribsCuts.prompt();
        }

        private void objectCoordynates_Click(object sender, RoutedEventArgs e)
        {
            partCoordSyst.Draw();
        }

        private void setPartWorkPlane_Click(object sender, RoutedEventArgs e)
        {
            partCoordSyst.Set();
        }

        private void setTemporaryColor_Click(object sender, RoutedEventArgs e)
        {
            var @object = setComboColors.SelectedItem.ToString();
            string[] parts = @object.Split(' ');
            string lastWord = parts[parts.Length - 1];
            System.Drawing.Color color = System.Drawing.Color.FromName(lastWord);
            viewClass.setTemporaryColor(color);
        }

        private void selectTemporaryColor_Click(object sender, RoutedEventArgs e)
        {
            var @object = setComboColors.SelectedItem.ToString();
            string[] parts = @object.Split(' ');
            string lastWord = parts[parts.Length - 1];
            System.Drawing.Color color = System.Drawing.Color.FromName(lastWord);
            viewClass.selectTemporaryColor(color);
        }
    }
}
