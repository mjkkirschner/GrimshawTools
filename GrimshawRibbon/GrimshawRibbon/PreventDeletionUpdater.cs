using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace GrimshawRibbon
{
    class PreventDeletionUpdater : IUpdater
    {
        static AddInId _appId;
        UpdaterId _updaterId;
        FailureDefinitionId _failureId = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="addInId">Add-in id of the 
        /// add-in associated with this updater.</param>
        public PreventDeletionUpdater(AddInId addInId)
        {
            _appId = addInId;

            _updaterId = new UpdaterId(_appId, new Guid(
              "6f453eba-4b9a-40df-b637-eb72a9ebf008"));

            _failureId = new FailureDefinitionId(
              new Guid("33ba8315-e031-493f-af92-4f417b6ccf70"));

            FailureDefinition failureDefinition
              = FailureDefinition.CreateFailureDefinition(
                _failureId, FailureSeverity.Error,
                "PreventDeletion: Sorry, this element cannot be deleted. Please contact your BIM Manager to find out why.");
        }

        public void Execute(UpdaterData data)
        {
            Document doc = data.GetDocument();
            Application app = doc.Application;
            foreach (ElementId id in data.GetDeletedElementIds())
            {
                if (PreventDeletion.IsProtected(id))
                {
                    FailureMessage failureMessage
                      = new FailureMessage(_failureId);

                    failureMessage.SetFailingElement(id);
                    doc.PostFailure(failureMessage);
                }
            }
        }

        public string GetAdditionalInformation()
        {
            return "Prevent deletion of selected elements.";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.FloorsRoofsStructuralWalls;
        }

        public UpdaterId GetUpdaterId()
        {
            return _updaterId;
        }

        public string GetUpdaterName()
        {
            return PreventDeletion.Caption;
        }
    }
}
