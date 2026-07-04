using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Timers;

public static class PartTwo
{
    public static long SumInvalidIds(string inputFile)
    {
        string? idLine;

        try
        {
            using (var reader = new StreamReader(inputFile))
            {
                idLine = reader.ReadLine();
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading input: {ex.Message}");
            return 0;
        }

        if (idLine == null)
        {
            return 0;
        }

        return SumIds(idLine);
    }

    private static long SumIds(string input)
    {
        var idRanges = input.Split(',');
        var sum = 0L;

        foreach (var idRange in idRanges)
        {
            var startAndEnd = idRange.Split('-');
            var parsedStart = Int64.TryParse(startAndEnd[0], out var start);
            var parsedEnd = Int64.TryParse(startAndEnd[1], out var end);

            if (!parsedStart || !parsedEnd)
            {
                Console.WriteLine($"Error parsing range start and end for {idRange}");
                return 0;
            }

            var current = start;

            while (current <= end)
            {
                if (!IsValid(current))
                {
                    sum += current;
                }
                current++;
            }
        }

        return sum;
    }

    private static bool IsValid(long num)
    {
        var stringNum = num.ToString();
        var stringLength = stringNum.Length;
        // find all factors of the length
        // test for sequences of each of those lengths
        
        for (int seqLength = 1; seqLength <= Math.Floor(stringLength/2.0);  seqLength++)
        {
            if (stringLength % seqLength == 0)
            {
                var timesSequenceOccurs = stringLength / seqLength;
                var lastSequence = stringNum.Substring(0, seqLength);
                for (int seq = 1; seq < timesSequenceOccurs; seq++)
                {
                    var currentSequence = stringNum.Substring(seqLength * seq, seqLength);
                    if (!string.Equals(currentSequence, lastSequence))
                    {
                        break;
                    }
                    else if (seq == timesSequenceOccurs - 1)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private static IEnumerable<string> GetSubStringsByLength(this string str, int length)
    {
        for (int idx = 0; idx <str.Length; idx+=length)
        {
            yield return str.Substring(idx, Math.Min(length, str.Length - idx));
        }
    }
}
