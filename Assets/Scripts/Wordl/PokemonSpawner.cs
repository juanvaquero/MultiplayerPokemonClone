using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PokemonSpawner : MonoBehaviour
{
    public const string PLAYER = "Player";

    // Probability of encountering a Pokemon
    [SerializeField]
    private float _encounterProbability = 0.5f;

    // List of Pokemon that can be encountered in this area
    [SerializeField]
    private List<PokemonData> _pokemonList;

    // Delay before starting the battle
    [SerializeField]
    private float _battleDelay = 0.5f;

    [SerializeField]
    private Tilemap _spawneableTileMap;
    [SerializeField]
    private LayerMask _spawneableLayerMask;

    private Vector3Int _currentTilePosition;

    private bool CheckIsSpawnable(Vector3 playerPosition)
    {
        Vector3Int tilePosition = _spawneableTileMap.WorldToCell(playerPosition);
        if (_currentTilePosition == tilePosition)
        {
            return false;
        }
        else
        {
            _currentTilePosition = tilePosition;
        }

        return Physics2D.OverlapCircle(playerPosition, 0.2f, _spawneableLayerMask) != null;
    }

    /// <summary>
    /// The encounter when the player enters the grass
    /// </summary>
    public void TryEncounterPokemon(Vector3 playerPosition)
    {
        bool allFainted = GameManager.Instance.PlayerController.GetPokemonInventory().AreAllPokemonFainted();
        if (allFainted)
            Debug.Log("All pokemons are fainted, try to recovery them in the store.");

        // Roll the dice to determine if a Pokemon will be encountered
        if (CheckIsSpawnable(playerPosition) && Random.Range(0.0f, 1.0f) <= _encounterProbability && !allFainted)
        {
            if (_pokemonList.Count == 0)
                return;

            // Choose a random Pokemon from the list
            int index = Random.Range(0, _pokemonList.Count);
            PokemonData pokemonData = _pokemonList[index];

            Pokemon pokemon = new Pokemon(pokemonData);
            StartCoroutine(StartBattle(pokemon));
        }
    }

    // Coroutine to start the battle after a delay
    private IEnumerator StartBattle(Pokemon pokemon)
    {
        CombatManager combatManager = GameManager.Instance.CombatManager;
        combatManager.OnCombatStart.Invoke();

        yield return new WaitForSeconds(_battleDelay);

        // Start the battle with the Pokemon
        yield return combatManager.StartWildEncounter(pokemon);
    }

}
