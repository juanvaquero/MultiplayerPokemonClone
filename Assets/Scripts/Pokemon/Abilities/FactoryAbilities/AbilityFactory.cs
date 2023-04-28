
using System.Collections.Generic;

public class AbilityFactory
{
    private Dictionary<string, IAbilityFactory> factories = new Dictionary<string, IAbilityFactory>();

    public AbilityFactory()
    {
        RegisterFactory("Attack", new ModifyOpponentAttackFactory());
        RegisterFactory("Defense", new ModifyOpponentDefenseFactory());
        RegisterFactory("Attack", new ModifyPlayerAttackFactory());
        RegisterFactory("Defense", new ModifyPlayerDefenseFactory());
    }


    public void RegisterFactory(string type, IAbilityFactory factory)
    {
        factories[type] = factory;
    }

    public Ability CreateAbility(PokemonDataAbility pokemonDataAbility)
    {
        IAbilityFactory factory;
        if (factories.TryGetValue(pokemonDataAbility.AbilityType, out factory))
        {
            return factory.CreateAbility(pokemonDataAbility);
        }
        else
        {
            throw new System.ArgumentException("Unknown ability type: " + pokemonDataAbility.AbilityType);
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


