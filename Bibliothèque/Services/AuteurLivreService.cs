using Bibliothèque.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothèque.Services
{
    public class AuteurLivreService
    {
        private SqlConnection oConn;
        public AuteurLivreService(SqlConnection oConn)
        {
            this.oConn = oConn;
        }

        public List<AuteurLivre> GetByLivreId()
        {
            try
            {
                oConn.Open();
                string requete = @"SELECT Auteur.Nom, Livre.Titre
                              FROM Auteur INNER JOIN
                            AuteurLivre ON Auteur.Id = AuteurLivre.IdAuteur INNER JOIN
                             Livre ON AuteurLivre.IdLivre = Livre.Id";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                SqlDataReader reader = cmd.ExecuteReader();

                List<AuteurLivre> l = new List<AuteurLivre>();
                while (reader.Read())
                {
                    l.Add(
                        new AuteurLivre
                        {
                            IdLivreNavigation = new Livre { Titre = (string)reader["Titre"] },
                            IdAuteurNavigation = new Auteur { Nom = (string)reader["Nom"] }
                        });

                }
                return l;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oConn.Close();
            }

        }
        public int AddAuteurLivre(AuteurLivre auteurLivre)
        {
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "INSERT INTO AuteurLivre(IdAuteur, IdLivre) VALUES (@p1, @p2)";
                cmd.Parameters.AddWithValue("p1", auteurLivre.IdAuteur);
                cmd.Parameters.AddWithValue("p2", auteurLivre.IdLivre);
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oConn.Close();
            }
        }
        public bool UpdateAuteurLivre(AuteurLivre auteurLivre)
        {
            // UPDATE ... SET ... = ... WHERE ... = ...
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "UPDATE AuteurLivre SET IdAuteur, IdLivre = @p1, @p2 WHERE IdLivre = @p1";
                cmd.Parameters.AddWithValue("p1", auteurLivre.IdLivre);
                cmd.Parameters.AddWithValue("p2", auteurLivre.IdAuteur);
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oConn.Close();
            }
        }
        public bool Delete(int id)
        {
            // DELETE FROM ... WHERE ... = ....

            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "DELETE FROM AuteurLivre WHERE IdLivre = @p1";
                cmd.Parameters.AddWithValue("p1", id);
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oConn.Close();
            }
        }

    }
}
