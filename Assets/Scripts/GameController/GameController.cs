using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Start,
    Playing,
    GameOver,
}

public class GameController : SceneSingleton<GameController>
{
    public BirdController birdController;

    [SerializeField] private Transform _sceneMaxHeightPos;
    [SerializeField] private Transform _sceneMinHeightPos;

    public BackGroundController backGroundController;

    public GameStates gameStates;

    private int gameScore;

    public EnvironmentType environmentType;

    private void Start()
    {
        gameStates = GameStates.Start;
        environmentType = (EnvironmentType)System.Enum.Parse(typeof(EnvironmentType), PlayerPrefs.GetString("StoredEnumString"));
        backGroundController.CreateMap();
        float birdFlyConstraint = _sceneMaxHeightPos.localPosition.y;
        float birdFallMinConstraint = _sceneMinHeightPos.localPosition.y;
        gameScore = 0;
        birdController.SetBirdFly(birdFlyConstraint, birdFallMinConstraint);
    }

    public void IncreaseGameScore()
    {
        gameScore += 5;
        Messenger.Broadcast<int>(Constants.GameEvent.OnUpdateScoreText, gameScore);
    }

    public int GetGameScore()
    {
        return gameScore;
    }
}
