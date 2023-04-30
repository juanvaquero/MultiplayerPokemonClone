using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private float _delayOfOpponentAction = 1f;

    [SerializeField]
    private float _delayBettwenDialogs = 2f;

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

    private void Update()
    {

    }

    private IEnumerator SpawnPlayerPokemon(Pokemon pokemon)
    {
        _playerUnit.LoadCombatUnit(pokemon, true);
        yield return ShowDialogWithDelay("Player choose " + pokemon.Name + ",what shall we do?");

        _uiCombatController.LoadPokemonMovements(_playerUnit.Pokemon, this);
        yield return WaitToShowMovements();
    }

    private IEnumerator SpawnOpponentPokemon(Pokemon pokemon, bool isWildPokemon = true)
    {
        _opponentUnit.LoadCombatUnit(pokemon, false);
        if (isWildPokemon)
            yield return ShowDialogWithDelay("A wild" + pokemon.Name + " appeared in the grass.");
        else
            yield return ShowDialogWithDelay("Opponet choose " + pokemon.Name + ".");

    }

    public IEnumerator StartWildEncounter(Pokemon wildPokemon)
    {
        gameObject.SetActive(true);

        _playerPokemons = GameManager.Instance.PlayerController.GetPokemonInventory();
        _opponentPokemons = new PokemonInventory();
        _opponentPokemons.AddPokemon(wildPokemon);

        PokemonInventory pokemonInventory = GameManager.Instance.PlayerController.GetPokemonInventory();
        //Spawn first player pokemon
        Pokemon playerPokemon = pokemonInventory.GetFirstReadyPokemon();

        yield return SpawnOpponentPokemon(wildPokemon);
        yield return SpawnPlayerPokemon(playerPokemon);
    }

    private IEnumerator ShowDialogWithDelay(string text)
    {
        _uiCombatController.SetEnableGeneralButtons(false);
        yield return _uiCombatController.TypeCombatDialog(text);
        yield return new WaitForSeconds(_delayBettwenDialogs);
    }

    private IEnumerator MovementDialog(Pokemon pokemon, Movement movement, bool isPlayer)
    {
        _uiCombatController.SetEnableGeneralButtons(false);

        if (isPlayer)
            yield return _uiCombatController.TypeCombatDialog("Player's " + pokemon.Name + " do " + movement.Name, null);
        else
            yield return _uiCombatController.TypeCombatDialog("Opponent's " + pokemon.Name + " do " + movement.Name, null);

        yield return new WaitForSeconds(_delayBettwenDialogs);
    }

    private IEnumerator WaitToShowMovements()
    {
        yield return new WaitForSeconds(0.5f);
        _uiCombatController.SetEnableGeneralButtons(true);
    }

    public IEnumerator EndCombat(Pokemon winner = null)
    {
        // Display victory message and end combat
        if (winner != null)
            yield return _uiCombatController.TypeCombatDialog(winner.Name + " wins the battle!");

        yield return new WaitForSeconds(_delayBettwenDialogs);
        OnCombatEnd.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator CheckPokemonFainted(bool opponentFainted, PokemonInventory pokemonsInCombat, Pokemon pokemonFainted, Pokemon winner)
    {
        // Check if the defender has fainted
        if (opponentFainted)
        {
            yield return ShowDialogWithDelay(pokemonFainted.Name + " is fainted");

            Pokemon nextPokemon = pokemonsInCombat.GetFirstReadyPokemon();
            //If is a player, check if he have more pokemons
            if (nextPokemon == null)
            {
                // If the defender has fainted, end the combat
                yield return EndCombat(winner);
            }
            else
            {
                // If the defender has more pokemons change of turn and spawn a new pokemon.
                if (playerTurn)
                    SpawnOpponentPokemon(nextPokemon, false);
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
        StartCoroutine(CoroutineDoPlayerMovement(movement));
    }

    private IEnumerator CoroutineDoPlayerMovement(CombatAction movement)
    {
        yield return MovementDialog(_playerUnit.Pokemon, (Movement)movement, true);

        bool opponentFainted = movement.Execute(_playerUnit.Pokemon, _opponentUnit.Pokemon);

        _opponentUnit.HealthChanged.Invoke(_opponentUnit.Pokemon.CurrentHealth);

        yield return CheckPokemonFainted(opponentFainted, _opponentPokemons, _opponentUnit.Pokemon, _playerUnit.Pokemon);
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
        StartCoroutine(CoroutineDoOpponentMovement(movement));
    }

    private IEnumerator CoroutineDoOpponentMovement(CombatAction movement)
    {
        yield return MovementDialog(_opponentUnit.Pokemon, (Movement)movement, false);

        bool opponentFainted = movement.Execute(_opponentUnit.Pokemon, _playerUnit.Pokemon);

        _playerUnit.HealthChanged.Invoke(_playerUnit.Pokemon.CurrentHealth);

        yield return CheckPokemonFainted(opponentFainted, _playerPokemons, _playerUnit.Pokemon, _opponentUnit.Pokemon);

        yield return WaitToShowMovements();
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
