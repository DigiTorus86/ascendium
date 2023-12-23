using Ascendium.Core;
using Ascendium.Types;

namespace Ascendium.Ui;

public class StatusFrame : Frame
{
    public StatusMode Mode { get; set; } = StatusMode.Status;

    public Character Subject { get; set; }

    public StatusFrame(Character subject, int left, int top, int width, int height)
    : base(left, top, width, height)
    {
        Subject = subject;
    }

    public override void Draw()
    {
        if (!IsVisible)
        {
            return;
        }

        DrawBorder();

        switch (Mode)
        {
            case StatusMode.Status:
                DrawStatus();
                break;

            case StatusMode.Equipment:
                DrawEquipment();
                break;

            case StatusMode.Attributes:
                DrawAttributes();
                break;

            case StatusMode.Inventory:
                DrawInventory();
                break;

            case StatusMode.Spells:
                DrawSpells();
                break;

            default:
                break;
        }
    }

    private void DrawStatus()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 7, Top);
        Console.Write("STATUS");

        Console.SetCursorPosition(Left + 2, Top + 1);
        Console.Write("Level:");
        Console.SetCursorPosition(Left + 12, Top + 1);
        Console.Write(Subject.Level);

        Console.SetCursorPosition(Left + 2, Top + 2);
        Console.Write("Health:");
        Console.SetCursorPosition(Left + 12, Top + 2);
        Console.Write(Subject.Health);
        Console.Write("/");
        Console.Write(Subject.MaxHealth);

        Console.SetCursorPosition(Left + 2, Top + 3);
        Console.Write("Mana:");
        Console.SetCursorPosition(Left + 12, Top + 3);
        Console.Write(Subject.Mana);
        Console.Write("/");
        Console.Write(Subject.MaxMana);

        Console.SetCursorPosition(Left + 2, Top + 4);
        Console.Write("Attack:");
        Console.SetCursorPosition(Left + 12, Top + 4);
        Console.Write(Subject.Attack);

        Console.SetCursorPosition(Left + 2, Top + 5);
        Console.Write("Defense:");
        Console.SetCursorPosition(Left + 12, Top + 5);
        Console.Write(Subject.Defense);

        Console.SetCursorPosition(Left + 2, Top + 6);
        Console.Write("Fatigue:");
        Console.SetCursorPosition(Left + 12, Top + 6);
        Console.Write(Subject.Fatigue);

        Console.SetCursorPosition(Left + 2, Top + 7);
        Console.Write("Gold:");
        Console.SetCursorPosition(Left + 12, Top + 7);
        Console.Write(Subject.Gold);

        Console.SetCursorPosition(Left + 2, Top + 8);
        Console.Write("Favor:");
        Console.SetCursorPosition(Left + 12, Top + 8);
        Console.Write(Subject.Favor);

        Console.SetCursorPosition(Left + 2, Top + 9);
        Console.Write("Exp:");
        Console.SetCursorPosition(Left + 12, Top + 9);
        Console.Write(Subject.Experience);

        Console.SetCursorPosition(Left + 2, Top + 10);
        Console.Write("Next:");
        Console.SetCursorPosition(Left + 12, Top + 10);
        Console.Write(Subject.NextLevelExperience());

        Console.SetCursorPosition(Left + 2, Top + 11);
        Console.Write("Cond:");
        
        int cnt = 0;
        foreach (Condition condition in Subject.Conditions)
        {
            if (condition.ConditionCategoryType == ConditionCategoryType.Buff)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (condition.ConditionCategoryType == ConditionCategoryType.Buff)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition(Left + 12, Top + 11 + cnt);
            Console.Write(condition.Name);
        }

        if (!Subject.Conditions.Any())
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Left + 12, Top + 11);
            Console.Write("OK");
        }
    }

    private void DrawEquipment()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 6, Top);
        Console.Write("EQUIPMENT");

        int cnt = 0;
        foreach (var item in Subject.Equipment)
        {
            Console.SetCursorPosition(Left + 2, Top + 1 + cnt);
            Console.Write(item.Key);

            Console.SetCursorPosition(Left + 10, Top + 1 + cnt++);
            Console.Write(item.Value.Name);
        }
    }

    private void DrawInventory()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 6, Top);
        Console.Write("INVENTORY");

        int cnt = 0;
        foreach (Item item in Subject.Items)
        {
            Console.SetCursorPosition(Left + 2, Top + 1 + cnt++);
            Console.Write(item.Name);
        }
    }

    private void DrawAttributes()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 5, Top);
        Console.Write("ATTRIBUTES");
        
        Console.SetCursorPosition(Left + 2, Top + 1);
        Console.Write("Strength:");
        Console.SetCursorPosition(Left + 12, Top + 1);
        Console.Write($"{Subject.StrengthBase}({Subject.StrengthModified})");

        Console.SetCursorPosition(Left + 2, Top + 2);
        Console.Write("Speed:");
        Console.SetCursorPosition(Left + 12, Top + 2);
        Console.Write($"{Subject.SpeedBase}({Subject.SpeedModified})");

        Console.SetCursorPosition(Left + 2, Top + 3);
        Console.Write("Stamina:");
        Console.SetCursorPosition(Left + 12, Top + 3);
        Console.Write($"{Subject.StaminaBase}({Subject.StaminaModified})");

        Console.SetCursorPosition(Left + 2, Top + 4);
        Console.Write("Agility:");
        Console.SetCursorPosition(Left + 12, Top + 4);
        Console.Write($"{Subject.AgilityBase}({Subject.AgilityModified})");

        Console.SetCursorPosition(Left + 2, Top + 5);
        Console.Write("Intel:");
        Console.SetCursorPosition(Left + 12, Top + 5);
        Console.Write($"{Subject.IntelligenceBase}({Subject.IntelligenceModified})");

        Console.SetCursorPosition(Left + 2, Top + 6);
        Console.Write("Piety:");
        Console.SetCursorPosition(Left + 12, Top + 6);
        Console.Write($"{Subject.PietyBase}({Subject.PietyModified})");

        Console.SetCursorPosition(Left + 2, Top + 7);
        Console.Write("Const:");
        Console.SetCursorPosition(Left + 12, Top + 7);
        Console.Write($"{Subject.ConstitutionBase}({Subject.ConstitutionModified})");

        Console.SetCursorPosition(Left + 2, Top + 8);
        Console.Write("Vision:");
        Console.SetCursorPosition(Left + 12, Top + 8);
        Console.Write($"{Subject.VisionBase}({Subject.VisionModified})");
    }

    private void DrawSpells()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 7, Top);
        Console.Write("SPELLS");

        int cnt = 0;
        foreach (Spell spell in Subject.Spells)
        {
            Console.SetCursorPosition(Left + 2, Top + 1 + cnt++);
            Console.Write(spell.Name);
        }
    }

    private void DrawAbilities()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        Console.SetCursorPosition(Left + 6, Top);
        Console.Write("ABILITIES");

        int cnt = 0;
        foreach (Ability ability in Subject.Abilities)
        {
            Console.SetCursorPosition(Left + 2, Top + 1 + cnt++);
            Console.Write(ability.Name);
        }
    }
}