public class GameStateGame : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.PlayerMotor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game); 
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneChunkGeneration.ScanPosition();
    }
}
