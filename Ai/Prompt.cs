using Ascendium.Types;

namespace Ascendium.Ai;

public class Prompt
{
    public string NpcName { get; private set; }
    public string Key { get; private set; }

    public string Text { get;  set; } = string.Empty;

    public List<KeyValuePair<string, string>> ResponseOptions { get; set; } = new List<KeyValuePair<string, string>>();

    public bool IsCompletable { get; set; }

    public bool IsComplete { get; set; }

    public List<Item> ItemsRequired { get; set; } = new List<Item>();

    public List<Item> ItemsGiven { get; set; } = new List<Item>();

    public Prompt(string npcName, string key)
    {
        NpcName = npcName;
        Key = key;
    }
}