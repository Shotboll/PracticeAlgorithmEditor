using System.Data;
using System.Data.OleDb;
using SignalAlgorithmEditor.Models;

namespace SignalAlgorithmEditor.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";

        private string _filePath = "";

        public List<Signal> LoadSignals(string? filePath = "|DataDirectory|\\db_example.accdb")
        {
            var signals = new List<Signal>();

            using (var connection = new OleDbConnection(_connectionString+filePath+';'))
            {
                _filePath = filePath!;
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

        public void SaveSignal(Signal signal)
        {
            if (string.IsNullOrWhiteSpace(signal?.Code))
                throw new ArgumentException("Код сигнала не может быть пустым.");

            using var connection = new OleDbConnection(_connectionString + _filePath + ';');
            connection.Open();

            string query = "UPDATE [Сигналы] SET [Алгоритм] = ? WHERE [Код сигнала] = ?";

            using var command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@Algorithm", signal.Algorithm ?? "");
            command.Parameters.AddWithValue("@Code", signal.Code);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
                throw new InvalidOperationException($"Сигнал с кодом '{signal.Code}' не найден в БД.");
        }
    }
}
