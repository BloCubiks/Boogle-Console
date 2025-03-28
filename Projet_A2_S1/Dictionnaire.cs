﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_A2_S1
{
    public class Dictionnaire
    {
        /// <summary>
        /// Tri par insertion
        /// </summary>
        /// <param name="tab"></param>



        public static void TriInsertion(string[] tab)
        {
            string en_cours;
            for (int i = 1; i < tab.Length; i++)
            {
                en_cours = tab[i];
                int j = i - 1;
                while(j>=0 && tab[j].CompareTo(en_cours) > 0)
                {
                    tab[j+1] = tab[j];
                    j--;
                }
                tab[j+1] = en_cours;
            }
        }

        /// <summary>
        /// Tri bogo (aléatoire)
        /// </summary>
        /// <param name="tab"></param>
        public static string[] TriBogo(string[] tab)
        {
            Random random = new Random();
            while (!isSorted(tab))
            {
                MelangeAleatoire(tab, random);
            }
            return tab;
        }
        /// <summary>
        /// Fonction qui détermine si un tableau de string est trié
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public static bool isSorted(string[] tab)
        {
            for (int i = 0; i < tab.Length - 1; i++)
            {
                if (tab[i].CompareTo(tab[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Fonction qui mélange de manière aléatoire
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="random"></param>
        public static void MelangeAleatoire(string[] tab, Random random)
        {
            for (int i = tab.Length - 1; i > 0; i--)
            {
                int chiffrealeatoire = random.Next(i + 1);
                (tab[i], tab[chiffrealeatoire]) = (tab[chiffrealeatoire], tab[i]);
            }
        }

        /// <summary>
        /// Tri fusion servant a trier le dictionnaire
        /// </summary>
        /// <param name="tab"> dictionnaire</param>
        /// <returns></returns>
        public static string[] TriFusion(string[] tab)
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
        /// <summary>
        /// fusion de 2 tableaux triés
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>tableau trié composé d'une valeur des 2 tableaux</returns>
        public static string[] Fusion(string[] A, string[] B)
        {
            int a = A.Length;
            int b = B.Length;
            string[] result = new string[a + b];
            int i = 0, j = 0, k = 0;
            while (i < a && j < b)
            {
                if (A[i].CompareTo(B[j]) < 0)
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

        public string[] GetDictionnaire
        {
            get { return dictionnaire; }
        }
        /// <summary>
        /// constructeur de la classe Dictionnaire qui ouvre le fichier correspondant à la langue choisie et qui le trie
        /// </summary>
        /// <param name="Langue"></param>
        public Dictionnaire(string Langue)
        {
            langue = Langue;
            try
            {
                if (langue == "English")
                {
                    dictionnaire = File.ReadAllText("MotsPossiblesEN.txt").Split(' ');
                }
                else
                {
                    dictionnaire = File.ReadAllText("MotsPossiblesFR.txt").Split(' ');
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
            dictionnaire = TriFusion(dictionnaire);
        }
        /// <summary>
        /// Methode renvoyant les informations concernant le dictionnaire (nombre de mots de chaque taille, nombre de mots commençant par chaque lettre)
        /// </summary>
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
            foreach (KeyValuePair<char, int> entry in nbMotsParLettre)
            {
                resultat += $"mots commencant par {entry.Key} : {entry.Value}\n";
            }
            return resultat;
        }
        /// <summary>
        /// recherche recursive dichotomique d'un mot dans le dictionnaire
        /// </summary>
        /// <param name="mot"> mot recherché </param>
        /// <param name="debut"> variable permettant de reduire la zone de recherche</param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public bool RechDichoRecursif(string mot, int debut, int fin)
        {
            if (debut == fin) return mot == dictionnaire[debut];
            int milieu = (debut + fin) / 2;
            if (mot.CompareTo(dictionnaire[milieu]) < 0)
            {
                return RechDichoRecursif(mot, debut, milieu);
            }
            else if (dictionnaire[milieu] == mot)
            {
                return true;
            }
            else
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
        }
    }
}
