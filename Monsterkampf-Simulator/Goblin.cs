namespace Monsterkampf_Simulator;

class Goblin : MonsterBase
{
    public readonly static float DefaultHP = 6666f;
    public readonly static float DefaultAP = 222f;
    public readonly static float DefaultDP = 66f;
    public readonly static float DefaultSP = 88f;

    public static int CountCrit;   // Zählt Kritische Treffer.
    public static int CountIgnore; // Zählt Schwachpunkt-Treffer.
    public static int CountDodge;  // Zählt Erfolgreiche Ausweichen-Aktionen.
    public static int CountRound;  // Zählt Goblins Runden.

    public static bool HasDodged; // Ob Goblin den Angriff ausweicht.

    private readonly float _chanceCrit = 50f; // Chance vom kritischen Treffer.
    private readonly float _chanceIgnore = 25f; // Chance vom Ignorieren DP.

    private readonly int _minMulti = 10; // Höhe des kritischen Schaden / zusätzlich min. Multiplikand des Basic HP. 
    private readonly int _maxMulti = 20; // Höhe des kritischen Schaden / zusätzlich max. Multiplikand des Basic HP.

    private readonly float _minChanceDodge = 0f; // Minimale Chance Ausweichen.
    private readonly float _maxChanceDodge = 65f; // Maximale Chance Ausweichen.
    private readonly float _extraSpeed = 5f; // Geschwindigkeit erhöht jede Runde.

    private float _critDmg; // Kritisches Schaden.
    private float _chanceDodge; // Chance Ausweichen.

    private static readonly Random Rnd = new();
    

    /// <summary>
    /// Konstruktor für Goblin.
    /// </summary>
    /// <param name="race"></param>
    /// <param name="id"></param>
    /// <param name="hp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    /// <param name="speed"></param>
    /// <param name="combatClass"></param>
    public Goblin(string race, float hp, float ap, float dp, float speed, float combatClass)
        : base(race, hp, ap, dp, speed, combatClass)
    { }

    public override void ShowSkill()
    {
        Console.WriteLine($"- Fähigkeit: Präzision - {_chanceIgnore}% Chance Ignorieren DP und " +
                          $"{_chanceCrit}% Chance Kritischer Treffer (+ {_minMulti * 0.1f} bis {_maxMulti * 0.1f}-fach Basic AP).");
        Console.WriteLine($"- Fähigkeit: Schnelligkeit - {_minChanceDodge}-{_maxChanceDodge}% Chance Ausweichen, je nach Speed Unterschied zum Gegner. " +
                          $"Nach jede Runde erhöht Speed um {_extraSpeed} SP.");
        Console.WriteLine();
    }

    public override void Attack(MonsterBase defender)
    {
        Console.WriteLine($"{Race} greift an!");

        // - Fähigkeit: Präzision - {chanceIgnore}% Chance Ignorieren DP und {chanceCrit}% Chance Kritischer Treffer (+ {minMulti * 0.1} bis {maxMulti * 0.1}-fach Basic AP).

        if (Rnd.Next(0, 101) <= _chanceIgnore)
        {
            Console.Write("- Schwachpunkt getroffen! - Ignorieren ");
            Color.Blue();
            Console.WriteLine($"{(int)defender.DP} DP");
            Color.White();

            defender.DP = 0;

            CountIgnore++;
        }

        if (Rnd.Next(0, 101) <= _chanceCrit)
        {
            _critDmg = BasicAP * Rnd.Next(_minMulti, _maxMulti + 1) * 0.1f;

            Console.Write($"- Kritischer Treffer! ");
            Color.Red();
            Console.WriteLine($"+ {(int)_critDmg} Schaden");
            Color.White();

            CountCrit++;
        }

        AP = BasicAP + _critDmg;
    }

    public override void Defend(MonsterBase attacker)
    {
        // - Fähigkeit: Schnelligkeit - {minChanceDodge} - {maxChanceDodge}% Chance Ausweichen, je nach Speed Unterschied zum Gegner.

        float distanceSpeed = SP - attacker.SP; // Geschwindigkeit Unterschied.
        float distanceChance;

        if (distanceSpeed > 0)
        {
            distanceChance = _maxChanceDodge - _minChanceDodge;
            _chanceDodge = _minChanceDodge + distanceSpeed * 0.01f * distanceChance;

            if (_chanceDodge > _maxChanceDodge) _chanceDodge = _maxChanceDodge; // Chance begrenzt auf maxChance.
        }
        else _chanceDodge = 0;

        HasDodged = false;

        if ((int)_chanceDodge >= Rnd.Next(1, 101))
        {
            HasDodged = true;
            CountDodge++;
        }

        Console.Write($"- Goblins Schnelligkeit: "); Color.Yellow();
        Console.WriteLine($"{Math.Round(_chanceDodge, 2)}% Chance Ausweichen"); Color.White();

        if (Goblin.HasDodged) { attacker.AP = 0; Console.WriteLine("Goblin hat ausgewichen!"); }
    }

    public override void ActivateSkill2()
    {
        SP = SP + _extraSpeed;
        Console.Write("- Schnelligkeit "); Color.Yellow(); Console.WriteLine($"+ {_extraSpeed} SP"); Color.White();

        CountRound++;
    }

    public override void SetDefaultValue()
    {
        AP = BasicAP;
        _critDmg = 0;
        HasDodged = false;
    }

    /// <summary>
    /// Die kritische Treffer, die erfolgreiche Ausweichen-Aktionen und die Runden des Goblins ausprinten.
    /// </summary>
    public static void PrintResult()
    {
        Console.WriteLine($"Goblin: {CountCrit} Kritische Treffer / {CountIgnore} Schwachpunkt-Treffer / {CountDodge} Ausweichen-Aktionen / {CountRound} Runden.");
        Console.WriteLine();
    }
}