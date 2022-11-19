using KliczekPomocniczek.QuickMenu;
using KliczekPomocniczek.Skills;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Tekla.Structures.Model;
using MessageBox = System.Windows.MessageBox;

namespace KliczekPomocniczek
{
    public partial class MainWindow : Window
    {
        #region Definitions
        public static MainWindow main;
        public keyboardKeyListener listener;
        public static QuickMenuPage QuickMenuPage = new QuickMenuPage();
        public static System.Windows.Input.Key keyChangeWeldPosition = System.Windows.Input.Key.Space;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            DataContext = new comboboxes();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listener = new keyboardKeyListener();
            listener.OnKeyPressed += Listener_OnKeyPressed;
            listener.HookKeyboard();
            Thread trackerThread = new Thread(tracker);
            trackerThread.Start();
        }

        private void tracker()
        {
            while (true)
            {
                var point = Control.MousePosition;
                int x = point.X;
                MainWindow.main.Dispatcher.Invoke(new System.Action(delegate()
                {
                    Screen s = Screen.FromPoint(System.Windows.Forms.Control.MousePosition);
                    double screenWidth = s.WorkingArea.Width;
                    double screenHeight = s.WorkingArea.Height;
                    MainWindow.main.Text1.Text =
                                    "MousePositionX: " + Control.MousePosition.X.ToString() + " \n" +
                                    "MousePositionY: " + Control.MousePosition.Y.ToString() + " \n" +
                                    "VirtualScreenWidth: " + SystemParameters.VirtualScreenWidth.ToString() + " \n" +
                                    "VirtualScreenHeight: " + SystemParameters.VirtualScreenHeight.ToString() + " \n" +
                                    "PrimaryScreenWidth: " + SystemParameters.PrimaryScreenWidth.ToString() + " \n" +
                                    "PrimaryScreenHeight: " + SystemParameters.PrimaryScreenHeight.ToString() + " \n" +
                                    "FullPrimaryScreenWidth: " + SystemParameters.FullPrimaryScreenWidth.ToString() + " \n" +
                                    "FullPrimaryScreenHeight: " + SystemParameters.FullPrimaryScreenHeight.ToString() + " \n" +
                                    "screenWidth: " + screenWidth.ToString() + " \n" +
                                    "screenHeight: " + screenHeight.ToString() + " \n" +
                                    "QuickMenuPage.Width: " + QuickMenuPage.Width.ToString() + " \n" +
                                    "QuickMenuPage.Height: " + QuickMenuPage.Height.ToString() + " \n" +
                                    "QuickMenuPage.Left: " + QuickMenuPage.Left.ToString() + " \n" +
                                    "QuickMenuPage.Top: " + QuickMenuPage.Top.ToString() + " \n" +
                                    s.DeviceName.ToString();
                }));

            }
        }

        public void Listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            Model model = new Model();
            if (e.KeyPressed == keyChangeWeldPosition &&
                checkBox_WeldPosition.IsChecked == true)
                changeWeldDirection.weldPositionEnum();

            if (e.KeyPressed == System.Windows.Input.Key.Down &&
                QuickMenuPage.IsActive == false)
            {
                QuickMenuPage.Left = 0.8 * System.Windows.Forms.Control.MousePosition.X - 250;
                QuickMenuPage.Top = 0.8 * System.Windows.Forms.Control.MousePosition.Y - 250;
                QuickMenuPage.Topmost = true;
                QuickMenuPage.Show();
            }
        }

        private void DeleteClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.deleteClipPlanes();

        private void CreateClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.createClipPlanes();

        private void ObjectCoordynates_Click(object sender, RoutedEventArgs e) => partCoordSyst.Draw();

        private void SetPartWorkPlane_Click(object sender, RoutedEventArgs e) => partCoordSyst.Set();

        private void Test1_Click(object sender, RoutedEventArgs e)
        {
            var gridXLabels = gridManipulation.nameLabel()[0];
        }

        private void Test2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(activeWindows.ActiveWindowTitle());
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            listener.UnHookKeyboard();
            QuickMenuPage.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult result = (DialogResult)System.Windows.MessageBox.Show("Czy na pewno chcesz zamknąć program mordo?", "Closing",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if(result == System.Windows.Forms.DialogResult.Yes)
            {
                Window_Closed(sender, e);
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        
    }
}
