namespace Ascendium.Types;

public class Item : IDescribed
{
    public static readonly int UnlimitedUses = -1;

    public ItemType ItemType { get; private set; }

    public EffectType EffectType { get; set; } = EffectType.None;

    public ItemEquipType EquipType { get; set; } = ItemEquipType.NotEquipped;

    public string EquipSlot { get; set; } = string.Empty;

    public float Range { get; set; } = 0;

    public int CooldownRequired { get; set; } = 1;

    public int CooldownRemaining { get; set; } = 0;

    public int MinEffect { get; set; }

    public int MaxEffect { get; set; }

    public int UsesRemaining { get; set; } = UnlimitedUses;

    public int AttackBonus { get; set; }

    public int DefenseBonus { get; set; }

    public int StrengthBonus { get; set; }

    public int SpeedBonus { get; set; }

    public int StaminaBonus { get; set; }

    public int DexterityBonus { get; set; }

    public int AgilityBonus { get; set; }

    public int IntelligenceBonus { get; set; }

    public int PietyBonus { get; set; }

    public int ConstitutionBonus { get; set; }

    public int VisionBonus { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Item(ItemType itemType)
    {
        ItemType = itemType;
    }
}