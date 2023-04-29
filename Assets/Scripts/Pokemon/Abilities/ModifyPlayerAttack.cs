public class ModifyPlayerAttack : Ability
{
    public ModifyPlayerAttack(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override bool Execute(Pokemon attacker, Pokemon otherPokemon)
    {
        attacker.Attack += _amountStatAffected;

        return base.Execute(attacker, otherPokemon);
    }
}