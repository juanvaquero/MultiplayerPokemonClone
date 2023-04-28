using UnityEngine;

[CreateAssetMenu(fileName = "PokemonDataMovement", menuName = "Pokemon/PokemonDataMovement", order = 1)]
public class PokemonDataMovement : ScriptableObject
{
    public string Name;
    public int Power;
    public int Accuracy;
}