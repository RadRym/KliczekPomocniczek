using KliczekPomocniczek.QuickMenu;
using System;
using System.CodeDom;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Internal;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace KliczekPomocniczek.Skills
{
    public static class partCoordSyst
    {
        public static QuickMenuPage QuickMenuPage = new QuickMenuPage();
        public static void Draw()
        {
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                ModelObjectEnumerator.AutoFetch = true;
                TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
                TSM.ModelObjectEnumerator modelObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

                TransformationPlane currentPlane = Model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

                foreach (Part modelObject in modelObjects)
                {
                    if (modelObject is TSM.ModelObject)
                    {
                        var drawer = new GraphicsDrawer();
                        TransformationPlane localPlane = new TransformationPlane(modelObject.GetCoordinateSystem());
                        Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);
                        var location = modelObject.GetCoordinateSystem().Origin;

                        drawer.DrawLineSegment(location, location + new Point(100, 0, 0), new Color(1, 0, 0));
                        drawer.DrawText(location + new Point(100, 0, 0), "X", new Color(1, 0, 0));
                        drawer.DrawLineSegment(location, location + new Point(0, 100, 0), new Color(0, 1, 0.0));
                        drawer.DrawText(location + new Point(0, 100, 0), "Y", new Color(0, 1, 0.0));
                        drawer.DrawLineSegment(location, location + new Point(0, 0, 100), new Color(0, 0, 1));
                        drawer.DrawText(location + new Point(0, 0, 100), "Z", new Color(0, 0, 1));
                    }
                    else return;

                }
                Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            }
            else return;
        }
        
        public static void Set()
        {
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                Operation.DisplayPrompt("Wyemancypowany typie zaznacz parta do którego chcesz się przykleić");
                if (QuickMenuPage.IsActive == true)
                    QuickMenuPage.Hide();
                Model model = new Model();
                var modelObject = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_PART);
                CoordinateSystem PartCoordinate = modelObject.GetCoordinateSystem();
                TransformationPlane PartPlane = new TransformationPlane(PartCoordinate);
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(PartPlane);
                model.CommitChanges();
                ViewHandler.RedrawWorkplane();
            }
            else return;
        }

        public static void Redraw()
        {
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                ModelViewEnumerator ViewEnum = ViewHandler.GetAllViews();
                while (ViewEnum.MoveNext())
                {
                    View ViewSel = ViewEnum.Current;
                    ViewHandler.RedrawView(ViewSel);
                }
            }
            else return;
        }
    }
}
