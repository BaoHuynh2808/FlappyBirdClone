using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public static Dictionary<EnvironmentType, EnvironmentAsset> ENVIRONMENTASSETS_DATA;

    private void Awake()
    {
        ENVIRONMENTASSETS_DATA = new Dictionary<EnvironmentType, EnvironmentAsset>();
        EnvironmentAsset[] levelEntities = Resources.LoadAll<EnvironmentAsset>("EnvironmentAssets/");
        foreach (var ev in levelEntities)
        {
            ENVIRONMENTASSETS_DATA.Add(ev.environmentType, ev);
        }
    }
    
    /// <summary>
    /// Get environment asset
    /// </summary>
    /// <param name="envType"></param>
    /// <returns></returns>
    public static EnvironmentAsset GetEnvironmentAsset(EnvironmentType envType)
    {
        return ENVIRONMENTASSETS_DATA[envType];
    }
}
