using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject ammo;
    Shoot instance;
    public bool shootDirection = false;
    private Vector3 shootAxis = Vector3.left;
    public bool haveShot = false;
    private int hitInfo;
    public float waitSec;

    // Use this for initialization
    void Start () {
        if (shootDirection == true)
            shootAxis = shootAxis * -1;
        instance = GetComponent<Shoot>();
	}

    public Vector3 getShootAxis()
    {
        return shootAxis;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        RaycastHit hitInfo;
        if (haveShot)
            return;
        if (Physics.Raycast(transform.position, shootAxis, out hitInfo)) {
            if (!(hitInfo.collider.gameObject.tag == "Platform"))
            {
                Instantiate(ammo, transform.position, transform.rotation);
                StartCoroutine(wait(waitSec));
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        Destroy(instance);
        haveShot = true;
    }

    IEnumerator wait(float time)
    {
        haveShot = true;
        yield return new WaitForSeconds(time);
        haveShot = false;
    }
}
