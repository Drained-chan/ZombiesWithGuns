using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieShootBase : MonoBehaviour
{
    private ZombieMovementBase moveBase;

    /// <summary>
    /// If you use Start() in your behavior, you MUST call base.Start()!!!!
    /// </summary>
    virtual protected void Start()
    {
        moveBase = GetComponent<ZombieMovementBase>();

        if(!moveBase)
        {
            DebugUtils.CrashAndBurn("Movement base of some kind is required on " + gameObject.name + "!!!!!");
        }
    }


    /// <summary>
    /// This is provided by the moving behavior.
    /// 
    /// </summary>
    /// <returns>true if the zombie is currently moving, false otherwise.</returns>
    protected bool IsMoving()
    {
        return moveBase.IsMoving();
    }

    /// <summary>
    /// You must implement this method.
    /// </summary>
    /// <returns>true if the zombie is shooting or setting up a shot, false otherwise.</returns>
    abstract public bool IsShooting();
}
