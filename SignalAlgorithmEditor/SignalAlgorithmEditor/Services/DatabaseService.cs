using System.Data;
using System.Data.OleDb;
using SignalAlgorithmEditor.Models;

namespace SignalAlgorithmEditor.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";

        public List<Signal> LoadSignals(string? filePath = "|DataDirectory|\\db_example.accdb")
        {
            var signals = new List<Signal>();

            using (var connection = new OleDbConnection(_connectionString+filePath+';'))
            {
                connection.Open();

                string query = "SELECT [Код сигнала], [Алгоритм], [Формат] FROM [Сигналы]";

                using (var command = new OleDbCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        var signal = new Signal
                        {
                            Code = reader["Код сигнала"].ToString()!,
                            Algorithm = reader["Алгоритм"].ToString()!,
                            Format = reader["Формат"].ToString()!
                        };
                        if(signal.Algorithm != "")
                        {
                            signals.Add(signal);
                        }
                    }
                }
            }

            return signals;
        }
    }
}
