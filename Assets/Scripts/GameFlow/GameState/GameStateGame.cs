public class GameStateGame : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.PlayerMotor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game); 
    }
}
