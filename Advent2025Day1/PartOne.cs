


public static class PartOne
{
    const int MAXPOS = 99;

    public static int GetNumberLandOnZero(string inputFile)
    {
        int current = 50;
        string? line;
        char dir;
        int numZero = 0;

        try
        {
            using StreamReader reader = new(inputFile);

            while ((line = reader.ReadLine()) != null)
            {
                dir = line[0];
                var gotInt = int.TryParse(line.Substring(1), out int amount);

                if (gotInt)
                {
                    current = CalcPosition(dir, amount, current);
                }
                else
                {
                    continue;
                }

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

    private static int CalcPosition(char dir, int amount, int current)
    {
        if (dir == 'L')
        {
            current -= amount;
        }
        else
        {
            current += amount;
        }

        current %= (MAXPOS + 1);

        return current;
    }
}
