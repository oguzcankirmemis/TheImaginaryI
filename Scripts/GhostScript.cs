using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GhostScript : MonoBehaviour, ITR
{
    private class Status : TRObject
    {
        public Vector3 myPosition;
        public Quaternion myRotation;
    }

    public GameObject player;
    public GameObject master;
    public MyStatus[] playersArr;
    public int counter = 0;
    PlayerBehaviour instance;

    public TimeReverse trscript;

    public LayerMask ground;
    public float distanceToGround = 0.4f;

    private Vector3 start = new Vector3(-5.79f, 0.07f, -0.9279823f);
    private float jumpInput;
    private float sidewaysInput;
    private Vector3 velocity;
    private Rigidbody playerRigidbody;

    public MoveSettings moveSettings;

    void Awake()
    {
        velocity = Vector3.zero;
        sidewaysInput = jumpInput = 0;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        moveSettings = GetComponent<MoveSettings>();
    }

    void Start()
    {
        master = GameObject.Find("Master");
        transform.parent = master.transform;
        player = GameObject.Find("player");
        instance = player.GetComponent<PlayerBehaviour>();
        playersArr = instance.getPlayerOnQueue();
        instance.clearQueue();
        trscript = GetComponent<TimeReverse>();
        start = instance.manualStart;
        transform.position = start;

    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down,
            distanceToGround, ground);
    }

    void FixedUpdate()
    {
        if (!playerRigidbody.isKinematic)
        {
            Run();
            Jump();
        }
        if (playersArr != null && playersArr.Length >= 1 && counter < playersArr.Length)
        {
            MyStatus tmp = playersArr[counter];
            jumpInput = tmp.jumpInput;
            sidewaysInput = tmp.sidewaysInput;
            counter++;
        }
        else if (counter == playersArr.Length && Grounded())
        {
            playerRigidbody.isKinematic = true;
            instance.setActiveGhost();
        }
        else if (!Grounded())
        {
            playerRigidbody.isKinematic = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ammo")
        { 
            Destroy(c.gameObject);
        }
    }

    void Run()
    {
        velocity.x = sidewaysInput * moveSettings.runVelocity;
        velocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,
                moveSettings.jumpVelocity, playerRigidbody.velocity.z);
        }

    }

    public void SaveTRObject()
    {
        Status status = new Status();
        status.myRotation = transform.rotation;
        status.myPosition = transform.position;
        trscript.PushTRObject(status);
    }

    public void LoadTRObject(TRObject trobject)
    {
        Status newStatus = (Status)trobject;
        transform.rotation = newStatus.myRotation;
        transform.position = newStatus.myPosition;
    }
}
