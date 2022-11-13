﻿using KliczekPomocniczek.QuickMenu;
using KliczekPomocniczek.Skills;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using Tekla.Structures.Model;
using MessageBox = System.Windows.MessageBox;

namespace KliczekPomocniczek
{
    public partial class MainWindow : Window
    {
        public keyboardKeyListener listener;
        public static QuickMenuPage QuickMenuPage = new QuickMenuPage();
        public static System.Windows.Input.Key keyChangeWeldPosition = System.Windows.Input.Key.Space;

        public MainWindow()
        {
            InitializeComponent();
            foreach(String stringg in gridManipulation.nameLabel())
            {
                TextTesy.Text += stringg + "\n";
            }
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
                checkBox_WeldPosition.IsChecked == true)
                changeWeldDirection.weldPositionEnum();

            if (e.KeyPressed == System.Windows.Input.Key.Down &&
                QuickMenuPage.IsActive == false)
            {
                QuickMenuPage.WindowStartupLocation = WindowStartupLocation.Manual;
                QuickMenuPage.Left = System.Windows.Forms.Control.MousePosition.X - 250;
                QuickMenuPage.Top = System.Windows.Forms.Control.MousePosition.Y - 250;
                QuickMenuPage.Topmost = true;
                QuickMenuPage.Show();
            }
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
