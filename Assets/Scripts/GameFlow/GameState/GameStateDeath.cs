using Inputs;
using UnityEngine;

public class GameStateDeath : GameState
{
    public override void UpdateState()
    {
        if (InputManager.Instance.SwipeDown)
        {
            ToMenu();
        }

        if (InputManager.Instance.SwipeUp)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.PlayerMotor.RespawnPlayer();
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
        GameManager.Instance.PlayerMotor.ResetPlayer();
        GameManager.Instance.WorldGeneration.ResetWorld();
    }
}
