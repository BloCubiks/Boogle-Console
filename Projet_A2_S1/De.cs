﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    public class De
    {
        private char[] faces;
        private char lettre_visible;
        Random aleatoire = new Random();

        public char Visible
        {
            get { return lettre_visible; }
        }

        /// <summary>
        /// constructeur d'un Dé en fonction d'un tableau de 6 charactères
        /// </summary>
        public De(char[] lettres)
        {
            faces = new char[6];
            for (int i = 0; i < 6; i++)
            {
                faces[i] = lettres[i];
            }
            lettre_visible = lettres[aleatoire.Next(0, 6)];
        }
        
        /// <summary>
        /// attribue une valeur aleatoire parmis les faces à la lettre visible
        /// </summary>
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
