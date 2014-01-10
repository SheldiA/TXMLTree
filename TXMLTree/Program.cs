using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXMLTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TXMLTree tree = new TXMLTree("country","Belarus");
            tree.Insert("country", "Minsk", "1");
            tree.Insert("country", "Gomel", "2");
            tree.Insert("country", "Borisov", "3");
            tree.Insert("Minsk", "Gorodetskogo", "street");
            tree.Insert("Minsk", "Lavrova", "street");
            string s = tree.FindPath(@"country\Minsk\Lavrova");
        }
    }
}
