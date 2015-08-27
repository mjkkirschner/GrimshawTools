using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PreventDeletion
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            DeletionUpdater deletionUpdater
              = new DeletionUpdater(a.ActiveAddInId);

            UpdaterRegistry.RegisterUpdater(
              deletionUpdater);

            ElementClassFilter filter
              = new ElementClassFilter(
                typeof(Dimension), true);

            UpdaterRegistry.AddTrigger(
              deletionUpdater.GetUpdaterId(), filter,
              Element.GetChangeTypeElementDeletion());

            //FailureDefinitionRegistry

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}