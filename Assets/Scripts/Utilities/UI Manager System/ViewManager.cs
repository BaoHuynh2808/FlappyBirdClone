using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum ViewId
{
    None = -1,

    GameScoreView = 1,
    GameOverView = 2,
    GameMenuView = 3,
}

public class ViewManager : SceneSingleton<ViewManager>
{
    [HideInInspector] public CanvasGroup ThisCanvasGroup;

    public BaseView CurrentView
    {
        get { return viewStack.Count > 0 ? viewStack.Peek() : null; }
    }

    private const string PREFABS_BASE_PATH = "ViewPrefabs/";

    private Dictionary<ViewId, string> _viewsDictionary;
    private Dictionary<ViewId, BaseView> _instantiatedViews;

    [SerializeField] private Stack<BaseView> viewStack;

    //[SerializeField] ViewId _currentViewId;
    void Awake()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        ThisCanvasGroup = GetComponent<CanvasGroup>();
        _instantiatedViews = new Dictionary<ViewId, BaseView>();
        _viewsDictionary = new Dictionary<ViewId, string>
        {
            {ViewId.GameScoreView, "GameScoreView" },
            {ViewId.GameOverView, "GameOverView" },
            {ViewId.GameMenuView, "GameMenuView" },
        };

        viewStack = new Stack<BaseView>();
    }

    public void ClearViewGameObjectList()
    {
        _instantiatedViews.Clear();
    }

    public void AddViewGameObject(BaseView view)
    {
        _instantiatedViews.Add(view.Id, view);
    }

    public static void Show(ViewId viewID_, Dictionary<string, object> @params_ = null)
    {
        Instance._Show(viewID_, @params_);
    }

    public static void ShowAndRun(ViewId viewID_, Dictionary<string, object> @params_ = null, Action onCompleted_ = null)
    {
        Instance._ShowAndRun(viewID_, @params_, onCompleted_);
    }

    void _ShowAndRun(ViewId viewID_, Dictionary<string, object> @params_ = null, Action onCompleted_ = null)
    {
        BaseView view = GetView(viewID_);
        view.gameObject.SetActive(false);

        if (view == null)
        {
            Debug.LogError("View prefab not found " + viewID_.ToString());
            return;
        }

        if (viewStack.Count > 0)
        {
            CurrentView.canvasGroup.blocksRaycasts = false;

            if (view.Id == viewStack.Peek().Id)
            {
                //Debug.LogError("This view is showing: " + viewID_.ToString());
                view.Show();
                view.Initialize(@params_);

                StartCoroutine(ExecuteWalkIn(CurrentView, onCompleted_));
                return;
            }
        }

        view.Show();
        view.Initialize(@params_);
        viewStack.Push(view);

        StartCoroutine(ExecuteWalkIn(CurrentView, onCompleted_));
    }

    void _Show(ViewId viewID_, Dictionary<string, object> @params_ = null)
    {
        BaseView view = GetView(viewID_);
        view.gameObject.SetActive(false);

        if (view == null)
        {
            Debug.LogError("View prefab not found " + viewID_.ToString());
            return;
        }

        if (viewStack.Count > 0)
        {
            CurrentView.canvasGroup.blocksRaycasts = false;

            if (view.Id == viewStack.Peek().Id)
            {
                //Debug.LogError("This view is showing: " + viewID_.ToString());
                view.Show();
                view.Initialize(@params_);
                StartCoroutine(ExecuteWalkIn(CurrentView));
                return;
            }
        }

        view.Show();
        view.Initialize(@params_);
        viewStack.Push(view);

        StartCoroutine(ExecuteWalkIn(CurrentView));
    }

    BaseView GetView(ViewId index_)
    {
        if (_instantiatedViews.ContainsKey(index_))
        {
            return _instantiatedViews[index_];
        }
        else
        {
            BaseView view = FindViewInRoot(index_);
            if (view != null)
            {
                _instantiatedViews.Add(index_, view);
            }
            else
            {
                _instantiatedViews.Add(index_, Instantiate(Resources.Load<BaseView>(PREFABS_BASE_PATH + _viewsDictionary[index_]), transform, false));
            }

            return _instantiatedViews[index_];
        }
    }

    BaseView FindViewInRoot(ViewId viewId_)
    {
        foreach (var item in gameObject.GetComponentsInChildren<BaseView>())
        {
            BaseView view = item.GetComponent<BaseView>();
            if (view.Id.Equals(viewId_))
            {
                return view;
            }
        }
        return null;
    }

    public static void Hide(BaseView view_)
    {
        //if (Instance.CurrentView != null && Instance.CurrentView.Id != ViewId.None && Instance.CurrentView.Id != ViewId.CatPawLoading)
        //{
        //    //DataManager.PreviousViewId = view_.Id;
        //}
        HideAndRun(view_, null);
    }

    public static void Hide()
    {
        //if (Instance.CurrentView != null && Instance.CurrentView.Id != ViewId.None && Instance.CurrentView.Id != ViewId.CatPawLoading)
        //{
        //    //DataManager.PreviousViewId = Instance.CurrentView.Id;
        //}
        HideAndRun(Instance.CurrentView, null);
    }

    public static void HideAndRun(BaseView view_, Action onCompleted_)
    {
        //if (Instance.CurrentView != null && Instance.CurrentView.Id != ViewId.None && Instance.CurrentView.Id != ViewId.CatPawLoading)
        //{
        //    //DataManager.PreviousViewId = view_.Id;
        //}
        Instance._Hide(view_, onCompleted_);
    }

    public static void HideAndRun(Action onCompleted_)
    {
        //if (Instance.CurrentView != null && Instance.CurrentView.Id != ViewId.None && Instance.CurrentView.Id != ViewId.CatPawLoading)
        //{
        //    //DataManager.PreviousViewId = Instance.CurrentView.Id;
        //}
        Instance._Hide(Instance.CurrentView, onCompleted_);
    }

    public static void HideAllAndRun(Action onCompleted_)
    {
        Instance._HideAll(onCompleted_);
    }

    private void _HideAll(Action onCompleted_)
    {
        while (viewStack.Count > 0)
        {
            viewStack.Peek().gameObject.SetActive(false);
            viewStack.Pop();
        }

        onCompleted_?.Invoke();
    }

    void _Hide(BaseView view_, Action onCompleted_)
    {
        //isShowed = false;
        if (view_ == null)
        {
            return;
        }

        if (view_ != CurrentView)
        {
            Debug.LogError("[ViewManager] Hide wrong view!!!");
            return;
        }
        viewStack.Pop();
        if (viewStack.Count > 0)
        {
            CurrentView.canvasGroup.blocksRaycasts = true;
        }
        StartCoroutine(ExecuteWalkOut(view_, onCompleted_));
    }

    IEnumerator ExecuteWalkIn(BaseView view_, Action onCompleted_ = null)
    {
        if (view_ != null)
        {
            float waitTime = view_.GetWalkInTime();
            view_.WalkIn();
            yield return new WaitForSecondsRealtime(waitTime);
            onCompleted_?.Invoke();
        }

        yield break;
    }

    IEnumerator ExecuteWalkOut(BaseView view_, Action onCompleted_ = null)
    {
        if (view_ != null)
        {
            float waitTime = view_.GetWalkOutTime();
            view_.WalkOut();

            yield return new WaitForSecondsRealtime(waitTime);

            view_.Hide();

            onCompleted_?.Invoke();
        }

        yield break;
    }
}
