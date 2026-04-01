using UnityEngine;

//keeps track of which hint is currently showing so only one can be visible at a time
public static class HintManager
{

    private static CrewRoomWall _crewHint;
    private static HatchDoor _hatchHint;
    private static EngineRoomWall _engineHint;

    public static void Register(CrewRoomWall wall)
    {

        HideAll();
        _crewHint = wall;

    }

    public static void Register(HatchDoor hatch)
    {

        HideAll();
        _hatchHint = hatch;

    }

    public static void Register(EngineRoomWall wall)
    {

        HideAll();
        _engineHint = wall;

    }

    public static void HideAll()
    {

        if (_crewHint != null) _crewHint.HideHint();
        if (_hatchHint != null) _hatchHint.HideHint();
        if (_engineHint != null) _engineHint.HideHint();

        _crewHint = null;
        _hatchHint = null;
        _engineHint = null;

    }

}