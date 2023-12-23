using System.Collections;
using Ascendium.Ui;

namespace Ascendium.Components;

public class Tile : BaseDrawableGameComponent
{
    public MovementEffect Effect { get; set; } = MovementEffect.None;

    public Tile()
    {

    }

    public override void Draw()
    {
        Console.SetCursorPosition(Left, Top);
        
        if (IsVisible)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.Write(Symbol);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(Glyph.LightBlock);
        }
    }

    public static MovementEffect GetMovementEffect(string symbol)
    {
        MovementEffect effect;

        switch (symbol)
        {
            case " ":
                effect = MovementEffect.None;
                break;
            
            default:
                effect = MovementEffect.Blocked;
                break;
        }

        return effect;
    }

    public static MovementEffect GetVisibilityEffect(string symbol)
    {
        MovementEffect effect;

        switch (symbol)
        {
            case " ":
                effect = MovementEffect.None;
                break;
            
            default:
                effect = MovementEffect.Blocked;
                break;
        }

        return effect;
    }
}