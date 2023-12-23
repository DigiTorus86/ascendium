using Ascendium.Components.Enemies;
using Ascendium.Types;
using Ascendium.Ui;

namespace Ascendium.Components;

public class Enemy : Character
{
    public EnemyType EnemyType { get; set; }

    public Enemy(EnemyType enemyType)
    : base(CharacterType.Enemy)
    {
        EnemyType = enemyType;

        // Defaults
        ForegroundColor = ConsoleColor.Red;
        Symbol = Glyph.Section;
    }
}