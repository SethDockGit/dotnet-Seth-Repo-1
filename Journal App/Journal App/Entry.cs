using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Journal_App
{
    public class Entry
    {
        public string Title;
        public string BodyText;

        public Entry(string title, string body)
        {
            Title = title;
            BodyText = body;
        }
        public void Edit(Entry entry)
        {
            Console.Clear();
            Entry toEdit = new Entry(entry.Title, entry.BodyText);
            entry.Title = Title;
            entry.BodyText = BodyText;
            Console.WriteLine(entry.BodyText);
            Console.ReadLine();
        }
        public void Delete(Entry entry)
        {
            string blankBody = "";
            string blankTitle = "";
            Entry toRemove = new Entry(blankBody, blankTitle);

            toRemove = entry;
            Console.WriteLine("Your entry has been deleted.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
