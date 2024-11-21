using CsvHelper;
using Microsoft.Data.Sqlite;
using System.Globalization;


public class Program
{
    public record Barco(string Reg, string Portos, string DPC, string Estado, string Tipo, int Volume, int Ano);

    public static IEnumerable<Barco> ReadAuthorsFromCSV(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Skip header of the csv file
        csv.Read();

        // Read the header of the csv file to map to fields
        csv.ReadHeader();

        while (csv.Read())
        {
            var reg = csv.GetField<string>("Reg") ?? string.Empty;
            var portos = csv.GetField<string>("Portos") ?? string.Empty;
            var dcp = csv.GetField<string>("DPC") ?? string.Empty;
            var estado = csv.GetField<string>("Estado") ?? string.Empty;
            var tipo = csv.GetField<string>("Tipo") ?? string.Empty;
            var volume = csv.GetField<int>("Volume");
            var ano = csv.GetField<int>("Ano");

            yield return new Barco(reg, portos, dcp, estado, tipo, volume, ano);
        }
    }

    private static void Main(string[] args)
    {
        var csvFilePath = @"c:\Temp\b2024OUT.csv";
        var sql = "INSERT INTO Barcos(Reg, Portos, DPC, Estado, Tipo, Volume, Ano) VALUES (@Reg, @Portos, @DPC, @Estado, @Tipo, @Volume, @Ano)";

        try
        {
            // Open a new database connection
            using var connection = new SqliteConnection(@"Data Source=C:\Temp\mercado.db");
            connection.Open();

            // Insert lines of CSV into the authors table
            foreach (var barco in ReadAuthorsFromCSV(csvFilePath))
            {
                using var command = new SqliteCommand(sql, connection);
                command.Parameters.AddWithValue("@Reg", barco.Reg.TrimStart().TrimEnd());
                command.Parameters.AddWithValue("@Portos", barco.Portos.TrimStart().TrimEnd());
                command.Parameters.AddWithValue("@DPC", barco.DPC.TrimStart().TrimEnd());
                command.Parameters.AddWithValue("@Estado", barco.Estado.TrimStart().TrimEnd());
                command.Parameters.AddWithValue("@Tipo", barco.Tipo.TrimStart().TrimEnd());
                command.Parameters.AddWithValue("@Volume", barco.Volume);
                command.Parameters.AddWithValue("@Ano", barco.Ano);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Finished");
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}