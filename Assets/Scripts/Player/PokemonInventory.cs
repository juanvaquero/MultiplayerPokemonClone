using System.Collections.Generic;
using UnityEngine;

public class PokemonInventory : MonoBehaviour
{
    public const int MAX_POKEMON = 3;

    [SerializeField]
    private List<Pokemon> _pokemons = new List<Pokemon>();

    public List<Pokemon> GetPokemons()
    {
        return _pokemons;
    }

    private void Start()
    {
        GenerateRandomPokemons();
    }

    public void GenerateRandomPokemons()
    {
        // Generate a new inventory of Pokémon every time the game is executed
        for (int i = 0; i < MAX_POKEMON; i++)
        {
            // Instantiate the corresponding Pokémon prefab and add it to the list
            PokemonData pokemonData = Resources.Load<PokemonData>("Pokemons/" + Random.Range(1, MAX_POKEMON).ToString());
            if (pokemonData != null)
            {
                Pokemon pokemon = new Pokemon(pokemonData);
                _pokemons.Add(pokemon);
            }
        }
    }

    public Pokemon GetFirstReadyPokemon()
    {
        foreach (Pokemon p in _pokemons)
        {
            if (p.IsPokemonLife())
                return p;
        }
        return null;
    }

    public bool AreAllPokemonFainted()
    {
        return GetFirstReadyPokemon() == null;
    }

    public void AddPokemon(Pokemon pokemon)
    {
        if (_pokemons.Count < MAX_POKEMON)
            _pokemons.Add(pokemon);
    }

}
