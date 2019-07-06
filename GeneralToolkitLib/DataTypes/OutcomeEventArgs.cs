namespace GeneralToolkitLib.DataTypes
{
    public class OutcomeEventArgs
    {
        public static readonly OutcomeEventArgs Failure;
        public static readonly OutcomeEventArgs Success;

        static OutcomeEventArgs()
        {
            Failure = new OutcomeEventArgs {Successful = false};
            Success = new OutcomeEventArgs {Successful = true};
        }

        public bool Successful { get; set; }
    }
}