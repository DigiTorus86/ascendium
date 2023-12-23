using Ascendium.Ai.Results;
using Ascendium.Components;
using Ascendium.Types;

namespace Ascendium.Ai;

public class EnemyController : CharacterController
{
    public EnemyController(Character parent)
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

        Character player = room.GetPlayer();
        float targetDistance = DistanceToTarget(player);
        Item rangedWeapon = _parent.GetRangedWeapon();
        Item meleeWeapon = _parent.GetMeleeWeapon();

        bool canShoot = rangedWeapon.Range > targetDistance;
        bool canMelee = meleeWeapon.Range > targetDistance;

        if (player is null)
        {
            result = Wander(room);
            _parent.CommitNextLocation();
            return result;
        }

        _parent.IsVisible = CanSeeTarget(player, room, player.VisionModified);

        if (_canCast)
        {
            // Cast
        }
        else if (canShoot && CanSeeTarget(player, room, rangedWeapon.Range))
        {
            // Shoot
            result = RangedAttackPlayer(room, rangedWeapon);
        }
        else if (canMelee)
        {
            result = MeleeAttackPlayer(room, meleeWeapon);
        }
        else if (CanSeeTarget(player, room, _parent.VisionModified))
        {
            result = MoveTowardsPlayer(room);
        }
        else
        {
            result = Wander(room);
        }

        _parent.CommitNextLocation();
        return result;
    }

    public MeleeAttackResult MeleeAttackPlayer(Room room, Item weapon)
    {
        Player player = room.GetPlayer();
        var result = new MeleeAttackResult(_parent, player, weapon);

        // TODO: Set other result considerations here

        result.DetermineResult();

        return result;
    }

    public RangedAttackResult RangedAttackPlayer(Room room, Item weapon)
    {
        Player player = room.GetPlayer();
        var result = new RangedAttackResult(_parent, player, weapon);

        // TODO: Set other result considerations here

        result.DetermineResult();

        return result;
    }

}