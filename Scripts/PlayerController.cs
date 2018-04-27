using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    private Vector3 velocity;
    private float sidewaysInput, jumpInput;


    [System.Serializable]
    public class InputSettings
    {
        public string SIDEWAYS_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }


    // Use this for initialization
    void Start () {
        velocity = Vector3.zero;
        sidewaysInput = jumpInput = 0;
        moveSettings = GetComponent<MoveSettings>();
    }
	
	// Update is called once per frame
	void Update () {
        GetInput();
    }

    void GetInput()
    {
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        if (inputSettings.JUMP_AXIS.Length != 0)
            jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
    }

    void Run()
    {
        float run = sidewaysInput * moveSettings.runVelocity;
        //if (reverse)
        //    velocity.x = -velocity.x;
        gameObject.GetComponent<CharacterController>().SimpleMove(new Vector3(run, 0, 0));
        //playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Jump()
    {
        if (jumpInput != 0 && gameObject.GetComponent<CharacterController>().isGrounded)
        {
            gameObject.GetComponent<CharacterController>().SimpleMove(new Vector3(0, moveSettings.jumpVelocity, 0));
        }

    }
}
