using System;

namespace Game;


public enum Character
{
    //enum for characters available
    Animal,
    Plant
}

public enum Level
{
    Easy,
    Medium,
    Hard,
    LockedIn
}

public enum Status
{
    Active,
    Failed,
    Succeeded,
}

public static class Util
{
    public static string GenerateUUID()
    {
        return Guid.NewGuid().ToString();
    }
}
