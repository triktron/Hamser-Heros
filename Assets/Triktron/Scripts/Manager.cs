using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager main;

    void Awake()
    {
        main = this;
    }

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

    [System.Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2>
    {
    }
    

    public UnityEvent LoadSettings = new UnityEvent();
    public UnityEventTransform StartAnimation = new UnityEventTransform();
    public UnityEventTransform Playing = new UnityEventTransform();
    public UnityEvent End = new UnityEvent();

    public GameObject Player;
    public UnityEventVector2 PlayerMove = new UnityEventVector2();

    public void Start()
    {
        SetState(States.Loading);
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
        if (State == States.Loading) LoadSettings.Invoke();
        if (State == States.StartAnimation) StartAnimation.Invoke(Player.transform);
        if (State == States.Playing) Playing.Invoke(Player.transform);
        if (State == States.EndAnimation) StartAnimation.Invoke(Player.transform);
        if (State == States.End) End.Invoke();
    }

    public void NextLevel()
    {
        Timer.ResetTimer();
        SceneLoader.main.LoadLevel("Menu");
    }

    public void Move(InputAction.CallbackContext context)
    {
        PlayerMove.Invoke(context.ReadValue<Vector2>());
    }
    public void Move(Vector2 axis)
    {
        PlayerMove.Invoke(axis);
    }
}
