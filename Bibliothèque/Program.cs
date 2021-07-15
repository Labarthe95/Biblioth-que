using Bibliothèque.Entities;
using Bibliothèque.Services;
using System;
using System.Data.SqlClient;

namespace Bibliothèque
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"Data Source=SONIC-12;Initial Catalog=Bibliotheque;Uid=sa;Pwd=formation;";

            using (SqlConnection oConn = new SqlConnection(ConnectionString))
            {
                Console.WriteLine("Voulez-vous faire une manipulation de la bibliothèque?");
                string reponse = Console.ReadLine();
                do
                {
                    while (reponse != "oui" && reponse != "non")
                    {
                        Console.WriteLine("Voulez-vous faire une manipulation de la bibliothèque?");
                        reponse = Console.ReadLine();
                    }
                    Console.WriteLine("1. Afficher les livres/auteurs\n" +
                                  "2. Ajouter un auteur\n" +
                                  "3. Mettre à jour un auteur\n" +
                                  "4. Supprimer un auteur");
                    int.TryParse(Console.ReadLine(), out int choix);
                    Console.WriteLine();
                    switch (choix)
                    {
                        case 1:
                            Console.WriteLine("1. Afficher une liste des livres et de leur(s) auteur(s)\n" +
                                  "2. Afficher la liste des auteurs\n" +
                                  "3. Afficher une liste des livres contenant des informations complémentaires (date publication, éditeur...)\n");
                            int.TryParse(Console.ReadLine(), out int choix2);
                            Console.WriteLine();
                            switch (choix2)
                            {
                                case 1:
                                    AuteurLivreService serviceAfficherAuteurLivre = new AuteurLivreService(oConn);
                                    foreach (AuteurLivre item in serviceAfficherAuteurLivre.GetByLivreId())
                                    {
                                        Console.WriteLine($"{item.IdAuteurNavigation.Nom} - {item.IdLivreNavigation.Titre}");
                                    }
                                    break;
                                case 2:
                                    AuteurService serviceAfficherAuteur = new AuteurService(oConn);
                                    serviceAfficherAuteur.GetAuteur();
                                    foreach (Auteur item in serviceAfficherAuteur.GetAuteur())
                                    {
                                        Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                                    }
                                    break;
                                case 3: //Problème
                                    LivreService serviceAfficherLivre = new LivreService(oConn);
                                    serviceAfficherLivre.GetLivre();
                                    foreach (Livre item in serviceAfficherLivre.GetLivre())
                                    {
                                        Console.WriteLine($"{item.Id}. {item.Titre}");
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("A propos de quel livre souhaitez-vous des informations supplémentaires?");
                                    int.TryParse(Console.ReadLine(), out int idInfosSuppl);
                                    Console.WriteLine();
                                    Console.WriteLine("1. Date de publication\n 2. Nombre de pages\n 3. Editeur");
                                    int.TryParse(Console.ReadLine(), out int infosSuppl);
                                    Console.WriteLine();
                                    switch (infosSuppl)
                                    {
                                        case 1:
                                            foreach (Livre item in serviceAfficherLivre.GetDateParution(idInfosSuppl))
                                            {
                                                Console.WriteLine($"{item.Titre} - {item.DateParution}");
                                            }
                                            break;
                                        case 2:
                                            foreach (Livre item in serviceAfficherLivre.GetNbPages(idInfosSuppl))
                                            {
                                                Console.WriteLine($"{item.Titre} - {item.NbPages}");
                                            }
                                            break;
                                        case 3:
                                            foreach (Livre item in serviceAfficherLivre.GetEditeur(idInfosSuppl))
                                            {
                                                Console.WriteLine($"{item.Titre} - {item.Editeur}");
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 2:
                            AuteurService serviceAddAuteur = new AuteurService(oConn);
                            Console.WriteLine("Insérez nom auteur");
                            string nom = Console.ReadLine();
                            Console.WriteLine("Insérez prénom auteur");
                            string prenom = Console.ReadLine();
                            Console.WriteLine("Insérez pseudonyme");
                            string pseudonyme = Console.ReadLine();
                            Console.WriteLine();
                            Auteur a = new Auteur { Nom = nom, Prenom = prenom, Pseudonyme = pseudonyme == "" ? null : pseudonyme };
                            serviceAddAuteur.AddAuteur(a);

                            serviceAddAuteur.GetAuteur();
                            foreach (Auteur item in serviceAddAuteur.GetAuteur())
                            {
                                Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                            }
                            break;
                        case 3: //Problème
                            AuteurService serviceUpdateAuteur = new AuteurService(oConn);
                            serviceUpdateAuteur.GetAuteur();
                            foreach (Auteur item in serviceUpdateAuteur.GetAuteur())
                            {
                                Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                            }
                            Console.WriteLine();
                            Console.WriteLine("Choisir auteur à modifier");
                            int.TryParse(Console.ReadLine(), out int modif);
                            Console.WriteLine();
                            Console.WriteLine("Insérez nom auteur");
                            string nomModifie = Console.ReadLine();
                            Console.WriteLine("Insérez prénom auteur");
                            string prenomModifie = Console.ReadLine();
                            Console.WriteLine("Insérez pseudonyme");
                            string pseudonymeModifie = Console.ReadLine();
                            Console.WriteLine();
                            serviceUpdateAuteur.UpdateAuteur(new Auteur { Id = modif, Nom = nomModifie, Prenom = prenomModifie, Pseudonyme = pseudonymeModifie == "" ? null : pseudonymeModifie });
                            foreach (Auteur item in serviceUpdateAuteur.GetAuteur())
                            {
                                Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                            }
                            break;
                        case 4:
                            AuteurService serviceDeleteAuteur = new AuteurService(oConn);
                            serviceDeleteAuteur.GetAuteur();
                            foreach (Auteur item in serviceDeleteAuteur.GetAuteur())
                            {
                                Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                            }
                            Console.WriteLine();
                            Console.WriteLine("Choisir auteur à supprimer");
                            int.TryParse(Console.ReadLine(), out int delete);
                            Console.WriteLine();
                            serviceDeleteAuteur.Delete(delete);

                            serviceDeleteAuteur.GetAuteur();
                            foreach (Auteur item in serviceDeleteAuteur.GetAuteur())
                            {
                                Console.WriteLine($"{item.Id}. {item.Nom} {item.Prenom}");
                            }
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Voulez-vous refaire une manipulation de la bibliothèque?");
                    reponse = Console.ReadLine();
                    Console.Clear();
                } while (reponse == "oui");
                Console.WriteLine("La manipulation de la bibliothèque est terminée");
            }
        }
    }
}
