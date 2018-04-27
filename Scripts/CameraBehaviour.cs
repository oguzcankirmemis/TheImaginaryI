using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player, master;
    private PlayerBehaviour instance;
    public bool isMoving = false;
    private float position;

    // Use this for initialization
    void Start()
    {
        position = transform.position.z;
        instance = player.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isMoving == false)
        {
            if (!instance.reverse)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                foreach (Transform child in master.transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    child.position = new Vector3(child.position.x, child.position.y, 1.86f);
                }
                foreach (Transform child in master.transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                player.GetComponent<Rigidbody>().isKinematic = false;
                //isMoving = true;
            }
            else
            {
                foreach (Transform child in master.transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    child.position = new Vector3(child.position.x, child.position.y, -0.9279823f);
                }
                foreach (Transform child in master.transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                //isMoving = true;
            }
            instance.reverse = !instance.reverse;
            StartCoroutine(wait());
        }
        //if (transform.position.z > 9.995f && transform.position.z < 10 && position < 0)
        //{
        //    transform.RotateAround(Vector3.zero, Vector3.up, 70 * Time.deltaTime);
        //    isMoving = false;
        //    position = transform.position.z;
        //}
        //if (position > 0 && transform.position.z > -10 && transform.position.z < -9.995f)
        //{
        //    transform.RotateAround(Vector3.zero, Vector3.up, 70 * Time.deltaTime);
        //    isMoving = false;
        //    position = transform.position.z;
        //}

    }

    IEnumerator wait()
    {
        isMoving = true;
        yield return new WaitForSeconds(4);
        isMoving = false;
    }

    void LateUpdate()
    {
        if (isMoving)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 45 * Time.deltaTime);
        }
    }
}
