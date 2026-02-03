using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemaGestao.Models;

namespace SistemaGestao.DAL
{
    public class ClienteDAL
    {
        public void Inserir(Cliente cliente)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO Clientes (Nome, Telefone, Email, Endereco) VALUES (@Nome, @Telefone, @Email, @Endereco)";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Email", cliente.Email ?? "");
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? "");
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListarTodos()
        {
            var clientes = new List<Cliente>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Clientes ORDER BY Nome";

                using (var command = new SQLiteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString(),
                            Telefone = reader["Telefone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Endereco = reader["Endereco"].ToString()
                        });
                    }
                }
            }

            return clientes;
        }

        public void Atualizar(Cliente cliente)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "UPDATE Clientes SET Nome = @Nome, Telefone = @Telefone, Email = @Email, Endereco = @Endereco WHERE Id = @Id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Email", cliente.Email ?? "");
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? "");
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM Clientes WHERE Id = @Id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
