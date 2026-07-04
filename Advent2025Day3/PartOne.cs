namespace Advent2025Day3
{
    public static class PartOne
    {
        public static long GetJoltageSum(string inputFile)
        {
            long joltageSum = 0;
            string? joltageBank;
            char maxDigit;
            int maxDigitIdx = 0;
            char secondMaxDigit;
            int secondMaxDigitIdx;

            using (var reader = new StreamReader(inputFile))
            {
                while((joltageBank = reader.ReadLine()) != null)
                {
                    var bank = joltageBank.Trim().Substring(0, joltageBank.Length - 1);
                    (maxDigit, maxDigitIdx) = GetMaxDigit(bank, joltageBank.Length - 2);

                    var bankAfterMaxDigit = joltageBank.Substring(maxDigitIdx + 1, joltageBank.Length - maxDigitIdx - 1);
                    (secondMaxDigit, secondMaxDigitIdx) = GetMaxDigit(bankAfterMaxDigit, bankAfterMaxDigit.Length - 1);

                    var joltageParse = int.TryParse(maxDigit.ToString() + secondMaxDigit.ToString(), out var joltage);

                    if (joltageParse)
                    {
                        joltageSum += joltage;
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not parse joltage");
                    }
                }
            }

            return joltageSum;
        }

        // Gets max digit by starting at the end of the string 
        // and recursively working back to the start
        // on the way 'out' it compares  each digit to the last one
        // so the max one 'bubbles' up
        public static (char, int) GetMaxDigit(string bank, int idx)
        {
            if (bank.Length == 1)
            {
                return (bank[0], 0);
            }

            var lastDigit = bank[idx];
            var (testDigit, testIdx) = GetMaxDigit(bank.Substring(0, bank.Length - 1), idx - 1);

            var lastDigitParse = int.TryParse(lastDigit.ToString(), out var lastDigitInt);
            var testDigitParse = int.TryParse(testDigit.ToString(), out var testDigitInt);

            if (lastDigitParse && testDigitParse)
            {
                if (lastDigitInt > testDigitInt)
                {
                    return (lastDigit, idx);
                }
                else
                {
                    return (testDigit, testIdx);
                }
            }
            else
            {
                throw new InvalidDataException("Could not Parse digits");
            }
        }
    }
}
