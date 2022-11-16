using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using System.Collections;
using Point = Tekla.Structures.Geometry3d.Point;
using Vector = Tekla.Structures.Geometry3d.Vector;
using System.Collections.Generic;
using System.Linq;
using Operation = Tekla.Structures.Model.Operations.Operation;
using Part = Tekla.Structures.Model.Part;

namespace KliczekPomocniczek.Skills
{
    internal class clipPlanes
    {
        public static int clipPlanesOffset = 200;

        public static void createClipPlanes()
        {
            deleteClipPlanes();
            TSM.Model Model = new TSM.Model();
            TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
            TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

            List<Solid> solidPartsList = new List<Solid>();
            List<double> maxXList = new List<double>();
            List<double> minXList = new List<double>();
            List<double> maxYList = new List<double>();
            List<double> minYList = new List<double>();
            List<double> maxZList = new List<double>();
            List<double> minZList = new List<double>();

            while (selectedObjects.MoveNext())
            {
                if ((selectedObjects.Current as TSM.Part) != null)
                {
                    Part part = (Part)selectedObjects.Current;
                    Solid solidPart = part.GetSolid();
                    solidPartsList.Add(solidPart);
                }
                else if ((selectedObjects.Current as TSM.Assembly) != null)
                {
                    Assembly assembly = (Assembly)selectedObjects.Current;
                    Part mainPartAssembly = (Part)assembly.GetMainPart();
                    Solid solidMainPartAssembly = mainPartAssembly.GetSolid();
                    solidPartsList.Add(solidMainPartAssembly);
                    foreach (Part partSecondaries in assembly.GetSecondaries())
                    {
                        Solid solidPartSecondaries = partSecondaries.GetSolid();
                        solidPartsList.Add(solidPartSecondaries);
                    }
                }
                else if ((selectedObjects.Current as TSM.Component) != null)
                {
                    Operation.DisplayPrompt("Komponenty nie są obsługiwane");
                    break;
                }
            }

            foreach (Solid solid in solidPartsList)
            {
                double maxPointX = solid.MaximumPoint.X;
                maxXList.Add(maxPointX);
                double maxPointY = solid.MaximumPoint.Y;
                maxYList.Add(maxPointY);
                double maxPointZ = solid.MaximumPoint.Z;
                maxZList.Add(maxPointZ);
                double minPointX = solid.MinimumPoint.X;
                minXList.Add(minPointX);
                double minPointY = solid.MinimumPoint.Y;
                minYList.Add(minPointY);
                double minPointZ = solid.MinimumPoint.Z;
                minZList.Add(minPointZ);
            }

            if (solidPartsList.Count > 0)
            {
                double maxX = maxXList.Max<double>();
                double minX = minXList.Min<double>();
                double maxY = maxYList.Max<double>();
                double minY = minYList.Min<double>();
                double maxZ = maxZList.Max<double>();
                double minZ = minZList.Min<double>();

                ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
                ViewEnum.MoveNext();
                View ActiveView = ViewEnum.Current;
                ClipPlane CPlane = new ClipPlane();
                CPlane.View = ActiveView;
                CPlane.UpVector = new Vector(1, 0, 0);
                CPlane.Location = new Point(maxX + clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(-1, 0, 0);
                CPlane.Location = new Point(minX - clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, maxY + clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, -1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, minY - clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, 1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, maxZ + clipPlanesOffset);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, -1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, minZ - clipPlanesOffset);
                CPlane.Insert();

                Operation.DisplayPrompt("Clip planes created. Pozdro 600");
            }
        }

        public static void deleteClipPlanes()
        {
            ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
            ViewEnum.MoveNext();
            View ActiveView = ViewEnum.Current;
            ClipPlaneCollection ClipPlanes = ActiveView.GetClipPlanes();
            if (ClipPlanes.Count > 0)
            {
                IEnumerator PlaneEnum = ClipPlanes.GetEnumerator();
                while (PlaneEnum.MoveNext())
                {
                    ClipPlane CPlane = PlaneEnum.Current as ClipPlane;
                    if (CPlane != null)
                        CPlane.Delete();
                }
            }
        }
    }
}
