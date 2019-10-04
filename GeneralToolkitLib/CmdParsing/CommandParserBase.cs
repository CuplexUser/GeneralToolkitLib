using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
