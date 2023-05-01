using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Photon.Pun;
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
        string playerName = GetComponent<PlayerController>().PlayerName;
        Random.InitState(GameManager.Instance.GetPlayerSeed(playerName));

        // Generate a new inventory of Pokémon every time the game is executed
        for (int i = 0; i < MAX_POKEMON; i++)
        {
            // Instantiate the corresponding Pokémon prefab and add it to the list
            PokemonData pokemonData = Resources.Load<PokemonData>("Pokemons/" + Random.Range(1, MAX_POKEMON + 1).ToString());
            if (pokemonData != null)
            {
                Pokemon pokemon = new Pokemon(pokemonData);
                pokemon.Name += "-" + playerName;
                _pokemons.Add(pokemon);
            }
        }

        ShowPokemonsPlayer();
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

    public void ShowPokemonsPlayer()
    {
        string playerName = GetComponent<PlayerController>().PlayerName;

        for (int i = 0; i < _pokemons.Count; i++)
        {
            Debug.LogError(playerName + " -> " + i + "º pokemon : " + _pokemons[i].Name + " | " + _pokemons[i].CurrentHealth);
        }
        Debug.LogError("------");
    }

}
