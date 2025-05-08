using UnityEngine;

public static class IceAxeClimbManager
{
    private static IceAxe lastPressedAxe;

    public static void RegisterTriggerPress(IceAxe axe)
    {
        lastPressedAxe = axe;
    }

    public static bool CanClimb(IceAxe axe)
    {
        return axe == lastPressedAxe;
    }

    public static void Clear()
    {
        lastPressedAxe = null;
    }
}
