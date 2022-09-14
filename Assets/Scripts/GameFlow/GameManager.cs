using System;
using UnityEngine;

public enum GameCamera
{
    Init = 0,
    Game = 1,
    Shop = 2,
    Respawn = 3
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    public PlayerMotor PlayerMotor;
    public WorldGeneration.WorldGeneration WorldGeneration;
    public WorldGeneration.SceneChunkGeneration SceneChunkGeneration;
    public GameObject[] cameras;
    
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

    public void ChangeCamera(GameCamera cam)
    {
        // disabling all cameras and enabling the chosen one ("cam")
        foreach (GameObject go in cameras)
        {
            go.SetActive(false);
        }
        
        cameras[(int)cam].SetActive(true);
    }
}
