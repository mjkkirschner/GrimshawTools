

namespace GrimshawRibbon
{
    public class Globals
    {
        private static bool _viewVar;
        public static bool deleteViewsBool
        {
            get
            {
                return _viewVar;
            }
            set
            {
                _viewVar = value;
            }
        }
        private static bool _sheetVar;
        public static bool deleteSheetsBool
        {
            get
            {
                return _sheetVar;
            }
            set
            {
                _sheetVar = value;
            }
        }
    }
}
