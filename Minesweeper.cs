using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public static class Minesweeper
{
    private class MinesweeperBoard
    {
        public static readonly char Mine = '*';
        public static readonly char Empty = ' ';

        public readonly string[] MinesOnlyBoard;
        public string[] AnnotatedBoard { get; private set; }

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        private int LastRowIndex { get { return RowCount - 1; } }
        private int LastColIndex { get { return ColCount - 1; } }

        public MinesweeperBoard(string[] minesOnlyBoard)
        {
            MinesOnlyBoard = minesOnlyBoard;
            RowCount = MinesOnlyBoard.Length;

            AnnotatedBoard = new string[RowCount];
            if (RowCount > 0)
            {
                ColCount = MinesOnlyBoard[0].Length;
                AnnotateBoard();
            }
            else
            {
                MinesOnlyBoard.CopyTo(AnnotatedBoard, 0);
            }
        }


        private void AnnotateBoard()
        {
            foreach (int row in Enumerable.Range(0, RowCount))
            {
                AnnotatedBoard[row] =
                    String.Concat(
                        Enumerable.Range(0, ColCount)
                        .Select(
                            col => GetFieldAnnotation(row, col)
                        )
                    );
            }

            // Return the count of adjacent mines if any,
            // or the same char as on the input board
            char GetFieldAnnotation(int row, int col)
            {
                int myCount;
                if (MinesOnlyBoard[row][col] == MinesweeperBoard.Empty)
                {
                    myCount = CountAdjacentMines(row, col);
                    if (myCount == 0)
                        return MinesweeperBoard.Empty;
                    else
                        return Convert.ToChar(myCount.ToString());
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
                int peekedRow;
                int peekedCol;

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
                        else if (MinesOnlyBoard[peekedRow][peekedCol] == MinesweeperBoard.Mine)
                            totalCount++;
                    }

                return totalCount;
            }
        }

    }

    public static string[] Annotate(string[] input)
    {
        MinesweeperBoard board = new MinesweeperBoard(input);
        return board.AnnotatedBoard;
    }
}
