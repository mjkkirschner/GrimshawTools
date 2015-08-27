using System;
using System.Collections.Generic;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;

namespace GrimshawRibbon
{
    public class CsAddPanel : IExternalApplication
    {
        static void AddRibbonPanel(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            String tabName = "Grimshaw";
            application.CreateRibbonTab(tabName);

            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Grimshaw Architects");

            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            #region Curve Total Length Button
            PushButtonData buttonData = new PushButtonData("cmdCurveTotalLength",
               "Total Length", thisAssemblyPath, "GrimshawRibbon.CurveTotalLength");
            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Select Multiple Lines to Obtain Total Length";
            // Add image icon to 
            Uri uriImage = new Uri(@"D:\Stuff\RevitVisualStudio\CurveTotalLength\CurveTotalLength\bin\Debug\CurveTotalLength.png");
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;
            #endregion // Curve Total Length Button

            #region Workset 3d View, Upper Case Views on Sheet and Delete Reference Planes Buttons
            // Create two push buttons
            // Project Management Commands
            PushButtonData pushButton1 = new PushButtonData("cmdWorkset3dView", "Make 3D View/Workset", thisAssemblyPath, "GrimshawRibbon.Workset3dView");
            pushButton1.Image = new BitmapImage(new Uri(@"D:\Stuff\RevitVisualStudio\Workset3dView\Workset3dView\bin\Debug\favicon.png"));
            pushButton1.ToolTip = "Create one 3D View per workset with all elemets on that workset isolated.";

            PushButtonData pushButton2 = new PushButtonData("cmdUpperCaseViewsOnSheets", "UpperCase Sheet Views", thisAssemblyPath, "GrimshawRibbon.UpperCaseViewsOnSheets");
            pushButton2.Image = new BitmapImage(new Uri(@"D:\Stuff\RevitVisualStudio\UpperCaseViewsOnSheets\UpperCaseViewsOnSheets\bin\Debug\UpperCaseViewsOnSheets.png"));
            pushButton2.ToolTip = "Rename all Views in the Project to 'uppercase' if its on a Sheet and 'lowercase' if its not on a Sheet.";

            PushButtonData pushButton3 = new PushButtonData("cmdDeleteReferencePlanes", "Delete Reference Planes", thisAssemblyPath, "GrimshawRibbon.DeleteReferencePlanes");
            pushButton3.Image = new BitmapImage(new Uri(@"D:\Stuff\RevitVisualStudio\DeleteReferencePlanes\DeleteReferencePlanes\bin\Debug\deleteReferencePlanes.png"));
            pushButton3.ToolTip = "Delete all unnamed reference planes in the project.";

            // Add the buttons to the panel
            List<RibbonItem> projectButtons = new List<RibbonItem>();
            projectButtons.AddRange(ribbonPanel.AddStackedItems(pushButton1, pushButton2, pushButton3));
            #endregion // Workset 3d View, Upper Case Views on Sheet and Delete Reference Planes Buttons

            #region Prevent Deletion Button
            PushButtonData preventDelButtonData = new PushButtonData("cmdPreventDeletion",
               "Prevent Deletion", thisAssemblyPath, "GrimshawRibbon.PreventDeletion");
            PushButton preventDelButton = ribbonPanel.AddItem(preventDelButtonData) as PushButton;
            preventDelButton.ToolTip = "Prevent elements from being deleted.";
            // Add image icon to 
            Uri preventDelImage = new Uri(@"D:\Stuff\RevitVisualStudio\PreventDeletion\PreventDeletion\bin\Debug\preventDeletion.png");
            BitmapImage preventDellargeImage = new BitmapImage(preventDelImage);
            preventDelButton.LargeImage = preventDellargeImage;
            #endregion // Prevent Deletion Button
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);

            // Failure Definition Registry
            // Implemented in Prevent Deletion Tool
            PreventDeletionUpdater deletionUpdater
              = new PreventDeletionUpdater(application.ActiveAddInId);

            UpdaterRegistry.RegisterUpdater(
              deletionUpdater);

            ElementClassFilter filter
              = new ElementClassFilter(
                typeof(Dimension), true);

            UpdaterRegistry.AddTrigger(
              deletionUpdater.GetUpdaterId(), filter,
              Element.GetChangeTypeElementDeletion());

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}