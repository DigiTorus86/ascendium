using Ascendium.Components;
using Ascendium.Core;

namespace Ascendium.Ui;

public class Frame : BaseDrawableGameComponent
{
    public FrameStyleType FrameStyle { get; set; } = FrameStyleType.DoubleLine;
    public int Width { get; set; }
    public int Height { get; set; }

    public Frame(int left, int top, int width, int height)
    {
        Left = left;
        Top = top;
        Height = height;
        Width = width;
    }

    public override void Draw()
    {
        if (!IsVisible)
        {
            return;
        }

        DrawBorder();
    }

    protected void DrawBorder()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;

        switch (FrameStyle)
        {
            case FrameStyleType.DoubleLine:
                DrawDoubleLineBorder();
                break;
            case FrameStyleType.SingleLine:
                DrawSingleLineBorder();
                break;
            case FrameStyleType.Symbol:
                DrawSymbolBorder();
                break;

            default:
                DrawDoubleLineBorder();
                break;
        }
    }

    protected void DrawDoubleLineBorder()
    {
        // Draw border top
        Console.SetCursorPosition(Left, Top);
        Console.Write(Glyph.DblCornerTopLeft);
        Console.Write(string.Concat(Enumerable.Repeat(Glyph.DblHorizLine, Width - 2)));
        Console.Write(Glyph.DblCornerTopRight);

        // Draw frame rows
        for (int row = 1; row < Height - 1; row++)
        {
            Console.SetCursorPosition(Left, Top + row);
            Console.Write(Glyph.DblVertLine);
            Console.Write(string.Concat(Enumerable.Repeat(" ", Width - 2)));
            Console.Write(Glyph.DblVertLine);
        }

        // Draw border bottom
        Console.SetCursorPosition(Left, Top + Height - 1);
        Console.Write(Glyph.DblCornerBottomLeft);
        Console.Write(string.Concat(Enumerable.Repeat(Glyph.DblHorizLine, Width - 2)));
        Console.Write(Glyph.DblCornerBottomRight);
    }

    protected void DrawSingleLineBorder()
    {
        // Draw border top
        Console.SetCursorPosition(Left, Top);
        Console.Write(Glyph.CornerTopLeft);
        Console.Write(string.Concat(Enumerable.Repeat(Glyph.HorizLine, Width - 2)));
        Console.Write(Glyph.CornerTopRight);

        // Draw frame rows
        for (int row = 1; row < Height - 1; row++)
        {
            Console.SetCursorPosition(Left, Top + row);
            Console.Write(Glyph.VertLine);
            Console.Write(string.Concat(Enumerable.Repeat(" ", Width - 2)));
            Console.Write(Glyph.VertLine);
        }

        // Draw border bottom
        Console.SetCursorPosition(Left, Top + Height - 1);
        Console.Write(Glyph.CornerBottomLeft);
        Console.Write(string.Concat(Enumerable.Repeat(Glyph.HorizLine, Width - 2)));
        Console.Write(Glyph.CornerBottomRight);
    }

    protected void DrawSymbolBorder()
    {
        // Draw border top
        Console.SetCursorPosition(Left, Top);
        Console.Write(Symbol);
        Console.Write(string.Concat(Enumerable.Repeat(Symbol, Width - 2)));
        Console.Write(Symbol);

        // Draw frame rows
        for (int row = 1; row < Height - 1; row++)
        {
            Console.SetCursorPosition(Left, Top + row);
            Console.Write(Symbol);
            Console.Write(string.Concat(Enumerable.Repeat(" ", Width - 2)));
            Console.Write(Symbol);
        }

        // Draw border bottom
        Console.SetCursorPosition(Left, Top + Height - 1);
        Console.Write(Symbol);
        Console.Write(string.Concat(Enumerable.Repeat(Symbol, Width - 2)));
        Console.Write(Symbol);
    }
}