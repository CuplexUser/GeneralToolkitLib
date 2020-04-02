using System;
using System.Collections.Generic;
using System.Timers;

namespace GeneralToolkitLib.OTP
{
    /// <summary>
    /// Local, thread-save used codes manager implementation
    /// </summary>
    public class UsedCodesManager : IUsedCodesManager, IDisposable
    {
        internal sealed class UsedCode
        {
            public UsedCode(long timestamp, string code, object user)
            {
                UseDate = DateTime.Now;
                Code = code;
                Timestamp = timestamp;
                User = user;
            }

            internal DateTime UseDate { get; private set; }
            internal long Timestamp { get; }
            internal string Code { get; }
            internal object User { get; }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;

                return (obj is UsedCode other) && Code.Equals(other.Code) && Timestamp.Equals(other.Timestamp) && User.Equals(other.User);
            }

            public override string ToString()
            {
                return $"{Timestamp}: {Code}";
            }

            public override int GetHashCode()
            {
                return Code.GetHashCode() + (Timestamp.GetHashCode() + User.GetHashCode() * 17) * 17;
            }
        }

        private readonly Queue<UsedCode> codes;
        private readonly System.Threading.ReaderWriterLock rwlock = new System.Threading.ReaderWriterLock();
        private readonly TimeSpan lockingTimeout = TimeSpan.FromSeconds(5);
        private readonly Timer cleaner;

        public UsedCodesManager()
        {
            codes = new Queue<UsedCode>();
            cleaner = new Timer(TimeSpan.FromMinutes(5).TotalMilliseconds);
            cleaner.Elapsed += cleaner_Elapsed;
            cleaner.Start();
        }

        private void cleaner_Elapsed(object sender, ElapsedEventArgs e)
        {
            var timeToClean = DateTime.Now.AddMinutes(-5);

            try
            {
                rwlock.AcquireWriterLock(lockingTimeout);

                while (codes.Count > 0 && codes.Peek().UseDate < timeToClean)
                {
                    codes.Dequeue();
                }
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        public void AddCode(long timestamp, String code, object user)
        {
            try
            {
                rwlock.AcquireWriterLock(lockingTimeout);

                codes.Enqueue(new UsedCode(timestamp, code, user));
            }
            finally
            {
                rwlock.ReleaseWriterLock();
            }
        }

        public bool IsCodeUsed(long timestamp, String code, object user)
        {
            try
            {
                rwlock.AcquireReaderLock(lockingTimeout);

                return codes.Contains(new UsedCode(timestamp, code, user));
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }

        public void Dispose()
        {
            cleaner?.Dispose();
        }
    }
}