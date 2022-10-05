using KliczekPomocniczek.Skills;
using System.Windows;
using System.Windows.Media;
using Tekla.Structures.Model;

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

        private void DeleteClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.deleteClipPlanes();

        private void CreateClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.createClipPlanes();

        private void Ribs_Click(object sender, RoutedEventArgs e) => ribsCuts.prompt();

        private void ObjectCoordynates_Click(object sender, RoutedEventArgs e) => partCoordSyst.Draw();

        private void SetPartWorkPlane_Click(object sender, RoutedEventArgs e) => partCoordSyst.Set();

        private void SetTemporaryColor_Click(object sender, RoutedEventArgs e)
        {
            var @object = setComboColors.SelectedItem.ToString();
            string[] parts = @object.Split(' ');
            string lastWord = parts[parts.Length - 1];
            System.Drawing.Color color = System.Drawing.Color.FromName(lastWord);
            viewClass.setTemporaryColor(color);
        }

        private void SelectTemporaryColor_Click(object sender, RoutedEventArgs e)
        {
            var @object = setComboColors.SelectedItem.ToString();
            string[] parts = @object.Split(' ');
            string lastWord = parts[parts.Length - 1];
            System.Drawing.Color color = System.Drawing.Color.FromName(lastWord);
            viewClass.selectTemporaryColor(color);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            QuickMenu.QuickMenuPage quickMenu = new QuickMenu.QuickMenuPage();
            quickMenu.Show();
            //PieMenu.MainWindow.PieMenu pieMenu = new PieMenu.MainWindow.PieMenu();
            //pieMenu.Show();
        }
    }
}
