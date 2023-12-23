namespace Ascendium.Content;

public static class Story
{
    private static string[] _chapters = new string[]
    {
                    "When the end finally comes, it’s with a certain sense of relief. " +
                    "The last few days have been a steady descent through disbelief, " +
                    "anger, pain, and finally a lingering, reluctant despair. " +
                    "The noose is tight and uncomfortable around your neck, " +
                    "placed there by a <color=red/> dead-eyed executioner <color=white/> a few moments ago. " +
                    "<br/> <br/> " +
                    "The jeers of the crowd in the city square fade to an expectant murmur. Executions like this have become frequent events over the last year, as desperation has slowly overcome the population. Even respectable people will perform unthinkable acts when driven to the breaking point. "
    };

    public static string[] Chapters { get => _chapters; set => _chapters = value; }

    public static void ShowChapter(int chapter)
    {
        string text = $"CHAPTER {chapter}";
        if (chapter > 0 || chapter < _chapters.Length)
        {
           text = $"CHAPTER {chapter} <br/> <br/> {_chapters[chapter - 1]}"; 
        }

        var modal = new ModalTextFrame(2, 2, 64, 20);
        modal.Text = text; 
        modal.Draw();

        Console.Clear();
    }
}