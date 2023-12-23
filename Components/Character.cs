using Ascendium.Ai;
using Ascendium.Components;
using Ascendium.Types;
using Ascendium.Types.Factories;

public class Character : BaseDrawableGameComponent, IDescribed
{
    public bool Debug = true;

    public CharacterType CharacterType { get; protected set; }

    public ICharacterController Controller { get; protected set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Status
    public int Health { get; set; }

    public int MaxHealth { get; set; }

    public int Mana { get; set; }

    public int MaxMana { get; set; }

    public int Gold { get; set; }

    public int Fatigue { get; set; }

    public int Favor { get; set; }

    public int Level { get; set; } = 1;

    public int Experience { get; set; } = 0;

    public int Attack { get; set; } // Calculated

    public int Defense { get; set; } // Calculated

    public List<Condition> Conditions { get; private set; } 
        = new List<Condition>() { ConditionFactory.GetCondition(ConditionType.OK) };

    // Attributes (5 = "normal person")
    public int StrengthBase { get; set; } = 5;
    public int StrengthModified { get; set; }

    public int SpeedBase { get; set; } = 5;
    public int SpeedModified { get; set; }

    public int StaminaBase { get; set; } = 5;
    public int StaminaModified { get; set; }

    public int DexterityBase { get; set; } = 5;
    public int DexterityModified { get; set; }

    public int AgilityBase { get; set; } = 5;
    public int AgilityModified { get; set; }

    public int IntelligenceBase { get; set; } = 5;
    public int IntelligenceModified { get; set; }

    public int PietyBase { get; set; } = 5;
    public int PietyModified { get; set; }

    public int ConstitutionBase { get; set; } = 5;
    public int ConstitutionModified { get; set; }

    public int VisionBase { get; set; } = 7; // distance in # tiles
    public int VisionModified { get; set; }


    public Dictionary<string, Item> Equipment { get; set; } = new Dictionary<string, Item>();

    public List<Item> Items { get; private set; } = new List<Item>();

    public List<Spell> Spells { get; private set; } = new List<Spell>();

    public List<Ability> Abilities { get; private set; } = new List<Ability>();

    public float MeleeRange { get; set; } = 1;

    public float MissileRange { get; set; } = 3;

    public Character(CharacterType characterType)
    {
        CharacterType = characterType;
        Controller = CreateController(characterType);
    }

    public override void Update()
    {
        base.Update();

        CalculateAttributes();
    }

    public Item GetPrimaryWeapon()
    {
        if (!Equipment.TryGetValue(ItemFactory.SlotPrimary, out Item? item))
        {
            item = ItemFactory.GetItem(ItemType.Nothing);
        }

        return item;
    }

    public Item ToggleWeapon()
    {
        if (!Equipment.TryGetValue(ItemFactory.SlotPrimary, out Item? primary))
        {
            primary = ItemFactory.GetItem(ItemType.Nothing);
        }

        if (!Equipment.TryGetValue(ItemFactory.SlotAlternate, out Item? alternate))
        {
            alternate = ItemFactory.GetItem(ItemType.Nothing);
        }

        Equipment[ItemFactory.SlotPrimary] = alternate;
        Equipment[ItemFactory.SlotAlternate] = primary;

        return alternate; // now this is primary
    }

    public void Damage(int damage)
    {
        Health = Math.Max(Health - damage, 0);

        if (Health < 1)
        {
            Conditions.Clear();
            Conditions.Add(ConditionFactory.GetCondition(ConditionType.Dead));
        }
    }

    public void Heal(int healing)
    {
       Health = Math.Min(Health + healing, MaxHealth); 
    }

    public bool IsDead() => Health <= 0;

    public bool IsLocatedAt(int left, int top)
    {
        return (Left == left && Top == top);
    }

    public int GainExperience(int xp)
    {
        int levelsGained = 0;

        Experience += xp;

        while (Experience >= NextLevelExperience())
        {
            GainLevel();
            levelsGained++;
        }

        return levelsGained;
    }

    public int NextLevelExperience()
    {
        return Level * Level * 20;
    }

    public void RollbackNextLocation()
    {
        NextLeft = Left;
        NextTop = Top;
    }

    public void CommitNextLocation()
    {
        if (NextLeft >= 0) Left = NextLeft;
        if (NextTop >= 0) Top = NextTop;
    }

    public void UseItem(Item item, Character target)
    {
        if (!Items.Contains(item))
        {
            // TODO: return some kind of failure indicator?
            return;
        }

        item.UsesRemaining--;

        if (item.UsesRemaining < 1)
        {
            Items.Remove(item);
        }

    }

    public List<Item> GetItemDrop(Character recipient)
    {
        var items = new List<Item>();
        Random rnd = new Random();

        foreach(Item item in Items)
        {
            if (rnd.Next(100) > 90)
            {
                items.Add(item);
                recipient.Items.Add(item);
            }
        }

        foreach(Item item in items)
        {
            Items.Remove(item);
        }

        if (IsDead())
        {
            var keysToRemove = new List<string>();
            foreach(var kvp in Equipment)
            {
                if (rnd.Next(100) > 90)
                {
                    items.Add(kvp.Value);
                    recipient.Items.Add(kvp.Value);
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach(string key in keysToRemove)
            {
                Equipment.Remove(key);
            }
        }

        return items;
    }

    public Item GetMeleeWeapon()
    {
        var weapon = ItemFactory.GetItem(ItemType.Nothing);

        foreach (var item in Equipment)
        {
            if (item.Value.EquipType == ItemEquipType.MeleeWeapon)
            {
                weapon = item.Value;
                break;
            }
        }

        return weapon;
    }

    public Item GetRangedWeapon()
    {
        Item weapon = ItemFactory.GetItem(ItemType.Nothing);

        foreach (var item in Equipment)
        {
            if (item.Value.EquipType == ItemEquipType.RangedWeapon)
            {
                weapon = item.Value;
                break;
            }
        }

        return weapon;
    }

    public Spell? GetSpellFromName(string name) => Spells.Where(s => s.Name.Equals(name)).FirstOrDefault();

    public List<KeyValuePair<string, int>> GetSpellList()
    {
        var spells = new List<KeyValuePair<string, int>>();
        foreach (Spell spell in Spells)
        {
            spells.Add(new KeyValuePair<string, int>(spell.Name, spell.ManaCost));
        }

        return spells;
    }

    public List<KeyValuePair<string, int>> GetItemList()
    {
        var items = new List<KeyValuePair<string, int>>();
        foreach (Item item in Items)
        {
            items.Add(new KeyValuePair<string, int>(item.Name, item.UsesRemaining));
        }

        return items;
    }


    protected void GainLevel()
    {
        Level += 1;

        // TODO: add attribute bumps
    }

    protected ICharacterController CreateController(CharacterType characterType)
    {
        ICharacterController controller = new StationaryController(this);

        switch (characterType)
        {
            case CharacterType.Player:
                controller = new PlayerController(this);
                break;

            case CharacterType.NPC:
                controller = new NpcController(this);
                break;

            case CharacterType.Enemy:
                controller = new EnemyController(this);
                break;

            default:
                break;
        }

        return controller;
    }

    protected void UpdateConditions()
    {
        var conditionsToRemove = new List<Condition>();
        foreach(Condition condition in Conditions.Where(c => c.TurnsRemaining > 0))
        {
            condition.TurnsRemaining--;

            if (condition.TurnsRemaining == 0)
            {
                conditionsToRemove.Add(condition);
            }
        }

        foreach(Condition condition in conditionsToRemove)
        {
            Conditions.Remove(condition);
        }
    }

    protected void CalculateAttributes()
    {
        UpdateConditions();

        StrengthModified = CalculateStrength();
        SpeedModified = CalculateSpeed();
        StaminaModified = CalculateStamina();
        AgilityModified = CalculateAgility();
        IntelligenceModified = CalculateIntelligence();
        PietyModified = CalculatePiety();
        ConstitutionModified = CalculateConstitution();
        VisionModified = CalculateVision();

        Health = CalculateHealth();
        Mana = CalculateMana();
        Attack = CalculateAttack();
        Defense = CalculateDefense();
    }

    private int CalculateStrength()
    {
        int modified = StrengthBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseStrength)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseStrength)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateSpeed()
    {
        int modified = SpeedBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseSpeed)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseSpeed)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateStamina()
    {
        int modified = StaminaBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseStamina)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseStamina)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateAgility()
    {
        int modified = AgilityBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseAgility)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseAgility)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateIntelligence()
    {
        int modified = IntelligenceBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseIntelligence)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseIntelligence)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculatePiety()
    {
        int modified = PietyBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreasePiety)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreasePiety)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateConstitution()
    {
        int modified = ConstitutionBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseConstitution)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseConstitution)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateVision()
    {
        int modified = VisionBase;

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseVision)
            {
                modified += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseVision)
            {
                modified -= condition.EffectAmount;
            }
        }

        return Math.Max(modified, 0);
    }

    private int CalculateHealth()
    {
        int health = Health;

       foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseHealth)
            {
                health += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseHealth)
            {
                health -= condition.EffectAmount;
            }
        }

        return Math.Min(Math.Max((int)health, 0), MaxHealth);
    }

    private int CalculateMana()
    {
        int mana = Mana;

       foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseMana)
            {
                mana += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseMana)
            {
                mana -= condition.EffectAmount;
            }
        }

        return Math.Min(Math.Max((int)mana, 0), MaxMana);
    }

    private int CalculateAttack()
    {
        Item weapon = GetPrimaryWeapon();
        float attack = weapon.AttackBonus;

        if (weapon.EquipType == ItemEquipType.RangedWeapon)
        {
            attack = (DexterityModified + VisionModified) / 2.0f;  
        }
        else
        {
            attack = (StrengthModified + SpeedModified) / 2.0f;
        }

        foreach(var kvp in Equipment)
        {
            attack += kvp.Value.AttackBonus;
        }

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseAttack)
            {
                attack += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseAttack)
            {
                attack -= condition.EffectAmount;
            }
        }

        return Math.Max((int)attack, 0);
    }

    private int CalculateDefense()
    {
        float defense = AgilityModified;

        foreach(var kvp in Equipment)
        {
            defense += kvp.Value.DefenseBonus;
        }

        foreach(Condition condition in Conditions)
        {
            if (condition.EffectType == EffectType.IncreaseDefense)
            {
                defense += condition.EffectAmount;
            }
            else if (condition.EffectType == EffectType.DecreaseDefense)
            {
                defense -= condition.EffectAmount;
            }
        }

        return Math.Max((int)defense, 0);
    }
}