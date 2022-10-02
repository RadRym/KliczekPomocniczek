using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Color = Tekla.Structures.Model.UI.Color;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace KliczekPomocniczek.Skills
{
    internal class viewClass
    {
        public static void setTemporaryColor(System.Drawing.Color color)
        {
            TSM.Model Model = new TSM.Model();
            TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
            TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

            while (selectedObjects.MoveNext())
            {
                List<ModelObject> list = new List<ModelObject>();
                if ((selectedObjects.Current as TSM.ModelObject) != null)
                {
                    ModelObject @object = selectedObjects.Current;
                    list.Add(@object);

                }
                ModelObjectVisualization.SetTemporaryState(list, new Color(color.R / 255, color.G / 255, color.B / 255, color.A / 255));
            }
        }
        public static void selectTemporaryColor(System.Drawing.Color color)
        {
            Model model = new Model();
            ModelObjectEnumerator.AutoFetch = true;
            ArrayList ObjectsToSelect = new ArrayList();

            var types = new[] { typeof(Part) };
            var modelParts = model.GetModelObjectSelector().GetAllObjectsWithType(types);
            foreach (Part modelPart in modelParts)
            {
                Color tempColor = new Color();
                ModelObjectVisualization.GetRepresentation(modelPart, ref tempColor);
                if (tempColor == new Color(color.R / 255, color.G / 255, color.B / 255, color.A / 255))
                ObjectsToSelect.Add(modelPart);
            }
            TSMUI.ModelObjectSelector MS = new TSMUI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);
            model.CommitChanges();
        }
    }
}
