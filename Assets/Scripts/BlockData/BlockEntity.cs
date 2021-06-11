using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockAnchor
{
    Top,
    Down,
}

public class BlockEntity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _blockChildGroup;

    [SerializeField] private Transform headBlockColliderPosition;
    [SerializeField] private Transform leftBlockColliderPosition;
    [SerializeField] private Transform rightBlockColliderPosition;

    public void SetSpriteForChildBlock(EnvironmentType environmentType, BlockAnchor anchor)
    {
        for (int i = 0; i< _blockChildGroup.Length; i++)
        {
            if (anchor == BlockAnchor.Top)
                _blockChildGroup[i].sprite = ConfigManager.ENVIRONMENTASSETS_DATA[environmentType].topBlockSprite;
            else
                _blockChildGroup[i].sprite = ConfigManager.ENVIRONMENTASSETS_DATA[environmentType].downBlockSprite;
        }
    }

    public float GetBlockHeight()
    {
       return _blockChildGroup[0].bounds.size.y * _blockChildGroup.Length;
    }

    public float GetHaftBlockWidth()
    {
        return _blockChildGroup[0].bounds.size.x;
    }

    public int GetChildGroupCount()
    {
        return _blockChildGroup.Length;
    }

    public float GetOneBlockHeight()
    {
        return _blockChildGroup[0].bounds.size.y;
    }
}
