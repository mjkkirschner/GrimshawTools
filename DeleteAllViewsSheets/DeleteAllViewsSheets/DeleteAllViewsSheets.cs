using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace GrimshawRibbon
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class DeleteAll : IExternalCommand
    {
        IList<Element> GetViewsToDelete(Document doc)
        {
            using (Transaction trans = new Transaction(doc))
            {
                // Collect all Views except ViewTemplates
                // Add one drafting view to project before deleting
                // all views because Revit will shut down when last view
                // is deleted from project.

                // Create a new Drafting view 
                ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>().First(vft => vft.ViewFamily == ViewFamily.Drafting);

                trans.Start("Delete All Views/Sheets");
                ViewDrafting view = ViewDrafting.Create(doc, viewFamilyType.Id);
                view.ViewName = "TempDraftingView";

                doc.Regenerate();

                // Collect all Views except newly created one
                List<ElementId> exclude = new List<ElementId>();
                exclude.Add(view.Id);

                ExclusionFilter filter = new ExclusionFilter(exclude);
                IList<Element> views = new FilteredElementCollector(doc)
                    .OfClass(typeof(View))
                    .WhereElementIsNotElementType()
                    .WherePasses(filter)
                    .ToElements();

                // Remove all ViewTemplates from views to be deleted
                for (var i = 0; i < views.Count; i++)
                {
                    View v = views[i] as View;
                    if (v.IsTemplate)
                    {
                        views.RemoveAt(i);
                    }
                }
                trans.Commit();
                return views;
            }
        }

        IList<Element> GetSheetsToDelete(Document doc)
        {
            // Collect all sheets
            IList<Element> allSheets = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSheet))
                .ToElements();

            return allSheets;
        }

        public Result Execute2(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements,
          bool delViews,
          bool delSheets)
        {
            // Get application and document objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            UIDocument uidoc = uiApp.ActiveUIDocument;

            try
            {
                using (Transaction trans = new Transaction(doc, "UpperCase Views on Sheets."))
                {
                    // Get All Elements to delete
                    List<Element> delElements = new List<Element>();
                    if (delViews == true)
                    { 
                        // add views
                        IList<Element> views = GetViewsToDelete(doc);
                        foreach (Element e in views)
                        {
                            delElements.Add(e);
                        }
                    }
                    if (delSheets == true)
                    { 
                        // add sheets
                        IList<Element> sheets = GetSheetsToDelete(doc);
                        foreach (Element e in sheets)
                        {
                            delElements.Add(e);
                        }
                    }

                    // Delete Elements based on input delElements
                    int n = delElements.Count;
                    string s = "{0} of " + n.ToString() + " Sheets/Views processed...";
                    string caption = "Delete All Views/Sheets";
                    using (ProgressForm pf = new ProgressForm(caption, s, n))
                    {
                        foreach (Element e in delElements)
                        {
                            try
                            {
                                doc.Delete(e.Id);
                            }
                            catch (Exception x)
                            {
                                message = x.Message;
                            }
                            pf.Increment();
                        }
                    }
                    trans.Commit();
                }
                return Result.Succeeded;
            }
            // Catch any Exceptions and display them.
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception x)
            {
                message = x.Message;
                return Result.Failed;
            }
        }

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            bool arg1 = Globals.deleteViewsBool;
            bool arg2 = Globals.deleteSheetsBool;
            return Execute2(commandData, ref message, elements, arg1, arg2);
        }
    }
}