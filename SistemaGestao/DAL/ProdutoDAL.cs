using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemaGestao.Models;

namespace SistemaGestao.DAL
{
    public class ProdutoDAL
    {
        public void Inserir(Produto produto)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO Produtos (Nome, Preco, QuantidadeEstoque) VALUES (@Nome, @Preco, @QuantidadeEstoque)";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@QuantidadeEstoque", produto.QuantidadeEstoque);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> ListarTodos()
        {
            var produtos = new List<Produto>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Produtos ORDER BY Nome";

                using (var command = new SQLiteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString(),
                            Preco = Convert.ToDecimal(reader["Preco"]),
                            QuantidadeEstoque = Convert.ToInt32(reader["QuantidadeEstoque"])
                        });
                    }
                }
            }

            return produtos;
        }

        public void Atualizar(Produto produto)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, QuantidadeEstoque = @QuantidadeEstoque WHERE Id = @Id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@QuantidadeEstoque", produto.QuantidadeEstoque);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM Produtos WHERE Id = @Id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarEstoque(int produtoId, int quantidade)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "UPDATE Produtos SET QuantidadeEstoque = QuantidadeEstoque - @Quantidade WHERE Id = @Id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", produtoId);
                    command.Parameters.AddWithValue("@Quantidade", quantidade);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
