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

        public string[] MinesOnlyBoard { get; private set; }
        public string[] AnnotatedBoard { get; private set; }

        public Minefield(string[] input)
        {
            MinesOnlyBoard = input;
            RowCount = MinesOnlyBoard.Length;

            AnnotatedBoard = new string[RowCount];
            MinesOnlyBoard.CopyTo(AnnotatedBoard, 0);

            if (RowCount > 0)
            {
                ColCount = MinesOnlyBoard[0].Length;
                AnnotateBoard();
            }
        }


        private void AnnotateBoard()
        {
            char[,] annotatedBoard = new char[RowCount, ColCount];

            for (int row = 0; row <= LastRowIndex; row++)
            {
                for (int col = 0; col <= LastColIndex; col++)
                {
                    annotatedBoard[row, col] = GetFieldValue(row, col);
                }
            }


            StringBuilder builder = new StringBuilder(ColCount);
            for (int row = 0; row <= LastRowIndex; row++)
            {
                for (int col = 0; col <= LastColIndex; col++)
                {
                    builder.Append(annotatedBoard[row, col]);
                }
                AnnotatedBoard[row] = builder.ToString();
                builder.Clear();
            }

            char GetFieldValue(int row, int col)
            {
                int myCount;
                if (MinesOnlyBoard[row][col] == Minefield.Empty)
                {
                    myCount = CountAdjacentMines(row, col);
                    if (myCount == 0)
                        return Minefield.Empty;
                    else
                        return Char.Parse(myCount.ToString());
                }
                else
                {
                    return MinesOnlyBoard[row][col];
                }
            }

            int CountAdjacentMines(int row, int col)
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
                        else if (MinesOnlyBoard[peekedRow][peekedCol] == Minefield.Mine)
                            totalCount++;
                    }

                return totalCount;
            }
        }

    }

    public static string[] Annotate(string[] input)
    {
        Minefield field = new Minefield(input);
        return field.AnnotatedBoard;
    }
}
