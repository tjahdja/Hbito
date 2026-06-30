using System;
using System.Collections.Generic;
using Game.Autoload;
using Godot;

namespace Game.NewHabit;

public partial class NewHabit : Control
{
    private LineEdit habitName;
    private LineEdit duration;
    private LineEdit level;
    private LineEdit characterName;
    private LineEdit character;
    private Button saveButton;

    public override void _Ready()
    {
        habitName = GetNode<LineEdit>("%HabitName");
        duration = GetNode<LineEdit>("%Duration");
        level = GetNode<LineEdit>("%Level");
        characterName = GetNode<LineEdit>("%CharacterName");
        character = GetNode<LineEdit>("%Character");
        saveButton = GetNode<Button>("%SaveButton");
        saveButton.Pressed += OnSaveButtonPressed;
    }

    private void OnSaveButtonPressed()
    {
        string habitNameText = habitName.Text;
        int durationInt = int.TryParse(duration.Text, out int durationValue) ? durationValue : 0;
        string levelText = level.Text;
        string characterNameText = characterName.Text;
        string characterText = character.Text;

        HabitManager.AddNewHabit(new NewHabitDTO
        {
            HabitName = habitNameText,
            Duration = durationInt,
            Level = Level.Easy,
            CharacterName = characterNameText,
            Character = Character.Animal
        });
    }

}
