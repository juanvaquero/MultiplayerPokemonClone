using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PokemonSpawner : MonoBehaviour
{
    public const string PLAYER = "Player";

    // Probability of encountering a Pokemon
    public float _encounterProbability = 0.5f;

    // List of Pokemon that can be encountered in this area
    public List<GameObject> PokemonList;

    // Delay before starting the battle
    public float BattleDelay = 1.0f;

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
        // Roll the dice to determine if a Pokemon will be encountered
        if (CheckIsSpawnable(playerPosition) && Random.Range(0.0f, 1.0f) <= _encounterProbability)
        {
            Debug.LogError("Start the pokemon encounter");
            if (PokemonList.Count == 0)
                return;

            // Choose a random Pokemon from the list
            int index = Random.Range(0, PokemonList.Count);
            GameObject pokemonPrefab = PokemonList[index];

            // Spawn the Pokemon and start the battle
            GameObject pokemonObject = Instantiate(pokemonPrefab, transform.position, Quaternion.identity);
            StartCoroutine(StartBattle(pokemonObject));
        }
    }

    // Coroutine to start the battle after a delay
    private IEnumerator StartBattle(GameObject pokemon)
    {
        yield return new WaitForSeconds(BattleDelay);

        // Start the battle with the Pokemon
        //TODO BattleSystem.Instance.StartBattle(pokemon);
    }

}
