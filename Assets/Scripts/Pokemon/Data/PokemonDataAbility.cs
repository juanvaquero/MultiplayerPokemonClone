using UnityEngine;

[CreateAssetMenu(fileName = "PokemonDataAbility", menuName = "Pokemon/PokemonDataAbility", order = 2)]
public class PokemonDataAbility : ScriptableObject
{
    public string Name;
    public string Description = "Default description ability";

    [Tooltip("Abilities types defined in Abilities Types script")]
    public string AbilityType;
    public int AmountStatAffected = 10;
    public int AbilityCooldown = 2;
}