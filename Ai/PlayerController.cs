using Ascendium.Ai.Results;
using Ascendium.Components;
using Ascendium.Types;

namespace Ascendium.Ai;

public class PlayerController : CharacterController
{
    public PlayerController(Character parent)
    : base(parent)
    {
    }
    
    public override BaseResult Update(Room room)
    {
        BaseResult result = new MoveResult();

        if (_parent.IsDead())
        {
            return result;
        }

        var targets = room.GetCharacters();

        foreach(Character character in targets)
        {
            if (character.IsLocatedAt(_parent.NextLeft, _parent.NextTop))
            {
                if (character.IsDead())
                {
                    result.Messages.Add($"You see a dead {character.Name}.");
                }
                else if (character.CharacterType == CharacterType.Enemy)
                {
                    Item meleeWeapon = _parent.GetMeleeWeapon();
                    result = MeleeAttackEnemy(room, character, meleeWeapon);
                }
                else if (character.CharacterType == CharacterType.NPC)
                {
                    result = InteractWithNpc(room, character);
                }

                _parent.RollbackNextLocation();
            }
        }

        _parent.CommitNextLocation();

        return result;
    }

    public AttackResult Attack(Room room)
    {
        var result = new AttackResult(_parent, _parent, AttackType.Melee);

        Character closestAttackableEnemy = null!;
        float closestDistance = float.MaxValue;

        var targets = room.GetCharacters().Where(c => c.IsVisible);
        var enemies = targets.Where(t => t.CharacterType == CharacterType.Enemy && !t.IsDead());
        foreach(Character enemy in enemies)
        {
            if (CanSeeTarget(enemy, room, _parent.GetPrimaryWeapon().Range))
            {
                float distance = DistanceToTarget(enemy);
                if (distance < closestDistance)
                {
                    closestAttackableEnemy = enemy;
                    closestDistance = distance;
                }
            }
        }

        if (closestAttackableEnemy is null)
        {
            result.Messages.Add($"There are no enemies that {_parent.Name} can attack.");
            return result;
        }

        var weapon = _parent.GetPrimaryWeapon();
        if (weapon.EquipType == ItemEquipType.RangedWeapon)
        {
            return RangedAttackEnemy(room, closestAttackableEnemy, weapon);
        }
        else
        {
            return MeleeAttackEnemy(room, closestAttackableEnemy, weapon);
        }
    }

    public MeleeAttackResult MeleeAttackEnemy(Room room, Character target, Item weapon)
    {
        
        var result = new MeleeAttackResult(_parent, target, weapon);

        // TODO: Set other result considerations here

        result.DetermineResult();

        return result;
    }

    public RangedAttackResult RangedAttackEnemy(Room room, Character enemy, Item weapon)
    {
        var result = new RangedAttackResult(_parent, enemy, weapon);

        // TODO: Set other result considerations here

        result.DetermineResult();

        return result;
    }

    public InteractionResult InteractWithNpc(Room room, Character target)
    {
        
        var result = new InteractionResult(_parent, target);

        // TODO: Set other result considerations here

        result.DetermineResult();

        return result;
    }
}