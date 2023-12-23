using System.Runtime.Serialization;
using Ascendium.Types;
using Ascendium.Types.Factories;
using Ascendium.Ui;

namespace Ascendium.Components.Enemies;

public static class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyType enemyType, int level)
    {
        var enemy = new Enemy(enemyType);
        enemy.ForegroundColor = GetLevelColor(level);

        var equipment = new List<ItemType>();
        var items = new List<ItemType>();
        var spells = new List<SpellType>();
        
        switch (enemyType)
        {
            case EnemyType.Kobold:
                enemy.Name = "Kobold";
                enemy.Description = "";
                enemy.Symbol = Glyph.Section;
                enemy.MaxHealth = level * 10;
                enemy.MaxMana = 0; 
                enemy.Experience = level * 5;
                equipment.Add(ItemType.RustySword);
                break;

            case EnemyType.Gnome:
                enemy.Name = "Gnome";
                enemy.Description = "";
                enemy.Symbol = Glyph.Yen;
                enemy.MaxHealth = level * 12;
                enemy.MaxMana = 0;
                enemy.Experience = level * 10;
                equipment.Add(ItemType.SmallKnife);
                equipment.Add(ItemType.ShortBow);
                break;

            default:
                break;
        }

        enemy.Health = enemy.MaxHealth;
        enemy.Mana = enemy.MaxMana;
        enemy.Conditions.Add(ConditionFactory.GetCondition(ConditionType.OK));

        foreach (ItemType itemType in equipment)
        {
            Item item = ItemFactory.GetItem(itemType);
            enemy.Equipment.Add(item.Name, item);
        }

        foreach (ItemType itemType in items)
        {
            Item item = ItemFactory.GetItem(itemType);
            enemy.Items.Add(item);
        }

        foreach (SpellType spellType in spells)
        {
            Spell spell = SpellFactory.GetSpell(spellType);
            enemy.Spells.Add(spell);
        }

        return enemy;
    }

    public static ConsoleColor GetLevelColor(int level)
    {
        ConsoleColor color;

        switch (level)
        {
            case int n when (n < 5):
                color = ConsoleColor.White;
                break;

            case int n when (n < 10):
                color = ConsoleColor.Yellow;
                break;

            case int n when (n < 15):
                color = ConsoleColor.DarkYellow;
                break;

            case int n when (n < 20):
                color = ConsoleColor.Cyan;
                break;

            case int n when (n < 25):
                color = ConsoleColor.Blue;
                break;

            case int n when (n < 30):
                color = ConsoleColor.DarkBlue;
                break;

            case int n when (n < 35):
                color = ConsoleColor.Magenta;
                break;

            case int n when (n < 40):
                color = ConsoleColor.DarkMagenta;
                break;

            case int n when (n < 45):
                color = ConsoleColor.Red;
                break;

            default:
                color = ConsoleColor.DarkRed;
                break;
        }

        return color;
    }
}