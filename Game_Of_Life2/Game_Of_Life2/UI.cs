using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game_Of_Life2.Data;

namespace Game_Of_Life2
{
    public class UI
    {

        private Manager Manager;
        private IData DataSource;

        public UI()
        {
            bool userExit = false;

            while(!userExit)
            {
                DisplayMenu();
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine("    *** Game of Life ***\n\n");
            Console.WriteLine("        Options:\n\n");
            Console.WriteLine("1. Select a map to run");
            Console.WriteLine("2. Create a new map");
            Console.WriteLine("3. Edit a map");
            Console.WriteLine("4. Delete a map");

            MenuSelection();

        }
        public void MenuSelection()
        {
            Console.WriteLine("\n\nPress a key to select an option");

            var cki = Console.ReadKey();

            switch(cki.Key)
            {
                case ConsoleKey.D1:
                    MenuSelectOne();
                    break;

                case ConsoleKey.D2:
                    MenuSelectTwo();
                    break;

                case ConsoleKey.D3:
                    EditMap();
                    break;

                case ConsoleKey.D4:
                    DeleteMap();
                    break;

                default:
                    Console.WriteLine("Sorry, that was an invalid selection. Press any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
        private void MenuSelectOne()
        {
            SelectDataSource();
            ListMaps(DataSource.Maps);
            Map map = SelectMap(DataSource.Maps);
            DisplayMap(map);

        }
        private void SelectDataSource()
        {
            Console.Clear();
            Console.WriteLine("\nWhich map set would you like to access?");
            Console.WriteLine("1. In Memory Data \n2. Text Data\n");

            var cki = Console.ReadKey();


            switch(cki.Key)
            {
                case ConsoleKey.D1:
                    DataSource = new InMemory();
                    break;

                case ConsoleKey.D2:
                    DataSource = new TxtData();
                    break;

                default:
                    Console.WriteLine("Error: Invalid selection. Press any key to return to the main menu.");
                    break;
            }

            Manager = new Manager(DataSource);

        }
        public void ListMaps(List<Map> maps)
        {
            Console.Clear();
            Console.WriteLine("        ***Maps***\n\n");

            maps = DataSource.ReadMaps();

            foreach (Map map in maps)
            {
                Console.WriteLine($"{map.ID}: {map.Name}");
            }
            if(maps == null)
            {
                throw new Exception("Review error handling and fix this message");
            }
        }
        private Map SelectMap(List<Map> maps)
        {
            Console.WriteLine("\nPlease select the number of the map you'd like to run, then press enter");

            string selection = Console.ReadLine();

            Map map = Manager.GetMap(selection);

            Console.Clear();

            return map;         
        }
        public void DisplayMap(Map map)
        {
            Initialize();
            RunMap(map.Cells);
        }
        private static void Initialize()
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 3);
            Console.SetBufferSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 3);
            Console.Title = "Game of Lyfe";
            Console.CursorVisible = false;
            Console.ForegroundColor = Settings.ForeGround;
            Console.BackgroundColor = Settings.BackGround;
        }
        private static void RunMap(bool[,] cells)
        {

            var timer = new Stopwatch();

            while (true)
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= Settings.RunSpeed)
                {
                    PrintGrid(cells);

                    cells = GetNextState(cells);

                    timer.Reset();
                }
            }
        }
        private static void PrintGrid(bool[,] cells)
        {

            StringBuilder sb = new StringBuilder();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y <= cells.GetLength(1) + 1; y++)
            {
                sb.Append("--");
            }
            sb.Append("\n");

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, y] == true)
                    {
                        if (x == 0)
                        {
                            sb.Append($"| {Settings.LiveCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) - 1)
                        {
                            sb.Append($"{Settings.LiveCellCharacter} |");
                        }
                        else
                        {
                            sb.Append($"{Settings.LiveCellCharacter}");
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            sb.Append($"| {Settings.DeadCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) - 1)
                        {
                            sb.Append($"{Settings.DeadCellCharacter} |");
                        }
                        else
                        {
                            sb.Append($"{Settings.DeadCellCharacter}");
                        }
                    }
                }
                sb.Append("\n");
            }

            for (int y = 0; y <= cells.GetLength(1) + 1; y++)
            {
                sb.Append("--");
            }
            sb.Append("\n");

            Console.WriteLine(sb.ToString());

        }
        private static bool[,] GetNextState(bool[,] previousState)
        {
            bool[,] currentState = (bool[,])previousState.Clone();

            for (int y = 0; y < previousState.GetLength(1); y++)
            {
                for (int x = 0; x < previousState.GetLength(0); x++)
                {
                    if (previousState[x, y] == true)
                    {
                        int neighbors = 0;

                        if (y > 0)
                        {
                            if (previousState[x, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y > 0 && x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) - 1)
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
                        if (y > 0 && x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) - 1)
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
        private void MenuSelectTwo()
        {
            DataSource = new TxtData();

            string title = CreateMap();

            string[] design = DesignMap();

            DataSource.SaveMap(design);

            Console.Clear();
        }
        private string CreateMap()
        {
            Console.Clear();
            Console.WriteLine("Enter a name for the new map\n");
            string title = Console.ReadLine();

            if(title == "")
            {
                title = "Untitled";
            }
            DataSource.CreateMap(title);

            Console.Clear();

            return title;

            //get user input into the form of a string array
            //pass back to data layer to save.
            //all implementations of IData will probably need to have SaveMap()
        }
        private string[] DesignMap()
        {

            Console.WriteLine("Enter your design. Enter a '.' for dead cells, or a '0' for live cells.");

            Console.WriteLine("At the end of your last line, hit enter, then write 'save', then hit enter again.");

            string[] design = new string[30];

            for (int i = 0; i < design.Length; i++)
            {
                design[i] = Console.ReadLine();

                if (design[i] == "save")
                {
                    design[i] = "save";
                    i++;
                }
            }
            return design;
        }
        private void DeleteMap()
        {
            throw new NotImplementedException();
        }
        private void EditMap()
        {
            throw new NotImplementedException();
        }
    }
}
