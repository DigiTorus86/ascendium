using Ascendium.Core;

namespace Ascendium.Components;

public class Map : IUpdatable, IDrawable
{
    private int _rows;
    private int _cols;
    
    public Room CurrentRoom { get; private set; }

    public Room[,] Rooms { get; private set; }

    public Map(int columns, int rows)
    {
        _cols = columns;
        _rows = rows;

        Rooms = new Room[rows, columns];
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Rooms[row, col] = new Room();
            }
        }

        CurrentRoom = Rooms[0,0];
    }

    public void Update()
    {
        CurrentRoom.Update();
    }

    public void Draw()
    {
        CurrentRoom.Draw();
    }
}