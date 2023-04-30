using System.Collections;
using System.Collections.Generic;
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
                DontDestroyOnLoad(_instance);
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


}
