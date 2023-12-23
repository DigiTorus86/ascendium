using Ascendium.Core;

namespace Ascendium.Components;

public abstract class BaseDrawableGameComponent : IUpdatable, IDrawable
{
    public static readonly int InvalidLocation = -1;

    private int _previousLeft = InvalidLocation;
    private int _previousTop = InvalidLocation;

    public int DrawOrder { get; set; }

    public bool IsVisible { get; set; } = true;

    public int MapX { get; set; }

    public int MapY { get; set; }

    public int Top { get; set; }

    public int Left { get; set; }

    public int NextTop { get; set; } = InvalidLocation;

    public int NextLeft { get; set; } = InvalidLocation;

    public string Symbol { get; set; } = string.Empty;

    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

    public virtual void Initialize()
    {
    }

    public virtual void Update()
    {

    }

    public virtual void Draw()
    {
        if (!IsVisible)
        {
            return;
        }

        if (_previousLeft >= 0 && _previousTop >= 0)
        {
            Console.SetCursorPosition(_previousLeft, _previousTop);
            Console.Write(" ");
        }

        if (Top >= 0 && Left >= 0)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.SetCursorPosition(Left, Top);
            Console.Write(Symbol);
            _previousLeft = Left;
            _previousTop = Top;
        }
    }
}