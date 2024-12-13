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

        public Dictionnaire(string Langue, string[] Dictionnaire)
        {
            langue = Langue;
            if (langue == "English") dictionnaire = File.ReadAllText("Mots_PossiblesEN.txt").Split(' ');
            else dictionnaire = File.ReadAllText("Mots_PossiblesFR.txt").Split(' ');
            TriFusion(dictionnaire);

        }
    }
}
