using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using Tekla.Structures.Dialog;
using Tekla.Structures.Model.UI;
using Tekla.Structures;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;
using Tekla.Structures.Model;
using System.Text;
using Tekla.Structures.Geometry3d;
using System.Drawing.Printing;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.Serialization;
using View = Tekla.Structures.Model.UI.View;

namespace KliczekPomocniczek.Skills
{
    public static class SettingsSave
    {
        public static string stringKey(string nameOfSetting, string model, string nameOfButton)
        {
            string s = string.Format("{0}${1}${2}", nameOfSetting, model, nameOfButton);
            return s;
        }
        public static void disassembeSetings(Model model, MainWindow mainWindow, Hashtable hashtable)
        {
            string modelName = MainWindow.cutNameOfProject(model);
            string settingsName = nameOfSetting(mainWindow);
            string key = stringKey(modelName, settingsName, "Points_CheckBox");
            bool b = hashtable.ContainsKey(key);
            if (!hashtable.ContainsKey(key))
            {
                hashtable.Remove(stringKey(modelName, settingsName, "PointsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "LinesCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "BoltsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "WeldsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "CutsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "ComponentsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "GridsCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "ReferencesCheckBox"));
                hashtable.Remove(stringKey(modelName, settingsName, "ColorAndTransparency"));
                hashtable.Remove(stringKey(modelName, settingsName, "Phases"));
                hashtable.Remove(stringKey(modelName, settingsName, "ClipPlanes"));
            }
        }

        public static void assembeSetings(Model model, MainWindow mainWindow, Hashtable hashtable)
        {
            List<string> s = new List<string>();
            ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
            ViewEnum.MoveNext();
            View ActiveView = ViewEnum.Current;
            ClipPlaneCollection clips = ActiveView.GetClipPlanes();
            foreach (ClipPlane clipPlane in clips)
            {
                string x = string.Format("{0}${1}$", clipPlane.Location.ToString(), clipPlane.UpVector.ToString());
                s.Add(x);
            }
            string stringClip = string.Join("$", s);

            string modelName = MainWindow.cutNameOfProject(model);
            string settingsName = nameOfSetting(mainWindow);
            string key = stringKey(modelName, settingsName, "Points_CheckBox");
            bool b = hashtable.ContainsKey(key);
            if (hashtable.ContainsKey(key) != false)
            {
                hashtable.Add(stringKey(modelName, settingsName, "PointsCheckBox"), mainWindow.Points_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "LinesCheckBox"), mainWindow.Lines_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "BoltsCheckBox"), mainWindow.Bolts_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "WeldsCheckBox"), mainWindow.Welds_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "CutsCheckBox"), mainWindow.Cuts_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "ComponentsCheckBox"), mainWindow.Components_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "GridsCheckBox"), mainWindow.Grids_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "ReferencesCheckBox"), mainWindow.References_CheckBox.IsChecked.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "ColorAndTransparency"), mainWindow.ColorAndTransparency.SelectedValue.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "Phases"), mainWindow.Phases.Text.ToString());
                hashtable.Add(stringKey(modelName, settingsName, "ClipPlanes"), stringClip);
            }
            else
            {
                hashtable[stringKey(modelName, settingsName, "PointsCheckBox")] = mainWindow.Points_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "LinesCheckBox")] = mainWindow.Lines_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "BoltsCheckBox")] = mainWindow.Bolts_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "WeldsCheckBox")] = mainWindow.Welds_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "CutsCheckBox")] = mainWindow.Cuts_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "ComponentsCheckBox")] = mainWindow.Components_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "GridsCheckBox")] = mainWindow.Grids_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "ReferencesCheckBox")] = mainWindow.References_CheckBox.IsChecked.ToString();
                hashtable[stringKey(modelName, settingsName, "ColorAndTransparency")] = mainWindow.ColorAndTransparency.SelectedValue.ToString();
                hashtable[stringKey(modelName, settingsName, "Phases")] = mainWindow.Phases.Text.ToString();
                hashtable[stringKey(modelName, settingsName, "ClipPlanes")] = stringClip;
            }
        }

        public static void Save(Hashtable hashtable)
        {
            // write the data to a file
            var binformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var fs = File.Create(filePath()))
            {
                binformatter.Serialize(fs, hashtable);
            }
        }
        public static Hashtable ReadHashtable()
        {
            // read the data from the file
            Hashtable hashtablele = new Hashtable();
            var binformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            if (isFileCreated(filePath()))
            {
                using (var fs = File.Open(filePath(), FileMode.Open))
                {
                    hashtablele = (Hashtable)binformatter.Deserialize(fs);
                }
            }
            return hashtablele;
        }

        public static void Load(MainWindow mainWindow, Model model, Hashtable hashtable)
        {
            string modelName = MainWindow.cutNameOfProject(model);
            List<string> savedKeys = new List<string>();
            List<string> savedKeysCurrentModel = new List<string>();
            foreach (object key in hashtable.Keys)
            {
                savedKeys.Add(key.ToString());
            }
            for(int i = 0; i < savedKeys.Count; i++)
            {
                string[] keys = savedKeys[i].Split('$');
                if(keys[0] == modelName && !savedKeysCurrentModel.Contains(keys[1]))
                    savedKeysCurrentModel.Add(keys[1]);

            }
            savedKeysCurrentModel = savedKeysCurrentModel.OrderBy(q => q).ToList();
            mainWindow.LoadSavedViewSettings.ItemsSource = savedKeysCurrentModel;
        }

        public static string filePath()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("\\", "_");
            string strName = userName.Remove(userName.Length - 1) + "_setting.txt";
            string strSetPath = strWorkPath + "\\" + strName;
            return strSetPath;
        }

        public static bool isFileCreated(string filePath)
        {
            if (!File.Exists(filePath))
                return false;
            else if (File.Exists(filePath))
                return true;
            else
            {
                System.Windows.MessageBox.Show("Coś poszło nie tak");
                return false;
            }
        }

        public static void FileCreate(string filePath)
        {
            if (!isFileCreated(filePath))
                File.Create(filePath);
        }

        public static string nameOfSetting(MainWindow mainWindow)
        {
            if (mainWindow.LoadSavedViewSettings.Text != null)
            {
                return mainWindow.LoadSavedViewSettings.Text;
            }
            else
            {
                System.Windows.MessageBox.Show("Wprowadź nazwę ustawienia");
                return null;
            }
        }
    }
}
