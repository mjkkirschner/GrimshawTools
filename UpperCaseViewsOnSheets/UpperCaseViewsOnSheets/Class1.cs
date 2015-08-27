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

public class UpperCaseViewsOnSheets : IExternalCommand
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
                trans.Start();
                // Get all Views on sheets and change name to upper case
                int counter = 0;
                List<ElementId> viewsOnSheets = new List<ElementId>();
                foreach (View v in new FilteredElementCollector(doc)
                    .OfClass(typeof(View)))
                {
                    foreach (ViewSheet vs in new FilteredElementCollector(doc)
                        .OfClass(typeof(ViewSheet))
                        .Cast<ViewSheet>())
                    {
                        ICollection<ElementId> viewsIds = vs.GetAllPlacedViews();
                        List<View> views = new List<View>();
                        foreach (ElementId id in viewsIds)
                        {
                            View e = doc.GetElement(id) as View;
                            views.Add(e);
                        }
                        foreach (View view in views)
                        {
                            if (view.Id == v.Id)
                            {
                                try
                                {
                                    view.Name = view.Name.ToUpper();
                                    viewsOnSheets.Add(view.Id);
                                    counter++;
                                }
                                catch (Exception x)
                                {
                                    message = x.Message;
                                }
                            }
                        }
                        views.Clear();
                    }
                }
                // get all views not on sheets by using exclusion filter and change names to lowercase
                ExclusionFilter filter = new ExclusionFilter(viewsOnSheets);
                IList<Element> viewsNotOnSheets = new FilteredElementCollector(doc)
                    .OfClass(typeof(View))
                    .WhereElementIsNotElementType()
                    .WherePasses(filter)
                    .ToElements();
                try
                {
                    foreach (View v in viewsNotOnSheets)
                    {
                        v.Name = v.Name.ToLower();
                        counter++;
                    }
                }
                catch (Exception x)
                {
                    message = x.Message;
                }

                // Get all schedules on sheets and change name to upper case
                IList<Element> schInstances = new FilteredElementCollector(doc)
                    .OfClass(typeof(ScheduleSheetInstance))
                    .ToElements();
                List<ViewSchedule> masterSchedules = new List<ViewSchedule>();
                List<ElementId> schedulesOnSheet = new List<ElementId>();

                foreach (ScheduleSheetInstance ssi in schInstances)
                {
                    ViewSchedule masterSchedule = doc.GetElement(ssi.ScheduleId) as ViewSchedule;
                    masterSchedules.Add(masterSchedule);
                }
                var distinctSchedules = masterSchedules.GroupBy(x => x.Id).Select(y => y.First());
                foreach (ViewSchedule vs in distinctSchedules)
                {
                    try
                    {
                        vs.Name = vs.Name.ToUpper();
                        schedulesOnSheet.Add(vs.Id);
                        counter++;
                    }
                    catch (Exception x)
                    {
                        message = x.Message;
                    }
                }
                // Get all schedules not on sheets and change name to lower case
                ExclusionFilter schFilter = new ExclusionFilter(schedulesOnSheet);
                IList<Element> schedulesNotOnSheet = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewSchedule))
                    .WhereElementIsNotElementType()
                    .WherePasses(schFilter)
                    .ToElements();

                foreach (ViewSchedule vs in schedulesNotOnSheet)
                {
                    try
                    {
                        vs.Name = vs.Name.ToLower();
                        counter++;
                    }
                    catch (Exception x)
                    {
                        message = x.Message;
                    }
                }
                trans.Commit();
                TaskDialog.Show("Views on Sheets - Uppercase", "You have successfully changed names for " + counter + " views! ");
                return Result.Succeeded;
            }
        }
        // Catch any Exceptions and display them.
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