using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public static class Minesweeper
{
    private class Minefield
    {
        public static readonly char Mine = '*';
        public static readonly char Empty = ' ';

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        private int LastRowIndex { get { return RowCount - 1; } }
        private int LastColIndex { get { return ColCount - 1; } }

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
            AnnotatedBoard =
                Enumerable.Range(0, RowCount)
                .Select(
                    row => string.Concat(
                        Enumerable.Range(0, ColCount)
                        .Select(
                            col => GetFieldAnnotation(row, col)
                        )
                    )
                ).ToArray();

            // Return the count of adjacent mines if any,
            // or the same char as on the input board
            char GetFieldAnnotation(int row, int col)
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

            // Count Minefield.Mine around the position defined by row, col
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

                        //don't look off the board, or at the current position
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
