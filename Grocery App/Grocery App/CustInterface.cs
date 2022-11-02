using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery_App
{
    public class CustInterface
    {
        public List<Product> Products;

        public CustInterface(List<Product> products)
        {

            Products = products;

            bool isEmployee = false;

            if (!isEmployee)
            {
                WelcomeCust();
            }
            else
            {
                WelcomeEmp();
            }
        }

        private void WelcomeCust()
        {
            Console.WriteLine("Hello, welcome to Dockmart. Here are our available products:");
            Console.WriteLine();

            //CheckForZeroInv()

            bool isItOut = false;
            foreach (Product product in Products)
            {
                if (!isItOut)
                {
                    Console.WriteLine($"{product.Name}, {product.Category}, {product.Cost}");
                }
                else
                {
                    Console.WriteLine($"{product.Name}, {product.Category}, {product.Cost}, {product.NumberInStock}");
                }
            }
            Console.ReadKey();
        }

        private void WelcomeEmp()
        {

        }
        //private void CheckForZeroInv
        
        }
}
