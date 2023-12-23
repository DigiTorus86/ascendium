namespace Ascendium.Types;

public class Spell : IDescribed
{
    public SpellType SpellType { get; private set; } = SpellType.None;

    public EffectCategoryType EffectCategory { get; set; } = EffectCategoryType.Other;

    public EffectType EffectType { get; set; } = EffectType.Other;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int ManaCost { get; set; }

    public double Range { get; set; }

    public int CooldownRequired { get; set; } = 1;

    public int CooldownRemaining { get; set; } = 0;

    public int MinEffect { get; set; }

    public int MaxEffect { get; set; }

    public Spell(SpellType spellType)
    {
        SpellType = spellType;
    }
}