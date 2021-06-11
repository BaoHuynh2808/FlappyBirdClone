using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScoreView : BaseView
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CanvasGroup _viewCanvas;
    public override ViewId Id => ViewId.GameScoreView;

    public override void Initialize(Dictionary<string, object> params_ = null)
    {
        base.Initialize(params_);
        InitUI();
    }

    private void InitUI()
    {
        _viewCanvas.alpha = 0;
    }

    public override void WalkIn()
    {
        base.WalkIn();
        LeanTween.alphaCanvas(_viewCanvas, 1, 0.5f).setEase(LeanTweenType.easeOutQuad);
    }

    public override void WalkOut()
    {
        base.WalkOut();
        LeanTween.alphaCanvas(_viewCanvas, 0, 0.5f).setEase(LeanTweenType.easeOutQuad);
    }

    public override float GetWalkInTime()
    {
        return base.GetWalkInTime();
    }

    public override float GetWalkOutTime()
    {
        return base.GetWalkOutTime();
    }

    private void OnUpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        Messenger.AddListener<int>(Constants.GameEvent.OnUpdateScoreText, OnUpdateScoreText);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Constants.GameEvent.OnUpdateScoreText, OnUpdateScoreText);
    }
}
