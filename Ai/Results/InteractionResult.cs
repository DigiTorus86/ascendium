using System.ComponentModel;
using Ascendium.Types;
using Ascendium.Ui.Frames;

namespace Ascendium.Ai.Results;

public class InteractionResult : BaseResult
{
    public Character Initiator { get; private set; }

    public Character Target { get; private set; }

    public InteractionResult(Character initiator, Character target)
    {
        Initiator = initiator;
        Target = target;
    }

    public override void DetermineResult()
    {
        Messages.Add($"You encounter {Target.Name}.");

        this.ShowModally = NpcInteractions.HasPrompt(Target.Name);
    }
}