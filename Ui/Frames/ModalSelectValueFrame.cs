using Ascendium.Components;

namespace Ascendium.Ui.Frames;

public class ModalSelectValueFrame : Frame
{
    private readonly List<KeyValuePair<string, int>> _values;
    private readonly int _maxValue;
    private int _selectedIndex = 0;
    
    public string Title { get; set; } = string.Empty;

    public string SelectedValue { get; private set; } = string.Empty;

    public ConsoleColor DisabledForegroundColor { get; private set; } = ConsoleColor.Gray;

    public ModalSelectValueFrame(int left, int top, int width, int height, IEnumerable<KeyValuePair<string, int>> items, int maxValue = int.MaxValue)
    : base(left, top, width, height)
    {
        _values = items.ToList();
        _maxValue = maxValue;

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

            if (keyInfo.Key == ConsoleKey.Enter &&
                _values[_selectedIndex].Value <= _maxValue)
            {
                SelectedValue = _values[_selectedIndex].Key;
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

            if (_values[i].Value > _maxValue)
            {
                Console.ForegroundColor = DisabledForegroundColor;
            }
    
            Console.SetCursorPosition(Left + 2, itemTop);
            Console.Write(_values[i].Key);

            Console.SetCursorPosition(Left + 22, itemTop++);
            Console.Write(_values[i].Value); // TODO: right-justify
        }
    }
}