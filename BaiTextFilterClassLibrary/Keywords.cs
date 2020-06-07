namespace BaiTextFilterClassLibrary
{
    public static class Keywords
    {
        public const string declareVariable = "say('DECLARE');var ";
        public const string useVariable = "say('USE_VAR');";

        //TIP: Keywords
        public const string NoDelimiter = "Delay3";
        //public const string clickAndTypeDelimiter = "Delay3_";
        public static string clickAndTypeDelimiter { get { return NoDelimiter + "_"; } }
        //public const string TypeAndTabDelimiter = "Delay3_?";
        public static string TypeAndTabDelimiter { get { return NoDelimiter + "_?"; } }
        //public const string JustTypeOnlyDelimiter = "Delay3_@";
        public static string JustTypeOnlyDelimiter { get { return NoDelimiter + "_@"; } }
        //public const string ClickAndEndDelimiter = "Delay3_END";
        public static string ClickAndEndDelimiter { get { return NoDelimiter + "_END"; } }

        //Grab value keywords
        public static string GrabSingleDelimiter { get { return NoDelimiter + "_GrabSingle"; } }
        public static string GrabMultiDelimiter { get { return NoDelimiter + "_GrabMulti"; } }
    }
}
