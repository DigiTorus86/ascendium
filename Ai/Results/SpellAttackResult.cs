using Ascendium.Types;

namespace Ascendium.Ai.Results;

public class SpellAttackResult : AttackResult
{
    private Spell _spell;

    public SpellAttackResult(Character attacker, Character target, Spell spell)
    : base(attacker, target, AttackType.Spell)
    {
        _spell = spell;

        Symbol = "·•○";
        ForegroundColor = ConsoleColor.White;
        AnimationDelay = TimeSpan.FromMilliseconds(250);
    }

    public override void DetermineResult()
    {
        if (_spell.CooldownRemaining-- > 0)
        {
            Messages.Add($"{Attacker.Name} is casting a spell.");
            return;
        }

        Messages.Add($"{Attacker.Name} casts {_spell.Name} on {Target.Name}.");
        _spell.CooldownRemaining = _spell.CooldownRequired;

        // TODO: determine effect/damage 
    }
}