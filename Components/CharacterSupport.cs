
using Ascendium.Components;

public static class CharacterSupport
{
    public static Character? GetNearestVisbleCharacter(int left, int top, bool onlyAlive, IEnumerable<Character> characters)
    {
        Character? nearestCharacter = null;

        List<Character> matchingCharacters = 
            characters.Where(c => c.IsVisible && (!c.IsDead() || !onlyAlive)).ToList();

        if (!matchingCharacters.Any())
        {
            return nearestCharacter;
        }

        float minDistance = float.MaxValue;
        foreach(Character character in matchingCharacters)
        {
            float distance = Room.LinearDistance(left, top, character.Left, character.Top);
            if (distance < minDistance)
            {
                nearestCharacter = character;
                minDistance = distance;
            }
        }

        return nearestCharacter;
    }
}