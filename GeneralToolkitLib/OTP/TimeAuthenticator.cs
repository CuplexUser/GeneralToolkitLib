﻿using System;
using Serilog;

namespace GeneralToolkitLib.OTP
{
    /// <summary>
    /// Implementation of rfc6238 Time-Based One-Time Password Algorithm
    /// </summary>
    public class TimeAuthenticator : Authenticator
    {
        private static readonly Lazy<IUsedCodesManager> DefaultUsedCodeManager = new Lazy<IUsedCodesManager>(() => new UsedCodesManager());

        private readonly Func<DateTime> NowFunc;
        private readonly IUsedCodesManager UsedCodeManager;
        private readonly int IntervalSeconds;

        public TimeAuthenticator(IUsedCodesManager usedCodeManager = null, Func<DateTime> nowFunc = null, int intervalSeconds = 30)
        {
            NowFunc = nowFunc ?? (() => DateTime.Now);
            UsedCodeManager = usedCodeManager ?? DefaultUsedCodeManager.Value;
            IntervalSeconds = intervalSeconds;
        }

        /// <summary>
        /// Generates One-Time Password.
        /// </summary>
        /// <param name="secret">Shared Secret</param>
        /// <returns>OTP</returns>
        public string GetCode(string secret)
        {
            return GetCode(secret, NowFunc());
        }

        /// <summary>
        /// Generates One-Time Password.
        /// </summary>
        /// <param name="secret">Shared Secret</param>
        /// <param name="date">Time to use as challenge</param>
        /// <returns>OTP</returns>
        public string GetCode(string secret, DateTime date)
        {
            return GetCodeInternal(secret, (ulong)GetInterval(date));
        }

        /// <summary>
        /// Checks if the passed code is valid.
        /// </summary>
        /// <param name="secret">Shared Secret</param>
        /// <param name="code">OTP</param>
        /// <param name="user">The user</param>
        /// <returns>true if code matches</returns>
        public bool CheckCode(string secret, string code, object user)
        {
            bool result= CheckCode(secret, code, user, out var successfullTime);
            Log.Debug("CheckingCode {code}. Result was {result}. Datetime used to validate code was {successfullTime}", code, result, successfullTime);

            return result;
        }

        /// <summary>
        /// Checks if the passed code is valid.
        /// </summary>
        /// <param name="secret">Shared Secret</param>
        /// <param name="code">OTP</param>
        /// <param name="user">The user</param>
        /// <param name="usedDateTime">Matching time if successful</param>
        /// <returns>true if code matches</returns>
        public bool CheckCode(string secret, string code, object user, out DateTime usedDateTime)
        {
            var baseTime = NowFunc();
            DateTime successfulTime = DateTime.MinValue;

            // We need to do this in constant time
            var codeMatch = false;
            for (int i = -2; i <= 1; i++)
            {
                var checkTime = baseTime.AddSeconds(IntervalSeconds * i);
                var checkInterval = GetInterval(checkTime);

                if (ConstantTimeEquals(GetCode(secret, checkTime), code) && (user == null || !UsedCodeManager.IsCodeUsed(checkInterval, code, user)))
                {
                    codeMatch = true;
                    successfulTime = checkTime;

                    UsedCodeManager.AddCode(checkInterval, code, user);
                }
            }

            usedDateTime = successfulTime;
            return codeMatch;
        }


        /// <summary>
        /// Checks if the passed code is valid.
        /// </summary>
        /// <param name="secret">Shared Secret</param>
        /// <param name="code">OTP</param>
        /// <returns>true if code matches</returns>
        public bool CheckCode(string secret, string code)
        {
            bool result = CheckCode(secret, code, null, out var successfullTime);
            Log.Debug("CheckingCode {code}. Result was {result}. Datetime used to validate code was {successfullTime}", code, result, successfullTime);

            return result;
        }

        private long GetInterval(DateTime dateTime)
        {
            TimeSpan ts = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)ts.TotalSeconds / IntervalSeconds;
        }
    }
}