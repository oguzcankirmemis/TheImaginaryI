using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLever : MonoBehaviour
{
    public GameObject passage;
    public GameObject passage2;
    public GameObject end;

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Ghost" && c.gameObject.layer == 8)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                passage.GetComponent<Collider>().isTrigger = true;
                passage.GetComponent<Renderer>().enabled = false;
                passage2.GetComponent<Collider>().isTrigger = true;
                passage2.GetComponent<Renderer>().enabled = false;
                end.GetComponent<Collider>().enabled = false;
                end.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                passage.GetComponent<Collider>().isTrigger = false;
                passage.GetComponent<Renderer>().enabled = true;
                passage2.GetComponent<Collider>().isTrigger = false;
                passage2.GetComponent<Renderer>().enabled = true;
                end.GetComponent<Collider>().enabled = true;
                end.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        passage.GetComponent<Collider>().isTrigger = false;
        passage.GetComponent<Renderer>().enabled = true;
        passage2.GetComponent<Collider>().isTrigger = false;
        passage2.GetComponent<Renderer>().enabled = true;
        end.GetComponent<Collider>().enabled = true;
        end.GetComponent<Renderer>().enabled = true;
    }
}
