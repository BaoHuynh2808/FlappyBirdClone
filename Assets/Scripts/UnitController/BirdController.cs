using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState
{
    Idle = 0,
    Flying = 1,
    Hurt = 2,
}

public class BirdController : MonoBehaviour
{
    [SerializeField] private Animator _birdAnimator;

    [SerializeField] private float _birdFlyUpSpeed;
    [SerializeField] private float _birdFallDownSpeed;

    [SerializeField] private SpriteRenderer _birdSpriteRenderer;

    private float _flyMaxConstraint;
    private float _fallMaxConstraint;

    private BirdState birdState;

    public void SetBirdIdle()
    {
        birdState = BirdState.Idle;
        _birdAnimator.Play(Constants.Animation.BirdIdle);
    }

    public void SetBirdFly(float flyHighConstraint, float fallDownConstraint)
    {
        _flyMaxConstraint = flyHighConstraint;
        _fallMaxConstraint = fallDownConstraint;
        GameController.Instance.gameStates = GameStates.Playing;
        birdState = BirdState.Flying;
    }

    // Update is called once per frame
    void Update()
    {
        switch (birdState)
        {
            case BirdState.Flying:
                if (CheckBirdDeath())
                {
                    birdState = BirdState.Hurt;
                    ViewManager.Show(ViewId.GameOverView);
                    break;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // bird fly up
                    gameObject.transform.position += new Vector3(0, 1, 0) * _birdFlyUpSpeed * Time.deltaTime;
                    _birdAnimator.Play(Constants.Animation.BirdFly);
                }
                else
                {
                    // bird fall down
                    gameObject.transform.position += new Vector3(0, -1, 0) * _birdFallDownSpeed * Time.deltaTime;
                    _birdAnimator.Play(Constants.Animation.BirdIdle);
                }
                break;
            case BirdState.Hurt:
                GameController.Instance.gameStates = GameStates.GameOver;
                _birdAnimator.Play(Constants.Animation.BirdKo);
                break;
        }
    }

    /// <summary>
    /// Check bird death
    /// </summary>
    /// <returns></returns>
    private bool CheckBirdDeath()
    {
        //Check bird fly higher constraint
        if ((gameObject.transform.position.y) >= _flyMaxConstraint)
        {
            return true;
        }


        //Check bird fall lowe constraint
        if ((gameObject.transform.position.y) <= _fallMaxConstraint)
        {
            return true;
        }

        if (GameController.Instance.backGroundController.IsBirdCollisionBlock(gameObject.transform.position))
        {
            return true;
        }

        return false;
    }
}
