﻿using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using Nipper.DataManager.Models;

namespace Nipper.DataManager.Utilities;

public class SettingsManager
{
    private string cfgPath;
    public SettingsManager()
    {
        cfgPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "appsettings.json");
        cfgPath = Path.GetFullPath(cfgPath);
    }

    public string? GetXlsPath()
    {
        var cfg = GetSetting();
        return cfg.XlsOutputFolder;
    }

    public void SetXlsPath(string path)
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
        if (cfg == null)
        {
            throw new Exception("Missing config file");
        }
        return cfg;
    }
}
