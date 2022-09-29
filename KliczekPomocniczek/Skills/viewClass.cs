using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Object = Tekla.Structures.Model.Object;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace KliczekPomocniczek.Skills
{
    internal class viewClass
    {
        public static void TemporaryColor()
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
                ModelObjectVisualization.SetTemporaryState(list, new TSM.UI.Color(0.0, 0.0, 1.0));
            }
        }
    }
}
