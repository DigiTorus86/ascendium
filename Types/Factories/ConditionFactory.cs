namespace Ascendium.Types.Factories;

public static class ConditionFactory
{
    public static Condition GetCondition(ConditionType conditionType)
    {
        var condition = new Condition(conditionType);
        
        switch (conditionType)
        {
            case ConditionType.OK:
                condition.Name = "OK";
                condition.Description = "No adverse conditions";
                condition.EffectType = EffectType.None;
                condition.ConditionCategoryType = ConditionCategoryType.Neutral;
                break;

            case ConditionType.Weakened:
                condition.Name = "Weakened";
                condition.Description = "Strength is decreased";
                condition.EffectType = EffectType.DecreaseStrength;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Poisoned:
                condition.Name = "Poisoned";
                condition.Description = "Poisoned";
                condition.EffectType = EffectType.DecreaseHealth;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 1;
                break;

            case ConditionType.Slowed:
                condition.Name = "Slowed";
                condition.Description = "Movement speed decreased";
                condition.EffectType = EffectType.DecreaseSpeed;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Clumsy:
                condition.Name = "Clumsy";
                condition.Description = "Agility is decreased";
                condition.EffectType = EffectType.DecreaseAgility;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Stupefied:
                condition.Name = "Stupefied";
                condition.Description = "Intelligence is decreased";
                condition.EffectType = EffectType.DecreaseIntelligence;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Corrupted:
                condition.Name = "Corrupted";
                condition.Description = "Piety is decreased";
                condition.EffectType = EffectType.DecreasePiety;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Enfeebled:
                condition.Name = "Enfeebled";
                condition.Description = "Constitution is decreased";
                condition.EffectType = EffectType.DecreaseConstitution;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Blinded:
                condition.Name = "Blinded";
                condition.Description = "Vision is decreased";
                condition.EffectType = EffectType.DecreaseVision;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 4;
                break;

            case ConditionType.Pacified:
                condition.Name = "Pacified";
                condition.Description = "Decreases attack power";
                condition.EffectType = EffectType.DecreaseAttack;
                condition.ConditionCategoryType = ConditionCategoryType.Debuff;
                condition.EffectAmount = 3;
                break;

            case ConditionType.Enraged:
                condition.Name = "Enraged";
                condition.Description = "Increases attack power";
                condition.EffectType = EffectType.IncreaseAttack;
                condition.ConditionCategoryType = ConditionCategoryType.Buff;
                condition.EffectAmount = 3;
                break;



            // TODO: set other type properties

            default:
                condition = new Condition(ConditionType.Unknown);
                condition.Name = "Unknown";
                condition.Description = "An unknown condition";
                condition.EffectType = EffectType.None;
                condition.ConditionCategoryType = ConditionCategoryType.Neutral;
                break;
        }
        

        return condition;
    }
}