using System;
using UnityEngine;

[Serializable]
public class Pokemon
{
    private PokemonData _data;
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Attack;
    public int Defense;

    //Collection of movements and Abilities
    public Movement[] Movements;
    public Ability[] Abilities;


    public Sprite GetSprite()
    {
        return _data.sprite;
    }

    public Pokemon(PokemonData pokemonData)
    {
        _data = pokemonData;

        Name = pokemonData.Name;

        MaxHealth = pokemonData.MaxHealth;
        CurrentHealth = MaxHealth;
        Attack = pokemonData.Attack;
        Defense = pokemonData.Defense;

        Attack = pokemonData.Attack;
        Defense = pokemonData.Defense;

        //Build the movements and abilities
        Movements = CreateMovements(pokemonData.Movements);
        Abilities = CreateAbilities(pokemonData.Abilities);
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
            abilities[i] = AbilityFactory.Factory.CreateAbility(pokemonDataAbilities[i]);
        }

        return abilities;
    }


    public bool IsPokemonLife()
    {
        return CurrentHealth > 0;
    }

}