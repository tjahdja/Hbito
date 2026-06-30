using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Game.Autoload;

public partial class SaveManager : Node
{
    public static SaveManager Instance { get; private set; }

    public static List<HabitsData> SaveData { get; private set; } = new();
    private static readonly string SAVE_FILE_PATH = "user://save.json";

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            Instance = this;
            LoadSaveData();
        }
    }

    public static void Save(HabitsData data)
    {
        SaveData.Add(data);
        WriteSaveData();
    }

    private static void WriteSaveData()
    {
        var dataString = JsonConvert.SerializeObject(SaveData);

        using var saveFile = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Write);
        saveFile.StoreLine(dataString);
    }

    private static void LoadSaveData()
    {
        if (!FileAccess.FileExists(SAVE_FILE_PATH))
        {
            return;
        }

        using var saveFile = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Read);
        var dataString = saveFile.GetLine();

        try
        {
            SaveData = JsonConvert.DeserializeObject<List<HabitsData>>(dataString);
        }
        catch (Exception)
        {
            GD.PushWarning("Save file json was corrupted");
        }
    }
}
