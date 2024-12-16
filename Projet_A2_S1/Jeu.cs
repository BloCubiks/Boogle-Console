using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A2_S1
{
    internal class Jeu
    {
        static void Main(string[] args)
        {
            ///Initialisation du jeu
            Dictionnaire dico;
            Console.WriteLine("Bienvenue dans le jeu du Boggle");
            string langue = "";
            while (langue != "FR" && langue != "EN")
            {
                Console.WriteLine("Quelle langue du dictionnaire que vous souhaitez utiliser (français ou anglais)");
                Console.WriteLine("Saisissez FR ou EN");
                langue = Console.ReadLine();
            }
            if (langue == "EN")
            {
                dico = new Dictionnaire("English");
            }
            else
            {
                dico = new Dictionnaire("Francais");
            }
            int nbToursParJoueur = -1;
            while (nbToursParJoueur < 1)
            {
                Console.WriteLine("Saisissez le nombre de tours par joueur");
                int.TryParse(Console.ReadLine(), out nbToursParJoueur);
            }
            int nbJoueurs = -1;
            while (nbJoueurs < 1)
            {
                Console.WriteLine("Saisissez le nombre de joueurs");
                int.TryParse(Console.ReadLine(), out nbJoueurs);
            }
            Joueur[] joueurs = new Joueur[nbJoueurs];
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("Saisissez le nom du joueur");
                joueurs[i] = new Joueur(Console.ReadLine());
            }
            int taille = 3;
            while (taille < 4)
            {
                Console.WriteLine("Saisissez la taille du plateau (4 minimum)");
                int.TryParse(Console.ReadLine(), out taille);
            }
            Plateau plateau = new Plateau(taille);
            int tour = 1;
            bool[,] dejaVu;
            string mot;
            bool motpresent;

            ///Déroulement du jeu
            while (tour <= nbToursParJoueur)
            {
                Console.WriteLine("TOUR " + tour);
                ///Tour joueur
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Console.WriteLine("C'est au tour de " + joueurs[i].Nom);
                    plateau.ShufflePlateau();
                    Console.WriteLine(plateau.toString());
                    mot = Console.ReadLine().ToUpper();
                    if (mot.Length > 1)
                    {
                        if (dico.RechDichoRecursif(mot, 0, dico.GetDictionnaire.Length))
                        {
                            if (plateau.MotPresent(mot))
                            {
                                Console.WriteLine("Le mot est valide");
                                joueurs[i].Add_Mot(mot);

                                joueurs[i].Add_Score((byte)plateau.CalculerPoints(mot));
                                Console.WriteLine(joueurs[i].toString());
                            }
                            else
                            {
                                Console.WriteLine("Le mot n'est pas dans le plateau");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le mot n'est pas dans le dictionnaire");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'est pas valide");
                    }
                }
            }
        }  
    }
}
