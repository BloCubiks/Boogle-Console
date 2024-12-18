using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    public class Joueur
    {
        private string nom;
        private byte score;
        private Dictionary<string, int> motsTrouves;

        public byte Score
        {
            get { return score; }
        }   
        public string Nom
        {
            get { return nom; }
        }
        public Dictionary<string, int> Mots
        {
            get { return motsTrouves; }
        }
        /// <summary>
        /// Constructeur du joueur qui prend en parametre son nom
        /// </summary>
        /// <param name="Nom"></param>
        public Joueur(string Nom)
        {
            nom = Nom;
            score = 0;
            motsTrouves = new Dictionary<string, int>(); ;
        }
        /// <summary>
        /// verifie si un mot appartient aux mots trouvés par le joueur
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Contain(string mot)
        {
            bool contient = false;
            foreach (string key in motsTrouves.Keys)
            {
                if (mot == key) contient = true;
            }
            return contient;
        }
        /// <summary>
        /// Ajoute des points au score du joueur
        /// </summary>
        /// <param name="points"></param>
        public void Add_Score(byte points)
        {
            score += points;
        }
        /// <summary>
        /// ajoute un mot aux mots trouvés par le joueur
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            if (!Contain(mot))
            {
                motsTrouves.Add(mot, 1);
            }
            else motsTrouves[mot]++;
        }
        /// <summary>
        /// retourne les mots trouvés par le joueur
        /// </summary>
        /// <returns> string contenant tous les mots du joueur</returns>
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
            return $"Joueur : {nom}\n   Score : {score}\n   Mots trouvés : {MotsJoueur()}";
        }
    }
}
