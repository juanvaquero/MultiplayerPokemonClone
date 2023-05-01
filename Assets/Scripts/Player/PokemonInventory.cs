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
        Random.InitState(GetPlayerSeed());
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

        ShowPokemonsPlayer();
    }

    private int GetPlayerSeed()
    {
        string playerName = GetComponent<PlayerController>().PlayerName;
        SHA256 sha256 = SHA256.Create(); // Create an instance of the SHA256 class
        byte[] inputBytes = Encoding.UTF8.GetBytes(playerName); // Convert the room name into a byte array
        byte[] hashBytes = sha256.ComputeHash(inputBytes); // Compute the hash of the input
        int seed = System.BitConverter.ToInt32(hashBytes, 0); // Convert the first 4 bytes of the hash into a 32-bit integer
        Debug.LogError(seed + " seed");
        return seed;
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
