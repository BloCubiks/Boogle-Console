using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_A2_S1
{

    internal class Dictionnaire
    {
        static string[] TriFusion(string[] tab)
        {
            int n = tab.Length;
            if (n <= 1)
            {
                return tab;
            }
            int mid = n / 2;
            string[] left = new string[mid];
            string[] right = new string[n - mid];
            Array.Copy(tab, 0, left, 0, mid);
            Array.Copy(tab, mid, right, 0, n - mid);
            return Fusion(TriFusion(left), TriFusion(right));
        }
        static string[] Fusion(string[] A, string[] B)
        {
            int a = A.Length;
            int b = B.Length;
            string[] result = new string[a + b];
            int i = 0, j = 0, k = 0;
            while (i < a && j < b)
            {
                if (A[i].CompareTo(B[j])>0)
                {
                    result[k++] = A[i++];
                }
                else
                {
                    result[k++] = B[j++];
                }
            }
            while (i < a)
            {
                result[k++] = A[i++];
            }
            while (j < b)
            {
                result[k++] = B[j++];
            }
            return result;
        }
        private string langue;
        private string[] dictionnaire;

        public Dictionnaire(string Langue)
        {
            langue = Langue;
            if (langue == "English") dictionnaire = File.ReadAllText("MotsPossiblesEN.txt").Split(' ');
            else dictionnaire = File.ReadAllText("MotsPossiblesFR.txt").Split(' ');
            TriFusion(dictionnaire);

        }
        public string toString()
        {
            string resultat = "Ce dictionnaire "+langue+" contient : \n";
            int[] nbMotsParTaille = new int[16];
            char lettre;
            for (int i = 0;i<dictionnaire.Length;i++)
            {
                nbMotsParTaille[dictionnaire[i].Length] +=1;
            }
            Dictionary<char,int> nbMotsParLettre = new Dictionary<char,int>();
            for (int i = 0; i < dictionnaire.Length; i++)
            {
                if (dictionnaire[i] != null && dictionnaire[i].Length > 0)
                {
                    lettre = dictionnaire[i][0];
                    if (!nbMotsParLettre.ContainsKey(lettre))
                    {
                        nbMotsParLettre.Add(lettre, 1);
                    }
                    else nbMotsParLettre[lettre] += 1;
                }
            }
            for (int i = 2; i < 16; i++)
            {
                if (nbMotsParTaille[i] != 0)
                {
                    resultat += $"{nbMotsParTaille[i]} mots de taille {i}\n";
                }
            }
            resultat += "\n";
            // ne sait pas pourquoi ca ne marche pas
            //for (int i=97; i < 123; i++) //lettres a-z
            //{
            //    if (nbMotsParLettre.ContainsKey((char)i))
            //    {
            //        resultat += $"mots commencant par {(char)i} : {nbMotsParLettre[(char)i]}+=\n";
            //    }
            //}
            foreach (KeyValuePair<char, int> entry in nbMotsParLettre)
            {
                resultat += $"mots commencant par {entry.Key} : {entry.Value}\n";
            }
            return resultat;
        }
        public bool RechDicoRecursif(string mot, int debut, int fin)
        {
            if (debut == fin) return mot == dictionnaire[debut];
            int milieu = (debut + fin) / 2;
            if (mot.CompareTo(dictionnaire[milieu]) < 0)
            {
                return RechDicoRecursif(mot, debut, milieu);
            }
            else
            {
                return RechDicoRecursif(mot, milieu + 1, fin);
            }
        }
    }
}
