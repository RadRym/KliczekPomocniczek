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
using System;

namespace KliczekPomocniczek.Skills
{
    internal class Components
    {
        public static List<Component> selectedComponents()
        {
            List<Component> selectedComponents = new List<Component>();
            TSM.Model Model = new TSM.Model();
            if (Model.GetConnectionStatus())
            {
                TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
                TSM.ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as TSM.ModelObjectEnumerator);
                while (selectedObjects.MoveNext())
                {
                    if ((selectedObjects.Current as TSM.Component) != null)
                    {
                        Component component = (Component)selectedObjects.Current;   
                        selectedComponents.Add(component);
                    }
                }
            }
            return selectedComponents;
        }
        public static List<Component> selectedConceptualComponents(List<Component> selectedComponents)
        {
            List<Component> selectedConceptualComponents = new List<Component>();
            foreach (Component component in selectedComponents)
            {
                if (TSM.Operations.Operation.ObjectMatchesToFilter(component, "_isConceptual__"))
                {
                    selectedConceptualComponents.Add(component);
                }
            }
            return selectedConceptualComponents;
        }
        public static List<Component> selectedDetailedComponents(List<Component> selectedComponents)
        {
            List<Component> selectedDetailedComponents = new List<Component>();
            foreach (Component component in selectedComponents)
            {
                if (TSM.Operations.Operation.ObjectMatchesToFilter(component, "_isConceptual__") == false)
                {
                    selectedDetailedComponents.Add(component);
                }
            }
            return selectedDetailedComponents;
        }

        public static void ConceptualToDetailed(List<Component> selectedConceptualComponents)
        {

        }

        public static void DetailedToConceptual(List<Component> selectedDetailedComponents)
        {

        }
    }
}
