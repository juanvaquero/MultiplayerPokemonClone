using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
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
            // Player's turn logic goes here
        }
        else
        {
            // Enemy's turn logic goes here
        }
    }

    void SpawnPlayerPokemon(Pokemon pokemon)
    {
        _playerUnit.LoadCombatUnit(pokemon, true);
    }

    void SpawnOpponentPokemon(Pokemon pokemon)
    {
        _opponentUnit.LoadCombatUnit(pokemon, false);
    }

    public void StartWildEncounter(Pokemon wildPokemon)
    {
        SpawnOpponentPokemon(wildPokemon);

        PokemonInventory pokemonInventory = GameManager.Instance.PlayerController.GetPokemonInventory();
        //Spawn first player pokemon
        SpawnPlayerPokemon(pokemonInventory.GetFirstReadyPokemon());

        _playerUnit.gameObject.SetActive(true);
        _opponentUnit.gameObject.SetActive(true);

        _uiCombatController.Initialize(_playerUnit.Pokemon, _opponentUnit.Pokemon, this);

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

    #region Player movement and abilities

    public void DoPlayerMovement(CombatAction movement)
    {
        bool opponentFainted = movement.Execute(_playerUnit.Pokemon, _opponentUnit.Pokemon);

        _opponentUnit.HealthChanged.Invoke(_opponentUnit.Pokemon.CurrentHealth);

        // Check if the defender has fainted
        if (opponentFainted)
        {
            // If the defender has fainted, end the combat
            //TODO Check it the opponent have more pokemons
            EndCombat(_playerUnit.Pokemon);
        }
        else
        {
            // If the defender has not fainted, it becomes the other player's turn
            playerTurn = !playerTurn;
        }
    }

    public void DoPlayerAbility(CombatAction ability)
    {
        Debug.LogError("Execute " + ability.Name);
        // ability.Execute(_playerUnit.Pokemon, _opponentUnit.Pokemon);

        // playerTurn = !playerTurn;
        //TODO check if the ability is unlocked fot enable the button
    }

    #endregion

    #region Opponent movement and abilities

    #endregion

}
