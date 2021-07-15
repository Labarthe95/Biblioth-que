using Bibliothèque.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothèque.Services
{
    public class AuteurService
    {
        private SqlConnection oConn;
        public AuteurService(SqlConnection oConn)
        {
            this.oConn = oConn;
        }
        public List<Auteur> GetAuteur()
        {
            try
            {
                oConn.Open();
                string requete = "SELECT Nom, Prenom, Id FROM Auteur";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Auteur> result = new List<Auteur>();
                while (reader.Read())
                {
                    result.Add(new Auteur
                    {
                        Nom = (string)reader["Nom"],
                        Prenom = (string)reader["Prenom"],
                        Id = (int)reader["Id"]
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                oConn.Close();
            }
        }

        public int AddAuteur(Auteur auteur)
        {
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "INSERT INTO Auteur(Nom, Prenom, Pseudonyme) OUTPUT inserted.Id VALUES (@p1, @p2, @p3)";
                cmd.Parameters.AddWithValue("p1", auteur.Nom);
                cmd.Parameters.AddWithValue("p2", auteur.Prenom);
                cmd.Parameters.AddWithValue("p3", (object)auteur.Pseudonyme ?? DBNull.Value);
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

        public bool UpdateAuteur(Auteur auteur)
        {
            // UPDATE ... SET ... = ... WHERE ... = ...
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "UPDATE Auteur SET Nom, Prenom, Pseudonyme = @p1, @p2, @p3 WHERE Id = @p4";
                cmd.Parameters.AddWithValue("p1", auteur.Nom);
                cmd.Parameters.AddWithValue("p2", auteur.Prenom);
                cmd.Parameters.AddWithValue("p3", (object)auteur.Pseudonyme ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p4", auteur.Id);
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
                cmd.CommandText = "DELETE FROM Auteur WHERE Id = @p1";
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
