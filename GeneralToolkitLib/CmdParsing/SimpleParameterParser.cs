using System.Collections.Specialized;
using System.Text;

namespace GeneralToolkitLib.CmdParsing
{
    //public class SimpleParameterParser : CommandParserBase
    //{
    //    private readonly NameValueCollection _cmdValueCollection;
    //    private string _commandLine;

    //    public SimpleParameterParser(bool allowCommandOnlyArgs) : base(allowCommandOnlyArgs)
    //    {
    //        _cmdValueCollection = new NameValueCollection();
    //        AllowCommandOnlyArgs = allowCommandOnlyArgs;

    //    }

    //    public bool AllowCommandOnlyArgs { get; }


    //    public override ParameterParserResult ParseCommands(string[] args, string separator)
    //    {
    //        bool result = false;
    //        string cmdLine = "";
    //        string errorMessage;


    //        NameValueCollection nameValue = new NameValueCollection();

    //        for (int i = 0; i < args.Length; i += 2)
    //        {
    //            nameValue.Add(args[i], i + 1 > args.Length ? args[i + 2] : "");
    //        }

    //        _commandLine = AssembleCommandLine(args, separator);


    //        var parseResult = new ParameterParserResult(result, _commandLine);
    //        return parseResult;
    //    }

    //    protected string AssembleCommandLine(string[] args, string separator)
    //    {
    //        var sb = new StringBuilder();
    //        for (int i = 0; i < args.Length; i += 2)
    //        {
    //            sb.Append(separator + args[i] + " " + (i + 1 < args.Length ? args[i + 1] : ""));
    //        }

    //        return sb.ToString();
    //    }
    //}
}