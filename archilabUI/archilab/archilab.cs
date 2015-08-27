using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using RevitServices.Persistence;
using Revit.Elements;
using Autodesk.DesignScript.Runtime;

namespace archilab
{
    #region archi-lab_Grimshaw.Selection.ParameterGroups
    [IsVisibleInDynamoLibrary(false)] 
    public class GetParameterGroup
    {
        [IsVisibleInDynamoLibrary(false)]
        public static BuiltInParameterGroup byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Print Range by name
            var pg = (BuiltInParameterGroup)Enum.Parse(typeof(BuiltInParameterGroup), name);

            return pg;
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.ParameterGroups

    #region archi-lab_Grimshaw.Selection.FillPatternTarget
    [IsVisibleInDynamoLibrary(false)] 
    public class GetFillPatternTarget
    {
        [IsVisibleInDynamoLibrary(false)]
        public static FillPatternTarget byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Print Range by name
            var fpt = (FillPatternTarget)Enum.Parse(typeof(FillPatternTarget), name);

            return fpt;
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.FillPatternTarget

    #region archi-lab_Grimshaw.Printing.PrintSettings
    [IsVisibleInDynamoLibrary(false)] 
    public class GetPrintSetting
    {
        [IsVisibleInDynamoLibrary(false)]
        public static Revit.Elements.Element byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Print Setting by name
            PrintSetting ps = new FilteredElementCollector(DocumentManager.Instance.CurrentDBDocument)
                .OfClass(typeof(PrintSetting))
                .FirstOrDefault(q => q.Name == name) as PrintSetting;
            var dsElement = ElementWrapper.ToDSType(ps, true);

            return dsElement;
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.PrintSettings

    #region archi-lab_Grimshaw.Printing.ViewSets
    [IsVisibleInDynamoLibrary(false)] 
    public class GetViewSet
    {
        [IsVisibleInDynamoLibrary(false)]
        public static Revit.Elements.Element byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Print Setting by name
            ViewSheetSet vs = new FilteredElementCollector(DocumentManager.Instance.CurrentDBDocument)
                .OfClass(typeof(ViewSheetSet))
                .FirstOrDefault(q => q.Name == name) as ViewSheetSet;
            var dsElement = ElementWrapper.ToDSType(vs, true);

            return dsElement;
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.ViewSets

    #region archi-lab_Grimshaw.Selection.ParameterTypes
    [IsVisibleInDynamoLibrary(false)] 
    public class GetParamType
    {
        [IsVisibleInDynamoLibrary(false)]
        public static ParameterType byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Parameter Type by name
            var pt = (ParameterType)Enum.Parse(typeof(ParameterType), name);

            return pt;
        }
    }
    #endregion //archi-lab_Grimshaw.Selection.ParameterTypes

    #region archi-lab_Grimshaw.Printing.PrintRange
    [IsVisibleInDynamoLibrary(false)] 
    public class GetPrintRange
    {
        [IsVisibleInDynamoLibrary(false)]
        public static PrintRange byName(string name)
        {
            //exceptions
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            //Gets the Print Range by name
            var pr = (PrintRange)Enum.Parse(typeof(PrintRange), name);

            return pr;
        }
    }
    #endregion //archi-lab_Grimshaw.Printing.PrintRange

}
