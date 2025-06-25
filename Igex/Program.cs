
using Igex.Models;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            if (args[0] == "-RankSong")
                Console.Write(RankSong_gex.gex_Song(args[1]).FullName);
            else if (args[0] == "-Recommends")
                Console.Write(Recommend_gex.gex_Song(args[1]).FullName);
        }
        else if (args[0] == "-?")
        {
            Console.WriteLine("Igex tool for bilimusic ver.0");
            Console.ReadKey();
        }
    }
}