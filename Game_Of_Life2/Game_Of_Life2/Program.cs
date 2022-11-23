using System;
using System.Security.Cryptography;
using System.Threading;

namespace Game_Of_Life2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool[,] cells = Random();

            while (true)
            {
                PrintGrid(cells);

                cells = GetNextState(cells);
            }
        }
        private static void PrintGrid(bool[,] cells)
        {
            Console.Clear();
            for(int y = 0; y < cells.GetLength(1); y++)
            {
                for(int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x,y] == true)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        private static bool[,] GetNextState(bool[,] previousState)
        {
            //Any live cell with two or three live neighbours survives.
            //Any dead cell with three live neighbours becomes a live cell.

            bool[,] currentState = (bool[,])previousState.Clone();

            for (int y = 0; y < previousState.GetLength(1); y++)
            {
                for(int x = 0; x < previousState.GetLength(0); x++)
                {
                    if(previousState[x,y] == true)
                    {
                        int neighbors = 0;

                        if (y > 0)
                        {
                            if (previousState[x, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y > 0 && x < previousState.GetLength(0) -1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) -1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) -1 && y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x - 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0)
                        {
                            if (previousState[x - 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (previousState[x - 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (neighbors < 2 || neighbors > 3)
                        {
                            currentState[x, y] = false;
                        }
                    }
                    if (previousState[x, y] == false)
                    {
                        int neighbors = 0;

                        if (y > 0)
                        {
                            if (previousState[x, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y > 0 && x < previousState.GetLength(0) -1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) -1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) -1 && y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) -1)
                        {
                            if (previousState[x - 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0)
                        {
                            if (previousState[x - 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (previousState[x - 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (neighbors == 3)
                        {
                            currentState[x, y] = true;
                        }
                    }
                }
            }
            return currentState;
        }
        private static bool[,] StillLife()
        {
            bool[,] cells = new bool[10, 10];


            cells[3, 7] = true;
            cells[3, 8] = true;
            cells[3, 9] = true;
            cells[6, 7] = true;
            cells[6, 8] = true;
            cells[6, 9] = true;

            return cells;

        }
        private static bool[,] Random()
        {
            Random rng = new Random();

            bool[,] cells = new bool[30, 30];

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    int randomInt = rng.Next(0, 2);

                    if(randomInt == 0)
                    {
                        cells[x, y] = true;
                    }
                    else
                    {
                        cells[x, y] = false;
                    }
                }
            }
            return cells;
        }
    }
}
