namespace GeneralToolkitLib.CmdParsing
{
    public abstract class CommandParserBase
    {
        protected CommandParserBase(bool allowCommandOnlyArgs)
        {
            AllowCommandOnlyArgs = allowCommandOnlyArgs;
        }

        public abstract ParameterParserResult ParseCommands(string[] args, string separator);
        public virtual bool AllowCommandOnlyArgs { get; }
    }
}
