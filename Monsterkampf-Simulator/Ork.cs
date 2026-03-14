namespace Monsterkampf_Simulator;

class Ork : MonsterBase
{
    public readonly static float DefaultHP = 8000f;
    public readonly static float DefaultAP = 300f;
    public readonly static float DefaultDP = 80f;
    public readonly static float DefaultSP = 30f;

    private readonly static float BloodDamagePercent = 5f; // % verbleibenden HP.

    private float _lostHP;
    private float _percentageLostHP;
    private float _currentAP; // Wert fürs Speichern um AP am Ende des Zuges wieder zu implementieren, z.B. wenn Goblin ausweicht und AP = 0.
    private float _currentDP; // Wert fürs Speichern um DP am Ende des Zuges wieder zu implementieren, z.B. wenn Goblin trifft und DP = 0.

    /// <summary>
    /// Konstruktor für Ork.
    /// </summary>
    /// <param name="race"></param>
    /// <param name="id"></param>
    /// <param name="hp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    /// <param name="speed"></param>
    /// <param name="combatClass"></param>
    public Ork(string race, float hp, float ap, float dp, float speed, float combatClass)
        : base(race, hp, ap, dp, speed, combatClass)
    { }

    public override void ShowSkill()
    {
        // - Fähigkeit: Kampflust - Für x % verlorende HP zu Basic HP steigt AP um x % Basic AP und DP um x % Basic DP.

        Console.Write("- Fähigkeit: Kampflust - Für "); Color.Red();
        Console.Write("x"); Color.White();
        Console.Write(" % verlorende HP zu Basic HP steigt AP um "); Color.Red();
        Console.Write("x"); Color.White();
        Console.Write(" % Basic AP und DP um "); Color.Red();
        Console.Write("x"); Color.White();
        Console.WriteLine(" % Basic DP.");

        Console.WriteLine($"- Fähigkeit: Blutung - Blutung wirkt auf Gegner 2 Runden nach einen Treffer. " +
                          $"Nach jede Runde erleidet Gegner zusätzlich an {BloodDamagePercent}% verbleibenden HP.");
        Console.WriteLine();
    }

    public override void ActivateSkill(MonsterBase defender)
    {
        // - Fähigkeit: Kampflust - Für x % verlorende HP zu Basic HP steigt AP um x % Basic AP und DP um x % Basic DP.

        _lostHP = BasicHP - HP;
        _percentageLostHP = _lostHP / BasicHP;

        AP = BasicAP + BasicAP * _percentageLostHP;
        DP = BasicDP + BasicDP * _percentageLostHP;

        // Fähigkeiten in der Konsole anzeigen.
        Console.Write($"- Kampflust "); Color.Red(); Console.Write($"+ {Math.Round(_percentageLostHP * 100, 2)}% Basic AP"); Color.White();
        Console.Write("   /   "); Color.Blue();  Console.WriteLine($"+ {Math.Round(_percentageLostHP * 100, 2)}% Basic DP"); Color.White();

        _currentAP = AP;
        _currentDP = DP;
    }

    /// <summary>
    /// Orks Fähigkeit: Blutung.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    public static void Bleeding(MonsterBase attacker, MonsterBase defender)
    {
        MonsterBase ork = attacker;
        MonsterBase enemy = defender;

        if (attacker.Race == "Ork") // 2-Runden-Blutung-Effekt tritt beim Hitten ein.
        {
            Program.CountRoundDown = 2;

            if (defender.Race == "Goblin") // Goblins Ausweichen.
            {
                if (Goblin.HasDodged) Program.CountRoundDown = 0;
            }
        }

        if (defender.Race == "Ork")
        {
            ork = defender;
            enemy = attacker;
        }

        if (Program.CountRoundDown > 0)
        {
            BleedingAfterEffect(ork, enemy); // Effekt tritt nach 2 Runden nicht mehr ein.

            Program.CountRoundDown--;
        }
    }

    /// <summary>
    /// Blutungs Nachwirkung.
    /// </summary>
    static void BleedingAfterEffect(MonsterBase ork, MonsterBase enemy)
    {
        // - Fähigkeit: Blutung - Blutung wirkt auf Gegner 2 Runden nach einen Treffer. Nach jede Runde erleidet Gegner zusätzlich an {bloodDmgPcent}% verbleibenden HP.

        float bloodDmg = enemy.HP * Ork.BloodDamagePercent * 0.01f;

        enemy.HP -= bloodDmg;

        if (enemy.HP < 0) enemy.HP = 0;

        Console.Write($"- Blutung: {enemy.Race} erleidet an ");
        Color.Red();
        Console.Write((int)bloodDmg);
        Color.White();
        Console.WriteLine($" Schaden. {enemy.Race} hat noch {(int)enemy.HP} HP.");
    }

    public override void SetDefaultValue()
    {
        AP = _currentAP;
        DP = _currentDP;
    }
}