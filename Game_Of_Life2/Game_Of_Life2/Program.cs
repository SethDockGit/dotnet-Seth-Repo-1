using System;
using System.Security.Cryptography;
using System.Threading;

namespace Game_Of_Life2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var map1 = Random(30, 50, 20);
            var map2 = Random(15, 20, 30);
            var map3 = Random(20, 30, 50);
            var stillLife = StillLife();

            Run(stillLife);
        }

        //re-write... set cursor position at the point of change and have it draw the new data?
        private static void Run(bool[,] cells)
        {
            while (true)
            {
                PrintGrid(cells);

                cells = GetNextState(cells);
            }
        }
        private static void PrintGrid(bool[,] cells)
        {
            Console.Clear();

            for(int y = 0; y <= cells.GetLength(1) +1; y++)
            {
                Console.Write("--");
            }
            Console.WriteLine();

            for(int y = 0; y < cells.GetLength(1); y++)
            {
                for(int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, y] == true)
                    {
                        if (x == 0)
                        {
                            Console.Write($"| {Settings.LiveCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) -1)
                        {
                            Console.Write($"{Settings.LiveCellCharacter} |");
                        }
                        else
                        {
                            Console.Write($"{Settings.LiveCellCharacter}");
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            Console.Write($"| {Settings.DeadCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) -1)
                        {
                            Console.Write($"{Settings.DeadCellCharacter} |");
                        }
                        else
                        {
                            Console.Write($"{Settings.DeadCellCharacter}");
                        }
                    }
                }
                Console.WriteLine();
            }

            for (int y = 0; y <= cells.GetLength(1) +1; y++)
            {
                Console.Write("--");
            }
            Console.WriteLine();

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
        private static bool[,] Random(int length, int height, int percentChanceToLive)
        {
            Random rng = new Random();

            bool[,] cells = new bool[length, height];

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    int randomInt = rng.Next(0, 100);

                    if(randomInt > percentChanceToLive - 1)
                    {
                        cells[x, y] = false;
                    }
                    else
                    {
                        cells[x, y] = true;
                    }
                }
            }
            return cells;
        }
    }
}
