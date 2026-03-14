namespace Monsterkampf_Simulator;

class DisplayBar
{
    private static readonly int BarSpace = 26; //        |     Platz für Anzeige-Balken     | 
    //                                                                     |
    // Anzeige Beispiel:  HP 100/200    |/////////_________|                HP...
    //                                                                     |
    private static int _barValue;         //             |  Wert  |         |               |
    private static int _barLength;         //            |      Länge       |               |
    private static float _floatValue; //  Momentane Wert


    /// <summary>
    /// Anzeige-Balken ausprinten.
    /// </summary>
    /// <param name="monster1"></param>
    /// <param name="monster2"></param>
    public static void Print(MonsterBase monster1, MonsterBase monster2)
    {
        // Für einen besseren Überblick werden Methoden sich auf Zeilen ähnlich wie das Text-Muster zusammengerückt, welches in der Konsole ausgeprintet wird.

        Console.WriteLine($"{"",-60}{"",-15} {$"{monster1.Race}",-40} {monster2.Race}");

        //                                                                              Ork                                               Troll
        // Anzeige Beispiel:                                              HP 100/200    |/////////_________|                HP 100/200    |/////////_________|

        Console.Write($"{"",-60} {$"HP {(int)monster1.HP}/{(int)monster1.BasicHP}",-15}"); BarHP(monster1); Console.Write($"{$" HP {(int)monster2.HP}/{(int)monster2.BasicHP}",-15}"); BarHP(monster2); Console.WriteLine();
        Console.Write($"{"",-60} {$"AP {(int)monster1.AP}/{(int)monster1.BasicAP}",-15}"); BarAP(monster1); Console.Write($"{$" AP {(int)monster2.AP}/{(int)monster2.BasicAP}",-15}"); BarAP(monster2); Console.WriteLine();
        Console.Write($"{"",-60} {$"DP {(int)monster1.DP}/{(int)monster1.BasicDP}",-15}"); BarDP(monster1); Console.Write($"{$" DP {(int)monster2.DP}/{(int)monster2.BasicDP}",-15}"); BarDP(monster2); Console.WriteLine();
        Console.Write($"{"",-60} {$"SP {(int)monster1.SP}/{(int)monster1.BasicSP}",-15}"); BarSP(monster1); Console.Write($"{$" SP {(int)monster2.SP}/{(int)monster2.BasicSP}",-15}"); BarSP(monster2); Console.WriteLine();

        Output.PrintLine();
    }

    /// <summary>
    /// HP Wert wird ausgelesen und in der Anzeige-Balken als Striche definiert.
    /// </summary>
    static void BarHP(MonsterBase monster)
    {
        _barLength = 20;
        _floatValue = monster.HP / monster.BasicHP * _barLength;
        Color.Green();
        PrintBar();
    }

    /// <summary>
    /// AP Wert wird ausgelesen und in der Anzeige-Balken als Striche definiert.
    /// </summary>
    static void BarAP(MonsterBase monster)
    {
        _barLength = 10;
        _floatValue = monster.AP / monster.BasicAP * _barLength;
        Color.Red();
        PrintBar();
    }

    /// <summary>
    /// DP Wert wird ausgelesen und in der Anzeige-Balken als Striche definiert.
    /// </summary>
    static void BarDP(MonsterBase monster)
    {
        _barLength = 10;
        _floatValue = monster.DP / monster.BasicDP * _barLength;
        Color.Blue();
        PrintBar();
    }

    /// <summary>
    /// SP Wert wird ausgelesen und in der Anzeige-Balken als Striche definiert.
    /// </summary>
    static void BarSP(MonsterBase monster)
    {
        _barLength = 5;
        _floatValue = monster.SP / monster.BasicSP * _barLength;
        Color.Yellow();
        PrintBar();
    }

    /// <summary>
    /// Anzeige-Balken ausprinten.
    /// </summary>
    static void PrintBar()
    {
        /*                              |    Platz für Anzeige-Balken      |
                                                                       |
    Anzeige Beispiel:  HP 100/200   |///////___________|                HP...
                                                                       |
                                    | Wert |           |               |
                                    |      |Unterstrich|               |
                                    |      Länge       |               |
     */

        char[] charField = new char[BarSpace];

        _barValue = (int)_floatValue;

        if (_floatValue != 0 && _floatValue < 1) // 1 Balken soll noch angezeigen werden, wenn Wert kleiner als 1 und noch nicht 0 ist.
            _barValue = 1;

        if (_barValue >= BarSpace) // Wert geht über Platz hinaus.
        {
            _barValue = BarSpace;
        }

        for (int i = 0; i < _barValue; i++) // Wert
        {
            charField[i] = '/';
        }

        for (int i = _barValue; i < BarSpace; i++) // Leere Platz
        {
            charField[i] = ' ';
        }

        for (int i = _barValue; i < _barLength; i++) // Unterstriche
        {
            charField[i] = '_';
        }

        for (int i = 0; i < BarSpace; i++)
        {
            if (i == 0 || i == _barLength - 1) charField[i] = '|';

            Console.Write(charField[i]);
        }

        Color.White();

        _barValue = 0;
    }
}