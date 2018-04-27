using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHold : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ghost" && c.gameObject.layer == 2)
        {
            c.gameObject.layer = 8;
        }
    }
}
