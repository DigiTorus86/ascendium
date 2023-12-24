using System.IO.Compression;
using Ascendium.Components;

namespace Ascendium.Ai;

public static class NpcInteractions
{
    public static Dictionary<string, Prompt>  Prompts { get; private set; } = new Dictionary<string, Prompt>();

    private static Prompt DefaultPrompt;

    private static KeyValuePair<string, string> Goodbye;
    
    static NpcInteractions()
    {
        Goodbye = new KeyValuePair<string, string>("EXIT", "Goodbye");
        DefaultPrompt = new Prompt(string.Empty, string.Empty);
        DefaultPrompt.Text = "...";
        DefaultPrompt.ResponseOptions.Add(Goodbye);

        LoadPrompts();
    }

    public static bool HasPrompt(string npcName, string promptKey = "")
    {
        string key = GetKey(npcName, promptKey);
        return Prompts.ContainsKey(key);
    } 

    public static Prompt GetPrompt(string npcName, string promptKey = "")
    {
        string key = GetKey(npcName, promptKey);
        if (Prompts.TryGetValue(key, out var prompt))
        {
            return prompt;
        }

        // DefaultPrompt.Text = $"{key}: {string.Join(",", Prompts.Keys)}";
        return DefaultPrompt;
    } 

    private static void LoadPrompts()
    {
        Prompts.Clear();

        Prompt bubba = new Prompt("Bubba", string.Empty);
        bubba.Title = "Welcome to Ascendium!";
        bubba.ResponseOptions.Add(new KeyValuePair<string, string>("ASCENDIUM", "Ascendium?"));
        bubba.ResponseOptions.Add(Goodbye);
        Prompts.Add(GetKey(bubba), bubba);

        Prompt bubba1 = new Prompt("Bubba", "ASCENDIUM");
        bubba1.Title = "What is Ascendium?";
        bubba1.Text = "Ascendium is a classic text console fantasy RPG.";
        bubba1.ResponseOptions.Add(Goodbye);
        Prompts.Add(GetKey(bubba1), bubba1);
    }

    private static string GetKey(Prompt prompt) => $"{prompt.NpcName}_{prompt.Key}";

    private static string GetKey(string npcName, string promptKey) => $"{npcName}_{promptKey}";

}