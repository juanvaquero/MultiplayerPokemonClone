
public class ModifyOpponentDefense : Ability
{
    public ModifyOpponentDefense(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override bool Execute(Pokemon attacker, Pokemon otherPokemon)
    {
        otherPokemon.Defense += _amountStatAffected;

        return base.Execute(attacker, otherPokemon);
    }
}