using Ascendium.Ai.Results;
using Ascendium.Components;

namespace Ascendium.Ai;

public class StationaryController : CharacterController
{
    public StationaryController(Character parent)
    : base(parent)
    {
    }

    public override BaseResult Update(Room room)
    {
        return new MoveResult();
    }
}