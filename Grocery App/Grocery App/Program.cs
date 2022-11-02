using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Grocery_App
{
    internal class Program
    {

        //    Allow a customer to check the store inventory
        // --they can't see the number in stock, unless the item is out of stock
        //   Allow an employe to check the store inventory
        // --they can see all the data



//        print all products from the deli section

//   print a list of all products that are out of stock

//   print a list of products that are less than 5.00$

//   print the most expensive product

//   print a list of all products ordered from most expensive to least expensive

//   print a list of products ordered by category, then from least to most expensive

//   print the ID and name of all products out of stock

//   print a list of each category and how many products are out of stock

//   print a list of each category and the name/id of the most expensive product in it

        static void Main(string[] args)
        {

            List<Product> products = LoadProducts();
            //PrintNameIDOOS(products);

            ExpByCat(products);
            //CustInterface custInterface = new CustInterface(categoryOOS);

            //Console.WriteLine($"{mostExp.Name}");
            //Console.ReadLine();





        }
        public static List<Product> LoadProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Cost = 0.50M,
                    Name = "Banana",
                    NumberInStock = 55,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 2,
                    Cost = 0.60M,
                    Name = "Apple",
                    NumberInStock = 16,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 3,
                    Cost = 0.78M,
                    Name = "Carrot",
                    NumberInStock = 3,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 4,
                    Cost = 1.20M,
                    Name = "Lime",
                    NumberInStock = 1,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 5,
                    Cost = 2.50M,
                    Name = "Lemon",
                    NumberInStock = 0,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 6,
                    Cost = 5.70M,
                    Name = "Ham",
                    NumberInStock = 21,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 7,
                    Cost = 4.50M,
                    Name = "Turkey",
                    NumberInStock = 21,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 8,
                    Cost = 3.50M,
                    Name = "Provelone",
                    NumberInStock = 4,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 9,
                    Cost = 7.50M,
                    Name = "Corned Beef",
                    NumberInStock = 0,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 10,
                    Cost = 12.50M,
                    Name = "Chicken Noodle Soup",
                    NumberInStock = 69,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 11,
                    Cost = 7.50M,
                    Name = "Mac 'n Cheese",
                    NumberInStock = 54,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 12,
                    Cost = 3.26M,
                    Name = "Egg Noodles",
                    NumberInStock = 67,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 13,
                    Cost = 11.22M,
                    Name = "Canned Tuna",
                    NumberInStock = 46,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 14,
                    Cost = 4.00M,
                    Name = "Ice",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },
                new Product()
                {
                    Id = 15,
                    Cost = 16.40M,
                    Name = "Toilet Paper",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },
                new Product()
                {
                    Id = 16,
                    Cost = 9.50M,
                    Name = "Napkins",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },
            };
        }

        //so for your to-do list, you could pass in a list to the edit or delete method, use linq to identify which ID to remove (where ID is not equal to ID)
        //.. and then return a new list to populate the library with.
        public static List<Product> PrintDeli(List<Product> products)
        {
            List<Product> deliList = products.Where(p => p.Category == Category.Deli).ToList();

            return deliList;
        }

        public static List<Product> PrintOutOfStock(List<Product> products)
        {
            List<Product> outOfStock = products.Where(p => p.NumberInStock == 0).ToList();
            return outOfStock;
        }
        public static List<Product> PrintOverFive(List<Product> products)
        {
            List<Product> overFive = products.Where(p => p.Cost > 5.00M).ToList();
            return overFive;
        }
        public static Product FindMostExpensive(List<Product> products)
        {
            Product mostExp = products.OrderByDescending(p => p.Cost).First();

            return mostExp;
        }
        public static List<Product> PrintMostToLeast(List<Product> products)
        {
            List <Product> mostToLeast = products.OrderByDescending(p => p.Cost).ToList();
            return mostToLeast;
        }
        public static List<Product> LeastMostByCategory(List<Product> products)
        {
            List<Product> leastMost = products.OrderBy(p => p.Category).ThenBy(p => p.Cost).ToList();
            return leastMost;
        }
        public static void PrintNameIDOOS(List<Product> products)
        {
            List<Product> outOfStock = PrintOutOfStock(products);
            var namesAndID = outOfStock.Select(product => new
            {
                Name = product.Name,
                ID = product.Id,
            }).ToList();

            Console.WriteLine("Out of Stock:");
            foreach (var v in namesAndID)
            {
                Console.WriteLine($"{v.Name} {v.ID}");
            }
            Console.ReadKey();
        }
        public static void OOSByCat(List<Product> products)
        {

        }
        public static void ExpByCat(List<Product> products)
        {

            var groupedByCat = products.GroupBy(p => p.Category);

            var mostExpByCat = groupedByCat.Select(cat => new
            {
                Category = cat.Key,
                MostExp = cat.OrderByDescending(c => c.Cost).First()
            });
            
            foreach (var c in mostExpByCat)
            {
                Console.WriteLine($"{c.Category}: {c.MostExp.Name} {c.MostExp.Id} is the most expensive item");
            }
            Console.ReadKey();



        }
    }
}
