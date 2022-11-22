using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life.Models
{
    public class Generation
    {
        public List<Cell> Cells { get; set; }

        public string Image { get; set; }   


        public Generation(List<Cell> cells)
        {
            foreach(Cell cell in cells)
            {
                cell.IsAlive = true;
            }
            Cells = GetListOfTheLivingAndDead(cells);
        }


        //I BELIEVE ALL THAT'S MISSING HERE IS THE LOGIC FOR CREATING A NEW CELL BASED ON 3 LIVE NEIGHBORS, WHICH IS NOT GOING TO BE EASY I DONT THINK

        //Based on my logic, I don't see why the cells with 1 or less neighbor are not dying out.

        public List<Cell> GetListOfTheLivingAndDead(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                int neighbors = CheckNeighbors(cell.X, cell.Y, cells);
                bool isAliveOrDead = LiveOrDie(neighbors, cell.IsAlive);
                cell.IsAlive = isAliveOrDead;
            }
            List<Cell> newGen = cells.Where(cell => cell.IsAlive = true).ToList();
            return newGen;
        }
        public int CheckNeighbors(int x, int y, List<Cell> currentCells)  //this method gets an integer of neighbors based on coordinates of each cell.
        {
            List<Cell> neighbors = currentCells.Where(c => c.X == x + 1 || c.X == x - 1 || c.Y == y + 1 || c.Y == y - 1).ToList();

            int numOfNeighbors = neighbors.Count;

            return numOfNeighbors;
        }
        public bool LiveOrDie(int neighbors, bool isAlive)  //I tried to put logic in here to handle if a cell was fed into this method as "dead" but by the logic of my program that will never happen.
        {
            bool willBeAlive = false;
            if (neighbors < 4 && neighbors > 1 && isAlive)
            {
                willBeAlive = true;
            }
            else if (!isAlive)
            {
                if (neighbors == 3)
                {
                    willBeAlive = true;
                }
                else
                {
                    willBeAlive = false;
                }
            }
            else
            {
                willBeAlive = false;
            }
            return willBeAlive;
        }
    }
}
