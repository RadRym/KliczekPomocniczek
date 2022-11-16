using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KliczekPomocniczek.Skills
{
    public class comboboxes
    {
        public List<string> GridCollection { get; set; }

        public comboboxes()
        {
            GridCollection = gridManipulation.LabelsGrid();
        }
    }
}
