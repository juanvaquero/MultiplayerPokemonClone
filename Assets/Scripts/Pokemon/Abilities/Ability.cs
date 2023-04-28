using UnityEngine;

public class Ability
{
    public string Name { get; }
    public string Description { get; }

    protected string _abilityType;
    protected int _amountStatAffected;

    protected int _abilityCooldown;
    protected int _currentAbilityCooldown;

    public Ability(PokemonDataAbility pokemonDataAbility)
    {
        Name = pokemonDataAbility.Name;
        Description = pokemonDataAbility.Description;
        _abilityType = pokemonDataAbility.AbilityType;
        _amountStatAffected = pokemonDataAbility.AmountStatAffected;
        _abilityCooldown = pokemonDataAbility.AbilityCooldown;
        _currentAbilityCooldown = 0;
    }

    public virtual void ExecuteAbility(Pokemon playerPokemon, Pokemon otherPokemon)
    {
        CheckAndUpdateCooldown();
    }

    protected bool CheckAndUpdateCooldown()
    {
        if (_currentAbilityCooldown == 0)
        {
            _currentAbilityCooldown = _abilityCooldown;
            return false;
        }
        _currentAbilityCooldown--;
        return true;
    }
}