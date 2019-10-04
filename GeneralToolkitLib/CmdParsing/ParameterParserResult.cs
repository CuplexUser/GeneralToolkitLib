namespace GeneralToolkitLib.CmdParsing
{
    public class ParameterParserResult
    {
        public bool ParsedSuccessfully { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public string CommandLine { get; }

        public ParameterParserResult(bool result, string commandLine)
        {
            ParsedSuccessfully = result;
            CommandLine = commandLine;
            ErrorMessage = "";
        }

        public ParameterParserResult(bool result, string commandLine, string errorMessage)
        {
            ParsedSuccessfully = result;
            CommandLine = commandLine;
            ErrorMessage = errorMessage;
        }
    }
}