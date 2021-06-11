using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEntity : MonoBehaviour
{
    [SerializeField] private CloudGroupController _cloudGroupController;
    [SerializeField] private MidGroundGroupController _midGroundGroupController;
    [SerializeField] private ForGroundGroupController _forGroundGroupController;

    public void SetUpEnvironment()
    {
        _cloudGroupController.CreateCloud();
        _midGroundGroupController.CreateMidGround();
        _forGroundGroupController.CreateForGround();
    }

    public void EnvironmentMove()
    {
        _cloudGroupController.MoveCloud();
        _midGroundGroupController.MoveMidGround();
        _forGroundGroupController.MoveForGround();
    }
}
