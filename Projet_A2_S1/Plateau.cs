using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Projet_A2_S1
{
    internal class Plateau
    {
        private int taille;
        private int nbDes;
        private De[] des;

        public Plateau(int Taille)
        {
            taille = Taille;
            nbDes = taille * taille;
            des = new De[nbDes];
            // remplissage des des
            Dictionary<string, int> probaLettre = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines("lettres.txt");
            string[][] infoLettres = new string[26][];
            double n;
            double coeff = (taille ==4) ? 1 : (taille*6)/100.0; //associe 1 si taille == 4 sinon attribue un coeff d'apparition des lettres
            for (int i = 0; i < 26; i++)
            {
                infoLettres[i] = lines[i].Split(';');
                n = double.Parse(infoLettres[i][2]);
                n = n * coeff;
                n = Math.Ceiling(n);
                infoLettres[i][2] = n.ToString();
            }
            Random rand = new Random();
            Stack<char> lettres = new Stack<char>();
            int random;
            while (lettres.Count() < 6 * nbDes)
            {
                random = rand.Next(26);
                if (infoLettres[random][2] != "0")
                {
                    lettres.Push(char.Parse(infoLettres[random][0]));
                    infoLettres[random][2] = (int.Parse(infoLettres[random][2]) - 1).ToString();
                }
            }
            char[] lettresDe = new char[6];
            lettres.Count();
            for (int i = 0; i < nbDes; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    lettresDe[j] = lettres.Pop();
                }
                des[i] = new De(lettresDe);
            }
        }
        public string toString()
        {
            string plateau = "";
            for (int i = 0; i < taille; i++)
            {
                for (int j =0; j < taille; j++)
                {
                    plateau += des[i * taille + j].Visible + " ";
                }
                plateau += "\n";    
            }
            return plateau;
        }
    }
}
