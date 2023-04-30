using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private float _delayOfOpponentAction = 1f;

    [SerializeField]
    private CombatUnit _playerUnit; // Instance of the player's unit
    public CombatUnit PlayerUnit
    {
        get { return _playerUnit; }
    }

    [SerializeField]
    private CombatUnit _opponentUnit; // Instance of the enemy's unit
    public CombatUnit OpponentUnit
    {
        get { return _opponentUnit; }
    }

    private PokemonInventory _playerPokemons;
    private PokemonInventory _opponentPokemons;

    [SerializeField]
    private UICombatController _uiCombatController; // Instance of the UI Combat Controller

    private bool playerTurn = true; // Flag to indicate whether it's the player's turn

    public UnityAction OnCombatStart { get; set; }
    public UnityAction OnCombatEnd { get; set; }

    public UnityAction<CombatAction> MovementExecuted;
    public UnityAction<CombatAction> AbilityExecuted;

    private void Awake()
    {
        gameObject.SetActive(false);

        MovementExecuted += DoPlayerMovement;
        AbilityExecuted += DoPlayerAbility;
    }

    void Update()
    {
        if (playerTurn)
        {
            Debug.LogError("Player turn");
            // Player's turn logic goes here
        }
        else
        {
            Debug.LogError("Opponent turn");
            // Enemy's turn logic goes here
        }
    }

    void SpawnPlayerPokemon(Pokemon pokemon)
    {
        Debug.LogError("SpawnPlayerPokemon -> " + pokemon.Name);
        _playerUnit.LoadCombatUnit(pokemon, true);

        _uiCombatController.Initialize(_playerUnit.Pokemon, this);
    }

    void SpawnOpponentPokemon(Pokemon pokemon)
    {
        Debug.LogError("SpawnOpponentPokemon -> " + pokemon.Name);
        _opponentUnit.LoadCombatUnit(pokemon, false);
    }

    public void StartWildEncounter(Pokemon wildPokemon)
    {
        _playerPokemons = GameManager.Instance.PlayerController.GetPokemonInventory();
        _opponentPokemons = new PokemonInventory();
        _opponentPokemons.AddPokemon(wildPokemon);

        SpawnOpponentPokemon(wildPokemon);

        PokemonInventory pokemonInventory = GameManager.Instance.PlayerController.GetPokemonInventory();
        //Spawn first player pokemon
        SpawnPlayerPokemon(pokemonInventory.GetFirstReadyPokemon());

        gameObject.SetActive(true);
    }

    public void EndCombat(Pokemon winner = null)
    {
        // Display victory message and end combat
        if (winner != null)
            Debug.LogError(winner.Name + " wins the battle!");

        OnCombatEnd.Invoke();
        gameObject.SetActive(false);
    }

    private void CheckPokemonFainted(bool opponentFainted, PokemonInventory pokemonsInCombat, Pokemon winner)
    {
        // Check if the defender has fainted
        if (opponentFainted)
        {
            Pokemon nextPokemon = pokemonsInCombat.GetFirstReadyPokemon();
            //If is a player, check if he have more pokemons
            if (nextPokemon == null)
            {
                // If the defender has fainted, end the combat
                EndCombat(winner);
            }
            else
            {
                // If the defender has more pokemons change of turn and spawn a new pokemon.
                if (playerTurn)
                    SpawnOpponentPokemon(nextPokemon);
                else
                    SpawnPlayerPokemon(nextPokemon);

                playerTurn = !playerTurn;
            }
        }
        else
        {

            if (playerTurn)
                // If the defender has not fainted, it becomes the other player's turn
                StartCoroutine(CoroutineOpponentAction());
            playerTurn = !playerTurn;
        }
    }

    #region Player movement and abilities

    public void DoPlayerMovement(CombatAction movement)
    {
        bool opponentFainted = movement.Execute(_playerUnit.Pokemon, _opponentUnit.Pokemon);

        _opponentUnit.HealthChanged.Invoke(_opponentUnit.Pokemon.CurrentHealth);

        CheckPokemonFainted(opponentFainted, _opponentPokemons, _playerUnit.Pokemon);
    }

    public void DoPlayerAbility(CombatAction ability)
    {
        Debug.LogError("Execute " + ability.Name);
        // ability.Execute(_playerUnit.Pokemon, _opponentUnit.Pokemon);

        // //TODO check if the ability is unlocked fot enable the button
        playerTurn = !playerTurn;
    }

    #endregion

    #region Opponent movement and abilities

    public void DoOpponentMovement(CombatAction movement)
    {
        bool playerFainted = movement.Execute(_opponentUnit.Pokemon, _playerUnit.Pokemon);

        _playerUnit.HealthChanged.Invoke(_playerUnit.Pokemon.CurrentHealth);

        CheckPokemonFainted(playerFainted, _playerPokemons, _opponentUnit.Pokemon);
    }

    public void DoOpponentAbility(CombatAction ability)
    {
        Debug.LogError("Execute " + ability.Name);
        // ability.Execute(_opponentUnit.Pokemon, _playerUnit.Pokemon);

        // //TODO check if the ability is unlocked fot enable the button
        playerTurn = !playerTurn;
    }

    private void DoRandomOpponentAction()
    {
        if (Random.Range(0, 1f) < 1f)
            DoOpponentMovement(_opponentUnit.Pokemon.Movements[Random.Range(0, _opponentUnit.Pokemon.Movements.Length)]);
        else
            DoOpponentAbility(_opponentUnit.Pokemon.Abilities[Random.Range(0, _opponentUnit.Pokemon.Abilities.Length)]);
    }

    public IEnumerator CoroutineOpponentAction()
    {
        yield return new WaitForSeconds(_delayOfOpponentAction);
        DoRandomOpponentAction();
    }

    #endregion

}
