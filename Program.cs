using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Puissance4
{
    class Program
    {

        static void Main(string[] args)
        {
            Grille maGrille = new Grille();

            Console.Clear();
            if (ReadName(1) != "Q") {
                if (ReadName(2) != "Q") {
                    Console.Clear();
                    maGrille.DrawGraph();
                    int joueur = 1;
                    Boolean theEnd = false;
                    while (theEnd != true) {
                        theEnd = maGrille.Play(joueur);
                        if (theEnd != true)
                        {
                            maGrille.DrawGraph();
                            joueur++;
                            if (joueur == 3) joueur = 1;
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        public static string nomJoueur1;
        public static string nomJoueur2;
 
        static string ReadName(int num)
        {
            string nom;
            Console.WriteLine("");
            Console.WriteLine("Puis-je vous demander le nom du joueur numéro " + num + " (ou 'Q' pour quitter) ?");
            nom = Console.ReadLine();
            if (num == 1) nomJoueur1 = nom; else nomJoueur2 = nom;
            return nom;
        }
    }

    class Grille
    {
        public const int ROW = 6;
        public const int COL = 7;

        public int[,] grille = new int[ROW, COL];

        public Grille() {
            for (int i = 0; i < ROW; i++) {
                for (int j = 0; j < COL; j++) {
                    grille[i, j] = 0;
                }
            }
        }

        public void DrawGraph()
        {
            string ligne = "";

            Console.Clear();
            Console.WriteLine("");
            ligne = "     1   2   3   4   5   6   7";
            Console.WriteLine(ligne);

            for (int i = ROW - 1; i > -1; i--) {
                ligne = "   .___.___.___.___.___.___.___.";
                Console.WriteLine(ligne);
                ligne = "   !";
                for (int j = 0; j < COL; j++) {
                    if (grille[i, j] == 0) {
                        ligne = ligne + "   !";
                    } else {
                        if (grille[i, j] == 1) {
                            ligne = ligne + " O !";
                        } else {
                            ligne = ligne + " X !";
                        }
                    }
                }
                Console.WriteLine(ligne);
            }
            ligne = "   .___.___.___.___.___.___.___.";
            Console.WriteLine(ligne);
        }

        public Boolean Play(int num)
        {
            string sChoixCol = "";
            int choixCol = 0;

            Console.WriteLine("");
            if (num == 1) {
                Console.WriteLine(Program.nomJoueur1 + ", votre choix (Q pour quitter) ? ");
            } else {
                Console.WriteLine(Program.nomJoueur2 + ", votre choix (Q pour quitter) ? ");
            }

            sChoixCol = Console.ReadLine();
            if ((sChoixCol == "Q") || (sChoixCol == "q")) {
                return true;
            } else {
                choixCol = Int32.Parse(sChoixCol) - 1;
                int i = 0;
                Boolean placed = false;
                while ((i < ROW) & (placed == false)) {
                    if (grille[i, choixCol] == 0) {
                        grille[i, choixCol] = num;
                        placed = true;
                    }
                    i++;
                }
            }

            if (!WinTest(num)) return false; else return true;
        }

        public Boolean WinTest(int joueur)
        {
            Boolean theEnd = false;

            // test en horizontal
            for (int i = 0; i < ROW; i++) {
                for (int j = 0; j < (COL - 4); j++) {
                    if (((grille[i, j] == grille[i, j + 1]) &
                        (grille[i, j] == grille[i, j + 2]) &
                        (grille[i, j] == grille[i, j + 3])) &
                        ((grille[i, j] != 0) & (grille[i, j + 1] != 0) & 
                        (grille[i, j + 2] != 0) & (grille[i, j + 3] != 0))) theEnd = true;
                }
            }

            // test en vertical
            for (int j = 0; j < COL; j++) {
                for (int i = 0; i < (ROW - 4); i++) {
                    if (((grille[i, j] == grille[i + 1, j]) &
                        (grille[i, j] == grille[i + 2, j]) &
                        (grille[i, j] == grille[i + 3, j])) &
                        ((grille[i, j] != 0) & (grille[i + 1, j] != 0) &
                        (grille[i + 2, j] != 0) & (grille[i + 3, j] != 0))) theEnd = true;
                }
            }

            // test en oblique
            for (int i = 0; i < (ROW - 4) ; i++)
            {
                for (int j = 0; j < (COL - 4); j++)
                {
                    if (((grille[i, j] == grille[i + 1, j + 1]) &
                        (grille[i, j] == grille[i + 2, j + 2]) &
                        (grille[i, j] == grille[i + 3, j + 3])) &
                        ((grille[i, j] != 0) & (grille[i + 1, j + 1] != 0) &
                        (grille[i + 2, j + 2] != 0) & (grille[i + 3, j + 3] != 0))) theEnd = true;
                }
            }

            Console.WriteLine("");
            if (theEnd) {
                DrawGraph();
                Console.WriteLine("");
                if (joueur == 1) {
                    Console.WriteLine(Program.nomJoueur1 + ", vous avez gagné !");
                } else {
                    Console.WriteLine(Program.nomJoueur2 + ", vous avez gagné !");
                }
            }
            return theEnd;
        }
    }
}
