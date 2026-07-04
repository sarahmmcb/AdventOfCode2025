
namespace Advent2025Day4
{
    public static class PartOne
    {
        const char ROLL = '@';
        const char EMPTY = '.';

        public static int GetNumberOfRollsThatCanBeMoved(string inputFile)
        {
            var rows = new List<List<char>>();
            var numCanMove = 0;

            using (var reader = new StreamReader(inputFile))
            {
                var line1 = reader.ReadLine();
                var line2 = reader.ReadLine();
                var line3 = reader.ReadLine();

                if (line1 is null || line2 is null || line3 is null)
                {
                    return 0;
                }

                rows.Add([.. line1]);
                rows.Add([.. line2]);
                rows.Add([.. line3]);
                string? newLine;

                numCanMove += CalcNumCanMoveInRow(rows, 0);
                numCanMove += CalcNumCanMoveInRow(rows, 1);

                while ((newLine = reader.ReadLine()) != null)
                {
                    rows[0] = rows[1];
                    rows[1] = rows[2];
                    rows[2] = [.. newLine];

                    numCanMove += CalcNumCanMoveInRow(rows, 1);
                }

                // Check last row
                numCanMove += CalcNumCanMoveInRow(rows, 2);
            }

            return numCanMove;
        }

        private static int CalcNumCanMoveInRow(List<List<char>> rows, int rowIdx)
        {
            var numOccupied = 0;
            var numCanMove = 0;

            for (int colIdx = 0; colIdx < rows[rowIdx].Count; colIdx++)
            {
                if (rows[rowIdx][colIdx] == EMPTY) continue;

                if (colIdx - 1 >= 0)
                {
                    numOccupied += CheckColumn(rows, rowIdx, colIdx - 1);
                }

                numOccupied += CheckColumn(rows, rowIdx, colIdx, true);

                if (colIdx + 1 < rows[rowIdx].Count)
                {
                    numOccupied += CheckColumn(rows, rowIdx, colIdx + 1);
                }

                if (numOccupied < 4)
                {
                    numCanMove++;
                    Console.WriteLine($"Coords of roll that can be moved: {rowIdx} {colIdx}");
                }

                // reset
                numOccupied = 0;
            }

            return numCanMove;
        }

        private static int CheckColumn(List<List<char>> rows, int i, int j, bool skipMiddlePosition = false)
        {
            var numOccupied = 0;
            var currentRow = rows[i];

            if (i - 1 >= 0)
            {
                if (rows[i - 1][j] == ROLL) numOccupied++;
            }

            if (rows[i][j] == ROLL && !skipMiddlePosition) numOccupied++;

            if (i + 1 < rows.Count)
            {
                if (rows[i + 1][j] == ROLL) numOccupied++;
            }

            return numOccupied;
        }
    }
}
