using System;
using System.Collections.Generic;

namespace Game;

public class HabitsData
{
    public string Id { get; set; }
    public string HabitName { get; set; }
    public string CharacterName { get; set; }
    public int Duration { get; set; }
    public Character Character { get; set; }
    public Level Level { get; set; }
    public Status Status { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<DateOnly> CompletionLogs { get; set; }
}


public class NewHabitDTO
{
    public string HabitName { get; set; }
    public string CharacterName { get; set; }
    public int Duration { get; set; }
    public Character Character { get; set; }
    public Level Level { get; set; }

}
