using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] private Image groundImage;
    [SerializeField] private Image mountainImage;
    [SerializeField] private Image backGround;
    private void Start()
    {
        ViewManager.Show(ViewId.GameScoreView);
        CreateBackGround();
    }

    private void CreateBackGround()
    {
        var type = (EnvironmentType)System.Enum.Parse(typeof(EnvironmentType), PlayerPrefs.GetString("StoredEnumString"));
        groundImage.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[type].floorSprite;
        mountainImage.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[type].mountainSprite;
        backGround.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[type].skypeSprite;
    }
}
