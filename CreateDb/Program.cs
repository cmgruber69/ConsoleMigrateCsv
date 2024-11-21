using Microsoft.Data.Sqlite;

internal class Program
{
    private static void Main(string[] args)
    {
        var sql = @"CREATE TABLE Barcos(
                    id INTEGER PRIMARY KEY,
                    Reg TEXT NOT NULL,
                    Portos TEXT NOT NULL,
                     DPC TEXT NOT NULL,
                     Estado TEXT NOT NULL,
                     Rebocador TEXT NOT NULL,
                     Volume INTEGER NOT NULL
                )";

        try
        {
            using var connection = new SqliteConnection(@"Data Source=C:\Temp\mercado.db");
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Table 'Barcos' created successfully.");

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}