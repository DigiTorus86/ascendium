using System.ComponentModel;
using Ascendium.Components;
using Ascendium.Types;
using Ascendium.Types.Factories;
using Ascendium.Ui;

namespace Ascendium.Ai.Results;

public class AttackResult : BaseResult
{
    public Character Attacker { get; private set; }

    public Character Target { get; private set; }

    public AttackType AttackType { get; private set; }

    public int MinDamage { get; set; }

    public int MaxDamage { get; set; }

    public AttackResult(Character attacker, Character target, AttackType attackType)
    {
        Attacker = attacker;
        Target = target;
        AttackType = attackType;
    }

    public override void DetermineResult()
    {
        if (Attacker is null || Target is null)
        {
            return;
        }
        
        int damage = CalculateDamage();

        if (damage > 0)
        {
            Messages.Add($"{Attacker.Name} hits {Target.Name} for {damage} damage.");
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
        if (ShouldDraw == false || Symbol.Length < 1)
        {
            return;
        }

        Console.ForegroundColor = ForegroundColor;

        foreach(char ch in Symbol)
        {
            Console.SetCursorPosition(Target.Left, Target.Top);
            Console.Write(ch);
            Thread.Sleep(AnimationDelay);
        }

        Target.Draw();
    }

    public virtual int CalculateDamage()
    {
        Random rnd = new Random();

        float damageModifier = (float)Attacker.Attack / (float)(Target.Defense + 1);

        if (Attacker.Debug) Messages.Add($"DEBUG: Atk {Attacker.Attack} Def {Target.Defense} Dmg mod {damageModifier}");

        // TODO: handle misses, spells, etc.

        float damage = rnd.Next(MinDamage, MaxDamage) * damageModifier;
        return (int)damage;
    }

    public virtual void CheckTargetDeath()
    {
        if (Target.Health > 0)
        {
            return;
        }

        Target.Conditions.Clear();
        Target.Conditions.Add(ConditionFactory.GetCondition(ConditionType.Dead));
        Target.Symbol = "_";

        Messages.Add($"{Target.Name} has been killed.");

        if (Target is Player)
        {
            // Game over
            Messages.Clear();
            Messages.Add("YOU HAVE DIED!");
            ShowModally = true;
            return;
        }

        if (Target.Gold > 0)
        {
            Attacker.Gold += Target.Gold;
            Messages.Add($"{Attacker.Name} gains {Target.Gold} gold.");
            Target.Gold = 0;
        }

        if (Target.Favor > 0)
        {
            Attacker.Favor += Target.Favor;
            Messages.Add($"{Attacker.Name} gains {Target.Favor} favor.");
            Target.Favor = 0;
        }

        Messages.Add($"{Attacker.Name} gains {Target.Experience} experience.");

        int levelsGained = Attacker.GainExperience(Target.Experience);
        for (int i = 0; i < levelsGained; i++)
        {
            Messages.Add($"{Attacker.Name} gains a level!");
        }

        if (levelsGained > 0)
        {
            Messages.Add($"{Attacker.Name} is now level {Attacker.Level}.");
            ShowModally = true;
        }

        List<Item> items = Target.GetItemDrop(Attacker);
        foreach(Item item in items)
        {
            Messages.Add($"Found {item.Name}"); // could use item Description here?
            Attacker.Items.Add(item);
            // Could scatter these on the ground instead?
        }
    }
}