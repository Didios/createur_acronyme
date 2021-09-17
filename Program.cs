using System;
using System.IO;
using System.Collections.Generic;

namespace createur_acronyme
{
    class Program
    {
        static void Main(string[] args)
        {
            // Permet de lire un fichier texte et de mettre le tout dans une variable
            String line; 

            // on créer un dictionnaire pour acceuillir les mots trouvés
            Dictionary<char, List<string>> dico = new Dictionary<char, List<string>> { };

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader fichier = new StreamReader("D:/Computer/activite/projet C#/createur_acronyme/liste_mots_francais.txt");
                //on initialise la variable de lecture avec la premiere ligne du document
                line = fichier.ReadLine();
                //tant qu'on n'as pas atteint la fin du fichier
                while (line != null)
                {
                    // on obtient la premiere lettre du mot
                    char firstLetter = line[0];

                    if (!dico.ContainsKey(firstLetter))
                    {
                        dico.Add(firstLetter, new List<string> { });
                    }

                    dico[firstLetter].Add(line);

                    //lire la prochaine ligne
                    line = fichier.ReadLine();
                }
                //close the file
                fichier.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            // permet de parcourir le dico et les listes
            //foreach (KeyValuePair<char, List<string>> langage in dico)
            //{
            //    Console.WriteLine("Clé: {0}",
            //        langage.Key);
            //    foreach (string item in langage.Value)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            Console.WriteLine("Veuillez entrez le mot à transformer en acronyme :");
            string mot_base = Console.ReadLine();
            
            if (mot_base == "ajout liste")
            {
                Console.WriteLine("En cours, pas encore fait...");

                Console.WriteLine("Le mot à ajouté est : ");
                string nouvMot = Console.ReadLine();

                if (!dico[nouvMot[0]].Contains(nouvMot))
                {
                    Console.WriteLine("Le mot n'est pas présent dans la liste.");
                    Console.WriteLine("Ajout en cours ...");
                    
                    try
                    {
                        // Instanciation du StreamWriter avec passage du nom du fichier 
                        StreamWriter doc = new StreamWriter("D:/Computer/activite/projet C#/createur_acronyme/liste_mots_francais.txt");

                        //Ecriture du texte dans le fichier 
                        // on va réécrire ce qu'il y avait précédemment car ca efface tous sinon
                        foreach (KeyValuePair<char, List<string>> lettre in dico)
                        {
                            foreach (string element in lettre.Value)
                            {
                                doc.WriteLine(element);
                            }
                        }
                        // on écrit notre nouveau mot en dernier
                        doc.WriteLine(nouvMot);

                        // Fermeture du StreamWriter (Très important) 
                        doc.Close();
                    }
                    catch (Exception ex)
                    {
                        // Code exécuté en cas d'exception 
                        Console.Write(ex.Message);
                    }

                    Console.WriteLine("\nAjout terminé.");
                }
                else
                {
                    Console.WriteLine("Le mot demandé est déjà présent dans la liste.");
                }
            }
            else
            {
                string acronyme = "";
                Random choice = new Random();

                foreach (char lettre in mot_base)
                {
                    if (!dico.ContainsKey(lettre))
                    {
                        acronyme += "* ";
                    }
                    else
                    {
                        List<string> liste = dico[lettre];
                        if (liste.Count == 0)
                        {
                            acronyme += "* ";
                        }
                        else
                        {
                            acronyme += liste[choice.Next(liste.Count)] + " ";
                        }
                    }
                }

                Console.WriteLine("Le mot : {0} et l'acronyme de : {1}.", mot_base, acronyme);
            }
        }
    }
}
