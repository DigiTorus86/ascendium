using Ascendium.Ui;

namespace Ascendium.Components;

public class Player : Character
{
    public Player()
    : base(Types.CharacterType.Player)
    {
        ForegroundColor = ConsoleColor.Yellow;
        Symbol = Glyph.Smiley;
    }
}