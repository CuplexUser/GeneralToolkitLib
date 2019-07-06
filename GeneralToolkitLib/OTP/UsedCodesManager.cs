using System;
using System.Collections.Generic;
using System.Timers;

namespace GeneralToolkitLib.OTP
{
    /// <summary>
    /// Local, thread-save used codes manager implementation
    /// </summary>
    public class UsedCodesManager : IUsedCodesManager
    {
        internal sealed class UsedCode
        {
            public UsedCode(long timestamp, String code, object user)
            {
                this.UseDate = DateTime.Now;
                this.Code = code;
                this.Timestamp = timestamp;
                this.User = user;
            }

            internal DateTime UseDate { get; private set; }
            internal long Timestamp { get; private set; }
            internal String Code { get; private set; }
            internal object User { get; private set; }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;

                var other = obj as UsedCode;
                return (other != null) && this.Code.Equals(other.Code) && this.Timestamp.Equals(other.Timestamp) && this.User.Equals(other.User);
            }

            public override string ToString()
            {
                return String.Format("{0}: {1}", this.Timestamp, this.Code);
            }

            public override int GetHashCode()
            {
                return this.Code.GetHashCode() + (this.Timestamp.GetHashCode() + this.User.GetHashCode() * 17) * 17;
            }
        }

        private readonly Queue<UsedCode> codes;
        private readonly System.Threading.ReaderWriterLock rwlock = new System.Threading.ReaderWriterLock();
        private readonly TimeSpan lockingTimeout = TimeSpan.FromSeconds(5);
        private readonly Timer cleaner;

        public UsedCodesManager()
        {
            this.codes = new Queue<UsedCode>();
            this.cleaner = new Timer(TimeSpan.FromMinutes(5).TotalMilliseconds);
            this.cleaner.Elapsed += this.cleaner_Elapsed;
            this.cleaner.Start();
        }

        private void cleaner_Elapsed(object sender, ElapsedEventArgs e)
        {
            var timeToClean = DateTime.Now.AddMinutes(-5);

            try
            {
                this.rwlock.AcquireWriterLock(this.lockingTimeout);

                while (this.codes.Count > 0 && this.codes.Peek().UseDate < timeToClean)
                {
                    this.codes.Dequeue();
                }
            }
            finally
            {
                this.rwlock.ReleaseWriterLock();
            }
        }

        public void AddCode(long timestamp, String code, object user)
        {
            try
            {
                this.rwlock.AcquireWriterLock(this.lockingTimeout);

                this.codes.Enqueue(new UsedCode(timestamp, code, user));
            }
            finally
            {
                this.rwlock.ReleaseWriterLock();
            }
        }

        public bool IsCodeUsed(long timestamp, String code, object user)
        {
            try
            {
                this.rwlock.AcquireReaderLock(this.lockingTimeout);

                return this.codes.Contains(new UsedCode(timestamp, code, user));
            }
            finally
            {
                this.rwlock.ReleaseReaderLock();
            }
        }
    }
}