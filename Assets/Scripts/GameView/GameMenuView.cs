using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuView : BaseView
{
    [SerializeField] private RectTransform upGo;
    [SerializeField] private RectTransform downGo;

    private Vector3 UP_POS => Vector3.up * 1000f;
    private Vector3 DOWN_POS => Vector3.down * 1000f;

    public override ViewId Id => ViewId.GameOverView;

    public override void Initialize(Dictionary<string, object> params_ = null)
    {
        base.Initialize(params_);
        InitUI();
    }

    private void InitUI()
    {
        upGo.localPosition = UP_POS;
        downGo.localPosition = DOWN_POS;
    }

    public override void WalkIn()
    {
        base.WalkIn();
        LeanTween.move(upGo, Vector3.zero, GetWalkInTime()).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(downGo, Vector3.zero, GetWalkInTime()).setEase(LeanTweenType.easeOutQuad);
    }

    public override void WalkOut()
    {
        base.WalkOut();
        LeanTween.move(upGo, UP_POS, GetWalkOutTime()).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(downGo, DOWN_POS, GetWalkOutTime()).setEase(LeanTweenType.easeOutQuad);
    }

    public override float GetWalkOutTime()
    {
        return 0.5f;
    }
    public override float GetWalkInTime()
    {
        return 0.5f;
    }

    public void BtnEvent_LoadScene()
    {
        SceneManager.LoadScene(Constants.ScenePlay);
    }
}
