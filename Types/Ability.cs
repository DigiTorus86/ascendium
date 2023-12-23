using Ascendium.Types;

public class Ability : IDescribed
{
    public AbilityType AbilityType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Ability(AbilityType abilityType)
    {
        AbilityType = abilityType;
    }
}