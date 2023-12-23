using System.Text;
using Ascendium.Core;

namespace Ascendium.Ui;

/// <summary>
/// Displays scrollable text inside a frame of the given size.
/// </summary>
public class TextFrame : Frame
{
    private readonly int _textHeight;
    private readonly int _textWidth;
    private int _startIndex;
    private string _text; 
    private string [] _textLines;

    public static string LineBreak = "<br/>";
    public static string PaddedLineBreak = " <br/> ";

    public string Text 
    { 
        get
        {
            return _text ?? string.Empty;
        } 

        set
        {
            _text = value;
            ParseText(_text);
            _startIndex = 0;

            if (TextBufferLength < _text.Length)
            {
                TextBufferLength = _text.Length;
            }
        }
    }

    public int TextBufferLength { get; set; } = 1000;

    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

    public TextFrame(int left, int top, int width, int height)
    : base(left, top, width, height)
    {
        _textHeight = height - 2;
        _textWidth = width - 4;
        _text = string.Empty;
        _textLines = new [] { _text };
    }

    public void SetText(string[] text)
    {
        var sb = new StringBuilder();

        foreach(string textLine in text)
        {
            sb.AppendLine(textLine);
            sb.Append(PaddedLineBreak);
        }

        Text = sb.ToString();
    }

    public void AddTextLine(string? textLine)
    {
        if (textLine is null)
        {
            return;
        }

        string frameText = $"{textLine} {LineBreak} {_text}";
        if (frameText.Length > TextBufferLength)
        {
            frameText = frameText[..TextBufferLength];
        }

        Text = frameText;
    }

    public void AddDebugLine(string? textLine)
    {
        if (textLine is null)
        {
            return;
        }

        string frameText = $"DEBUG: {textLine} {LineBreak} {_text}";
        if (frameText.Length > TextBufferLength)
        {
            frameText = frameText[..TextBufferLength];
        }

        Text = frameText;
        Draw();
    }

    public void ScrollUp()
    {
        if (_startIndex > 0)
        {
            _startIndex--;
        }
    }

    public void ScrollDown()
    {
        if (_startIndex < _textLines.Length - _textHeight)
        {
            _startIndex++;
        }
    }

    public override void Update()
    {
        
    }

    public override void Draw()
    {
        if (!IsVisible)
        {
            return;
        }

        DrawBorder();
        DrawText();
    }

    protected void DrawText()
    {
        Console.ForegroundColor = TextColor;
        Console.BackgroundColor = BackgroundColor;
        
        int line = 0;
        do
        {
            Console.SetCursorPosition(Left + 2, Top + 1 + line);
            Console.Write(_textLines[_startIndex + line++]);
        } while ((_startIndex + line < _textLines.Length) && (line < _textHeight));
    }

    /// <summary>
    /// Parses the Text property into lines of text that will wrap cleanly when displayed in the frame. 
    /// </summary>
    protected void ParseText(string text)
    {
        var lines = new List<string>();
        string[] words = text.Split(" ");
        int col = 0;
        var rowText = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].StartsWith("<color="))
            {
                // Ignore these control tags for now
            }
            else if (words[i].Equals(LineBreak))
            {
                // Wrap to the next line
                lines.Add(rowText.ToString());
                rowText.Clear();
                col = 0;
            }
            else if (words[i].Length < _textWidth - col)
            {
                rowText.Append(words[i]);
                rowText.Append(' ');
                col += words[i].Length + 1;
            }
            else
            {
                // Wrap to the next line
                lines.Add(rowText.ToString());
                rowText.Clear();

                rowText.Append(words[i]);
                rowText.Append(' ');
                col = words[i].Length + 1;
            }    
        }

        lines.Add(rowText.ToString());
        _textLines = lines.ToArray();
    }
    

    protected ConsoleColor ParseColor(string word)
    {
        ConsoleColor color = ConsoleColor.Green;

        var terms = word.Split("=");

        if (terms.Length < 2)
        {
            return color;
        }

        string colorTerm = terms[1].Replace("/>", string.Empty).ToLowerInvariant();
        switch (colorTerm)
        {
            case "blue":
                color = ConsoleColor.Blue;
                break;

            case "green":
                color = ConsoleColor.Green;
                break;

            case "red":
                color = ConsoleColor.Red;
                break;

            case "yellow":
                color = ConsoleColor.Red;
                break;

            default:
                break;
        }

        return color;
    }
}