using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using TSMW = Tekla.Structures.Model.Weld.WeldPositionEnum;
using Tekla.Structures.Model;

namespace KliczekPomocniczek.Skills
{
    internal class GridManipulation
    {
        public static List<string> NameLabel()
        {
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                System.Type[] types = new System.Type[1];
                types.SetValue(typeof(Grid), 0);
                List<string> strings = new List<string>();
                ModelObjectEnumerator myEnum = Model.GetModelObjectSelector().GetAllObjectsWithType(types);
                foreach (Grid grid in myEnum)
                {
                    strings.Add(grid.LabelX);
                    strings.Add(grid.LabelY);
                    strings.Add(grid.LabelZ);
                }
                return strings;
            }
            else return null;
        }
        
        public static List<string> LabelsGrid()
        {
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                List<string> LabelsGrid = new List<string>();
                for (int k = 0; k < GridManipulation.NameLabel().Count(); k++)
                {
                    string stringNameLabel = GridManipulation.NameLabel()[k];
                    string[] strings = stringNameLabel.Split(' ');
                    for (int i = 0; i < strings.Length; i++)
                    {
                        LabelsGrid.Add(strings[i]);
                    }
                }
                return LabelsGrid;
            }
            else return null;
        }
    }
}
