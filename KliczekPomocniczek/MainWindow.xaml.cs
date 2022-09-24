using KliczekPomocniczek.Skills;
using System.Windows;
using System.Windows.Controls;
using Tekla.Structures.Model;
using System.Windows.Input;

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
    }
}
