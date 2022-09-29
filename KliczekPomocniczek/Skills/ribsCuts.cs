using System;
using System.Collections;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace KliczekPomocniczek.Skills
{
    public static class ribsCuts
    {
        public static void Run()
        {
            Model model = new Model();
            ModelObjectEnumerator.AutoFetch = true;

            getAllParts(model);
            foreach(Beam beam in allBeams)
            {
                foreach (ContourPlate contourPlate in getContourPlates(beam))
                {
                    TransformationPlane currentPlane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                    foreach (Point point in contourPoints(contourPlate))
                    {
                        TransformationPlane localPlane = new TransformationPlane(beam.GetCoordinateSystem());
                        model.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);
                        var location = beam.GetCoordinateSystem().Origin;


                        var profiles = new Hashtable(); 
                        beam.GetDoubleReportProperties(doubleReportProperties, ref profiles);
                        double ts = (double)profiles[0];

                        if (point.Z == -0.5 * ts && point.X > 0 && point.X < beam.EndPoint.X)
                        {
                            var drawer = new GraphicsDrawer();
                            drawer.DrawText(point, point.Z.ToString(), new Color(1, 0, 0));
                        }
                        else if (point.Z == 0.5 * ts)
                        {
                            var drawer = new GraphicsDrawer();
                            drawer.DrawText(point, point.X.ToString() + point.Y.ToString() + point.Z.ToString(), new Color(1, 0, 0));
                        }
                    }
                    model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
                }
            }
        }

        public static List<Beam> allBeams = new List<Beam>();

        private static readonly ArrayList doubleReportProperties = new ArrayList
        {
            "WEB_THICKNESS"
        };

        public static void prompt()
        {
            TSM.Model Model = new TSM.Model();
            TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
            TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);

            while (selectedObjects.MoveNext())
            {
                if ((selectedObjects.Current as TSM.Beam) != null)
                {
                    var profiles = new Hashtable();
                    Beam beam = selectedObjects.Current as Beam;
                    beam.GetDoubleReportProperties(doubleReportProperties, ref profiles);
                    foreach(DictionaryEntry ele1 in profiles.Values)
                    {
                        Operation.DisplayPrompt($"TSS to: {ele1}");
                        System.Threading.Thread.Sleep(1000);
                    }

                }
            }
        }


        static void getAllParts(Model model)
        {
            var types = new[] { typeof(Beam) };
            var modelObjects = model.GetModelObjectSelector().GetAllObjectsWithType(types);
            foreach (var modelObject in modelObjects)
            {
                allBeams.Add(modelObject as Beam);
                
            }
        }

        static List<ContourPlate> getContourPlates(Beam beam)
        {
            List<ContourPlate> contourPlates = new List<ContourPlate>();
            Assembly beamAssembly = beam.GetAssembly();
            var secondaryObjects = beamAssembly.GetSecondaries();
            foreach (var Object in secondaryObjects)
            {
                if(Object.GetType() == typeof(ContourPlate))    
                    contourPlates.Add(Object as ContourPlate);
            }
            return contourPlates;
        }

        static List<Point> contourPoints(ContourPlate contourPlate)
        {
            List<Point> contourPoints = new List<Point>();
            contourPlate.Contour.ContourPoints.CopyTo(new Point[contourPlate.Contour.ContourPoints.Count]);
            foreach (Point point in contourPlate.Contour.ContourPoints)
            {
                contourPoints.Add(new Point(point.X, point.Y));
            }
            return contourPoints;
        }
    }
}
