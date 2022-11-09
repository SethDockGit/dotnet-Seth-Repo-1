using MaterialsApp.Data;
using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application(new InMemoryDataSource());

            application.Run();
        }
    }
}
