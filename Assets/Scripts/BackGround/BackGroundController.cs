using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum EnvironmentType
{
    Desert,
    PinkCaro,
    BlueSky,
}

public class BackGroundController : Singleton<BackGroundController>
{
    [SerializeField] private EnvironmentEntity[] environmentEntities;

    [SerializeField] private BlockGroupEntity[] blockGroupEntities;

    [SerializeField] private float offsetXCollectBackGround;
    [SerializeField] private float offsetXReturnBackGround;

    [SerializeField] private float offsetXCollectBlock;
    [SerializeField] private float offsetXReturnBlock;

    private int birdCheckPoint;

    public void CreateMap()
    {
        // create environment
        for (int i = 0; i < environmentEntities.Length; i++)
        {
            environmentEntities[i].SetUpEnvironment();
        }

        // create block
        for (int i = 0; i < blockGroupEntities.Length; i++)
        {
            blockGroupEntities[i].CreateBlock(GameController.Instance.environmentType);
        }

        birdCheckPoint = 0;
    }

    private void Update()
    {
        if (GameController.Instance.gameStates == GameStates.Playing)
        {
            HandleEnvironmentMove();
            HandleBlockMove(); ;
        }
    }

    private void HandleEnvironmentMove()
    {
        for (int i = 0; i < environmentEntities.Length; i++)
        {
            environmentEntities[i].EnvironmentMove();
        }
    }

    private void HandleBlockMove()
    {
        for (int i = 0; i < blockGroupEntities.Length; i++)
        {
            blockGroupEntities[i].BlockMove();
        }
    }

    public float GetOffsetXCollectBackGround()
    {
        return offsetXCollectBackGround;
    }

    public float GetOffsetXReturnBackGround()
    {
        return offsetXReturnBackGround;
    }

    public float GetOffSetXCollectBlock()
    {
        return offsetXCollectBlock;
    }

    public float GetOffSetXReturnBlock()
    {
        return offsetXReturnBlock;
    }

    public bool IsBirdCollisionBlock(Vector3 birdPosition)
    {
        IncreaseCheckPoint(birdPosition);

        if (blockGroupEntities[birdCheckPoint].IsCollisionTopBlock(birdPosition))
            return true;
        if (blockGroupEntities[birdCheckPoint].IsCollisionDownBlock(birdPosition))
            return true;

        return false;
    }

    public bool IsAtCheckPointToIncreaseScore(Vector3 birdPosition)
    {
        if (blockGroupEntities[birdCheckPoint].IsBirdVeryFarThisBlockGroup(birdPosition))
            return true;

        return false;
    }

    private void IncreaseCheckPoint (Vector3 birdPosition)
    {
        if (blockGroupEntities[birdCheckPoint].IsBirdVeryFarThisBlockGroup(birdPosition))
        {
            GameController.Instance.IncreaseGameScore();
            birdCheckPoint++;
            if (birdCheckPoint >= blockGroupEntities.Length)
                birdCheckPoint = 0;
        }
    }
}
