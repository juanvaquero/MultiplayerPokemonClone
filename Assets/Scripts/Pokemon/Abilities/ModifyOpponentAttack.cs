
public class ModifyOpponentAttack : Ability
{
    public ModifyOpponentAttack(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override void ExecuteAbility(Pokemon playerPokemon, Pokemon otherPokemon)
    {
        base.ExecuteAbility(playerPokemon, otherPokemon);

        otherPokemon.Attack += _amountStatAffected;
    }
}