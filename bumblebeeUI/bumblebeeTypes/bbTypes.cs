using System;
using System.Collections.Generic;
using System.Linq;

namespace bumblebeeTypes
{
    #region Color Scale Criteria Types

    public class bb_ColorScaleCriteriaTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_ColorScaleCriteriaTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"LowestValue", 1},
                {"Number", 0},
                {"Percent", 3},
                {"Formula", 4},
                {"Percentile", 5},
                {"HighestValue", 2},
                {"AutomaticMax", 7},
                {"AutomaticMin", 6},
                {"None", -1}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_ColorScaleCriteriaTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Direction Types

    #region Direction Types

    public class bb_DirectionTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_DirectionTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"LeftToRight", -5003},
                {"RightToLeft", -5004},
                {"Context", -5002}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_DirectionTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Direction Types

    #region Line Chart Types

    public class bb_LineChartTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_LineChartTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Line", 4},
                {"LineStacked", 63},
                {"LineStacked100", 64},
                {"3dLine", -4101}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_LineChartTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Line Chart Types

    #region Pie Chart Types

    public class bb_PieChartTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_PieChartTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"3dPie", -4102},
                {"3dPieExploded", 70},
                {"Pie", 5},
                {"PieExploded", 69}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_PieChartTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Pie Chart Types

    #region Legend Position Types

    public class bb_LegendPositionTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_LegendPositionTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Bottom", -4107},
                {"Upper Right Corner", 2},
                {"Custom", -4161},
                {"Left", -4131},
                {"Right", -4152},
                {"Top", -4160}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_LegendPositionTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Legend Position Types

    #region Operator Types

    public class bb_OperatorTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_OperatorTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Equal", 3},
                {"NotEqual", 4},
                {"Greater", 5},
                {"GreaterEqual", 7},
                {"Less", 6},
                {"LessEqual", 8},
                {"Between", 1},
                {"NotBetween", 2}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_OperatorTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Operator Types

    #region Line Weight Types

    public class bb_LineWeightTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_LineWeightTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Hairline", 1},
                {"Medium", -4138},
                {"Thick", 4},
                {"Thin", 2}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_LineWeightTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Line Weight Types

    #region Line Types

    public class bb_LineTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_LineTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Continuous", 1},
                {"Dash", -4115},
                {"DashDot", 4},
                {"DashDotDot", 5},
                {"RoundDot", -4118},
                {"LongDash", -4115},
                {"DoubleXL", -4119},
                {"NoneXL", -4142}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_LineTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Line Types

    #region Label Position Types

    public class bb_LabelPositionTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_LabelPositionTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Above", 0},
                {"Below", 1},
                {"BestFit", 5},
                {"Center", -4108},
                {"Custom", 7},
                {"InsideBase", 4},
                {"InsideEnd", 3},
                {"Left", -4131},
                {"Mixed", 6},
                {"OutsideEnd", 2},
                {"Right", -4152}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_LabelPositionTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Label Position Types

    #region Pattern Types

    public class bb_PatternTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_PatternTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"xlCheckerBoard", 9},
                {"xlCrissCross", 16},
                {"xlDarkDiagonalDown", -4121},
                {"xlGrey16", 17},
                {"xlGray25", -4124},
                {"xlGray50", -4124},
                {"xlGray75", -4126},
                {"xlGray8", 18},
                {"xlGrid", 15},
                {"xlDarkHorizontal", -4128},
                {"xlLightDiagonalDown", 13},
                {"xlLightHorizontal", 11},
                {"xlLightDiagonalUp", 14},
                {"xlLightVertical", 12},
                {"xlNone", -4142},
                {"xlSemiGray75", 10},
                {"xlSolid", 1},
                {"xlDarkDiagonalUp", -4162},
                {"xlDarkVertical", -4166}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_PatternTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Pattern Types

    #region Horizontal Align Types

    public class bb_HorizontalAlignTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_HorizontalAlignTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Left", -4131},
                {"Center", -4108},
                {"Right", -4152}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_HorizontalAlignTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Horizontal Align Types

    #region Vertical Align Types

    public class bb_VerticalAlignTypes
    {
        public Dictionary<string, int> Types { get; set; }
        public bb_VerticalAlignTypes()
        {
            this.Types = new Dictionary<string, int>()
            {
                {"Bottom", -4017},
                {"Center", -4108},
                {"Top", -4160}
            };
        }
        public static int byName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name");
            }
            int intValue = new bb_VerticalAlignTypes().Types[name];
            return intValue;
        }
    }

    #endregion // Vertical Align Types

}
