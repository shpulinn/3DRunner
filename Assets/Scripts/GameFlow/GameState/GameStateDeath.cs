using Inputs;

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
        GameManager.Instance.PlayerMotor.RespawnPlayer();
        brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
