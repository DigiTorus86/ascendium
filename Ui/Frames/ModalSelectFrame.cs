using Ascendium.Components;

namespace Ascendium.Ui.Frames;

public class ModalSelectFrame : Frame
{
    private readonly List<KeyValuePair<string, string>> _values = new List<KeyValuePair<string, string>>();
    private int _selectedIndex = 0;

    public string Title { get; set; } = string.Empty;
    public string SelectedValue { get; private set; } = string.Empty;

    public ModalSelectFrame(int left, int top, int width, int height, IEnumerable<string> items)
    : base(left, top, width, height)
    {
        foreach(string item in items)
        {
            _values.Add(new KeyValuePair<string, string>(item, item));
        }

        FrameStyle = FrameStyleType.SingleLine;
    }

    public ModalSelectFrame(int left, int top, int width, int height, IEnumerable<KeyValuePair<string, string>> items)
    : base(left, top, width, height)
    {
        _values = items.ToList();

        FrameStyle = FrameStyleType.SingleLine;
    }

    public override void Draw()
    {
        bool exit = false;

        do
        {
            DrawBorder();
            DrawTitle();
            DrawItems();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.Backspace)
            {
               if (_selectedIndex > 0)
                {
                    _selectedIndex--;
                }
            }

            if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.Spacebar)
            {
                if (_selectedIndex < _values.Count - 1)
                {
                    _selectedIndex++;
                }
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (_selectedIndex < _values.Count)
                {
                    SelectedValue = _values[_selectedIndex].Key;
                }
                
                exit = true;
            }

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                SelectedValue = string.Empty;
                exit = true;
            }
            
        } while (!exit);
    }

    protected void DrawTitle()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;
        Console.SetCursorPosition(Left + 2, Top + 1);
        Console.Write(Title);
    }

    protected void DrawItems()
    {
        // TODO: handle lists longer than the displayable area

        int itemTop = Top + 2;
        for (int i =0; i < _values.Count; i++)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;

            if (_selectedIndex == i)
            {
                Console.ForegroundColor = BackgroundColor;
                Console.BackgroundColor = ForegroundColor;
            }
    
            Console.SetCursorPosition(Left + 2, itemTop++);
            Console.Write(_values[i].Value);
        }
    }
}