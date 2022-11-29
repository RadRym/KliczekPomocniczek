using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using Tekla.Structures.Dialog;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using View = Tekla.Structures.Model.UI.View;

namespace KliczekPomocniczek.Skills
{
    public static class ViewDetails
    {
        public static bool CheckBox_Bool(CheckBox checkBox)
        {
            if ((bool)checkBox.IsChecked)
                return true;
            else return false;
        }
        public static void Run(MainWindow mainWindow)
        {
            ViewVisibilitySettings viewVisibilitySettings = new ViewVisibilitySettings();
            View view = new View();
            ModelViewEnumerator ViewEnum = ViewHandler.GetAllViews();
            while (ViewEnum.MoveNext())
            {
                view = ViewEnum.Current;
                #region Settings
                viewVisibilitySettings.PointsVisible = CheckBox_Bool(mainWindow.Points_CheckBox);
                viewVisibilitySettings.ConstructionLinesVisible = CheckBox_Bool(mainWindow.Lines_CheckBox);
                viewVisibilitySettings.BoltsVisible = CheckBox_Bool(mainWindow.Bolts_CheckBox);
                viewVisibilitySettings.BoltsVisibleInComponents = CheckBox_Bool(mainWindow.Bolts_CheckBox);
                viewVisibilitySettings.WeldsVisible = CheckBox_Bool(mainWindow.Welds_CheckBox);
                viewVisibilitySettings.WeldsVisibleInComponents = CheckBox_Bool(mainWindow.Welds_CheckBox);
                viewVisibilitySettings.CutsVisible = CheckBox_Bool(mainWindow.Cuts_CheckBox);
                viewVisibilitySettings.CutsVisibleInComponents = CheckBox_Bool(mainWindow.Cuts_CheckBox);
                viewVisibilitySettings.FittingsVisible = CheckBox_Bool(mainWindow.Cuts_CheckBox);
                viewVisibilitySettings.FittingsVisibleInComponents = CheckBox_Bool(mainWindow.Cuts_CheckBox);
                viewVisibilitySettings.GridsVisible = CheckBox_Bool(mainWindow.Grids_CheckBox);
                viewVisibilitySettings.ReferenceObjectsVisible = CheckBox_Bool(mainWindow.References_CheckBox);
                viewVisibilitySettings.ComponentsVisible = CheckBox_Bool(mainWindow.Components_CheckBox);
                viewVisibilitySettings.ComponentsVisibleInComponents = CheckBox_Bool(mainWindow.Components_CheckBox);
                #endregion 

                view.VisibilitySettings = viewVisibilitySettings;
                view.Modify();
            }
        }
        public static List<String> permamentVisualisation()
        {
            ModelViewEnumerator PermView = ViewHandler.GetPermanentViews();
            
            List <String> ListNamesPermViews = new List<String>();
            while (PermView.MoveNext())
            {
                ListNamesPermViews.Add(PermView.Current.Name);
            }
            return ListNamesPermViews;
        }

        public static void sssss()
        {
            //Model model = new Model();
            //bool ViewEnum = ViewHandler.SetRepresentation("standard");
            //ModelViewEnumerator PermView = ViewHandler.GetPermanentViews();
            //while (ViewEnum.MoveNext())
            //{
            //    View View = ViewEnum.Current;
            //    ViewHandler.GetPermanentViews();
            //}
        }
    }
}
