using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    internal class Jeu
    {
        static void Main(string[] args)
        {
            Plateau plateau = new Plateau(4);
            Console.WriteLine(plateau.toString());
            bool[,] oui = new bool[4, 4];
            string mot = Console.ReadLine();
            bool motpresent = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4 &&!motpresent; j++)
                {

                    if(plateau.Test_Plateau(mot, i, j, oui, 0))
                    {
                        Console.WriteLine("Le mot est présent");
                        motpresent = true;
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
