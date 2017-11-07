using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatController : MonoBehaviour {


    public AudioSource heartSource;


	void Start () {
        heartSource = GetComponent<AudioSource>();

    }
	

	void Beat () {
        heartSource.Play();

    }
}
