using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager Instance;
    [SerializeField]
    private View _startingView;
    [SerializeField]
    private View[] _views;
    private View _currentView;
    private readonly Stack<View> _history = new Stack<View>();
    private void Awake() => Instance = this;

    public static T GetView<T>() where T: View
    {
        for (int i = 0; i < Instance._views.Length; i++)
        {
            if (Instance._views[i] is T tView)
            {
                return tView;
            }
        }
        return null;
    }

    public static void Show<T>(bool remember = true) where T: View
    {
        for (int i = 0; i < Instance._views.Length; i++)
        {
            if(Instance._views[i] is T)
            {
                if (Instance._currentView != null)
                {
                    if (remember)
                    {
                        Instance._history.Push(Instance._currentView);
                    }
                    Instance._views[i].Show();
                    Instance._currentView = Instance._views[i];
                }
            }
        }
    }

    public static void Show(View view, bool remember = true)
    {
        if(Instance._currentView != null)
        {
            if (remember)
            {
                Instance._history.Push(Instance._currentView);
            }
            Instance._currentView.Hide();
        }
        view.Show();
        Instance._currentView = view;
    }

    public static void ShowLast()
    {
        if(Instance._history.Count != 0)
        {
            Show(Instance._history.Pop(), false);
        }
    }
}
