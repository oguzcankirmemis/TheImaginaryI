using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    public GameObject door;
    private Collider myCol;

    void Start()
    {
        myCol = door.GetComponent<Collider>();
    }

    bool isDoor = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isDoor == false)
        {
            myCol.isTrigger = false;
            myCol.gameObject.GetComponent<Renderer>().enabled = true;
            isDoor = true;
        }
    }

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ghost")
        {
            isDoor = false;
            myCol.isTrigger = true;
            myCol.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
