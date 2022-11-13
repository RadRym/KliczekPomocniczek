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
            TSM.Model Model = new TSM.Model();
            System.Type[] types = new System.Type[1];
            types.SetValue(typeof(Grid), 0);
            List<string> strings = new List<string>();
            ModelObjectEnumerator myEnum = Model.GetModelObjectSelector().GetAllObjectsWithType(types);
            foreach(Grid grid in myEnum)
            {
                strings.Add(grid.LabelX);
                strings.Add(grid.LabelY);
                strings.Add(grid.LabelZ);
            }
            return strings;
        }
    }
}
