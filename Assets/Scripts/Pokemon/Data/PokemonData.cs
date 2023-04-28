using UnityEngine;

[CreateAssetMenu(fileName = "PokemonData", menuName = "Pokemon/PokemonData", order = 0)]
public class PokemonData : ScriptableObject
{
    public string Name;

    public int MaxHealth;
    public int Attack;
    public float Defense;

    public Sprite sprite;
    //For fix some creatures sprites start direction
    public bool spriteFlipped = false;

    //Collection of movements and Abilities
    public PokemonDataMovement[] Movements;
    public PokemonDataAbility[] Abilities;
}