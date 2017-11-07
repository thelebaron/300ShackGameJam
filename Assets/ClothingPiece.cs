using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingPiece : MonoBehaviour {
    
    public GameManager GameManager;
    public float Price;
    public bool Sale;
    public bool Jeans;
    public BoxCollider ClothingCollider;
    public bool Revealed = false;

	void Start () {
        GameManager = GameObject.FindWithTag("GameController").transform.GetComponent<GameManager>();
        ClothingCollider = transform.gameObject.AddComponent<BoxCollider>();


        //remove any unneeded colliders to prevent click system mucking up
        foreach (Transform tr in transform)
        {
            var col = tr.GetComponent<Collider>();
            Destroy(col);
        }
    }
	 
	void Update () {
		
	}

    public void PulseClothingPrice() {

        GameManager.ToggleScale = true;
        GameManager.UIPrice.text = "$" + Price.ToString("f0");// + ".99";

    }
}
