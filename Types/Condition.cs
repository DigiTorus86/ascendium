using Ascendium.Types;

public class Condition : IDescribed
{
    public ConditionType ConditionType { get; private set; } = ConditionType.OK;

    public ConditionCategoryType ConditionCategoryType { get; set; } = ConditionCategoryType.Neutral;

    public EffectType EffectType { get; set; } = EffectType.None;

    public int EffectAmount { get; set; }

    public int TurnsRemaining { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Condition(ConditionType conditionType)
    {
        ConditionType = conditionType;
    }
}