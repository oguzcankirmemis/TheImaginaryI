using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {
    public GameObject player;
    private PlayerBehaviour instance;
    public Canvas flash;
    
    void Start()
    {
        instance = player.GetComponent<PlayerBehaviour>();
    }

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ghost" && instance.lockMove == false)
        {
            instance.lockMove = true;
            flash.GetComponent<Flash>().MineHit();
        }
    }
}
