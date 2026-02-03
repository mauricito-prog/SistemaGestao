using System;
using System.Data.SQLite;
using System.IO;

namespace SistemaGestao
{
    public static class DatabaseHelper
    {
        private static string dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "gestao.db"
        );

        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InicializarBanco()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                        CREATE TABLE IF NOT EXISTS Clientes (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Telefone TEXT NOT NULL,
                            Email TEXT,
                            Endereco TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Produtos (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Preco REAL NOT NULL,
                            QuantidadeEstoque INTEGER NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS Vendas (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ClienteId INTEGER NOT NULL,
                            ProdutoId INTEGER NOT NULL,
                            Quantidade INTEGER NOT NULL,
                            ValorTotal REAL NOT NULL,
                            DataVenda TEXT NOT NULL,
                            FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
                            FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
                        );
                    ";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
