using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHeart : MonoBehaviour {
    public GameObject Player;
    public float rate;
    public ShackerController ShackerController;
    
    void Start () {
        Player = GameObject.FindWithTag("Player");
        ShackerController = Player.transform.GetComponent<ShackerController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("hello");
        //Destroy(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
            ShackerController.Stress += rate;
    }
}
