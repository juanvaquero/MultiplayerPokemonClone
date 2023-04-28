
using System.Collections.Generic;

public class AbilityFactory
{
    private Dictionary<string, IAbilityFactory> _factories;

    public AbilityFactory()
    {
        _factories = new Dictionary<string, IAbilityFactory>();

        RegisterFactory(AbilityTypes.MODIFY_OPPONENT_ATTACK, new ModifyOpponentAttackFactory());
        RegisterFactory(AbilityTypes.MODIFY_OPPONENT_DEFENSE, new ModifyOpponentDefenseFactory());
        RegisterFactory(AbilityTypes.MODIFY_PLAYER_ATTACK, new ModifyPlayerAttackFactory());
        RegisterFactory(AbilityTypes.MODIFY_PLAYER_DEFENSE, new ModifyPlayerDefenseFactory());
    }


    public void RegisterFactory(string type, IAbilityFactory factory)
    {
        _factories[type] = factory;
    }

    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        IAbilityFactory factory;
        if (_factories.TryGetValue(pokemonDataAbility.AbilityType, out factory))
        {
            return factory.CreateAbility(pokemonDataAbility);
        }
        else
        {
            throw new System.ArgumentException("Unknown ability type: " + pokemonDataAbility.AbilityType + ", please review the abilities type class.");
        }
    }
}

public class ModifyOpponentAttackFactory : IAbilityFactory
{
    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        return new ModifyOpponentAttack(pokemonDataAbility);
    }
}

public class ModifyOpponentDefenseFactory : IAbilityFactory
{
    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        return new ModifyOpponentDefense(pokemonDataAbility);
    }
}


public class ModifyPlayerAttackFactory : IAbilityFactory
{
    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        return new ModifyPlayerAttack(pokemonDataAbility);
    }
}

public class ModifyPlayerDefenseFactory : IAbilityFactory
{
    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        return new ModifyPlayerDefense(pokemonDataAbility);
    }
}


