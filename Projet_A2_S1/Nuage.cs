using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_A2_S1 { 
    class NuageDeMots
    {
        public static void GenererNuageDeMots(Dictionary<string, int> mots, string nomJoueur)
        {
            int largeur = 800;
            int hauteur = 600;
            Bitmap bitmap = new Bitmap(largeur, hauteur);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Fond blanc
            graphics.Clear(Color.White);

            Random random = new Random();
            List<Rectangle> zonesUtilisees = new List<Rectangle>();

            Point centre = new Point(largeur / 2, hauteur / 2);

            // Trier les mots par fréquence (décroissante)
            var motsTries = new List<KeyValuePair<string, int>>(mots);
            motsTries.Sort((a, b) => b.Value.CompareTo(a.Value));

            foreach (var mot in motsTries)
            {
                int taillePolice = 15 + mot.Value * 8; // Taille en fonction de la fréquence
                Font font = new Font("Arial", taillePolice, FontStyle.Bold);

                // Couleur aléatoire
                Color couleur = Color.FromArgb(random.Next(50, 255), random.Next(50, 255), random.Next(50, 255));
                Brush brush = new SolidBrush(couleur);

                // Placement en spirale à partir du centre
                Point position = TrouverPositionSansCollision(mot.Key, font, graphics, centre, zonesUtilisees);

                // Dessiner le mot
                graphics.DrawString(mot.Key, font, brush, position);
                SizeF tailleMot = graphics.MeasureString(mot.Key, font);
                zonesUtilisees.Add(new Rectangle(position.X, position.Y, (int)tailleMot.Width, (int)tailleMot.Height));
            }

            // Sauvegarder l'image
            bitmap.Save($"{nomJoueur}_nuage_de_mots.png", ImageFormat.Png);
            Console.WriteLine($"Nuage de mots généré pour {nomJoueur} : {$"{nomJoueur}_nuage_de_mots.png"}");
            string fichierSortie = $"{nomJoueur}_NuageDeMots.png";
            string filePath = $"{nomJoueur}_nuage_de_mots.png";
            System.Diagnostics.Process.Start($"{nomJoueur}_nuage_de_mots.png");
            graphics.Dispose();
            bitmap.Dispose();
        }

        public static Point TrouverPositionSansCollision(string mot, Font font, Graphics graphics, Point centre, List<Rectangle> zonesUtilisees)
        {
            double angle = 0;       // Angle pour la spirale
            double rayon = 0;       // Rayon croissant de la spirale
            double pas = 5;         // Pas d'incrémentation pour le rayon
            double facteurAngle = 0.1; // Facteur pour ajuster l'angle de la spirale

            SizeF tailleMot = graphics.MeasureString(mot, font);

            while (true)
            {
                int x = (int)(centre.X + rayon * Math.Cos(angle));
                int y = (int)(centre.Y + rayon * Math.Sin(angle));

                Rectangle rect = new Rectangle(x, y, (int)tailleMot.Width, (int)tailleMot.Height);

                if (!Collision(rect, zonesUtilisees))
                {
                    return new Point(x, y);
                }

                // Progression dans la spirale
                angle += facteurAngle;
                rayon += pas * facteurAngle;
            }
        }

        public static bool Collision(Rectangle nouvelleZone, List<Rectangle> zonesExistantes)
        {
            foreach (var zone in zonesExistantes)
            {
                if (nouvelleZone.IntersectsWith(zone))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

            
        

    