using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveSettings : MonoBehaviour
{
    public float runVelocity = 8f;
    public float jumpVelocity = 10f;
    public float distanceToGround = 0.4f;
    public LayerMask ground;
}

