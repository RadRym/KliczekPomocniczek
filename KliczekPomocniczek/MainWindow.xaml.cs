using KliczekPomocniczek.Skills;
using System.Windows;
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
    }
}
