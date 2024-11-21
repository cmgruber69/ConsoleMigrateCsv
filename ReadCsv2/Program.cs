using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

internal class Program
{
    public class Boat
    {
        public string Reg { get; set; }
        public string Portos { get; set; }
        public string DPC { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public int Volume { get; set; }
        public int Ano { get; set; }
    }

    private static void Main(string[] args)
    {
        var csvFilePath = @"c:\Temp\b2024.csv";
        var csvFilePathOut = @"c:\Temp\b2024OUT.csv";

        // Reading from CSV
        var boatstringList = ReadFromTxt(csvFilePath);

        List<Boat> BoatList = new();
        Console.WriteLine("Reading from CSV file:");
        int cout = 0;
        foreach (var boatline in boatstringList)
        {
            Console.WriteLine($"{boatline.TrimStart().TrimEnd()}");
            string[] arrayOfStrings = boatline.Split(',');
            if(cout != 0)
            {
                var res = new Boat
                {
                    Reg = arrayOfStrings[0],
                    Portos = arrayOfStrings[1],
                    DPC = arrayOfStrings[2],
                    Estado = arrayOfStrings[3],
                    Tipo = arrayOfStrings[4],
                    Volume = int.Parse(arrayOfStrings[5]),
                    Ano = 2024
                };
                BoatList.Add(res);
            }
            cout++;
        }

        WriteUsersToCsv(csvFilePathOut, BoatList);

        Console.WriteLine("Finished!");
    }

    public static List<string> ReadFromTxt(string filePath)
    {
        List<string> listabarco = new();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var res = RemoveSpace(line);
                listabarco.Add(res);
            }
        }

        return listabarco;
    }

    public static void WriteUsersToCsv(string filePath, List<Boat> boats)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(boats);
        }
    }

    public static string RemoveSpace(string str)
    {
        while (str.Contains("   ")) str = str.Replace("   ", ",");

        var myString = Regex.Replace(str, @",{2,}", ",");
        myString = myString.Replace("(Catamarã, Trimarã, Tetramarã, etc)","");
        myString = myString.Replace(", Production, Storage and Off-Loading Unit (FPSO)", "");
        myString = myString.Replace("/ Carga Geral", "");
        myString = myString.Replace("(transporte de granéis líquidos)", "");
        myString = myString.Replace("/similar", "");
        myString = myString.Replace("Apoio a", "");
        myString = myString.Replace("Roll-on / Roll-off Passageiro (Ferry Boat)", "Ferry Boat");
        myString = myString.Replace("(Navio de Cargas Especiais)", "");
        myString = myString.Replace("(HSC Passageiro)", "");
        myString = myString.Replace("(AHTS)", "");
        myString = myString.Replace("(Supply)", "");
        myString = myString.Replace("(FSU)", "");
        return myString.TrimStart().TrimEnd();
    }
}