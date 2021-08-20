using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Derive your zombie movement behaviors from this.
/// </summary>
public abstract class ZombieMovementBase : MonoBehaviour
{
    private ZombieShootBase shootBase;

    /// <summary>
    /// If you use Start() in your behavior, you MUST call base.Start()!!!!
    /// </summary>
    virtual protected void Start()
    {
        shootBase = GetComponent<ZombieShootBase>();

        if(!shootBase)
        {
            DebugUtils.CrashAndBurn("Shooting base of some kind is required on " + gameObject.name + "!!!!!");
        }
    }


    /// <summary>
    /// You must implement this method.
    /// </summary>
    /// <returns>true if the zombie is currently moving, false otherwise.</returns>
    abstract public bool IsMoving();

    /// <summary>
    /// This is provided by the shooting behavior.
    /// </summary>
    /// <returns>true if the zombie is shooting or setting up a shot, false otherwise.</returns>
    protected bool IsShooting()
    {
        return shootBase.IsShooting();
    }
}
