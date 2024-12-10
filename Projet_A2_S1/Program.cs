using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Joueur michel = new Joueur("michel");
            michel.Add_Mot("oui");
            michel.Add_Mot("noui");
            michel.Add_Mot("oui");
            Console.WriteLine(michel.toString());
        }
    }
}
