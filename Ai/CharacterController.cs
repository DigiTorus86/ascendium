using Ascendium.Ai.Results;
using Ascendium.Components;
using Ascendium.Types;

namespace Ascendium.Ai;

public abstract class CharacterController : ICharacterController
{
    protected Character _parent;
    protected bool _canShoot;
    protected bool _canCast;

    public CharacterController(Character parent)
    {
        _parent = parent;
    }

    public virtual BaseResult Update(Room room)
    {
        BaseResult result = new MoveResult();

        if (_parent.IsDead())
        {
            return result;
        }

        // Default behavior for characters
        result = Wander(room);

        _parent.CommitNextLocation();
        return result;
    }

    protected MoveResult Wander(Room room)
    {
        var result = new MoveResult();

        // Only move about 50% of the time
        var rnd = new Random();
        if (rnd.Next(100) > 50)
        {
            return result;
        }

        var targets = room.GetCharacters();

        for (int i = 0; i < 4; i++)
        {
            var (left, top) = GetRandomDirection();
            int targetLeft = _parent.Left + left;
            int targetTop = _parent.Top + top;

            if (room.CanMoveTo(targetLeft, targetTop) && 
                !targets.Any(t => t.Left == targetLeft && t.Top == targetTop))
            {
                _parent.NextLeft = targetLeft;
                _parent.NextTop = targetTop;
                result.Moved = true;
                break;
            }
        }

        return result;
    }

    protected MoveResult MoveTowardsPlayer(Room room)
    {
        var result = new MoveResult();

        Player player = room.GetPlayer();
        List<Character> characters = room.GetCharacters();

        int leftDiff = player.Left - _parent.Left;
        int topDiff = player.Top - _parent.Top;

        int leftSign = Math.Sign(leftDiff);
        int topSign = Math.Sign(topDiff);

        if ((Math.Abs(leftDiff) > Math.Abs(topDiff)) &&
            room.CanMoveTo(_parent.Left + leftSign, _parent.Top) && 
            !characters.Any(t => t.Left == _parent.Left + leftSign && t.Top == _parent.Top))
        {
            _parent.NextLeft = _parent.Left + leftSign;
            _parent.NextTop = _parent.Top;
            result.Moved = true;
        }
        else if (room.CanMoveTo(_parent.Left, _parent.Top + topSign) && 
            !characters.Any(t => t.Left == _parent.Left && t.Top == _parent.Top + topSign))
        {
            _parent.NextLeft = _parent.Left;
            _parent.NextTop = _parent.Top + topSign;
            result.Moved = true;
        }
        else
        {
            result = Wander(room);
        }

        return result;
    }

    protected Character? GetClosestTarget(IList<Character> targets)
    {
        Character? closestTarget = null;
        double minDistance = 0;

        foreach(Character character in targets)
        {
            float distance = DistanceToTarget(character);
            if (closestTarget is null || distance < minDistance)
            {
                closestTarget = character;
                minDistance = distance;
            }
        }

        return closestTarget;
    }

    protected float DistanceToTarget(Character? target)
    {
        if (target is null)
        {
            return float.MaxValue;
        }

        // Use Pythagoras to determine linear distance to target
        return (float)Math.Sqrt(((target.Top - _parent.Top) * (target.Top - _parent.Top)) + 
                                ((target.Left - _parent.Left) * (target.Left - _parent.Left)));
    }

    protected bool CanSeeTarget(Character? target, Room room, float maxDistance)
    {
        if (target is null)
        {
            return false;
        }

        return room.HasLineOfSight(_parent.Left, _parent.Top, target.Left, target.Top, maxDistance + room.VisionModifier);
    }

    protected bool CanShootTarget(Character? target, Room room, float weaponRange)
    {
        if (target is null)
        {
            return false;
        }

        return room.HasLineOfSight(_parent.Left, _parent.Top, target.Left, target.Top, weaponRange);
    }

    protected static Character GetPlayer(IList<Character> targets)
    {
        return targets.Where(t => t.CharacterType == CharacterType.Player).First();
    }

    protected static (int left, int top) GetRandomDirection()
    {
        int left = 0;
        int top = 0;

        Random rnd = new Random();
        int dir = rnd.Next(4);

        switch (dir)
        {
            case 0: // up
                top = -1;
                break;

            case 1: // right
                left = 1;
                break;

            case 2: // down
                top = 1;
                break;

            case 3: // left
                left = -1;
                break;
        }

        return (left, top);
    }
}