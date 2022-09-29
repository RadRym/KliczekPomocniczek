using KliczekPomocniczek.Skills;
using System;
using System.Windows;
using System.Windows.Controls;
using Tekla.Structures.Model;
using static Tekla.Structures.Model.LoadGroup;
using Grid = System.Windows.Controls.Grid;

namespace KliczekPomocniczek
{
    public partial class MainWindow : Window
    {
        public keyboardKeyListener listener;
        public static System.Windows.Input.Key keyChangeWeldPosition = System.Windows.Input.Key.Space;

        public MainWindow()
        {
            InitializeComponent();
            comboColors.ItemsSource = typeof(Colors).GetProperties();
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

        private static readonly int COLUMS = 5;

        private void table_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            if (grid != null)
            {
                if (grid.RowDefinitions.Count == 0)
                {
                    for (int r = 0; r <= comboColors.Items.Count / COLUMS; r++)
                        grid.RowDefinitions.Add(new RowDefinition());
                }
                if (grid.ColumnDefinitions.Count == 0)
                {
                    for (int c = 0; c < Math.Min(comboColors.Items.Count, COLUMS); c++)
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    Grid.SetColumn(grid.Children[i], i % COLUMS);
                    Grid.SetRow(grid.Children[i], i / COLUMS);
                }
            }
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

        private void temporaryColor_Click(object sender, RoutedEventArgs e)
        {
            viewClass.TemporaryColor();
        }
    }
}
