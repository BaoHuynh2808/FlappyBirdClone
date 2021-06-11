using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupEntity : MonoBehaviour
{
    [SerializeField] private BlockEntity[] blockTops;
    [SerializeField] private BlockEntity[] blockDowns;

    private int currentTopBlockCreatedIndex;
    private int currentDownBlockCreatedIndex;

    public void CreateBlock(EnvironmentType environmentType)
    {
        currentTopBlockCreatedIndex = Random.Range(0, blockTops.Length);
        currentDownBlockCreatedIndex = Random.Range(0, blockDowns.Length);

        blockTops[currentTopBlockCreatedIndex].gameObject.SetActive(true);
        blockTops[currentTopBlockCreatedIndex].SetSpriteForChildBlock(environmentType, BlockAnchor.Top);

        blockDowns[currentDownBlockCreatedIndex].gameObject.SetActive(true);
        blockDowns[currentDownBlockCreatedIndex].SetSpriteForChildBlock(environmentType, BlockAnchor.Down);
    }

    public void BlockMove()
    {
        SwapBlock();
        gameObject.transform.position += new Vector3(-1, 0, 0) * Constants.BlockMoveSpeed * Time.deltaTime;
    }

    private void SwapBlock()
    {
        if (gameObject.transform.position.x <= GameController.Instance.backGroundController.GetOffSetXCollectBlock())
        {
            gameObject.transform.position = new Vector3(GameController.Instance.backGroundController.GetOffSetXReturnBlock(), gameObject.transform.position.y, gameObject.transform.position.z);

            blockTops[currentTopBlockCreatedIndex].gameObject.SetActive(false);
            blockDowns[currentDownBlockCreatedIndex].gameObject.SetActive(false);

            CreateBlock(EnvironmentType.Desert);
        }
    }

    public bool IsCollisionTopBlock(Vector3 birdPosition)
    {
        if (birdPosition.x >= GetLeftBlockPoxX() && birdPosition.x <= GetRightBlockPoxX() && birdPosition.y >= GetTopBlockPosY() + 0.2f)
            return true;

        return false;
    }

    public bool IsCollisionDownBlock(Vector3 birdPosition)
    {
        if (birdPosition.x >= GetLeftBlockPoxX() && birdPosition.x <= GetRightBlockPoxX() && birdPosition.y <= GetDownBlockPosY() - 0.2f)
            return true;

        return false;
    }

    public bool IsBirdVeryFarThisBlockGroup(Vector3 birdPosition)
    {
        if (birdPosition.x >= GetRightBlockPoxX())
        {
            return true;
        }

        return false;
    }

    private float GetLeftBlockPoxX()
    {
        return this.gameObject.transform.position.x - blockTops[currentTopBlockCreatedIndex].GetHaftBlockWidth() + 0.2f;
    }

    private float GetRightBlockPoxX()
    {
        return this.gameObject.transform.position.x + blockTops[currentTopBlockCreatedIndex].GetHaftBlockWidth();
    }

    private float GetTopBlockPosY()
    {
        float temp = blockTops[currentTopBlockCreatedIndex].gameObject.transform.position.y - blockTops[currentTopBlockCreatedIndex].GetBlockHeight() / 2;
        return this.gameObject.transform.position.y + temp;
    }

    private float GetDownBlockPosY()
    {
        float temp = Mathf.Abs(blockDowns[currentDownBlockCreatedIndex].transform.position.y) - blockDowns[currentDownBlockCreatedIndex].GetBlockHeight();
        return this.gameObject.transform.position.y - temp;
    }
}
