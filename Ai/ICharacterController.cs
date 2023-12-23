using Ascendium.Ai.Results;
using Ascendium.Components;

namespace Ascendium.Ai;
public interface ICharacterController
{
    public BaseResult Update(Room room);
}