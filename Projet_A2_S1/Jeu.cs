using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Projet_A2_S1
{
    internal class Jeu
    {
        static void Passer()
        {
            Console.WriteLine("Appuyez sur une Entrée pour continuer");
            string j = Console.ReadLine();
        }
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
                langue = Console.ReadLine().ToUpper();
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
                if (!int.TryParse(Console.ReadLine(), out taille)) Console.WriteLine("mauvaise saisie");
            }
            Plateau plateau = new Plateau(taille);
            string mot;
            int compteurTour = 0;
            bool trouve;
            int nbguess;
            List<string> motsParRound;
            Console.WriteLine(" ____    __ _           _         _           _            \r\n|  _ \\  /_/| |__  _   _| |_    __| |_   _    (_) ___ _   _ \r\n| | | |/ _ \\ '_ \\| | | | __|  / _` | | | |   | |/ _ \\ | | |\r\n| |_| |  __/ |_) | |_| | |_  | (_| | |_| |   | |  __/ |_| |\r\n|____/ \\___|_.__/ \\__,_|\\__|  \\__,_|\\__,_|  _/ |\\___|\\__,_|\r\n                                           |__/            ");
            ///Déroulement du jeu
            while (compteurTour < nbToursParJoueur)
            {
                int JoueurEnCours = 0;
                compteurTour++;
                while (JoueurEnCours < nbJoueurs)
                {
                    nbguess = 1;
                    motsParRound = new List<string>();
                    Console.WriteLine("TOUR " + compteurTour);
                    Console.WriteLine("C'est au tour de " + joueurs[JoueurEnCours].Nom);
                    plateau.ShufflePlateau();
                    Console.WriteLine(plateau.toString());
                    ///Tour joueur
                    DateTime finTour = DateTime.Now.AddSeconds(60);
                    Console.WriteLine("Décompte de 60 secondes :");
                    while (finTour > DateTime.Now)
                    {
                        if (nbguess % 2 == 0) Console.WriteLine(plateau.toString());
                        Console.WriteLine("\nTemps restant : " + (finTour - DateTime.Now) + " secondes");

                        mot = Console.ReadLine().ToUpper();
                        if (DateTime.Now > finTour)
                        {
                            Console.WriteLine("Temps écoulé");
                            break;
                        }

                        else if (mot.Length > 1)
                        {
                            trouve = false;
                            for (int i = 0; i < motsParRound.Count && !trouve; i++)
                            {
                                if (motsParRound[i] == mot)
                                {
                                    trouve = true;
                                }
                            }
                            if (trouve)
                            {
                                Console.WriteLine("Le mot a déjà été trouvé");
                            }
                            else
                            {
                                if (dico.RechDichoRecursif(mot, 0, dico.GetDictionnaire.Length))
                                {
                                    if (plateau.MotPresent(mot))
                                    {
                                        Console.WriteLine("Le mot est valide");
                                        joueurs[JoueurEnCours].Add_Mot(mot);
                                        motsParRound.Add(mot);
                                        joueurs[JoueurEnCours].Add_Score((byte)plateau.CalculerPoints(mot));
                                        Console.WriteLine(joueurs[JoueurEnCours].toString());
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
                        }
                        else
                        {
                            Console.WriteLine("Le mot n'est pas valide");
                        }
                        nbguess += 1;
                    }
                    JoueurEnCours += 1;
                    Passer();
                }
            }
            int max = 0;
            ///max des scores
            for (int i = 1; i < joueurs.Length; i++)
            {
                if (joueurs[max].Score < joueurs[i].Score) max = i;
            }
            Console.WriteLine("Le gagnant est " + joueurs[max].Nom + " avec un score de " + joueurs[max].Score);
            Console.WriteLine(" _____ _             _           _              _ \r\n|  ___(_)_ __     __| |_   _    (_) ___ _   _  | |\r\n| |_  | | '_ \\   / _` | | | |   | |/ _ \\ | | | | |\r\n|  _| | | | | | | (_| | |_| |   | |  __/ |_| | |_|\r\n|_|   |_|_| |_|  \\__,_|\\__,_|  _/ |\\___|\\__,_| (_)\r\n                              |__/              ");
            foreach (Joueur joueur in joueurs)
            {
                NuageDeMots.GenererNuageDeMots(joueur.Mots, joueur.Nom);
            }
        }
    }
}