using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour {
    private Vector3 shootAxis;
    public GameObject shooter;
    private Shoot instance;
    public float speed;

	// Use this for initialization
	void Start () {
        shooter = GameObject.Find("Finisher");
        instance = shooter.GetComponent<Shoot>();
        shootAxis = instance.getShootAxis();
	}

	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + shootAxis * speed * Time.deltaTime;
        if (transform.position.x < -15 || transform.position.x > 15)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer != 8)
        {
            Destroy(gameObject);
        }
    }
}
