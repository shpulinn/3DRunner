using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 knockbackForce = new Vector3(0, 4, -3);

    private Vector3 _currentKnockback;
    
    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Death");
        _currentKnockback = knockbackForce;
    }

    public override Vector3 ProcessMotion()
    {
        //Vector3 move = knockbackForce;

        _currentKnockback = new Vector3(0,
            _currentKnockback.y - playerMotor.gravity * Time.deltaTime,
            _currentKnockback.z += 2.0f * Time.deltaTime);

        if (_currentKnockback.z > 0)
        {
            _currentKnockback.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
            _currentKnockback = Vector3.zero;
        }

        return _currentKnockback;
    }
}
