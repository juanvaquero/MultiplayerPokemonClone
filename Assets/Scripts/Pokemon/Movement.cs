using UnityEngine;

public class Movement : CombatAction
{
    public float Power { get; set; }
    public float Accuracy { get; }

    public Movement(PokemonDataMovement pokemonDataMovement)
    {
        Name = pokemonDataMovement.Name;
        Power = pokemonDataMovement.Power;
        Accuracy = pokemonDataMovement.Accuracy;
    }

    /// <summary>
    /// Use a move from the attacker to the defender, based on the stats of the pokemons
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns>True: Defender defeated. False: Defender alive.</returns>
    public override bool Execute(Pokemon attacker, Pokemon defender)
    {
        float attackNormalized = attacker.Attack / 255f;
        // Calculate the damage the attacker's move will do to the defender
        float damage = attackNormalized * Power - ((float)attacker.Attack / defender.Defense) + 2;

        // Apply the damage to the defender's health
        defender.CurrentHealth -= Mathf.FloorToInt(damage);

        // Check if the defender has fainted
        if (defender.CurrentHealth <= 0)
        {
            defender.CurrentHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}