using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace GrimshawRibbon
{
    // Create Elment Filter that passes all elements through
    public class ElementFilter : ISelectionFilter
    {
        public bool AllowElement(Element e)
        {
            return true;
        }
        public bool AllowReference(Reference r, XYZ p)
        {
            return false;
        }
    }

    /// <summary>
    /// External command to select elements 
    /// to protect from deletion.
    /// </summary>
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class PreventDeletion : IExternalCommand
    {
        public const string Caption = "Prevent Deletion";

        static List<ElementId> _protectedIds
          = new List<ElementId>();

        static public bool IsProtected(ElementId id)
        {
            return _protectedIds.Contains(id);
        }

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uiapp.ActiveUIDocument.Document;

            IList<Reference> refs = null;
            ElementFilter selFilter = new ElementFilter();
            List<Reference> preSelectedElems = new List<Reference>();

            if (_protectedIds.Count != 0)
            {
                foreach (ElementId id in _protectedIds)
                {
                    Reference elemRef = new Reference(doc.GetElement(id));
                    preSelectedElems.Add(elemRef);
                }
            }

            try
            {
                // prompt user to add to selection or remove from it
                Selection sel = uidoc.Selection;
                refs = sel.PickObjects(ObjectType.Element, selFilter, "Please pick elements to prevent from deletion.", preSelectedElems);
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled;
            }

            _protectedIds.Clear();

            if (null != refs && 0 < refs.Count)
            {
                foreach (Reference r in refs)
                {
                    ElementId id = r.ElementId;

                    if (!_protectedIds.Contains(id))
                    {
                        _protectedIds.Add(id);
                    }
                }
                int n = refs.Count;

                TaskDialog.Show(Caption, string.Format(
                  "{0} new element{1} selected and protected "
                  + " from deletion, {2} in total.",
                  n, (1 == n ? "" : "s"),
                  _protectedIds.Count));
            }
            return Result.Succeeded;
        }
    }
}
