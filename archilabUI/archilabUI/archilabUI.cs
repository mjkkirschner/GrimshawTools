using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using DSCore;
using DSCoreNodesUI;
using Dynamo.Applications;
using Dynamo.DSEngine;
using Dynamo.Models;
using Dynamo.Utilities;
using ProtoCore.AST.AssociativeAST;
using Revit.Elements;
using RevitServices.EventHandler;
using RevitServices.Persistence;

using Category = Revit.Elements.Category;
using Element = Autodesk.Revit.DB.Element;
using Family = Autodesk.Revit.DB.Family;
using FamilyInstance = Autodesk.Revit.DB.FamilyInstance;
using FamilySymbol = Autodesk.Revit.DB.FamilySymbol;
using Level = Autodesk.Revit.DB.Level;
using Parameter = Autodesk.Revit.DB.Parameter;

using archilab;

namespace archilabUI
{

    # region Revit Drop Down Base
    public abstract class RevitDropDownBase : DSDropDownBase
    {

        protected RevitDropDownBase(string value)
            : base(value)
        {
            DynamoRevitApp.EventHandlerProxy.DocumentOpened += Controller_RevitDocumentChanged;
        }

        void Controller_RevitDocumentChanged(object sender, EventArgs e)
        {
            PopulateItems();

            if (Items.Any())
            {
                SelectedIndex = 0;
            }
        }

        public override void Dispose()
        {
            DynamoRevitApp.EventHandlerProxy.DocumentOpened -= Controller_RevitDocumentChanged;
            base.Dispose();
        }
    }
    #endregion //Revit Drop Down Base

    #region archi-lab_Grimshaw.Selection.ParameterGroups
    [NodeName("Parameter Groups")]
    [NodeCategory("Archi-lab_Grimshaw.Selection")]
    [NodeDescription("Retrieve all available Parameter Groups.")]
    [IsDesignScriptCompatible]
    public class ParameterGroupUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No Parameter Groups Available.";
        public ParameterGroupUI() : base("Parameter Group") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var pgList = Enum.GetValues(typeof(BuiltInParameterGroup))
                    .Cast<BuiltInParameterGroup>()
                    .ToList();

            if (pgList.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (var pg in pgList)
            {
                Items.Add(new DynamoDropDownItem(pg.ToString(), pg));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((BuiltInParameterGroup) Items[SelectedIndex].Item).ToString())
            };

            var func = new Func<string, BuiltInParameterGroup>(GetParameterGroup.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.ParameterGroups

    #region archi-lab_Grimshaw.Selection.FillPatternTarget
    [NodeName("Fill Pattern Targets")]
    [NodeCategory("Archi-lab_Grimshaw.Selection")]
    [NodeDescription("Retrieve FillPatternTarget types.")]
    [IsDesignScriptCompatible]
    public class FillPatternTargetsUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No FillPatternTarget Types Available.";
        public FillPatternTargetsUI() : base("Fill Pattern Target") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var prList = Enum.GetValues(typeof(FillPatternTarget))
                    .Cast<FillPatternTarget>()
                    .ToList();

            if (prList.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (var fpt in prList)
            {
                Items.Add(new DynamoDropDownItem(fpt.ToString(), fpt));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((FillPatternTarget) Items[SelectedIndex].Item).ToString())
            };

            var func = new Func<string, FillPatternTarget>(GetFillPatternTarget.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.FillPatternTarget

    #region archi-lab_Grimshaw.Printing.PrintSettings
    [NodeName("Print Settings")]
    [NodeCategory("Archi-lab_Grimshaw.Printing")]
    [NodeDescription("Retrieve all available Print Settings from Revit project.")]
    [IsDesignScriptCompatible]
    public class PrintSettings : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No Print Settings Available.";
        public PrintSettings() : base("Print Settings") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var printSettings = new FilteredElementCollector(DocumentManager.Instance.CurrentDBDocument)
                .OfClass(typeof(PrintSetting))
                .ToElements();

            if (printSettings.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (PrintSetting ps in printSettings)
            {
                Items.Add(new DynamoDropDownItem(ps.Name, ps));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((PrintSetting) Items[SelectedIndex].Item).Name)
            };

            var func = new Func<string, Revit.Elements.Element>(GetPrintSetting.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.PrintSettings

    #region archi-lab_Grimshaw.Printing.ViewSets
    [NodeName("View Sets")]
    [NodeCategory("Archi-lab_Grimshaw.Printing")]
    [NodeDescription("Retrieve all available View Sets from Revit project.")]
    [IsDesignScriptCompatible]
    public class ViewSets : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No View Sets Available.";
        public ViewSets() : base("View Sets") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var vsList = new FilteredElementCollector(DocumentManager.Instance.CurrentDBDocument)
                .OfClass(typeof(ViewSheetSet))
                .ToElements();

            if (vsList.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (ViewSheetSet vs in vsList)
            {
                Items.Add(new DynamoDropDownItem(vs.Name, vs));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((ViewSheetSet) Items[SelectedIndex].Item).Name)
            };

            var func = new Func<string, Revit.Elements.Element>(GetViewSet.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.ViewSets

    #region archi-lab_Grimshaw.Selection.ParameterTypes
    [NodeName("Parameter Types")]
    [NodeCategory("Archi-lab_Grimshaw.Selection")]
    [NodeDescription("Retrieve all available Parameter Types from Revit project.")]
    [IsDesignScriptCompatible]
    public class ParameterTypes : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No Parameter Types Available.";
        public ParameterTypes() : base("Parameter Types") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var ptList = Enum.GetValues(typeof(ParameterType))
                    .Cast<ParameterType>()
                    .ToList();

            if (ptList.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (var pt in ptList)
            {
                Items.Add(new DynamoDropDownItem(pt.ToString(), pt));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((ParameterType) Items[SelectedIndex].Item).ToString())
            };

            var func = new Func<string, ParameterType>(GetParamType.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.ParameterTypes

    #region archi-lab_Grimshaw.Printing.PrintRange
    [NodeName("Print Range")]
    [NodeCategory("Archi-lab_Grimshaw.Printing")]
    [NodeDescription("Retrieve all available Print Ranges from Revit project.")]
    [IsDesignScriptCompatible]
    public class PrintRanges : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No Print Ranges Available.";
        public PrintRanges() : base("Print Range") { }
        public override void PopulateItems()
        {
            Items.Clear();

            var prList = Enum.GetValues(typeof(PrintRange))
                    .Cast<PrintRange>()
                    .ToList();

            if (prList.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (var pr in prList)
            {
                Items.Add(new DynamoDropDownItem(pr.ToString(), pr));
            }
            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 ||
                Items[0].Name == NO_FAMILY_TYPES ||
                SelectedIndex == -1)
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var args = new List<AssociativeNode>
            {
                AstFactory.BuildStringNode(((PrintRange) Items[SelectedIndex].Item).ToString())
            };

            var func = new Func<string, PrintRange>(GetPrintRange.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.PrintRange

}