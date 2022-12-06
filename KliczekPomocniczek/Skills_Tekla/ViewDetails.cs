using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using Tekla.Structures;
using Tekla.Structures.Dialog;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using View = Tekla.Structures.Model.UI.View;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;
using Tekla.Structures.Dialog.UIControls;
using System.Linq;

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
        public static void Run(MainWindow mainWindow, string modelName, string SelectedSetting)
        {
            ViewVisibilitySettings viewVisibilitySettings = new ViewVisibilitySettings();
            string whatphase = mainWindow.Phases.Text;
            int leng = whatphase.Split((char)32).Length;
            string[] phases = new string[leng];
            phases = whatphase.Split((char)32);
            visiblePhase(phases);
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

                clipPlanes.deleteClipPlanes();
                Hashtable hashtable = SettingsSave.ReadHashtable();
                string ClitPlanes = hashtable[SettingsSave.stringKey(modelName, SelectedSetting, "ClipPlanes")].ToString();
                List<string> strings = ClitPlanes.Split('$').ToList();
                strings.RemoveAll(s => string.IsNullOrWhiteSpace(s));
                if (strings.Count > 0)
                {
                    for (int i = 0; i < strings.Count - 1; i = i + 2)
                    {
                        ClipPlane CPlane = new ClipPlane();
                        CPlane.View = view;
                        var locatro = strings[i].Replace("(", string.Empty).Replace(")", string.Empty).Split(',');
                        int loco1 = (int)double.Parse(locatro[0]);
                        int loco2 = (int)double.Parse(locatro[1]);
                        int loco3 = (int)double.Parse(locatro[2]);
                        var vectro = strings[i + 1].Replace("(", string.Empty).Replace(")", string.Empty).Split(',');
                        int veco1 = (int)double.Parse(vectro[0]);
                        int veco2 = (int)double.Parse(vectro[1]);
                        int veco3 = (int)double.Parse(vectro[2]);
                        CPlane.Location = new Tekla.Structures.Geometry3d.Point(loco1, loco2, loco3);
                        CPlane.UpVector = new Tekla.Structures.Geometry3d.Vector(veco1, veco2, veco3);
                        CPlane.Insert();
                    }
                }
                if (mainWindow.ColorAndTransparency.SelectedItem != null)
                {
                    string ColorAndTranspareaancy = mainWindow.ColorAndTransparency.SelectedItem.ToString();
                    ViewHandler.SetRepresentation(ColorAndTranspareaancy);
                }
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

        public static void visiblePhase(string[] phases)
        {
            
            Model model = new Model();
            ModelViewEnumerator ViewEnum = ViewHandler.GetAllViews();
            AssemblyFilterExpressions.Phase assemblyPhase = new AssemblyFilterExpressions.Phase();
            StringConstantFilterExpression phase = new StringConstantFilterExpression(phases);
            BinaryFilterExpression binaryFilterExpression1 = new BinaryFilterExpression(assemblyPhase, StringOperatorType.IS_EQUAL, phase);
            BinaryFilterExpressionCollection ExpressionCollection = new BinaryFilterExpressionCollection();
            ExpressionCollection.Add(new BinaryFilterExpressionItem(binaryFilterExpression1, BinaryFilterOperatorType.BOOLEAN_OR));
            Filter Filter = new Filter(ExpressionCollection);
            ModelInfo modelInfo = model.GetInfo();
            string filterName = "PMJ_AR_custom";
            string fullFilterName = modelInfo.ModelPath + "/attributes/" + filterName;
            Filter.CreateFile(FilterExpressionFileType.OBJECT_GROUP_VIEW, fullFilterName);

            while (ViewEnum.MoveNext())
            {
                View ViewSel = ViewEnum.Current;
                string s = ViewSel.ViewFilter;
                ViewSel.ViewFilter = filterName;
                ViewSel.Modify();
            }
        }
    }
}
