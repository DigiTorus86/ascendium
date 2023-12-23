using Ascendium.Ai;

namespace Ascendium.Types.Factories;

public static class ItemFactory
{
    public static readonly string SlotPrimary = "Primary";
    public static readonly string SlotAlternate = "Alternate";
    public static readonly string SlotHead = "Head";
    public static readonly string SlotNeck = "Neck";
    public static readonly string SlotBody = "Body";
    public static readonly string SlotFinger = "Finger";
    public static readonly string SlotLegs = "Legs";
    public static readonly string SlotFeet = "Feet";

    public static Item GetItem(ItemType itemType)
    {
        var item = new Item(itemType);

        switch (itemType)
        {
            case ItemType.Nothing:
                item.Name = "Nothing";
                item.Description = "Nothing at all";
                item.EquipType = ItemEquipType.NotEquipped;
                item.CooldownRequired = 0;
                break;

            // --- Melee weapons --- //
            // Thief
            case ItemType.SmallKnife:
                item.Name = "Small Knife";
                item.Description = "A small hunting knife";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.0f;
                item.AttackBonus = 1;
                item.MinEffect = 1;
                item.MaxEffect = 4;
                item.CooldownRequired = 0;
                break;

            case ItemType.Dagger:
                item.Name = "Dagger";
                item.Description = "A heavy double-edged knife";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.0f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 5;
                item.CooldownRequired = 0;
                break;

            case ItemType.Stiletto:
                item.Name = "Stiletto";
                item.Description = "A slender dagger with a needle point";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.0f;
                item.AttackBonus = 2;
                item.MinEffect = 3;
                item.MaxEffect = 6;
                item.CooldownRequired = 0;
                break;

            case ItemType.Poignard:
                item.Name = "Poignard";
                item.Description = "A long pointed knife with a cross guard";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.0f;
                item.AttackBonus = 2;
                item.MinEffect = 4;
                item.MaxEffect = 7;
                item.CooldownRequired = 0;
                break;

            case ItemType.Rapier:
                item.Name = "Rapier";
                item.Description = "A slender pointed sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.5f;
                item.AttackBonus = 3;
                item.MinEffect = 4;
                item.MaxEffect = 8;
                item.CooldownRequired = 0;
                break;

            case ItemType.Ninjato:
                item.Name = "Ninjato";
                item.Description = "A straight bladed marvel of exceptional sharpness";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.5f;
                item.AttackBonus = 4;
                item.MinEffect = 5;
                item.MaxEffect = 10;
                item.CooldownRequired = 0;
                break;

            // Warrior
            case ItemType.RustySword:
                item.Name = "Rusty Sword";
                item.Description = "a fairly solid but rusty sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.5f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 8;
                item.CooldownRequired = 1;
                break;

            case ItemType.ShortSword:
                item.Name = "Short Sword";
                item.Description = "a basic short sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.5f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 8;
                item.CooldownRequired = 1;
                break;

            case ItemType.LongSword:
                item.Name = "Long Sword";
                item.Description = "a serviceable long sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 1.5f;
                item.AttackBonus = 2;
                item.MinEffect = 4;
                item.MaxEffect = 10;
                item.CooldownRequired = 1;
                break;

            case ItemType.GreatSword:
                item.Name = "Great Sword";
                item.Description = "a two-handed sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 3;
                item.MinEffect = 6;
                item.MaxEffect = 12;
                item.CooldownRequired = 1;
                break;

            case ItemType.FireSword:
                item.Name = "Great Sword";
                item.Description = "a two-handed sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 4;
                item.MinEffect = 8;
                item.MaxEffect = 14;
                item.CooldownRequired = 1;
                break;

            case ItemType.SingingSword:
                item.Name = "Singing Sword";
                item.Description = "a mystical singing sword";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 4;
                item.MinEffect = 8;
                item.MaxEffect = 14;
                item.CooldownRequired = 1;
                break;

            case ItemType.PhasedSword:
                item.Name = "Phased Sword";
                item.Description = "a mysterioius sword that bends reality";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 5;
                item.MinEffect = 8;
                item.MaxEffect = 16;
                item.CooldownRequired = 0;
                break;

            case ItemType.WarHammer:
                item.Name = "War Hammer";
                item.Description = "a heavy hammer on a long handle";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2.5f;
                item.AttackBonus = 4;
                item.MinEffect = 12;
                item.MaxEffect = 24;
                item.CooldownRequired = 2;
                break;

            case ItemType.GiantMaul:
                item.Name = "Giant Maul";
                item.Description = "an enormous war hammer";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 3f;
                item.AttackBonus = 5;
                item.MinEffect = 24;
                item.MaxEffect = 36;
                item.CooldownRequired = 3;
                break;

            // Priest
            case ItemType.WoodenStaff:
                item.Name = "Wooden Staff";
                item.Description = "a stout oaken staff";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 8;
                item.CooldownRequired = 1;
                break;

            case ItemType.Mace:
                item.Name = "Mace";
                item.Description = "a basic blunt mace";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 8;
                item.CooldownRequired = 1;
                break;

            case ItemType.Croizer:
                item.Name = "Croizer";
                item.Description = "a metal cross-tipped staff";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 3;
                item.MinEffect = 4;
                item.MaxEffect = 8;
                item.CooldownRequired = 1;
                break;

            case ItemType.BlessedMace:
                item.Name = "Blessed Mace";
                item.Description = "a heaven-blessed heavy mace";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 4;
                item.MinEffect = 6;
                item.MaxEffect = 10;
                item.CooldownRequired = 1;
                break;

            case ItemType.HolyStaff:
                item.Name = "Holy Staff";
                item.Description = "a staff containing the finger bone of a saint";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 5;
                item.MinEffect = 6;
                item.MaxEffect = 12;
                item.CooldownRequired = 1;
                break;

            case ItemType.DivineMace:
                item.Name = "Divine Mace";
                item.Description = "a mace imbued with the power of a deity";
                item.EquipType = ItemEquipType.MeleeWeapon;
                item.EquipSlot = SlotPrimary;
                item.Range = 2f;
                item.AttackBonus = 6;
                item.MinEffect = 8;
                item.MaxEffect = 16;
                item.CooldownRequired = 1;
                break;


            // --- Ranged Weapons --- //

            case ItemType.ShortBow:
                item.Name = "Short Bow";
                item.Description = "A small bow";
                item.EquipType = ItemEquipType.RangedWeapon;
                item.EquipSlot = SlotAlternate;
                item.Range = 4.0f;
                item.AttackBonus = 1;
                item.MinEffect = 0;
                item.MaxEffect = 4;
                item.CooldownRequired = 2;
                break;

            case ItemType.CrossBow:
                item.Name = "Crossbow";
                item.Description = "A mechanical crossbow";
                item.EquipType = ItemEquipType.RangedWeapon;
                item.EquipSlot = SlotAlternate;
                item.Range = 5.0f;
                item.AttackBonus = 2;
                item.MinEffect = 2;
                item.MaxEffect = 6;
                item.CooldownRequired = 3;
                break;

            case ItemType.LongBow:
                item.Name = "Long Bow";
                item.Description = "A stout long bow";
                item.EquipType = ItemEquipType.RangedWeapon;
                item.EquipSlot = SlotAlternate;
                item.Range = 6.0f;
                item.AttackBonus = 2;
                item.MinEffect = 0;
                item.MaxEffect = 6;
                item.CooldownRequired = 2;
                break;

            case ItemType.ElvenBow:
                item.Name = "Elven Bow";
                item.Description = "A long bow crafted by elves";
                item.EquipType = ItemEquipType.RangedWeapon;
                item.EquipSlot = SlotAlternate;
                item.Range = 8.0f;
                item.AttackBonus = 3;
                item.MinEffect = 2;
                item.MaxEffect = 8;
                item.CooldownRequired = 2;
                break;

            case ItemType.ThrowingAxe:
                item.Name = "Throwing Axe";
                item.Description = "A small axe for throwing";
                item.EquipType = ItemEquipType.RangedWeapon;
                item.EquipSlot = SlotAlternate;
                item.Range = 6.0f;
                item.AttackBonus = 5;
                item.MinEffect = 4;
                item.MaxEffect = 12;
                item.CooldownRequired = 3;
                break;

            // --- Armor ---//

            case ItemType.LeatherCap:
                item.Name = "Leather Cap";
                item.Description = "A cap made out of leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotHead;
                item.DefenseBonus = 1;
                break;

            case ItemType.ChainCoif:
                item.Name = "Chain Coif";
                item.Description = "A chain mail hood";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotHead;
                item.DefenseBonus = 2;
                break;

            case ItemType.IronHelm:
                item.Name = "Iron Helm";
                item.Description = "An open helmet made of iron";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotHead;
                item.DefenseBonus = 3;
                break;

            case ItemType.Bassinet:
                item.Name = "Bassinet";
                item.Description = "A steel helmet with a full face guard";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotHead;
                item.DefenseBonus = 4;
                break;

            case ItemType.SpikedHelm:
                item.Name = "Spiked Helm";
                item.Description = "A steel helmet with a spikes";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotHead;
                item.DefenseBonus = 4;
                item.AttackBonus = 1;
                break;

            // --- Body Armor --- //

            case ItemType.PaddedTunic:
                item.Name = "Padded Tunic";
                item.Description = "A shirt with cotton padding";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 1;
                break;

            case ItemType.LeatherVest:
                item.Name = "Leather Vest";
                item.Description = "A vest made from soft leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 2;
                break;

            case ItemType.LeatherArmor:
                item.Name = "Leather Armor";
                item.Description = "Armor made from boiled leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 3;
                break;

            case ItemType.ScaleVest:
                item.Name = "Scale Vest";
                item.Description = "A vest with small overlapping metal plates";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 4;
                break;

            case ItemType.ChainMail:
                item.Name = "Chain Mail";
                item.Description = "A tunic made of chain mail";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 5;
                break;

            case ItemType.PlateMail:
                item.Name = "Plate Mail";
                item.Description = "Armor made from steel plates";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 6;
                break;

            case ItemType.HeavyPlate:
                item.Name = "Heavy Plate";
                item.Description = "Armor made from thick steel plates";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotBody;
                item.DefenseBonus = 7;
                break;

            // --- Leg Armor --- //

            case ItemType.CanvasPants:
                item.Name = "Canvas Pants";
                item.Description = "Pants made of heavy cloth";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotLegs;
                item.DefenseBonus = 1;
                break;

            case ItemType.PaddedLeggings:
                item.Name = "Padded Leggings";
                item.Description = "Reinforced padded pants";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotLegs;
                item.DefenseBonus = 2;
                break;

            case ItemType.LeatherPants:
                item.Name = "Leather Pants";
                item.Description = "pants made of leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotLegs;
                item.DefenseBonus = 3;
                break;

            case ItemType.ScaleLeggings:
                item.Name = "Scale Leggings";
                item.Description = "Leggings with smale metal plates sewn in";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotLegs;
                item.DefenseBonus = 4;
                break;

            case ItemType.PlateLeggings:
                item.Name = "Plate Leggings";
                item.Description = "Leggings made of steel plates";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotLegs;
                item.DefenseBonus = 4;
                break;

            // --- Foot Armor --- //

            case ItemType.Sandals:
                item.Name = "Sandals";
                item.Description = "Simple sandals made of leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotFeet;
                item.DefenseBonus = 1;
                break;

            case ItemType.LeatherBoots:
                item.Name = "Leather Boots";
                item.Description = "Sturdy boots made of leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotFeet;
                item.DefenseBonus = 2;
                break;

            case ItemType.LeatherGreaves:
                item.Name = "Leather Greaves";
                item.Description = "Shin and instep protectors made of leather";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotFeet;
                item.DefenseBonus = 3;
                break;

            case ItemType.SteelGreaves:
                item.Name = "Steel Greaves";
                item.Description = "Shin and instep protectors made of steel plates";
                item.EquipType = ItemEquipType.Armor;
                item.EquipSlot = SlotFeet;
                item.DefenseBonus = 4;
                break;

            // --- Finger Jewelry --- //

            case ItemType.EagleEye:
                item.Name = "Eagle Eye";
                item.Description = "A ring that increases vision";
                item.EquipType = ItemEquipType.Jewelry;
                item.EquipSlot = SlotFinger;
                item.VisionBonus = 2;
                break;

            case ItemType.TigerEye:
                item.Name = "Tiger Eye";
                item.Description = "A ring with a tiger eye jewel that boosts vision and attack";
                item.EquipType = ItemEquipType.Jewelry;
                item.EquipSlot = SlotFinger;
                item.AttackBonus = 1;
                item.VisionBonus = 2;
                break;

            // --- Neck Jewelry --- //

            case ItemType.SnakeCharm:
                item.Name = "Snake Charm";
                item.Description = "A pendant imbued with the speed of a snake strike";
                item.EquipType = ItemEquipType.Jewelry;
                item.EquipSlot = SlotNeck;
                item.SpeedBonus = 2;
                break;

            case ItemType.OgreTusk:
                item.Name = "Ogre Tusk";
                item.Description = "The large tooth of an ogre on a leather thong";
                item.EquipType = ItemEquipType.Jewelry;
                item.EquipSlot = SlotNeck;
                item.StrengthBonus = 2;
                item.AttackBonus = 1;
                break;

            case ItemType.ThirdEye:
                item.Name = "Third Eye";
                item.Description = "A pendant that provides greatly-enhanced vision";
                item.EquipType = ItemEquipType.Jewelry;
                item.EquipSlot = SlotNeck;
                item.VisionBonus = 4;
                break;

            // --- Single-use items --- //

            case ItemType.HealingTea:
                item.Name = "Healing Tea";
                item.Description = "A cup of tea that restores up to 10 health";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 8;
                item.MaxEffect = 10;
                item.UsesRemaining = 1;
                break;

            case ItemType.HealingWine:
                item.Name = "Healing Wine";
                item.Description = "A bottle of wine that restores up to 50 health per use";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 40;
                item.MaxEffect = 50;
                item.UsesRemaining = 4;
                break;

            case ItemType.HealingBrandy:
                item.Name = "Healing Brandy";
                item.Description = "A bottle of liquor that restores up to 200 health per use";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 150;
                item.MaxEffect = 200;
                item.UsesRemaining = 4;
                break;

            case ItemType.MysticTea:
                item.Name = "Mystic Tea";
                item.Description = "A cup of tea that restores up to 10 mana";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 8;
                item.MaxEffect = 10;
                item.UsesRemaining = 1;
                break;

            case ItemType.MysticWine:
                item.Name = "Mystic Wine";
                item.Description = "A bottle of wine that restores up to 50 mana per use";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 40;
                item.MaxEffect = 50;
                item.UsesRemaining = 4;
                break;

            case ItemType.MysticBrandy:
                item.Name = "Mystic Brandy";
                item.Description = "A bottle of liquor that restores up to 200 mana per use";
                item.EquipType = ItemEquipType.NotEquipped;
                item.MinEffect = 40;
                item.MaxEffect = 50;
                item.UsesRemaining = 4;
                break;

            default:
                item.Name = "Unknown item";
                item.Description = "An unknown item";
                break;
        }

        return item;
    }
}