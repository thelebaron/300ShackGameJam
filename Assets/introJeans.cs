using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introJeans : MonoBehaviour {
    public int spawncount = 500;
    public GameObject jeans;
    public GameObject spawner;
    public List<Transform> jeanlist = new List<Transform>();
    public int counter;
	// Use this for initialization
	void Awake () {

        for (int i = 0; i < spawncount; i++) {

            GameObject g;
            g = Instantiate(jeans, spawner.transform.position, spawner.transform.rotation) as GameObject;
            g.SetActive(false);
            jeanlist.Add(g.transform);

        }

    }

    void Start() {
        InvokeRepeating("Releasethejeans", 1, 0.1f);
    }
    void Releasethejeans() {
        if (counter >= spawncount)
            return;

        jeanlist[counter].gameObject.SetActive(true);
        counter++;

    }
	void Update () {
		
	}
}
