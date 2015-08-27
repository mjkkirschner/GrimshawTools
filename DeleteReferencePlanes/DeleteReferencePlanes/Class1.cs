using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]

public class DeleteReferencePlanes : IExternalCommand
{
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
        // Get application and document objects
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;
        UIDocument uidoc = uiApp.ActiveUIDocument;

        try
        {
            using (Transaction trans = new Transaction(doc, "UpperCase Views on Sheets."))
            {
                // Delete all unnamed Reference Planes in the project
                // Create Filter to check for Name
                BuiltInParameter bip = BuiltInParameter.DATUM_TEXT;
                ParameterValueProvider provider = new ParameterValueProvider(new ElementId(bip));
                FilterStringEquals evaluator = new FilterStringEquals();
                FilterStringRule rule = new FilterStringRule(provider, evaluator, "", false);
                ElementParameterFilter filter = new ElementParameterFilter(rule);

                ICollection<ElementId> refPlanes = new FilteredElementCollector(doc)
                    .OfClass(typeof(ReferencePlane))
                    .WherePasses(filter)
                    .ToElementIds();

                int count = refPlanes.Count;
                try
                {
                    trans.Start();
                    doc.Delete(refPlanes);
                    trans.Commit();
                }
                catch (Exception x)
                {
                    message = x.Message;
                    return Result.Failed;
                }
               
                TaskDialog.Show("Delete Reference Planes", "You have successfully delete " + count.ToString() + " unnamed reference planes!");
                return Result.Succeeded;
            }
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
}