
public class ModifyPlayerDefense : Ability
{
    public ModifyPlayerDefense(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override bool Execute(Pokemon attacker, Pokemon otherPokemon)
    {
        attacker.Defense += _amountStatAffected;

        return base.Execute(attacker, otherPokemon);
    }
}