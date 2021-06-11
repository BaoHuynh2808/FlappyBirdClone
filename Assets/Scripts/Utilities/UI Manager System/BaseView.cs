using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    public abstract ViewId Id { get; }

    [HideInInspector] public CanvasGroup canvasGroup;

    public virtual void Initialize(Dictionary<string, object> @params_ = null)
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public virtual void WalkIn()
    {
        ViewManager.Instance.ThisCanvasGroup.blocksRaycasts = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(WaitWalkCompleted(GetWalkInTime()));
    }

    public virtual void WalkOut()
    {
        ViewManager.Instance.ThisCanvasGroup.blocksRaycasts = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(WaitWalkCompleted(GetWalkOutTime()));
    }

    public virtual float GetWalkInTime()
    { return 0.5f; }

    public virtual float GetWalkOutTime()
    { return GetWalkInTime(); }

    IEnumerator WaitWalkCompleted(float duration_)
    {
        yield return new WaitForSecondsRealtime(duration_);
        ViewManager.Instance.ThisCanvasGroup.blocksRaycasts = true;
        canvasGroup.blocksRaycasts = true;
    }

    // update for sairento
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void BlockRaycasts()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void UnblockRaycasts()
    {
        canvasGroup.blocksRaycasts = false;
    }
}