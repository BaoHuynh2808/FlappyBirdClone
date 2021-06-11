using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnvironmentAsset", menuName ="Environment")]
public class EnvironmentAsset : ScriptableObject
{
    public EnvironmentType environmentType;
    public Sprite topBlockSprite;
    public Sprite downBlockSprite;
    public Sprite cloudSprite;
    public Sprite mountainSprite;
    public Sprite forGroundSprite;
    public Sprite midGroundSpirte;
    public Sprite floorSprite;
    public Sprite skypeSprite;
}
