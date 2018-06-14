using System;
using System.Text;

public static class Minesweeper
{
    private class Minefield
    {
        public static readonly char Mine = '*';
        public static readonly char Empty = ' ';

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public int LastRowIndex { get { return RowCount - 1; } }
        public int LastColIndex { get { return ColCount - 1; } }

        public string[] MinesOnlyField { get; private set; }
        public string[] AnnotatedField { get; private set; }

        public Minefield(string[] input)
        {
            MinesOnlyField = input;
            ColCount = MinesOnlyField[0].Length;
            RowCount = MinesOnlyField.Length;

            AnnotateField();
        }


        private void AnnotateField()
        {
            char[,] annotatedField = new char[RowCount, ColCount];

            int myCount = 0;
            for (int row = 0; row <= LastRowIndex; row++)
            {
                for (int col = 0; col <= LastColIndex; col++)
                {
                    if (MinesOnlyField[row][col] == Minefield.Empty)
                    {
                        myCount = CountAdjacentMines(row, col);
                        if (myCount == 0)
                            annotatedField[row, col] = Minefield.Empty;
                        else
                            annotatedField[row, col] = Char.Parse(myCount.ToString());
                    }
                    else
                    {
                        annotatedField[row, col] = MinesOnlyField[row][col];
                    }
                }
            }

            AnnotatedField = new string[RowCount];

            StringBuilder builder = new StringBuilder(ColCount);
            for (int row = 0; row <= LastRowIndex; row++)
            {
                for (int col = 0; col <= LastColIndex; col++)
                {
                    builder.Append(annotatedField[row, col]);
                }
                AnnotatedField[row] = builder.ToString();
                builder.Clear();
            }
        }

        private int CountAdjacentMines(int row, int col)
        {
            int totalCount = 0;

            int peekedRow = 0;
            int peekedCol = 0;

            for (int dRow = -1; dRow <= 1; dRow++)
                for (int dCol = -1; dCol <= 1; dCol++)
                {
                    peekedRow = row + dRow;
                    peekedCol = col + dCol;
                    if (peekedRow < 0
                        || peekedRow > LastRowIndex
                        || peekedCol < 0
                        || peekedCol > LastColIndex
                        || (dRow == 0 && dCol == 0)
                        )
                        continue;
                    else if (MinesOnlyField[peekedRow][peekedCol] == Minefield.Mine)
                        totalCount++;
                }

            return totalCount;
        }
    }

    public static string[] Annotate(string[] input)
    {
        if (input == null)
            return null;
        if (input.Length == 0)
            return new string[0];

        Minefield field = new Minefield(input);
        return field.AnnotatedField;
    }
}
