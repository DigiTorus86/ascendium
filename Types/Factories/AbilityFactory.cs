using  Ascendium.Types;

namespace Ascendium.Types.Factories;

public static class AbilityFactory
{
    public static Ability GetAbility(AbilityType abilityType)
    {
        var ability = new Ability(abilityType);
        
        switch (abilityType)
        {
            case AbilityType.DetectTraps:
                ability.Name = "Detect Traps";
                ability.Description = "Detect traps within a given area";
                break;
            case AbilityType.FindValuables:
                ability.Name = "Find Valuables";
                ability.Description = "Detect hidden valuables in a given area";
                break;
            case AbilityType.Steal:
                ability.Name = "Steal";
                ability.Description = "Steal a random item from an opponent";
                break;
            case AbilityType.PickLocks:
                ability.Name = "Pick Locks";
                ability.Description = "Attempt to open a lock without a key";
                break;

            default:
                ability.Name = "Unknown";
                ability.Description = "Unknown";
                break;
        }

        return ability;
    }
}