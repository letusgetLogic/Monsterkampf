namespace Monsterkampf_Simulator;

class Troll : MonsterBase
{
    public readonly static float DefaultHP = 9200f;
    public readonly static float DefaultAP = 400f;
    public readonly static float DefaultDP = 90f;
    public readonly static float DefaultSP = 5f;

    private readonly float _hpPercent = 35f; // Unter (_hpPercent + 1) % maximalen HP wird Wutanfall aktiviert.
    private readonly float _apPercent = 200f; // Beim Wutanfall werden BasicAP um _apPercent % maximalen AP erhöht.
    private readonly float _healPercent = 2f; // Bei der Heilung bekommt Troll _healPercent % maximalen HP.

    private float _leftHP; // Übrige Lebenspunkte von Troll.
    private float _leftHP2; // Übrige Lebenspunkte vom Gegner.
    private float _currentAP; // Wert fürs Speichern um AP am Ende des Zuges wieder zu implementieren, z.B. wenn Goblin ausweicht und AP = 0.

    /// <summary>
    /// Konstruktor für Troll.
    /// </summary>
    /// <param name="race"></param>
    /// <param name="id"></param>
    /// <param name="hp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    /// <param name="speed"></param>
    /// <param name="combatClass"></param>
    public Troll(string race, float hp, float ap, float dp, float speed, float combatClass)
        : base(race, hp, ap, dp, speed, combatClass)
    { }

    public override void ShowSkill()
    {
        Console.WriteLine($"- Fähigkeit: Wutanfall - Unter {_hpPercent + 1}% Basic HP von selbst oder vom Gegner steigt AP um {_apPercent}% Basic AP. " +
                          $"Heilung verfällt.");
        Console.WriteLine($"- Fähigkeit: Heilung - Zun Beginn seines Zuges heilt der Troll um {_healPercent}% maximalen HP.");
        Console.WriteLine();
    }

    public override void ActivateSkill(MonsterBase defender)
    {
        // - Fähigkeit: Heilung - Zun Beginn seines Zuges heilt der Troll um {healPcent}% maximalen HP.

        _leftHP = BasicHP * _hpPercent * 0.01f;
        _leftHP2 = defender.BasicHP * _hpPercent * 0.01f;
        
        if (!(HP <= _leftHP || defender.HP <= _leftHP2))
        {
            int healHP = (int)(BasicHP * _healPercent * 0.01f);

            HP += healHP;

            AP = BasicAP;

            Console.Write("- Heilung "); Color.Green(); Console.WriteLine($"+ {healHP} HP"); Color.White();
        }
        else // - Fähigkeit: Wutanfall - Unter {ragePcent + 1}% Basic HP von selbst oder Gegner steigt AP um 2-fach Basic AP. Heilung verfällt.
        {
            AP = BasicAP + BasicAP * _apPercent * 0.01f;

            Console.Write("- Wutanfall "); Color.Red(); Console.WriteLine($"+ {_apPercent}% Basic AP"); Color.White();
        }
        _currentAP = AP;
    }

    public override void SetDefaultValue()
    {
        AP = _currentAP;
        DP = BasicDP;
    }
}