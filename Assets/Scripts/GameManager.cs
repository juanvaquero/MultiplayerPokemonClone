using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Current GameManager Instance.
    /// </summary>
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                // DontDestroyOnLoad(_instance);
            }
            return _instance;
        }

    }

    [SerializeField]
    private PokemonSpawner _pokemonSpawner;
    public PokemonSpawner PokemonSpawner
    {
        get { return _pokemonSpawner; }
    }

    [SerializeField]
    private CombatManager _combatManager;
    public CombatManager CombatManager
    {
        get { return _combatManager; }
    }


    [SerializeField]
    private UIController _uiController;
    public UIController UiController
    {
        get { return _uiController; }
    }
    private Dictionary<int, PlayerController> _players;

    public Dictionary<int, PlayerController> Players
    {
        get
        {
            if (_players == null)
            {
                _players = new Dictionary<int, PlayerController>();
            }
            return _players;
        }
    }

    public int GetPlayerSeed(string playerName)
    {
        SHA256 sha256 = SHA256.Create(); // Create an instance of the SHA256 class
        byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(playerName); // Convert the room name into a byte array
        byte[] hashBytes = sha256.ComputeHash(inputBytes); // Compute the hash of the input
        int seed = System.BitConverter.ToInt32(hashBytes, 0); // Convert the first 4 bytes of the hash into a 32-bit integer
        return seed;
    }


}
