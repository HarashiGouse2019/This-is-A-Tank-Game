using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PowerUp
{
    public TankData pawn;
    public float duration;

    /// <summary>
    /// As a Power Up is taking effect.
    /// </summary>
    public abstract void OnActive();

    /// <summary>
    /// What to do when the Power Up is applied to a specific object.
    /// </summary>
    public abstract void OnApply(GameObject obj);

    /// <summary>
    /// What to do when the Power Up is removed from a specific object.
    /// </summary>
    public abstract void OnRemove(GameObject obj);


}
