


public static class PartTwo
{
    const int MAXPOS = 99;

    public static int GetNumberPassZero(string inputFile)
    {
        int current = 50;
        string? line;
        char dir;
        int numZero = 0;
        var extraRevolutions = 0;
        var passZero = false;

        try
        {
            using StreamReader reader = new(inputFile);

            while ((line = reader.ReadLine()) != null)
            {
                dir = line[0];
                var gotInt = int.TryParse(line.Substring(1), out int amount);

                if (gotInt)
                {
                    extraRevolutions = GetExtraRevolutions(amount);
                    amount = amount - (extraRevolutions * 100);
                    (current, passZero) = CalcPosition(dir, amount, current);
                }
                else
                {
                    continue;
                }

                numZero += passZero ? extraRevolutions + 1 : extraRevolutions;
                if (current == 0)
                {
                    numZero++;
                }
            }

            return numZero;

        }
        catch (IOException ex)
        {
            Console.WriteLine("Error reading from file");
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    private static (int, bool) CalcPosition(char dir, int amount, int current)
    {
        var passZero = false;
        int newCurrent;

        if (dir == 'L')
        {
            newCurrent = current - amount;
            if (newCurrent < 0)
            {
                if (current > 0)
                {
                    passZero = true;
                }

                newCurrent += (MAXPOS + 1);
            }
        }
        else
        {
            newCurrent = current + amount;
            if (newCurrent >= (MAXPOS + 1))
            {
                if (newCurrent > (MAXPOS + 1))
                {
                    passZero = true;
                }

                newCurrent -= (MAXPOS + 1);
            }
        }

        return (newCurrent, passZero);
    }

    private static int GetExtraRevolutions(int amount)
    {
        var revolutions = amount / 100.0;
        return (int)Math.Floor(revolutions);
    }
}