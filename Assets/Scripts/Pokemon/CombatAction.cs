public abstract class CombatAction
{
    public string Name { get; set; }
    public string Description { get; set; }

    public abstract bool Execute(Pokemon attacker, Pokemon defender);

}