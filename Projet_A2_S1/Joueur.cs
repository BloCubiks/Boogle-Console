using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    internal class Joueur
    {
        private string nom;
        private byte score;
        private Dictionary<string, int> motsTrouves;

        public string Nom
        {
            get { return nom; }
        }
        public Dictionary<string, int> Mots
        {
            get { return motsTrouves; }
        }
        public Joueur(string Nom)
        {
            nom = Nom;
            score = 0;
            motsTrouves = new Dictionary<string, int>(); ;
        }
        public bool Contain(string mot)
        {
            bool contient = false;
            foreach (string key in motsTrouves.Keys)
            {
                if (mot == key) contient = true;
            }
            return contient;
        }
        public void Add_Score(byte points)
        {
            score += points;
        }
        public void Add_Mot(string mot)
        {
            if (!Contain(mot))
            {
                motsTrouves.Add(mot, 1);
            }
            else motsTrouves[mot]++;
        }

        public string MotsJoueur()
        {
            string mots = "";
            foreach (string key in motsTrouves.Keys)
            {
                mots += $"{key}: {motsTrouves[key]} fois ,";
            }
            return mots;
        }
        public string toString()
        {
            return $"Joueur : {nom}\nScore : {score}\nMots trouvés : {MotsJoueur()}";
        }
    }
}
