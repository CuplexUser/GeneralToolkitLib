using System.Collections.Generic;

namespace GeneralToolkitLib.Utility
{
    public static class KeyValueNumericValidator
    {
        private static readonly int[] ValidIntegerInputCodes = {48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 46, 8, 9, 45, 35, 36};
        private static readonly List<int> ValidIntegerCodes;

        static KeyValueNumericValidator()
        {
            ValidIntegerCodes= new List<int>(ValidIntegerInputCodes);
        }
        public static bool ValidateIntegerInput(int keyValue)
        {
            return ValidIntegerCodes.Contains(keyValue);
        }

    }
}
