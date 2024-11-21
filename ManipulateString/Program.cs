using System.IO;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string str = "1º             Agência da Capitania dos Portos em Paraty                                        AGPRTI               RJ          Barcaça                                                                                              4";
        string str2 = str.Replace("   ", ",");

        string myString = Regex.Replace(str, @"\s+", ",");

        Console.WriteLine(str2);
        Console.WriteLine(RemoveSpace(str));
        Console.WriteLine(myString);
        Console.WriteLine(RemoveSpace2(str));
    }

    public static string RemoveSpace(string sentence)
    {
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options);
        sentence = regex.Replace(sentence, " ");

      return sentence.ToString();
    }

    public static string RemoveSpace2(string str)
    {
        while (str.Contains("   ")) str = str.Replace("   ", ",");

        var myString = Regex.Replace(str, @",{2,}", ",");

        return myString;
    }
}