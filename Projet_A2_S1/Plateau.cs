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
        private De[,] des;
        private Dictionary<char, int> pointsParLettre;

        public De[,] Des
        {
            get { return des; }
        }
        public Dictionary<char, int> Points
        {
            get { return pointsParLettre; }
        }
        public Plateau(int Taille)
        {
            taille = Taille;
            nbDes = taille * taille;
            des = new De[taille,taille];
            pointsParLettre = new Dictionary<char, int>();
            ///remplissage des des
            Dictionary<string, int> probaLettre = new Dictionary<string, int>();
            try
            {
                string[] lines = File.ReadAllLines("lettres.txt");
                string[][] infoLettres = new string[26][];
                double n;
                double coeff = (taille == 4) ? 1 : (taille * taille * 6) / 100.0; ///associe 1 si taille == 4 sinon attribue un coeff d'apparition des lettres
                for (int i = 0; i < 26; i++)
                {
                    infoLettres[i] = lines[i].Split(';');
                    pointsParLettre.Add(char.Parse(infoLettres[i][0]), int.Parse(infoLettres[i][1]));
                    n = double.Parse(infoLettres[i][2]);
                    n = n * coeff;
                    n = Math.Ceiling(n);
                    infoLettres[i][2] = n.ToString();
                }
                Random rand = new Random();
                Queue<char> lettres = new Queue<char>();
                int random;
                while (lettres.Count() < 6 * nbDes)
                {
                    random = rand.Next(26);
                    if (infoLettres[random][2] != "0")
                    {
                        lettres.Enqueue(char.Parse(infoLettres[random][0]));
                        infoLettres[random][2] = (int.Parse(infoLettres[random][2]) - 1).ToString();
                    }
                }
                char[] lettresDe = new char[6];
                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        for (int f = 0; f < 6; f++)
                        {
                            lettresDe[f] = lettres.Dequeue();
                        }
                        des[i, j] = new De(lettresDe);
                    }
                }
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("le fichier n'existe pas " + f.Message);
            }
            catch (ArgumentException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
            catch (PathTooLongException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
            catch (DirectoryNotFoundException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
            catch (UnauthorizedAccessException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
            catch (NotSupportedException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
            catch (IOException f)
            {
                Console.WriteLine("Erreur " + f.Message);
            }
        }
            
        public string toString()
        {
            string plateau = "";
            for (int i = 0; i < taille; i++)
            {
                for (int j =0; j < taille; j++)
                {
                    plateau += des[i,j].Visible + " ";
                }
                plateau += "\n";    
            }
            return plateau;
        }
        /// <summary>
        /// Fonction récursive permettant de tester si un mot est formable dans le plateau
        /// </summary>
        /// <param name="mot"> mot a chercher dans le plateau</param>
        /// <param name="i"> ligne d'une lettre dans le plateau</param>
        /// <param name="j"> colonne d'une lettre dans le plateau</param>
        /// <param name="dejaVu"> matrice permettant d'eviter de reutiliser la meme lettre</param>
        /// <param name="indiceMot"> indice de la lettre a rechercher dans le plateau</param>
        /// <returns> retourne vrai si le mot est formable dans le plateau</returns>
        public bool Test_Plateau(string mot, int i, int j, bool[,] dejaVu, int indiceMot = 0)
        {
            if (i < 0 || i >= taille || j < 0 || j >= taille || dejaVu[i, j] == true) return false; ///permet de ne pas reutiliser la meme lettre ou de ne pas faire d'IndexOutOfRange
            if (mot.Length == indiceMot) return true;
            if (mot[indiceMot] == des[i, j].Visible)
            {
                dejaVu[i, j] = true;
                if (Test_Plateau(mot, i - 1, j - 1, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i - 1, j, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i - 1, j + 1, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i, j + 1, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i + 1, j + 1, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i + 1, j, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i + 1, j - 1, dejaVu, indiceMot + 1)) return true;
                if (Test_Plateau(mot, i, j - 1, dejaVu, indiceMot + 1)) return true;
                dejaVu[i,j] = false;
            }
            return false;
        }   
        public bool MotPresent(string mot)
        {
            bool[,] dejaVu = new bool[taille, taille];
            bool motpresent = false;
            for (int j = 0; j < taille; j++)
            {
                for (int k = 0; k < taille && !motpresent; k++)
                {
                    if (Test_Plateau(mot, j, k, dejaVu, 0))
                    {
                        motpresent = true;
                    }
                }
            }
            return motpresent; 
        }
        public void ShufflePlateau()
        {
            for (int i = 0;i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    des[i, j].Lance();
                }
            }
        }
        public int CalculerPoints(string mot)
        {
            int points = 0;
            foreach (char lettre in mot)
            {
                points += pointsParLettre[lettre];
            }
            return points;
        }
    }
}
