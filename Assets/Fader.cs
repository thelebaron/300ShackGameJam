using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {
    private Material mat;
    public float Alpha = 1;
    public Color WaypointColor;
	// Use this for initialization
	void Start () {
        WaypointColor = Color.white;
        WaypointColor.a = 1;
		mat = this.GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        WaypointColor.a = Alpha;
        if (Alpha > 0)
            Alpha -= 0.05f;
        mat.SetColor("_TintColor", WaypointColor);
    }
}
