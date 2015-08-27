using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace GrimshawRibbon
{
    public class DetailLineFilter : ISelectionFilter
    {
        public bool AllowElement(Element e)
        {
            return (e.Category.Id.IntegerValue.Equals(
              (int)BuiltInCategory.OST_Lines));
        }
        public bool AllowReference(Reference r, XYZ p)
        {
            return false;
        }
    }

    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class CurveTotalLength : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            UIDocument uidoc = uiApp.ActiveUIDocument;

            try
            {
                IList<Element> pickedRef = null;
                Selection sel = uiApp.ActiveUIDocument.Selection;
                DetailLineFilter selFilter = new DetailLineFilter();
                pickedRef = sel.PickElementsByRectangle(selFilter, "Select lines");

                List<double> lengthList = new List<double>();
                foreach (Element e in pickedRef)
                {
                    DetailLine line = e as DetailLine;
                    if (line != null)
                    {
                        lengthList.Add(line.GeometryCurve.Length);
                    }
                }

                string lengthFeet = Math.Round(lengthList.Sum(), 2).ToString() + " ft";
                string lengthMeters = Math.Round(lengthList.Sum() * 0.3048, 2).ToString() + " m";
                string lengthMilimeters = Math.Round(lengthList.Sum() * 304.8, 2).ToString() + " mm";
                string lengthInch = Math.Round(lengthList.Sum() * 12, 2).ToString() + " inch";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Total Length is:");
                sb.AppendLine(lengthFeet);
                sb.AppendLine(lengthInch);
                sb.AppendLine(lengthMeters);
                sb.AppendLine(lengthMilimeters);

                TaskDialog.Show("Line Length", sb.ToString());

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
