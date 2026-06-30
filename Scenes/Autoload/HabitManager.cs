using System;
using Godot;

namespace Game.Autoload;

public partial class HabitManager : Node
{
    public static HabitManager Instance { get; private set; }

    public HabitsData ActiveHabit { get; private set; }

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            Instance = this;
            GetActiveHabit();
        }
    }

    public static void AddNewHabit(NewHabitDTO dto)
    {
        var habit = new HabitsData
        {
            Id = Guid.NewGuid().ToString(),
            HabitName = dto.HabitName,
            CharacterName = dto.CharacterName,
            Duration = dto.Duration,
            Character = dto.Character,
            Level = dto.Level,
            Status = Status.Active,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(dto.Duration)),
        };
        SaveManager.Save(habit);
    }

    public void GetActiveHabit()
    {
        ActiveHabit = SaveManager.SaveData.Find(h => h.Status == Status.Active);
    }
}
