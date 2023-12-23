using Ascendium.Core;

namespace Ascendium.Components;

public class GameComponents : IUpdatable, IDrawable
{
    public List<BaseDrawableGameComponent> Components { get; } = new List<BaseDrawableGameComponent>();

    public void Update()
    {
        foreach (var component in Components)
        {
            component.Update();
        }
    }

    public void Draw()
    {
        foreach (var component in Components.OrderBy(c => c.DrawOrder))
        {
            component.Draw();
        }
    }
}