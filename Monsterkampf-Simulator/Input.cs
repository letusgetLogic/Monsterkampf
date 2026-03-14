namespace Monsterkampf_Simulator;

class Input
{
    /// <summary>
    /// Cursor auf die erste Stelle der obere Zeile setzen.
    /// </summary>
    public static void ReplaceInput()
    {
        int previosLineCursor = Console.CursorTop - 1;  
        Console.SetCursorPosition(0, previosLineCursor);
    }

    /// <summary>
    /// Neue Eingabe von Spieler // Bringt den Cursor auf die erste Stelle der obere Zeile, leert die Zeile und setzt wieder am Anfang der Zeile.
    /// </summary>
    public static void NewPlayerInput()
    {
        int previosLineCursor = Console.CursorTop - 1;
        Console.SetCursorPosition(0, previosLineCursor);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, previosLineCursor);
    }

    /// <summary>
    /// Monster Auswahl Bestätigung.
    /// </summary>
    public static void AskConfirm()
    {
        Console.WriteLine("Bestätige mit ENTER oder ändere die Monster mit einer anderen beliebigen Taste");

        ConsoleKey consoleKey = Console.ReadKey().Key;

        if (consoleKey == ConsoleKey.Enter)
        {
            Program.SetMonster = true;
        }
    }

    /// <summary>
    /// Attributen Auswahl Bestätigung.
    /// </summary>
    public static void AskConfirmAttribute()
    {
        Console.WriteLine("Bestätige mit ENTER oder ändere alle Attributen-Werte mit einer anderen beliebigen Taste");

        ConsoleKey consoleKey = Console.ReadKey().Key;

        if (consoleKey == ConsoleKey.Enter)
        {
            Program.SetAttribute = true;
        }
    }

    /// <summary>
    /// Sieger Auswahl Bestätigung.
    /// </summary>
    public static void AskConfirmWinner()
    {
        Console.WriteLine("Bestätige mit ENTER oder ändere den Sieger mit einer anderen beliebigen Taste");

        ConsoleKey consoleKey = Console.ReadKey().Key;

        if (consoleKey == ConsoleKey.Enter)
        {
            Program.SetWinner = true;
        }
    }
}