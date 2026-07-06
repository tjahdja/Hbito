using System;
using System.Collections.Generic;
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

    private void GetActiveHabit()
    {
        var uncheckActiveHabit = SaveManager.SaveData.Find(h => h.Status == Status.Active);
        if (uncheckActiveHabit != null)
        {
            int allowedGrace = GetAllowedGracePeriod(uncheckActiveHabit.Level);
            int maxMissedDays = CheckActiveHabitStreak(uncheckActiveHabit.CompletionLogs, uncheckActiveHabit.StartDate);
            if (maxMissedDays > allowedGrace)
            {
                SaveManager.DeactivateHabit(uncheckActiveHabit.Id);
            }
            else
            {
                ActiveHabit = uncheckActiveHabit;
            }
        }
    }

    private static int CheckActiveHabitStreak(List<DateOnly> dates, DateOnly startDate)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        if (dates == null || dates.Count == 0)
        {
            if (today <= startDate) return 0;
            return today.DayNumber - startDate.DayNumber;
        }
        int maxGap = 0;
        for (int i = 1; i < dates.Count; i++)
        {
            int currentGap = dates[i].DayNumber - dates[i - 1].DayNumber - 1;
            if (currentGap > maxGap)
            {
                maxGap = currentGap;
            }
        }
        DateOnly lastCheckIn = dates[^1];
        int currentLiveGap = today.DayNumber - lastCheckIn.DayNumber - 1;
        return Math.Max(maxGap, currentLiveGap);
    }

    private static int GetAllowedGracePeriod(Level level)
    {
        return level switch
        {
            Level.Easy => 3,
            Level.Medium => 2,
            Level.Hard => 1,
            Level.LockedIn => 0,
            _ => 0
        };
    }
}
