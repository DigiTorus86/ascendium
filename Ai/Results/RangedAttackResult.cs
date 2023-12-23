using Ascendium.Components;
using Ascendium.Types;

namespace Ascendium.Ai.Results;

public class RangedAttackResult : AttackResult
{
    private Item _weapon;

    public RangedAttackResult(Character attacker, Character target, Item weapon)
    : base(attacker, target, AttackType.Ranged)
    {
        _weapon = weapon;

        Symbol = @"-\|/";
        ForegroundColor = ConsoleColor.DarkYellow;
        AnimationDelay = TimeSpan.FromMilliseconds(100);

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
            Messages.Add($"{Attacker.Name} prepares to shoot.");
            return;
        }

        int damage = CalculateDamage();
        _weapon.CooldownRemaining = _weapon.CooldownRequired;
        ShouldDraw = true;

        if (damage > 0)
        {
            Messages.Add($"{Attacker.Name} shoots {Target.Name} with {_weapon.Name.ToLower()} for {damage} damage.");
            Symbol += "XXX";

            Target.Damage(damage);
            CheckTargetDeath();
        }
        else
        {
            Messages.Add($"{Attacker.Name} shoots and misses.");
        }
    }

    public override void Draw()
    {
        if (ShouldDraw == false || Symbol.Length < 1)
        {
            return;
        }

        Console.ForegroundColor = ForegroundColor;
        string symbol = Symbol.Substring(0, 1);

        // Use Bresenham's line algorithm to draw projectile animation along the line between the characters
        int x1 = Attacker.Left;
        int y1 = Attacker.Top;
        int x2 = Target.Left;
        int y2 = Target.Top;

        int w = x2 - x1;
        int h = y2 - y1;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1 ; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1 ; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1 ; else if (w > 0) dx2 = 1;
        
        int longest = Math.Abs(w);
        int shortest = Math.Abs(h);
        if (!(longest > shortest)) 
        {
            longest = Math.Abs(h) ;
            shortest = Math.Abs(w) ;
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;            
        }

        bool first = true;
        int charIndex = 0;
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++) 
        {
            if (!first)
            {
                Console.SetCursorPosition(x1, y1);
                Console.Write(symbol);
                Thread.Sleep(AnimationDelay);

                charIndex = (charIndex < Symbol.Length - 1) ? charIndex + 1 : 0;
                symbol = Symbol.Substring(charIndex, 1);
                
                Console.SetCursorPosition(x1, y1);
                Console.Write(" ");
            }

            first = false;
            numerator += shortest ;
            if (!(numerator < longest)) 
            {
                numerator -= longest;
                x1 += dx1;
                y1 += dy1;
            } 
            else 
            {
                x1 += dx2;
                y1 += dy2;
            }
        }

        Target.Draw();
    }
}