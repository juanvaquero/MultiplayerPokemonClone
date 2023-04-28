using UnityEngine;

public class Pokemon : MonoBehaviour
{

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _maxHealth;

    public int CurrentHealth;
    public int Attack;
    public float Defense;

    //Collection of movements and Abilities
    private Movement[] _movements;
    private Ability[] _abilities;
    private SpriteRenderer _spriteRenderer;
    private AbilityFactory _abilityFactory;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _abilityFactory = new AbilityFactory();
    }

    public void Initialize(PokemonData pokemonData, bool playerPokemon = true)
    {
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        // _abilityFactory = new AbilityFactory();
        _name = pokemonData.Name;
        gameObject.name = _name;

        _maxHealth = pokemonData.MaxHealth;
        CurrentHealth = _maxHealth;
        Attack = pokemonData.Attack;
        Defense = pokemonData.Defense;

        Attack = pokemonData.Attack;
        Defense = pokemonData.Defense;

        //Build the movements and abilities
        _movements = CreateMovements(pokemonData.Movements);
        _abilities = CreateAbilities(pokemonData.Abilities);

        _spriteRenderer.sprite = pokemonData.sprite;

        InvertSpriteDirection(playerPokemon);
    }

    private Movement[] CreateMovements(PokemonDataMovement[] pokemonDataMovements)
    {
        Movement[] movements = new Movement[pokemonDataMovements.Length];

        for (int i = 0; i < pokemonDataMovements.Length; i++)
        {
            movements[i] = new Movement(pokemonDataMovements[i]);
        }

        return movements;
    }

    private Ability[] CreateAbilities(PokemonDataAbility[] pokemonDataAbilities)
    {
        Ability[] abilities = new Ability[pokemonDataAbilities.Length];

        for (int i = 0; i < pokemonDataAbilities.Length; i++)
        {
            abilities[i] = _abilityFactory.CreateAbility(pokemonDataAbilities[i]);
        }

        return abilities;
    }

    public void InvertSpriteDirection(bool invert)
    {
        _spriteRenderer.flipX = invert;
    }

}