using System.Text.Json;
using Nipper.DataManager.Models;

namespace Nipper.DataManager.Utilities;

public class SettingsManager
{
    private readonly string cfgPath;
    public SettingsManager()
    {
        cfgPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "appsettings.json");
        cfgPath = Path.GetFullPath(cfgPath);
    }

    public string? GetXlsSavePath()
    {
        var cfg = GetSetting();
        return cfg.XlsOutputFolder;
    }

    public void SetXlsSavePath(string path)
    {
        var cfg = GetSetting();
        cfg.XlsOutputFolder = path;
        string jsonString = JsonSerializer.Serialize(cfg, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(cfgPath, jsonString);
    }

    private Settings GetSetting()
    {
        string jsonString = File.ReadAllText(cfgPath);
        var cfg = JsonSerializer.Deserialize<Settings>(jsonString);
        return cfg ?? throw new Exception("Missing config file");
    }
}
