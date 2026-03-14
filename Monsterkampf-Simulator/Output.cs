namespace Monsterkampf_Simulator;

class Output
{
    private static readonly int DistanceBufferWidth = (int)(Console.BufferWidth * 0.5f);
    private static readonly int HalfLabelLength = 25;


    /// <summary>
    /// Label ausprinten.
    /// </summary>
    public static void PrintLabel()
    {
        PrintLabelDistance();

        Console.WriteLine("--------------------------------------------------"); PrintLabelDistance();
        Console.WriteLine("*(°W°)*   --- Monsterkampf-Simulator ---   ~(°~°)~"); PrintLabelDistance();
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine();
    }

    /// <summary>
    /// Intro ausprinten.
    /// </summary>
    public static void PrintIntro()
    {
        Console.WriteLine("Wähle 2 Monster aus der Liste der verfügbaren aus:");
        ListRaces();
        Console.WriteLine("z = Zufall-Auswahl");
        Console.WriteLine();
    }

    /// <summary>
    /// Alle Monster auslisten.
    /// </summary>
    public static void ListRaces()
    {
        for (int i = 1; i < Program.Races.Length; i++)
            
            Console.WriteLine($"{i} = {Program.Races[i]}");
    }

    /// <summary>
    /// Standard-Attributen-Werte ausprinten.
    /// </summary>
    public static void PrintDefaultAttributes()
    {
        Console.WriteLine($"{"Vorlage",-8}{"  ",-3}{"Ork        ",-20}{"  ",-3}{"Troll        ",-20}{"  ",-3}{"Goblin        ",-20}");
        Console.WriteLine($"{"       ",-8}{"HP",-3}{Ork.DefaultHP,-20}{"HP",-3}{Troll.DefaultHP,-20}{"HP",-3}{Goblin.DefaultHP,-20}");
        Console.WriteLine($"{"       ",-8}{"AP",-3}{Ork.DefaultAP,-20}{"AP",-3}{Troll.DefaultAP,-20}{"AP",-3}{Goblin.DefaultAP,-20}");
        Console.WriteLine($"{"       ",-8}{"DP",-3}{Ork.DefaultDP,-20}{"DP",-3}{Troll.DefaultDP,-20}{"DP",-3}{Goblin.DefaultDP,-20}");
        Console.WriteLine($"{"       ",-8}{"SP",-3}{Ork.DefaultSP,-20}{"SP",-3}{Troll.DefaultSP,-20}{"SP",-3}{Goblin.DefaultSP,-20}");
        Console.WriteLine();
    }

    /// <summary>
    /// Ergebnis ausprinten.
    /// </summary>
    /// <param name="monster1"></param>
    /// <param name="monster2"></param>
    public static void PrintResult(MonsterBase monster1, MonsterBase monster2)
    {
        Console.WriteLine($"\nDer Kampf ist vorbei nach {Program.Round} Runden!");

        if (monster1.HP == monster2.HP)
        {
            Console.WriteLine("Es ist ein Unentschieden!");
        }
        else if (monster1.HP > monster2.HP)
        {
            Console.WriteLine($"{monster1.Race} hat gewonnen!");
        }
        else
        {
            Console.WriteLine($"{monster2.Race} hat gewonnen!");
        }
        Console.WriteLine();
        Console.WriteLine($"Deine Wette: {Program.WinnerChoice} und {Program.RoundChoice} Runden");
        Console.WriteLine();

        // Falls Goblin ins Spiel ist, werden am Ende des Kampfs die zuällige Werte ausgeprintet.

        if (monster1.Race == "Goblin" || monster2.Race == "Goblin")
        {
            Goblin.PrintResult();
        }
    }

    /// <summary>
    /// Trennlinie ausprinten.
    /// </summary>
    public static void PrintLine()
    {
        for (int i = 0; i < Console.BufferWidth - 1; i++) 
            Console.Write("-"); 

        Console.WriteLine();
    }

    /// <summary>
    /// Leere Zeilen fürs Abstand zur Wiederholung des Spiels.
    /// </summary>
    public static void PrintBufferSpace()
    {
        for (int i = 0; i < Console.BufferHeight; i++)
            Console.WriteLine();
    }

    /// <summary>
    /// Leerzeichen für den Label für den Abstand zum linken Rand ausprinten.
    /// </summary>
    private static void PrintLabelDistance()
    {
        int distanceSpace = DistanceBufferWidth - HalfLabelLength;

        for (int i = 0; i < distanceSpace; i++) 
            Console.Write(" ");
    }
}