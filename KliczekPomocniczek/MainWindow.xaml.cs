using KliczekPomocniczek.QuickMenu;
using KliczekPomocniczek.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Dialog;
using Tekla.Structures.Dialog.UIControls;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using View = Tekla.Structures.Model.UI.View;


namespace KliczekPomocniczek
{
    public partial class MainWindow : Window
    {
        #region Definitions
        public static MainWindow main;
        public KeyboardKeyListener listener;
        public static QuickMenuPage QuickMenuPage = new QuickMenuPage();
        readonly KeyboardHook hook = new KeyboardHook();
        Thread trackerThread = new Thread(Tracker);
        public static System.Windows.Input.Key keyChangeWeldPosition = System.Windows.Input.Key.Space;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(StartQuickMenu);
            hook.RegisterHotKey(ModifierKeys.Control, Keys.Space);
            this.SizeToContent = SizeToContent.WidthAndHeight;

            #region Settings
            DateTime thisDay = DateTime.Today;
            ListNameTextBox.Text = thisDay.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) + "_";
            OpenListAfterCreatingCheckBox.IsChecked = Properties.Settings.Default.OpenListAfterCreating;
            LocalizationOfFilesTextBox.Text = Properties.Settings.Default.LocalizationOfFiles;
            LocalizationOfSavedListTextBox.Text = Properties.Settings.Default.LocalizationOfSavedList;
            Model model = new Model();
            SettingsSave.Load(this, model, SettingsSave.ReadHashtable());
            ModelInfo modelInfo = model.GetInfo();

            TeklaStructuresFiles files = new TeklaStructuresFiles(modelpath: modelInfo.ModelPath);
            ColorAndTransparency.ItemsSource = files.GetMultiDirectoryFileList("rep", false);
            #endregion
        }
        public static string cutNameOfProject(Model model)
        {
            string cutNameOfProject = model.GetInfo().ModelName.ToString().Remove(model.GetInfo().ModelName.ToString().Length - 4);
            return cutNameOfProject;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listener = new KeyboardKeyListener();
            listener.OnKeyPressed += Listener_OnKeyPressed;
            listener.HookKeyboard();
            trackerThread.Start();
        }

        private static void Tracker()
        {
            //while (true)
            //{
            //    var point = System.Windows.Forms.Control.MousePosition;
            //    int x = point.X;
            //    MainWindow.main.Dispatcher.Invoke(new System.Action(delegate ()
            //    {
            //        Screen s = Screen.FromPoint(System.Windows.Forms.Control.MousePosition);
            //        double screenWidth = s.WorkingArea.Width;
            //        double screenHeight = s.WorkingArea.Height;
            //        MainWindow.main.Text1.Text =
            //                        "MousePositionX: " + System.Windows.Forms.Control.MousePosition.X.ToString() + " \n" +
            //                        "MousePositionY: " + System.Windows.Forms.Control.MousePosition.Y.ToString() + " \n" +
            //                        "VirtualScreenWidth: " + SystemParameters.VirtualScreenWidth.ToString() + " \n" +
            //                        "VirtualScreenHeight: " + SystemParameters.VirtualScreenHeight.ToString() + " \n" +
            //                        "PrimaryScreenWidth: " + SystemParameters.PrimaryScreenWidth.ToString() + " \n" +
            //                        "PrimaryScreenHeight: " + SystemParameters.PrimaryScreenHeight.ToString() + " \n" +
            //                        "FullPrimaryScreenWidth: " + SystemParameters.FullPrimaryScreenWidth.ToString() + " \n" +
            //                        "FullPrimaryScreenHeight: " + SystemParameters.FullPrimaryScreenHeight.ToString() + " \n" +
            //                        "screenWidth: " + screenWidth.ToString() + " \n" +
            //                        "screenHeight: " + screenHeight.ToString() + " \n" +
            //                        "QuickMenuPage.Width: " + QuickMenuPage.Width.ToString() + " \n" +
            //                        "QuickMenuPage.Height: " + QuickMenuPage.Height.ToString() + " \n" +
            //                        "QuickMenuPage.Left: " + QuickMenuPage.Left.ToString() + " \n" +
            //                        "QuickMenuPage.Top: " + QuickMenuPage.Top.ToString() + " \n" +
            //                        s.DeviceName.ToString();
            //    }));
            //}
        }

        public void Listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (e.KeyPressed == keyChangeWeldPosition &&
                checkBox_WeldPosition.IsChecked == true)
                changeWeldDirection.weldPositionEnum();  
        }

        public void StartQuickMenu(object sender, KeyPressedEventArgs e) => ScreenOptions.RunQuickMenu(QuickMenuPage);

        private void DeleteClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.deleteClipPlanes();

        private void CreateClipPlanes_Click(object sender, RoutedEventArgs e) => clipPlanes.createClipPlanes();
        


        private void ConceptToDetailed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DetailedToConcept_Click(object sender, RoutedEventArgs e)
        {
            var macroBuilder = new MacroBuilder();
            macroBuilder.Callback("acmdChangeJointTypeToCallback", "DETAIL", "View_01 window_1");
        }

        public void CommitChangesInView_Click(object sender, RoutedEventArgs e) 
        {
            ViewDetails.Run(this);
            if (ColorAndTransparency.SelectedItem != null)
            {
                string ColorAndTranspareaancy = ColorAndTransparency.SelectedItem.ToString();
                ViewHandler.SetRepresentation(ColorAndTranspareaancy);
            }
        }

        public void CreateListOFFiles_Click(object sender, RoutedEventArgs e)
        {
            bool OpenListAfterCreating = (bool)OpenListAfterCreatingCheckBox.IsChecked;
            string LocalizationOfFiles = LocalizationOfFilesTextBox.Text;
            string LocalizationOfSavedList = LocalizationOfSavedListTextBox.Text;
            string ListName = ListNameTextBox.Text;
            CSVlist.filesToCSV(LocalizationOfFiles, LocalizationOfSavedList, ListName, OpenListAfterCreating);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            #region Settings
            Properties.Settings.Default.OpenListAfterCreating = (bool)OpenListAfterCreatingCheckBox.IsChecked;
            Properties.Settings.Default.LocalizationOfFiles = LocalizationOfFilesTextBox.Text;
            Properties.Settings.Default.LocalizationOfSavedList = LocalizationOfSavedListTextBox.Text;
            Properties.Settings.Default.Save();
            #endregion

            listener.UnHookKeyboard();
            QuickMenuPage.Close();
            trackerThread.Abort();  
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

        private void SaveDisplaySettings_Click(object sender, RoutedEventArgs e)
        {
            Model model = new Model();
            Hashtable hashtable  = SettingsSave.ReadHashtable();
            SettingsSave.assembeSetings(model, this, hashtable);
            SettingsSave.Save(hashtable);
            SettingsSave.Load(this, model, hashtable);
        }

        private void LoadSavedViewSettings_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Model model = new Model();
            
            string modelName = cutNameOfProject(model);
            if(LoadSavedViewSettings.SelectedItem != null)
            {
                string SelectedSetting = LoadSavedViewSettings.SelectedItem.ToString();
                Hashtable hashtablele = SettingsSave.ReadHashtable();
                Points_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "PointsCheckBox")].ToString());
                Lines_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "LinesCheckBox")].ToString());
                Bolts_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "BoltsCheckBox")].ToString());
                Welds_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "WeldsCheckBox")].ToString());
                Cuts_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "CutsCheckBox")].ToString());
                Grids_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "GridsCheckBox")].ToString());
                References_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "ReferencesCheckBox")].ToString());
                Components_CheckBox.IsChecked = bool.Parse(hashtablele[SettingsSave.stringKey(modelName, SelectedSetting, "ComponentsCheckBox")].ToString());
            }
        }

        private void ColorAndTransparency_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }
    }
}
