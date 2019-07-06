namespace GeneralToolkitLib.Storage.Models
{
    using System.IO;

    public delegate void DataAccessEventHandler(object sender, DataAccessEventArgs e);

    public class DataAccessEventArgs
    {
        public int DataWritten { get; set; }
        public int DataRead { get; set; }
    }

    public class ProgressMemoryStream : MemoryStream
    {
        public event DataAccessEventHandler OnDataRead;
        public event DataAccessEventHandler OnDataWritten;

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.OnDataWritten != null)
                this.OnDataWritten.Invoke(this, new DataAccessEventArgs {DataWritten = count});
            base.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            if (this.OnDataWritten != null)
                this.OnDataWritten.Invoke(this, new DataAccessEventArgs {DataWritten = 1});
            base.WriteByte(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int dataRead = base.Read(buffer, offset, count);
            if (this.OnDataRead != null)
                this.OnDataRead.Invoke(this, new DataAccessEventArgs {DataRead = dataRead});

            return dataRead;
        }

        public override int ReadByte()
        {
            if (this.OnDataRead != null)
                this.OnDataRead.Invoke(this, new DataAccessEventArgs {DataRead = 1});
            return base.ReadByte();
        }
    }
}