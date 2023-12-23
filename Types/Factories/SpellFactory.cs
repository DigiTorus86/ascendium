namespace Ascendium.Types.Factories;

public static class SpellFactory
{
    public static Spell GetSpell(SpellType spellType)
    {
        var spell = new Spell(spellType);

        switch (spellType)
        {
            case SpellType.Heal:
                spell.Name = "Heal";
                spell.Description = "Restores up to 10 health";
                spell.EffectCategory = EffectCategoryType.Heal;
                spell.EffectType = EffectType.IncreaseHealth;
                spell.ManaCost = 2;
                spell.MinEffect = 10;
                spell.MaxEffect = 10;
                break;

            case SpellType.ManaDart:
                spell.Name = "Mana Dart";
                spell.Description = "Causes up to 10 damage";
                spell.EffectCategory = EffectCategoryType.Attack;
                spell.ManaCost = 2;
                spell.MinEffect = 7;
                spell.MaxEffect = 10;
                break;

            case SpellType.Purify:
                spell.Name = "Purify";
                spell.Description = "Cures poison and venom";
                spell.EffectCategory = EffectCategoryType.RemoveCondition;
                spell.EffectType = EffectType.RemoveCondition;
                spell.ManaCost = 4;
                break;

            case SpellType.StoneSkin:
                spell.Name = "Stone Skin";
                spell.Description = "Cures poison and venom";
                spell.EffectCategory = EffectCategoryType.Buff;
                spell.EffectType = EffectType.IncreaseDefense;
                spell.ManaCost = 3;
                break;

            default:
                spell = new Spell(SpellType.None);
                spell.Name = "Unknown";
                spell.Description = "an unknown spell";
                spell.EffectType = EffectType.Other;
                break;
        }

        return spell;
    }
}