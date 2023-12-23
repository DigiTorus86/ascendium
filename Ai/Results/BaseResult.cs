using Ascendium.Components;

namespace Ascendium.Ai.Results;

public abstract class BaseResult : IDrawable
{
    public List<string> Messages { get; set; } = new List<string>();

    public string Symbol { get; set; } = string.Empty;

    public bool ShouldDraw { get; set; } = false;

    public bool ShowModally { get; set; } = false;

    public ConsoleColor ForegroundColor = ConsoleColor.Red;

    public TimeSpan AnimationDelay { get; set; } = TimeSpan.FromMilliseconds(500);

    public virtual void DetermineResult()
    {
    }

    public virtual void Draw()
    {
    }
}