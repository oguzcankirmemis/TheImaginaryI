using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    public GameObject goPoint;

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ghost")
        {
            c.gameObject.transform.position = goPoint.transform.position;
        }

    }
}
