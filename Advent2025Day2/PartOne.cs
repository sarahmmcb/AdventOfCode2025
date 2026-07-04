using System;

public static class PartOne
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
        if (stringLength % 2 != 0)
        {
            return true;
        }

        for (int i = 0, j = stringLength / 2; i < stringLength / 2 && j < stringLength; i++, j++)
        {
            if (stringNum[i] != stringNum[j])
            {
                return true;
            }
        }

        return false;
    }
}
