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
    internal class gridManipulation
    {
        public static List<string> nameLabel()
        {
            try
            {
                TSM.Model Model = new TSM.Model();
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
            catch (Exception ex)
            {
                Logger.WritrLog(ex.Message);
                return null;
            }
        }
        
        public static List<string> LabelsGrid()
        {
            try
            {
                List<string> LabelsGrid = new List<string>();
                for (int k = 0; k < gridManipulation.nameLabel().Count(); k++)
                {
                    string stringNameLabel = gridManipulation.nameLabel()[k];
                    string[] strings = stringNameLabel.Split(' ');
                    for (int i = 0; i < strings.Length; i++)
                    {
                        LabelsGrid.Add(strings[i]);
                    }
                }
                return LabelsGrid;
            }
            catch (Exception ex)
            {
                Logger.WritrLog(ex.Message);
                return null;
            }
        }
    }
}
