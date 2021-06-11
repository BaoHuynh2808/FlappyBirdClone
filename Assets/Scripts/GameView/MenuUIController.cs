using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUIController : MonoBehaviour
{
    [SerializeField] private Image groundImage;
    [SerializeField] private Image mountainImage;
    [SerializeField] private Image backGround;
    // Start is called before the first frame update
    void Start()
    {
        EnvironmentType randomEnv = (EnvironmentType)Random.Range(0, 3);
        PlayerPrefs.SetString("StoredEnumString", randomEnv.ToString());
        groundImage.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[randomEnv].floorSprite;
        mountainImage.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[randomEnv].mountainSprite;
        backGround.sprite = ConfigManager.ENVIRONMENTASSETS_DATA[randomEnv].skypeSprite;

        ViewManager.Show(ViewId.GameMenuView);
    }
}
