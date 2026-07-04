using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

namespace Advent2025Day3
{
    public static class PartTwo
    {
        public static BigInteger GetJoltageSum(string inputFile, bool useRecursion = true)
        {
            BigInteger joltageSum = 0;
            string? joltageBank;

            using (var reader = new StreamReader(inputFile))
            {
                while ((joltageBank = reader.ReadLine()) != null)
                {
                    string maxJoltage = useRecursion ? GetMaxJoltageRec(joltageBank, 88) : GetMaxJoltage(joltageBank);

                    joltageSum = BigInteger.Add(joltageSum, BigInteger.Parse(maxJoltage));
                }
            }

            return joltageSum;
        }

        private static string GetMaxJoltage(string joltageBank)
        {
            BigInteger maxJoltage = 0;
            Console.WriteLine($"Joltage Bank: {joltageBank}");
            string resultBank = "";

            for (int i = 0; i < joltageBank.Length - 2; i++)
            {
                for (int j = i + 1; j < joltageBank.Length - 1; j++)
                {
                    for (int k = j + 1; k < joltageBank.Length; k++)
                    {
                        var removeK = joltageBank.Remove(k, 1);
                        var removeJ = removeK.Remove(j, 1);
                        resultBank = removeJ.Remove(i, 1);

                        var resultBankBigInt = BigInteger.Parse(resultBank);

                        if (BigInteger.Compare(resultBankBigInt, maxJoltage) > 0)
                        {
                            maxJoltage = resultBankBigInt;
                            Console.WriteLine($"Result Bank: #{resultBank}");
                        }

                    }
                }
            }
            
            return maxJoltage.ToString();
        }

        // numDigits = number of Digits you can remove
        private static string GetMaxJoltageRec(string joltageBank, int numDigits)
        {
            var firstNDigits = joltageBank.Substring(0, numDigits + 1);

            var (maxDigit, idx) = PartOne.GetMaxDigit(firstNDigits, numDigits);

            // If we eliminated all digits at the same time
            if (idx == numDigits)
            {
                return joltageBank.Substring(idx, joltageBank.Length - idx);
            }

            // If we whittled it down to smallest subBank
            if (joltageBank.Length == numDigits + 1)
            {
                return maxDigit.ToString(); 
            }

            var subBank = joltageBank.Substring(idx + 1, joltageBank.Length - (idx + 1));

            var maxSubBank = GetMaxJoltageRec(subBank, numDigits - idx);

            return maxDigit + maxSubBank;
        }

    }
}

// 1114749386506752063236067648035482650914118022705123106267632067108375464743158788607669900866276162
// 1114749386506752063236067648035482650914118022705123106267632067108375464743158788607669900866276162
