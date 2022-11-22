using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using Game_of_Life.Models;

namespace Game_of_Life
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();

            List<Cell> startCells = map.TestMap();  ///choose the initial map using a method on the map class

            List<Cell> newCells = Initiate(startCells);

            while (true)
            {
                RegenerateGrid(newCells);

                newCells = RegenerateCells(newCells);
            }
        } 
        private static List<Cell> Initiate(List<Cell> startCells)
        {

            RegenerateGrid(startCells);

            List<Cell> newCells = RegenerateCells(startCells);

            return newCells;

        }
        private static void RegenerateGrid(List<Cell> cells)
        {
            Console.Clear();
            for (int y = 1; y <= 20; y++)
            {
                for (int x = 1; x <= 20; x++)
                {
                    if (TheresACellHere(x, y, cells))
                    {
                        Console.Write($"@");   //I want to write a character based on if the cell is alive or dead
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        private static bool TheresACellHere(int x, int y, List<Cell> cells)
        {
            var isCell = cells.Any(c => c.X == x && c.Y == y);
            return isCell;
        }
        private static List<Cell> RegenerateCells(List<Cell> cells)
        {
            Generation generation = new Generation(cells);

            List<Cell> newGen = generation.Cells;

            return newGen;
        }
    }
}
