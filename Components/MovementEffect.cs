namespace Ascendium.Components;

public enum MovementEffect
{
    None,       // can move to with no effect
    Blocked,
    MinorDamaged,
    MajorDamaged,
    Killed,
    Teleported,
    Healed,
    Restored,
    Triggered
}