using UnityEngine;

public class Movement
{
    public string Name { get; set; }
    public float Power { get; set; }
    public float Accuracy { get; }

    public Movement(PokemonDataMovement pokemonDataMovement)
    {
        Name = pokemonDataMovement.Name;
        Power = pokemonDataMovement.Power;
        Accuracy = pokemonDataMovement.Accuracy;
    }

    /// <summary>
    /// Use a move from the attacker to the defender, based on the stats of the pokemons.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns>True: Defender defeated. False: Defender alive.</returns>
    public bool ExecuteMovement(Pokemon attacker, Pokemon defender)
    {
        // Calculate the damage the attacker's move will do to the defender
        float damage = Power - defender.Defense;

        // Apply the damage to the defender's health
        defender.CurrentHealth -= Mathf.RoundToInt(damage);

        // Check if the defender has fainted
        if (defender.CurrentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}