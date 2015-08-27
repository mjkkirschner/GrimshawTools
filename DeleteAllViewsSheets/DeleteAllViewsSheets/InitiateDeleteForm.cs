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

    public class GenerateForm : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            try
            {
                ExternalEventExample handler = new ExternalEventExample();
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                ExternalEventExampleDialog m_MyForm = new ExternalEventExampleDialog(exEvent, handler);
                m_MyForm.Show();
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
            return Result.Succeeded;
        }
    }
}