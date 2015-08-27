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

using bumblebeeTypes;

namespace bumblebeeUI
{
    #region Revit Drop Down Base

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

    #endregion // Revit Drop Down Base

    #region Color Scale Criteria Types

    [NodeName("Color Scale Criteria Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Color Scale Criteria Types.")]
    [IsDesignScriptCompatible]
    public class bb_ColorScaleCriteriaTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_ColorScaleCriteriaTypesUI() : base("Color Scale Criteria Type") { }

        // Get Data Class that holds dictionary
        public static bb_ColorScaleCriteriaTypes cscTypes = new bb_ColorScaleCriteriaTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(cscTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_ColorScaleCriteriaTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Color Scale Criteria Types

    #region Direction Types

    [NodeName("Direction Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Direction Types.")]
    [IsDesignScriptCompatible]
    public class bb_DirectionTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_DirectionTypesUI() : base("Direction Type") { }

        // Get Data Class that holds dictionary
        public static bb_DirectionTypes dTypes = new bb_DirectionTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(dTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_DirectionTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Direction Types

    #region Line Chart Types

    [NodeName("Line Chart Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Line Chart Types.")]
    [IsDesignScriptCompatible]
    public class bb_LineChartTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_LineChartTypesUI() : base("Line Chart Type") { }

        // Get Data Class that holds dictionary
        public static bb_LineChartTypes lcTypes = new bb_LineChartTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(lcTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_LineChartTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Line Chart Types

    #region Pie Chart Types

    [NodeName("Pie Chart Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Pie Chart Types.")]
    [IsDesignScriptCompatible]
    public class bb_PieChartTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_PieChartTypesUI() : base("Pie Chart Type") { }

        // Get Data Class that holds dictionary
        public static bb_PieChartTypes pcTypes = new bb_PieChartTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(pcTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_PieChartTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Pie Chart Types

    #region Legend Position Types

    [NodeName("Legend Position Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Legend Position Types.")]
    [IsDesignScriptCompatible]
    public class bb_LegendPositionTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_LegendPositionTypesUI() : base("Legend Position Type") { }

        // Get Data Class that holds dictionary
        public static bb_LegendPositionTypes lpTypes = new bb_LegendPositionTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(lpTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_LegendPositionTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Legend Position Types

    #region Operator Types

    [NodeName("Operator Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Operator Types.")]
    [IsDesignScriptCompatible]
    public class bb_OperatorTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_OperatorTypesUI() : base("Operator Type") { }

        // Get Data Class that holds dictionary
        public static bb_OperatorTypes oTypes = new bb_OperatorTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(oTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_OperatorTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Operator Types

    #region Line Weight Types

    [NodeName("Line Weight Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Line Weight Types.")]
    [IsDesignScriptCompatible]
    public class bb_LineWeightTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_LineWeightTypesUI() : base("Line Weight Type") { }

        // Get Data Class that holds dictionary
        public static bb_LineWeightTypes lwTypes = new bb_LineWeightTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(lwTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_LineWeightTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Line Weight Types

    #region Line Types

    [NodeName("Line Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Line Types.")]
    [IsDesignScriptCompatible]
    public class bb_LineTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_LineTypesUI() : base("Line Type") { }

        // Get Data Class that holds dictionary
        public static bb_LineTypes lTypes = new bb_LineTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(lTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_LineTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Line Types

    #region Label Position Types

    [NodeName("Label Position Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Label Position Types.")]
    [IsDesignScriptCompatible]
    public class bb_LabelPositionTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_LabelPositionTypesUI() : base("Label Position Type") { }

        // Get Data Class that holds dictionary
        public static bb_LabelPositionTypes lpTypes = new bb_LabelPositionTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(lpTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_LabelPositionTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Label Position Types

    #region Pattern Types
    // Define Pattern Types dropdown UI

    [NodeName("Pattern Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Pattern Types.")]
    [IsDesignScriptCompatible]
    public class bb_PatternTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_PatternTypesUI() : base("Pattern Type") { }

        // Get Data Class that holds dictionary
        public static bb_PatternTypes pTypes = new bb_PatternTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(pTypes.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_PatternTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
    #endregion // Pattern Types

    #region Horizontal Align Types

    [NodeName("Horizontal Align Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Horizontal Alignment types.")]
    [IsDesignScriptCompatible]
    public class bb_HorizontalAlignTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_HorizontalAlignTypesUI() : base("Horizontal Align Type") { }

        // Get Data Class that holds dictionary
        public static bb_HorizontalAlignTypes hAlign = new bb_HorizontalAlignTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(hAlign.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_HorizontalAlignTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }

    #endregion // Horizontal Align Types

    #region Vertical Align Types

    [NodeName("Vertical Align Types")]
    [NodeCategory("Archi-lab_Bumblebee.Types")]
    [NodeDescription("Retrieve all available Vertical Alignment types.")]
    [IsDesignScriptCompatible]
    public class bb_VerticalAlignTypesUI : RevitDropDownBase
    {
        private const string NO_FAMILY_TYPES = "No types were found.";
        public bb_VerticalAlignTypesUI() : base("Vertical Align Type") { }

        // Get Data Class that holds dictionary
        public static bb_VerticalAlignTypes vAlign = new bb_VerticalAlignTypes();

        public override void PopulateItems()
        {
            Items.Clear();

            Dictionary<string, int> d = new Dictionary<string, int>(vAlign.Types);

            if (d.Count == 0)
            {
                Items.Add(new DynamoDropDownItem(NO_FAMILY_TYPES, null));
                SelectedIndex = 0;
                return;
            }

            foreach (KeyValuePair<string, int> pair in d)
            {
                Items.Add(new DynamoDropDownItem(pair.Key, pair.Value));
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
                AstFactory.BuildStringNode(((string) Items[SelectedIndex].Name))
            };

            var func = new Func<string, int>(bb_VerticalAlignTypes.byName);
            var functionCall = AstFactory.BuildFunctionCall(func, args);

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }

    #endregion // Vertical Align Types

}
