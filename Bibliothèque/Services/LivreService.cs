using Bibliothèque.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothèque.Services
{
    public class LivreService
    {
        private SqlConnection oConn;
        public LivreService(SqlConnection oConn)
        {
            this.oConn = oConn;
        }
        public List<Livre> GetLivre()
        {
            try
            {
                oConn.Open();
                string requete = "SELECT Id, Titre FROM Livre";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Livre> result = new List<Livre>();
                while (reader.Read())
                {
                    result.Add(new Livre
                    {
                        Id = (int)reader["Id"],
                        Titre = (string)reader["Titre"]
                    }); ;
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

        public List<Livre> GetDateParution(int id)
        {
            try
            {
                oConn.Open();
                string requete = "SELECT DateParution, Titre FROM Livre WHERE Id = @p1";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("p1", id);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Livre> result = new List<Livre>();
                while (reader.Read())
                {
                    result.Add(new Livre
                    {
                        DateParution = (int)reader["DateParution"],
                        Titre = (string)reader["Titre"]
                    }); ;
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
        public List<Livre> GetEditeur(int id)
        {
            try
            {
                oConn.Open();
                string requete = "SELECT Editeur, Titre FROM Livre WHERE Id = @p1";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("p1", id);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Livre> result = new List<Livre>();
                while (reader.Read())
                {
                    result.Add(new Livre
                    {
                        Editeur = (string)reader["Editeur"],
                        Titre = (string)reader["Titre"]
                    }); ;
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
        public List<Livre> GetNbPages(int id)
        {
            try
            {
                oConn.Open();
                string requete = "SELECT NbPages, Titre FROM Livre WHERE Id = @p1";
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("p1", id);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Livre> result = new List<Livre>();
                while (reader.Read())
                {
                    result.Add(new Livre
                    {
                        NbPages = (int)reader["NbPages"],
                        Titre = (string)reader["Titre"]
                    }); ;
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
        public int AddLivre(Livre livre)
        {
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "INSERT INTO Livre(Titre, DateParution, Editeur, NbPages, Serie, Tome, Isbn) OUTPUT inserted.Id VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                cmd.Parameters.AddWithValue("p1", livre.Titre);
                cmd.Parameters.AddWithValue("p2", livre.DateParution);
                cmd.Parameters.AddWithValue("p3", livre.Editeur);
                cmd.Parameters.AddWithValue("p4", livre.NbPages);
                cmd.Parameters.AddWithValue("p5", livre.Serie);
                cmd.Parameters.AddWithValue("p6", livre.Tome);
                cmd.Parameters.AddWithValue("p7", livre.Isbn);
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

        public bool UpdateAuteur(Livre livre)
        {
            // UPDATE ... SET ... = ... WHERE ... = ...
            try
            {
                oConn.Open();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.CommandText = "UPDATE Livre SET Titre, DateParution, Editeur, NbPages, Serie, Tome, Isbn = @p1, @p2, @p3, @p4, @p5, @p6, @p7 WHERE Id = @p8";
                cmd.Parameters.AddWithValue("p1", livre.Titre);
                cmd.Parameters.AddWithValue("p2", livre.DateParution);
                cmd.Parameters.AddWithValue("p3", livre.Editeur);
                cmd.Parameters.AddWithValue("p4", livre.NbPages);
                cmd.Parameters.AddWithValue("p5", livre.Serie);
                cmd.Parameters.AddWithValue("p6", livre.Tome);
                cmd.Parameters.AddWithValue("p7", livre.Isbn);
                cmd.Parameters.AddWithValue("p8", livre.Id);
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
                cmd.CommandText = "DELETE FROM Livre WHERE Id = @p1";
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
