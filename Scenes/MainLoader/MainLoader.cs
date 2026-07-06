using Game.Autoload;
using Godot;

namespace Game.MainLoader;

public partial class MainLoader : Node
{
    [Export(PropertyHint.File, "*.tscn")]
    public string MainMenuScene { get; private set; }

    [Export(PropertyHint.File, "*.tscn")]
    public string ActiveHabitScene { get; private set; }

    public override void _Ready()
    {
        if (HabitManager.Instance.ActiveHabit != null)
        {
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, ActiveHabitScene);
        }
        else
        {
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, MainMenuScene);
        }
    }
}
