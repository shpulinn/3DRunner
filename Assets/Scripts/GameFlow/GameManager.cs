using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    public PlayerMotor PlayerMotor;
    
    private GameState _state;

    private void Awake()
    {
        _instance = this;
        _state = GetComponent<GameStateInit>();
        _state.Construct();
    }

    private void Update()
    {
        _state.UpdateState();
    }

    public void ChangeState(GameState state)
    {
        _state.Destruct();
        _state = state;
        _state.Construct();
    }
}
