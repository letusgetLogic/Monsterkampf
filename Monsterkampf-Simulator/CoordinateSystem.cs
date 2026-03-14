namespace Monsterkampf_Simulator;

class CoordinateSystem
{
    private const int AxisLengthY = 30; // Länge der x-Achse (horizontal) und y-Achse (vertikal).
    private const int AxisLengthX = 100;
    
    private const float MaxValueInAxisY = 150f; // Werte, wo die Attributen-Werte sich orentieren.
    private const float ValueHp = 100f;
    private const float ValueAp = 50f;
    private const float ValueDp = 40f;
    private const float ValueSp = 30f;
     
    private const float ValueDistancesInAxisY = 10; // Größe der Abstände zwischen den zu zeigenden Werten in Achse Y.
    private const int ValueDistancesInAxisX = 10; // Größe der Abstände zwischen den zu zeigenden Werten in Achse X.
    
    private const char LineAxisX = '_';
    private const char LineAxisY = '|';
    private const char Empty = ' ';
    private const char CharHp = 'H';
    private const char CharAp = 'A';
    private const char CharDp = 'D';
    private const char CharSp = 'S';

    private static float[]? _valueInAxisY;


    /// <summary>
    /// Array Erstellung von Datenspeicher und Diagramm für jedes Monster.
    /// </summary>
    /// <param name="monster"></param>
    public static void InitializeArray(MonsterBase monster)
    {
        monster.ValuesEachRound = new float[AxisLengthX + 5, 4];
        monster.Diagram = new char[AxisLengthY + 2, AxisLengthX + 5];
    }

    /// <summary>
    /// Initialisierung von Werten von jedem Monster für jede Runde.
    /// </summary>
    /// <param name="monster"></param>
    public static void InitializeValues(MonsterBase monster)
    {
        monster.ValuesEachRound[Program.Round, 0] = monster.SP;
        monster.ValuesEachRound[Program.Round, 1] = monster.DP;
        monster.ValuesEachRound[Program.Round, 2] = monster.AP;
        monster.ValuesEachRound[Program.Round, 3] = monster.HP;
    }

    /// <summary>
    /// Diagramm initialiseren und ausprinten.
    /// </summary>
    public static void PrintDiagram(MonsterBase monster)
    {
        InitializeDiagram(monster);
        Print(monster);
        Console.WriteLine();
        Console.WriteLine();
    }

    /// <summary>
    /// Initialisierung von Diagramm.
    /// </summary>
    /// <param name="monster"></param>
    private static void InitializeDiagram(MonsterBase monster)
    {
        _valueInAxisY = new float[AxisLengthY + 2];

        for (int y = 0; y < AxisLengthY + 2; y++) // Werten in y-Achse aufteilen und in jeweiligen Zeilen zuordnen.
        {
            // Bs.             120f            *         (1 / 24          * (24 - 0))
            float i = (float)MaxValueInAxisY * ((float)1 / AxisLengthY * (AxisLengthY - y));
            _valueInAxisY[y] = (int)Math.Round(i);
        }

        for (int y = 0; y < AxisLengthY + 2; y++)
        {
            for (int x = 0; x < AxisLengthX + 5; x++)
            {
                monster.Diagram[y, x] = Empty;

                if (x == 0 && y != AxisLengthY + 1) monster.Diagram[y, x] = LineAxisY;

                if (y == AxisLengthY && x != 0) monster.Diagram[y, x] = LineAxisX;

                // Wenn Spalte X die entsprechende Runden-Nummer übereinstimmt, werden die Attributen-Werten in entsprechenden Zeilen gesetzt.

                if (x != 0 && x < Program.Round + 1) 
                {
                    int s = (int)((float)monster.ValuesEachRound[x, 0] / (float)monster.BasicSP * (float)ValueSp);
                    int d = (int)((float)monster.ValuesEachRound[x, 1] / (float)monster.BasicDP * (float)ValueDp);
                    int a = (int)((float)monster.ValuesEachRound[x, 2] / (float)monster.BasicAP * (float)ValueAp);
                    int h = (int)((float)monster.ValuesEachRound[x, 3] / (float)monster.BasicHP * (float)ValueHp);

                    if (s <= _valueInAxisY[y] && s > _valueInAxisY[y + 1]) monster.Diagram[y, x] = CharSp;
                    if (d <= _valueInAxisY[y] && d > _valueInAxisY[y + 1]) monster.Diagram[y, x] = CharDp;
                    if (a <= _valueInAxisY[y] && a > _valueInAxisY[y + 1]) monster.Diagram[y, x] = CharAp;
                    if (h <= _valueInAxisY[y] && h > _valueInAxisY[y + 1]) monster.Diagram[y, x] = CharHp;
                }
            }

            // Die zu zeigende Runden-Nummer unter Achse X.
            
            if (y == AxisLengthY + 1)
            {
                for (int x = 0; x < AxisLengthX + 5; x++)
                {
                    if (x % ValueDistancesInAxisX == 0)
                    {
                        string combinedString = "";

                        combinedString += x.ToString();  // Int in String umwandeln und hinzufügen.

                        char[] charValueX = combinedString.ToCharArray(); // In ein char-Array konvertieren.
                        // Ausgabe des char-Arrays.
                        for (int i = 0; i < charValueX.Length; i++)
                        {
                            monster.Diagram[y, x + i] = charValueX[i]; // Setzt den nächsten Char in den nächsten Spaltenindex.
                        }

                        x += charValueX.Length - 1;

                        continue; // Ignoriert den 'nicht leeren' Char.
                    }

                    monster.Diagram[y, x] = Empty;
                }
            }
        }
    }

    /// <summary>
    /// Diagramm ausprinten.
    /// </summary>
    /// <param name="monster"></param>
    public static void Print(MonsterBase monster)
    {
        Console.Write($"{$"{monster.Race} - Kampfklasse: {(int)monster.CombatClass}",-35}");
        Console.Write($"{$"100% HP",-11}{"-> RW",-10}");
        Console.Write($"{$"100% AP",-11}{"-> RW",-10}");
        Console.Write($"{$"100% DP",-11}{"-> RW",-10}");
        Console.Write($"{$"100% Speed",-11}{"-> RW",-10}");
        Console.WriteLine();
        Console.WriteLine();

        Console.Write($"{$"Richtwerte(RW)",-35}");

        Console.Write($"{$"HP {(int)monster.BasicHP}",-11}"); Color.Green();  Console.Write($"{$"-> {ValueHp}",-10}"); Color.White();
        Console.Write($"{$"AP {(int)monster.BasicAP}",-11}"); Color.Red();    Console.Write($"{$"-> {ValueAp}",-10}"); Color.White();
        Console.Write($"{$"DP {(int)monster.BasicDP}",-11}"); Color.Blue();   Console.Write($"{$"-> {ValueDp}",-10}"); Color.White();
        Console.Write($"{$"Speed {(int)monster.BasicSP}",-11}"); Color.Yellow(); Console.Write($"{$"-> {ValueSp}",-10}"); Color.White();
        Console.WriteLine();
        Console.WriteLine();

        for (int y = 0; y < AxisLengthY + 2; y++)
        {
            // Soll nur ausprinten, wenn der Wert durch die definierte Größe teilbar ist.

            if (y != AxisLengthY + 1 && _valueInAxisY[y] % ValueDistancesInAxisY == 0) 
            {
                Console.Write($"{$"{_valueInAxisY[y]}",-5}");
            }
            else
            {
                Console.Write($"{"",-5}");
            }

            for (int x = 0; x < AxisLengthX + 5; x++)
            {
                if (monster.Diagram[y, x] == CharHp) Color.Green();
                if (monster.Diagram[y, x] == CharAp) Color.Red();   
                if (monster.Diagram[y, x] == CharDp) Color.Blue();  
                if (monster.Diagram[y, x] == CharSp) Color.Yellow();

                Console.Write(monster.Diagram[y, x]);

                Color.White();
            }

            if (y == AxisLengthY + 1) // y ist in der letzte Zeile.
            { 
                Console.WriteLine($"{"",-3}Runden"); 
            }

            Console.WriteLine();
        }
    }
}