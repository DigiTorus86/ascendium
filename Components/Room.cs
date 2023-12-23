using Ascendium.Ai;

namespace Ascendium.Components;

public class Room : IUpdatable, IDrawable
{
    public static bool Debug = true;

    private static int _rows = 24;
    private static int _cols = 50;

    private string[] _room = {
        "▒0▒▒▒▒▒▒▒▒1▒▒▒▒▒▒▒▒▒2▒▒▒▒▒▒▒▒▒3▒▒▒▒▒▒▒▒▒4▒▒▒▒▒▒▒▒▒",
        "01234567890123456789012345678901234567890123456789",
        "▒2            ▒▒                                 ▒",
        "▒3             ▒▒                                ▒",
        "▒4              ▒▒▒▒▒▒▒▒▒▒                       ▒",
        "▒5                                               ▒",
        "▒6                                               ▒",
        "▒7                                               ▒",
        "▒8                                               ▒",
        "▒9                                               ▒",
        "▒10                                              ▒",
        "▒11                      ▒▒                      ▒",
        "▒12                      ▒▒                      ▒",
        "▒13                 ▒▒▒▒▒▒▒▒▒▒▒▒                 ▒",
        "▒14                      ▒▒                      ▒",
        "▒15                      ▒▒                      ▒",
        "▒16                                              ▒",
        "▒17                                              ▒",
        "▒18        ▒▒                     ▒▒             ▒",
        "▒19        ▒▒                     ▒▒             ▒",
        "▒20        ▒▒                     ▒▒             ▒",
        "▒21                                              ▒",
        "▒22                                              ▒",
        "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
    };

    public Tile[,] Tiles { get; private set; }

    public List<BaseDrawableGameComponent> Components { get; private set; } = new List<BaseDrawableGameComponent>();

    public string Description { get; set; } = "TODO: Room description goes here.";

    public int VisionModifier { get; set; } = 0;  // Set negative to decrease vision range

    public Room()
    {
        Tiles = new Tile[_rows, _cols];

        // TODO: load the tiles from a resource
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Tiles[row, col] = new Tile()
                {
                    Top = row,
                    Left = col,
                    Symbol = _room[row].Substring(col, 1),
                    ForegroundColor = ConsoleColor.DarkGray,
                    Effect = Tile.GetMovementEffect(_room[row].Substring(col, 1)),
                    IsVisible = false // to be revealed by player proximity
                };
            }
        }

        // TODO: load the components from a resource?

    }

    public void Update()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Tiles[row, col].Update();
            }
        }

        foreach (BaseDrawableGameComponent component in Components)
        {
            component.Update();
        }
    }

    public void Draw()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Tiles[row, col].Draw();
            }
        }

        foreach (BaseDrawableGameComponent component in Components)
        {
            component.Draw();
        }
    }

    public List<Character> GetCharacters() => Components.OfType<Character>().ToList();

    public List<Character> GetCharactersNotPlayer() 
        => Components.OfType<Character>().Where(c => c.CharacterType != Types.CharacterType.Player).ToList();

    public List<Enemy> GetEnemies() => Components.OfType<Enemy>().ToList();

    public List<Character> GetNpcs() 
        => Components.OfType<Character>().Where(c => c.CharacterType == Types.CharacterType.NPC).ToList();

    public Player GetPlayer() => Components.OfType<Player>().First();


    public void SetVisibility(Player player)
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                if (HasLineOfSight(player.Left, player.Top, col, row, player.VisionModified))
                {
                    Tiles[row, col].IsVisible = true;
                }
            }
        }

        foreach(Character character in GetCharactersNotPlayer())
        {
            character.IsVisible = HasLineOfSight(player.Left, player.Top, character.Left, character.Top, player.VisionModified + VisionModifier);
        }
    }

    public bool CanMoveTo(int left, int top)
    {
        if (left < 0 || left >= _cols || top < 0 || top >= _rows)
        {
            return false; // outside of room bounds
        }

        if (Tiles[top, left].Effect == MovementEffect.None) // May want to allow others here
        {
            return true;
        }

        return false;
    }

    public bool HasLineOfSight(int left1, int top1, int left2, int top2, float maxDistance)
    {
        if (LinearDistance(left1, top1, left2, top2) > maxDistance)
        {
            return false;
        }

        // Use Bresenham's line algorithm to check for obstacles along the line between the characters
        int x1 = left1;
        int y1 = top1;
        int x2 = left2;
        int y2 = top2;

        int w = x2 - x1;
        int h = y2 - y1;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1 ; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1 ; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1 ; else if (w > 0) dx2 = 1;
        
        int longest = Math.Abs(w);
        int shortest = Math.Abs(h);
        if (!(longest > shortest)) 
        {
            longest = Math.Abs(h) ;
            shortest = Math.Abs(w) ;
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;            
        }

        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++) 
        {
            if (!CanMoveTo(x1, y1))
            {
                return false;
            }
            
            numerator += shortest ;
            if (!(numerator < longest)) 
            {
                numerator -= longest;
                x1 += dx1;
                y1 += dy1;
            } 
            else 
            {
                x1 += dx2;
                y1 += dy2;
            }
        }

        return true;
    }

    public static float LinearDistance(int left1, int top1, int left2, int top2)
    {
        // Use Pythagoras to determine linear distance to target
        return (float)Math.Sqrt(((left1 - left2) * (left1 - left2)) + 
                                ((top1 - top2) * (top1 - top2)));
    }
}