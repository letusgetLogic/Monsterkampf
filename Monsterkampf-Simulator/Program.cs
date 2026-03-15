namespace Monsterkampf_Simulator;

class Program
{
    // Neues Monster -> Bearbeitung in CreateMonster() in switch-Abfrage.
    public readonly static string[] Races = ["None", "Ork", "Troll", "Goblin"];

    public static int Round;
    public static int CountRoundDown;

    public static bool SetMonster;
    public static bool SetAttribute;
    public static bool SetWinner;

    public static string? WinnerChoice;
    public static int RoundChoice;

    private const float MinHp = 1000f; // HP = Hit Points.
    private const float MaxHp = 10000f;

    private const float MinAp = 100f; // AP = Attack Points.
    private const float MaxAp = 500f;

    private const float MinDp = 10f; // DP = Defense Points.
    private const float MaxDp = 300f;

    private const float MinSp = 1f; // SP = Speed Points.
    private const float MaxSp = 100f;

    private static readonly Random Rnd = new();

    private static MonsterBase? _monster1;
    private static MonsterBase? _monster2;

    private static int _monsterChoice1;
    private static int _monsterChoice2;


    /// <summary>
    /// Hauptmethode, in der er für die Spielwiederholung sich selbst aufruft.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        SetDefaultVariables();

        PhaseMonsterChoice();
        PhaseAttributesChoice();
        PhaseWinnerChoice();

        CoordinateSystem.InitializeArray(_monster1);
        CoordinateSystem.InitializeArray(_monster2);

        Fight(_monster1, _monster2);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        CoordinateSystem.PrintDiagram(_monster1);
        CoordinateSystem.PrintDiagram(_monster2);

        Output.PrintResult(_monster1, _monster2);

        Console.ReadLine();
        Output.PrintLine();
        Output.PrintBufferSpace();
        Output.PrintLine();
        Main(args);
    }

    /// <summary>
    /// Monster Auswahl Phase.
    /// </summary>
    private static void PhaseMonsterChoice()
    {
        while (SetMonster == false)
        {
            _monsterChoice1 = 0;
            _monsterChoice2 = 0;
            Output.PrintLabel();
            Output.PrintIntro();
            AskMonster();
            Input.AskConfirm();
            Console.Clear();
        }
    }

    /// <summary>
    /// Attributen Auswahl Phase.
    /// </summary>
    private static void PhaseAttributesChoice()
    {
        while (SetAttribute == false)
        {
            AskAttributes();
            Input.AskConfirmAttribute();
            Console.Clear();
        }
    }

    /// <summary>
    /// Sieger Wette Phase.
    /// </summary>
    private static void PhaseWinnerChoice()
    {
        while (SetWinner == false)
        {
            AskAttributes();
            AskWinner();
            Input.AskConfirmWinner();
            Console.Clear();
        }
    }

    /// <summary>
    /// Monster-Auswahl vom Spieler und Erstellung vom Konstruktor.
    /// </summary>
    private static void AskMonster()
    {
        // --- Eingabe für 1. Monster ---

        Console.WriteLine("1. Monster (ID Nummer): ");
        InputMonsterChoice1();
        _monster1 = CreateMonster(_monsterChoice1); // Erstellung vom Konstruktor.

        // Ausgabe in der Konsole.
        Input.ClearCurrentLine();
        Input.SetCursorAtTopLine();
        Console.WriteLine(_monster1.Race);
        _monster1.ShowSkill();


        // --- Eingabe für 2. Monster ---

        Console.WriteLine("2. Monster (ID Nummer): ");
        InputMonsterChoice2();
        _monster2 = CreateMonster(_monsterChoice2); // Erstellung vom Konstruktor.

        // Ausgabe in der Konsole.
        Input.ClearCurrentLine();
        Input.SetCursorAtTopLine();
        Console.WriteLine(_monster2.Race);
        _monster2.ShowSkill();
    }

    /// <summary>
    /// Eingabe vom Spieler zum 1. Monster abfragen.
    /// </summary>
    public static void InputMonsterChoice1()
    {
        bool isInvalidInput = false;
        while (!(_monsterChoice1 >= 1 && _monsterChoice1 < Races.Length))
        {
            if (isInvalidInput)
                Console.Write("Die Eingabe ist nicht gültig!");
            else
                Console.WriteLine();

            Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

            string? input = Console.ReadLine();

            int.TryParse(input, out _monsterChoice1);

            if (input == "z") _monsterChoice1 = Rnd.Next(1, Races.Length); // Zufall-Wahl

            isInvalidInput = true; // Der Boolean wird ignoriert, wenn die Eingabe gültig ist, weil die while-Schleife nicht mehr ausgeführt wird.
        }
    }

    /// <summary>
    /// Eingabe vom Spieler zum 2. Monster abfragen.
    /// </summary>
    public static void InputMonsterChoice2()
    {
        bool isInvalidInput = false;
        while (!(_monsterChoice2 >= 1 && _monsterChoice2 < Races.Length && _monsterChoice2 != _monsterChoice1))
        {
            if (isInvalidInput)
                Console.Write("Die Eingabe ist nicht gültig!");
            else
                Console.WriteLine();

            Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

            string? input = Console.ReadLine();

            int.TryParse(input, out _monsterChoice2);

            if (input == "z") // Zufall-Wahl
            {
                do
                    _monsterChoice2 = Rnd.Next(1, Races.Length); // Zufall-Wahl
                while (_monsterChoice2 == _monsterChoice1);
            }

            isInvalidInput = true; // Der Boolean wird ignoriert, wenn die Eingabe gültig ist, weil die while-Schleife nicht mehr ausgeführt wird.
        }
    }

    /// <summary>
    /// Erstellung vom Konstruktor.
    /// </summary>
    /// <param name="numberChoice"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static MonsterBase CreateMonster(int numberChoice)
    {
        //  Die Attributen-Werte könnte als 0 ausgewählt werden.
        float hp = -1;
        float ap = -1;
        float dp = -1;
        float speed = -1;
        float combatClass = -1;

        switch (numberChoice)
        {
            case 1:
                return new Ork(Races[1], hp, ap, dp, speed, combatClass);
            case 2:
                return new Troll(Races[2], hp, ap, dp, speed, combatClass);
            case 3:
                return new Goblin(Races[3], hp, ap, dp, speed, combatClass);
            default:
                return null;
        }
    }

    /// <summary>
    /// Attributen-Auswahl vom Spieler.
    /// </summary>
    private static void AskAttributes()
    {
        Console.WriteLine();
        Output.PrintDefaultAttributes();
        Console.WriteLine("Gib die Attributen-Werte für die Monster ein: (z für Zufallswerte)");
        Console.WriteLine();

        GetSetAttributes(_monster1);
        GetSetAttributes(_monster2);
    }

    /// <summary>
    /// Bekommt Attributen Werte und übergibt in Konstruktor.
    /// </summary>
    /// <param name="monster"></param>
    private static void GetSetAttributes(MonsterBase monster)
    {
        Console.WriteLine($"- {monster.Race} -");
        GetInputHP(monster);
        GetInputAP(monster);
        GetInputDP(monster);
        GetInputSP(monster);

        monster.BasicHP = monster.HP;
        monster.BasicAP = monster.AP;
        monster.BasicDP = monster.DP;
        monster.BasicSP = monster.SP;

        monster.CombatClass = monster.HP + monster.AP + monster.DP + monster.SP;
        Console.WriteLine("- Kampfklasse: " + (int)monster.CombatClass);

        monster.ShowSkill();
        Console.WriteLine();
    }

    /// <summary>
    /// Eingabe für die Lebenspunkte.
    /// </summary>
    private static void GetInputHP(MonsterBase monster)
    {
        Console.WriteLine($"Hit Points HP ({MinHp} - {MaxHp}):");

        if (SetAttribute == false) // Bei der Bestätigung wird vorher die Konsole geleert und dann den eingegebenen Wert ausgeprintet.
        {
            Console.WriteLine();

            monster.HP = -1;

            while (!(monster.HP >= MinHp && monster.HP <= MaxHp)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
            {
                Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

                string? input = Console.ReadLine(); // Spieler Input

                float.TryParse(input, out monster.HP);

                if (input == "z") // Zufall-Wahl
                {
                    monster.HP = (float)Rnd.NextDouble() * (MaxHp - MinHp) + MinHp;

                    Input.SetCursorAtTopLine(); // Cursor auf die erste Stelle der obere Zeile setzen.

                    Console.WriteLine((int)monster.HP);
                }
            }
        }
        else Console.WriteLine((int)monster.HP);
    }

    /// <summary>
    /// Eingabe fürs Angriff-Wert.
    /// </summary>
    /// <param name="monster"></param>
    private static void GetInputAP(MonsterBase monster)
    {
        Console.WriteLine($"Attack Points AP ({MinAp} - {MaxAp}):");

        if (SetAttribute == false) // Bei der Bestätigung wird vorher die Konsole geleert und dann den eingegebenen Wert ausgeprintet.
        {
            Console.WriteLine();

            monster.AP = -1;

            while (!(monster.AP >= MinAp && monster.AP <= MaxAp)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
            {
                Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

                string? input = Console.ReadLine(); // Spieler Input

                float.TryParse(input, out monster.AP);

                if (input == "z") // Zufall-Wahl
                {
                    monster.AP = (float)Rnd.NextDouble() * (MaxAp - MinAp) + MinAp;

                    Input.SetCursorAtTopLine(); // Cursor auf die erste Stelle der obere Zeile setzen.

                    Console.WriteLine((int)monster.AP);
                }
            }
        }
        else Console.WriteLine((int)monster.AP);
    }

    /// <summary>
    /// Eingabe fürs Verteidigung-Wert.
    /// </summary>
    /// <param name="monster"></param>
    private static void GetInputDP(MonsterBase monster)
    {
        Console.WriteLine($"Defense Points DP ({MinDp} - {MaxDp}):");

        if (SetAttribute == false) // Bei der Bestätigung wird vorher die Konsole geleert und dann den eingegebenen Wert ausgeprintet.
        {
            Console.WriteLine();

            monster.DP = -1;

            while (!(monster.DP >= MinDp && monster.DP <= MaxDp)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
            {
                Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

                string? input = Console.ReadLine(); // Spieler Input

                float.TryParse(input, out monster.DP);

                if (input == "z") // Zufall-Wahl
                {
                    monster.DP = (float)Rnd.NextDouble() * (MaxDp - MinDp) + MinDp;

                    Input.SetCursorAtTopLine(); // Cursor auf die erste Stelle der obere Zeile setzen.

                    Console.WriteLine((int)monster.DP);
                }
            }
        }
        else Console.WriteLine((int)monster.DP);
    }

    /// <summary>
    /// Eingabe fürs Geschwindigkeit-Wert.
    /// </summary>
    /// <param name="monster"></param>
    private static void GetInputSP(MonsterBase monster)
    {
        Console.WriteLine($"Speed Points SP ({MinSp} - {MaxSp}):");

        if (SetAttribute == false) // Bei der Bestätigung wird vorher die Konsole geleert und dann den eingegebenen Wert ausgeprintet.
        {
            Console.WriteLine();
            monster.SP = -1;

            while (!(monster.SP >= MinSp && monster.SP <= MaxSp)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
            {
                Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

                string? input = Console.ReadLine(); // Spieler Input

                float.TryParse(input, out monster.SP);

                if (input == "z") // Zufall-Wahl
                {
                    monster.SP = (float)Rnd.NextDouble() * (MaxSp - MinSp) + MinSp;

                    Input.SetCursorAtTopLine(); // Cursor auf die erste Stelle der obere Zeile setzen.

                    Console.WriteLine((int)monster.SP);
                }
            }
        }
        else Console.WriteLine((int)monster.SP);
    }

    /// <summary>
    /// Sieger abfragen.
    /// </summary>
    private static void AskWinner()
    {
        int index1 = Array.IndexOf(Races, _monster1.Race);
        int index2 = Array.IndexOf(Races, _monster2.Race);

        Console.WriteLine("Was denkst du: Welche Monster wird gewinnen?");
        Console.WriteLine();
        Console.WriteLine($"Gib die ID-Nummer ein: {index1} = {_monster1.Race} oder {index2} = {_monster2.Race}");
        Console.WriteLine();

        int inputNum = 0;

        while (!(inputNum == index1 || inputNum == index2)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
        {
            Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

            string? input = Console.ReadLine(); // Spieler Input

            int.TryParse(input, out inputNum);
        }

        WinnerChoice = Races[inputNum]; // string Rasse

        Input.SetCursorAtTopLine(); // Cursor auf die erste Stelle der obere Zeile setzen.

        Console.WriteLine(WinnerChoice);
        Console.WriteLine();

        Console.WriteLine("Nach wie viel Runden wird der Kampf beendet?");
        Console.WriteLine();

        RoundChoice = 0;

        while (!(RoundChoice > 0)) // Solange der Wert nicht im Bereich liegt, führt nochmal aus.
        {
            Input.NewPlayerInput(); // Leert die obere Zeile und setzt den Cursor am Anfang der Zeile.

            string? input = Console.ReadLine(); // Spieler Input

            int.TryParse(input, out RoundChoice);
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Logik, Ablauf und Ausprinten der Kampfphasen.
    /// </summary>
    /// <param name="monster1"></param>
    /// <param name="monster2"></param>
    private static void Fight(MonsterBase monster1, MonsterBase monster2)
    {
        Output.PrintLabel();
        Output.PrintLine();
        Console.WriteLine($"{monster1.Race} gegen {monster2.Race}! Der Kampf beginnt!");
        Console.WriteLine();

        Console.WriteLine(monster1.Race);
        monster1.ShowSkill();
        Console.WriteLine();

        Console.WriteLine(monster2.Race);
        monster2.ShowSkill();

        Output.PrintLine(); // Linie zum Abtrennen zwischen Bereichen.

        // Bestimme, wer den ersten Angriff macht basierend auf der Geschwindigkeit.

        MonsterBase attacker = monster1.SP >= monster2.SP ? monster1 : monster2;
        MonsterBase defender = attacker == monster1 ? monster2 : monster1;

        // Der Kampf geht so lange, bis ein Monster besiegt ist oder bis Runde 100.

        while (monster1.HP > 0 && monster2.HP > 0 && Round < 100)
        {
            Round++;
            Console.WriteLine($"Runde {Round}:");

            // --- Kampfphasen ---

            attacker.ActivateSkill(defender);
            attacker.Attack(defender);
            defender.Defend(attacker);
            defender.TakeDamage(attacker);
            attacker.ActivateSkill2();
            MonsterBase.ActivateSkill3(attacker, defender);

            Console.WriteLine();
            Console.WriteLine();

            // --- Anzeige-Balken ---

            DisplayBar.Print(monster1, monster2);

            // Nach jede Runde werden Werte in Array gespeichert.

            CoordinateSystem.InitializeValues(monster1);
            CoordinateSystem.InitializeValues(monster2);

            // Setzt Standardwerte.

            attacker.SetDefaultValue();
            defender.SetDefaultValue();

            // Wechsel von Angreifer und Verteidiger.

            (attacker, defender) = (defender, attacker);
        }
    }

    /// <summary>
    /// Setzt die Variablen auf dem Standard.
    /// </summary>
    private static void SetDefaultVariables()
    {
        Round = 0;
        CountRoundDown = 0;

        SetMonster = false;
        SetAttribute = false;
        SetWinner = false;

        WinnerChoice = "";
        RoundChoice = 0;

        _monster1 = null;
        _monster2 = null;

        _monsterChoice1 = 0;
        _monsterChoice2 = 0;

        Goblin.CountCrit = 0;
        Goblin.CountIgnore = 0;
        Goblin.CountDodge = 0;
        Goblin.CountRound = 0;
    }
}