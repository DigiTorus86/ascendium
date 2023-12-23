using Ascendium.Types;

namespace Ascendium.Ai.Results;

public class MeleeAttackResult : AttackResult
{
    private Item _weapon;

    public MeleeAttackResult(Character attacker, Character target, Item weapon)
    : base(attacker, target, AttackType.Melee)
    {
        _weapon = weapon;

        Symbol = "X";
        ForegroundColor = ConsoleColor.Red;
        AnimationDelay = TimeSpan.FromMilliseconds(300);

        MinDamage = weapon.MinEffect;
        MaxDamage = weapon.MaxEffect;
    }

    public override void DetermineResult()
    {
        if (Attacker is null || Target is null || _weapon is null)
        {
            return;
        }

        if (_weapon.CooldownRemaining-- > 0)
        {
            Messages.Add($"{Attacker.Name} prepares to attack.");
            Symbol = Target.Symbol;
            return;
        }

        int damage = CalculateDamage();
        _weapon.CooldownRemaining = _weapon.CooldownRequired;

        if (damage > 0)
        {
            Messages.Add($"{Attacker.Name} hits {Target.Name} with {_weapon.Name.ToLower()} for {damage} damage.");
            ShouldDraw = true;
            Target.Damage(damage);
            CheckTargetDeath();
        }
        else
        {
            Messages.Add($"{Attacker.Name} attacks and misses.");
        }
    }

    public override void Draw()
    {
        base.Draw();
    }
}