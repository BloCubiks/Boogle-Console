using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    internal class De
    {
        private char[] faces;
        private char lettre_visible;

        public char Visible
        {
            get { return lettre_visible; }
        }

        public De(char[] lettres)
        {
            faces = new char[6];
            for (int i = 0; i < 6; i++)
            {
                faces[i] = lettres[i];
            }
            lettre_visible = lettres[0];
        }
        Random aleatoire = new Random();

        public void Lance()
        {
            lettre_visible=faces[aleatoire.Next(0,6)];
        }
        public string toString()
        {
            return $"Ce Dé contient les lettres {faces[0]}, {faces[1]}, {faces[2]}, {faces[3]}, {faces[4]}, {faces[5]}. La lettre visible est {lettre_visible}.";
        }
    }
}
