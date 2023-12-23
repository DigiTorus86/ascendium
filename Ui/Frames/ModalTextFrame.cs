using Ascendium.Core;
using Ascendium.Ui;

public class ModalTextFrame : TextFrame
{
    public ModalTextFrame(int left, int top, int width, int height)
    : base(left, top, width, height)
    {
        FrameStyle = FrameStyleType.SingleLine;
    }

    public override void Draw()
    {
        if (!IsVisible)
        {
            return;
        }

        bool exit = false;

        do
        {
            DrawBorder();
            DrawText();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.Backspace)
            {
                ScrollUp();
            }

            if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.Spacebar)
            {
                ScrollDown();
            }

            if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Escape)
            {
                exit = true;
            }
            
        } while (!exit);
    }
}