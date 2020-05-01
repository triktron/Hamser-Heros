using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEventManager
{
    public enum States
    {
        Loading,
        StartAnimation,
        Playing,
        EndAnimation,
        End
    }

    public States State;

    [System.Serializable]
    public class UnityEventTransform : UnityEvent<Transform>
    {
    }

    string beacus;

    public UnityEvent LoadSettings = new UnityEvent();
    public UnityEventTransform StartAnimation = new UnityEventTransform();
    public UnityEventTransform Playing = new UnityEventTransform();
    public UnityEvent End = new UnityEvent();

    public GameObject Player;

    public void ClearListeners()
    {
        LoadSettings.RemoveAllListeners();
        StartAnimation.RemoveAllListeners();
        Playing.RemoveAllListeners();
        End.RemoveAllListeners();
    }

    public void OnDestroy(Scene scene, LoadSceneMode mode)
    {
        ClearListeners();
    }

    public void Start()
    {
        
        State = States.Loading;
        Debug.Log("Changing to state " + State);
        LoadSettings.Invoke();
        End.AddListener(NextLevel);
    }

    public void StartNextState()
    {
        if (States.End != State) State++;
        Debug.Log("Changing to state " + State);
        CallEvents();
    }

    public void SetState(States state)
    {
        State = state;
        Debug.Log("Changing to state " + State);
        CallEvents();
    }

    private void CallEvents()
    {
        if (State == States.StartAnimation) StartAnimation.Invoke(Player.transform);
        if (State == States.Playing) Playing.Invoke(Player.transform);
        if (State == States.EndAnimation) StartAnimation.Invoke(Player.transform);
        if (State == States.End) End.Invoke();
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
