using System;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    protected PlayerMotor playerMotor;
    
    // entering the state
    public virtual void Construct()
    {
        
    }

    // leaving the state
    public virtual void Destruct()
    {
        
    }

    // switching 
    public virtual void Transition()
    {
        
    }

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public virtual Vector3 ProcessMotion()
    {
        Debug.LogError("Process motion isn't implemented in " + this.ToString());
        return Vector3.zero;
    }
}
