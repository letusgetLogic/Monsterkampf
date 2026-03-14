namespace Monsterkampf_Simulator;

abstract class MonsterBase
{
    public string Race;
    public float HP;
    public float BasicHP;
    public float AP;
    public float BasicAP;
    public float DP;
    public float BasicDP;
    public float SP;
    public float BasicSP;
    public float CombatClass;
    public float[,] ValuesEachRound;
    public char[,] Diagram;

    /// <summary>
    /// Konstruktor, welches die Monster-Konstruktoren vererben lässt.
    /// </summary>
    /// <param name="race"></param>
    /// <param name="id"></param>
    /// <param name="hp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    /// <param name="speed"></param>
    /// <param name="combatClass"></param>
    public MonsterBase(string race, float hp, float ap, float dp, float speed, float combatClass)
    {
        Race = race;
        HP = hp;
        AP = ap;
        DP = dp;
        SP = speed;
        CombatClass = combatClass;
    }

    /// <summary>
    /// Zeigen Skills von dem jeweiligen Monster.
    /// </summary>
    public abstract void ShowSkill();

    /// <summary>
    /// Aktiviert die Fähigkeiten vorm Angriff.
    /// </summary>
    public virtual void ActivateSkill(MonsterBase defender)
    { }

    /// <summary>
    /// Angriff-Aktion.
    /// </summary>
    public virtual void Attack(MonsterBase defender)
    {
        Console.WriteLine($"{Race} greift an!");
    }

    /// <summary>
    /// Verteidigung-Aktion.
    /// </summary>
    public virtual void Defend(MonsterBase attacker) 
    { }

    /// <summary>
    /// Hinzufügen vom Schaden.
    /// </summary>
    public void TakeDamage(MonsterBase attacker)
    {
        float damage = attacker.AP - DP;

        damage = damage > 0 ? damage : 0;  // Kein negatives Schaden.

        HP -= damage;

        if (HP < 0) HP = 0;

        Console.WriteLine($"{attacker.Race} verursacht {(int)damage} Schaden! {Race} hat noch {(int)HP} HP.");
    }

    /// <summary>
    /// Aktiviert die Fähigkeiten nachm Angriff.
    /// </summary>
    public virtual void ActivateSkill2()
    { }
    
    /// <summary>
    /// Aktiviert die Fähigkeiten nachm Angriff nach jede Runde auch während Gegners Zuges.
    /// </summary>
    public static void ActivateSkill3(MonsterBase attacker, MonsterBase defender)
    {
        if (attacker.Race == "Ork" || defender.Race == "Ork") // Ork-Blutung-Fähigkeit soll 2 Runden wirken.
        {
            Ork.Bleeding(attacker, defender);
        }
    }

    /// <summary>
    /// Nach jede Runde werden benötigte Werte zurückgesetzt.
    /// </summary>
    public abstract void SetDefaultValue();
}