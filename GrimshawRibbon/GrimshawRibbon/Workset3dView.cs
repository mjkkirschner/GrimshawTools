using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace GrimshawRibbon
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class Workset3dView : IExternalCommand
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
                if (!doc.IsWorkshared)
                {
                    TaskDialog.Show("Workset 3D View", "Project doesn't have any Worksets.");
                }
                else
                {
                    ViewFamilyType vft = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault(q => q.ViewFamily == ViewFamily.ThreeDimensional);

                    using (Transaction t = new Transaction(doc, "Workset View Creation"))
                    {
                        t.Start();
                        int i = 0;
                        // Loop through all User Worksets only
                        foreach (Workset wst in new FilteredWorksetCollector(doc)
                            .WherePasses(new WorksetKindFilter(WorksetKind.UserWorkset)))
                        {
                            // Create a 3D View
                            View3D view = View3D.CreateIsometric(doc, vft.Id);

                            // Set the name of the view to match workset
                            view.Name = "WORKSET - " + wst.Name;

                            // Isolate elements in the view using a filter to find elements only in this workset
                            view.IsolateElementsTemporary(new FilteredElementCollector(doc)
                                .WherePasses(new ElementWorksetFilter(wst.Id))
                                .Select(q => q.Id)
                                .ToList());
                            i++;
                        }
                        t.Commit();
                        TaskDialog.Show("Workset 3D View", i.ToString() + " Views Created Successfully!");
                    }
                } 
                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}