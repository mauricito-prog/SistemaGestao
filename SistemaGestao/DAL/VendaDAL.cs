using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemaGestao.Models;

namespace SistemaGestao.DAL
{
    public class VendaDAL
    {
        public void Inserir(Venda venda)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO Vendas (ClienteId, ProdutoId, Quantidade, ValorTotal, DataVenda) VALUES (@ClienteId, @ProdutoId, @Quantidade, @ValorTotal, @DataVenda)";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                    command.Parameters.AddWithValue("@ProdutoId", venda.ProdutoId);
                    command.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
                    command.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                    command.Parameters.AddWithValue("@DataVenda", venda.DataVenda.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Venda> ListarTodas()
        {
            var vendas = new List<Venda>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string sql = @"
                    SELECT v.*, c.Nome as NomeCliente, p.Nome as NomeProduto 
                    FROM Vendas v
                    INNER JOIN Clientes c ON v.ClienteId = c.Id
                    INNER JOIN Produtos p ON v.ProdutoId = p.Id
                    ORDER BY v.DataVenda DESC";

                using (var command = new SQLiteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vendas.Add(new Venda
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ClienteId = Convert.ToInt32(reader["ClienteId"]),
                            NomeCliente = reader["NomeCliente"].ToString(),
                            ProdutoId = Convert.ToInt32(reader["ProdutoId"]),
                            NomeProduto = reader["NomeProduto"].ToString(),
                            Quantidade = Convert.ToInt32(reader["Quantidade"]),
                            ValorTotal = Convert.ToDecimal(reader["ValorTotal"]),
                            DataVenda = DateTime.Parse(reader["DataVenda"].ToString())
                        });
                    }
                }
            }

            return vendas;
        }
    }
}
