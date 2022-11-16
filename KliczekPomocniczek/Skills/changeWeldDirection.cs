using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using TSMW = Tekla.Structures.Model.Weld.WeldPositionEnum;
using Tekla.Structures.Model;
using System;

namespace KliczekPomocniczek.Skills
{
    public class changeWeldDirection
    {
        public static void weldPositionEnum()
        {
            try
            {
                TSM.Model Model = new TSM.Model();
                if (activeWindows.isActive("TeklaStructures"))
                {
                    TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
                    TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

                    while (selectedObjects.MoveNext())
                    {
                        if(selectedObjects.Current is Weld)
                        {
                            var weld = selectedObjects.Current as TSM.Weld;
                            TSM.Weld.WeldPositionEnum weldPositionEnum = weld.Position;
                            if ((selectedObjects.Current as TSM.Weld) != null)
                            {
                                if (weldPositionEnum == TSMW.WELD_POSITION_PLUS_X)
                                    weld.Position = TSMW.WELD_POSITION_MINUS_X;
                                else if (weldPositionEnum == TSMW.WELD_POSITION_MINUS_X)
                                    weld.Position = TSMW.WELD_POSITION_PLUS_Y;
                                else if (weldPositionEnum == TSMW.WELD_POSITION_PLUS_Y)
                                    weld.Position = TSMW.WELD_POSITION_MINUS_Y;
                                else if (weldPositionEnum == TSMW.WELD_POSITION_MINUS_Y)
                                    weld.Position = TSMW.WELD_POSITION_PLUS_Z;
                                else if (weldPositionEnum == TSMW.WELD_POSITION_PLUS_Z)
                                    weld.Position = TSMW.WELD_POSITION_MINUS_Z;
                                else if (weldPositionEnum == TSMW.WELD_POSITION_MINUS_Z)
                                    weld.Position = TSMW.WELD_POSITION_PLUS_X;
                            }
                            weld.Modify();
                        }
                    }
                    Model.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WritrLog(ex.Message);
            }
        }
        public static bool isWeldSelected()
        {
            try
            {
                TSM.Model Model = new TSM.Model();
                TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
                TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

                int falseOrTrue = 0;
                foreach (var obj in selectedObjects)
                {
                    if (obj is Weld)
                        falseOrTrue++;
                    else continue;
                }

                if (falseOrTrue == 0)
                    return false;
                else return true;
            }
            catch (Exception ex)
            {
                Logger.WritrLog(ex.Message);
                return false;
            }
        }
    }
}
