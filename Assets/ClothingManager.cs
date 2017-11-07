using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager : MonoBehaviour {

	public int clothingToInstantiate;
	public bool combinepieces = false;
	public GameObject shirtPrefab;
	public GameObject pantsPrefab;

	public float minPrice = 12;
	public float maxPrice = 299;
	public List<Transform> Clothing = new List<Transform>();

	private int clothingCount;


	private void Awake()
	{
		for (int i = 0; i < clothingToInstantiate; i++)
		{
			var pantsorshort = Random.Range(0, 1);
            GameObject cloth;
			cloth = Instantiate(shirtPrefab, transform.position, transform.rotation) as GameObject;
            var c = cloth.transform.gameObject.AddComponent<ClothingPiece>();
                
            c.Price = Random.Range(minPrice, maxPrice);
            var sale = Random.Range(0, 10);
            if (sale == 10)
            {
                c.Sale = true;
                c.Price = c.Price / 2;
            }
            

            //set random position inside area
            var randomPos = transform.position = Random.insideUnitSphere * 20;
            randomPos = new Vector3(randomPos.x, 0.5f, randomPos.z);
            cloth.transform.position = randomPos;



            Clothing.Add(cloth.transform);
        }
        for (int i = 0; i < clothingToInstantiate; i++)
        {
            var pantsorshort = Random.Range(0, 1);
            GameObject cloth;
            cloth = Instantiate(pantsPrefab, transform.position, transform.rotation) as GameObject;
            var c = cloth.transform.gameObject.AddComponent<ClothingPiece>();
            c.Jeans = true;
            c.Price = Random.Range(minPrice, maxPrice);
            var sale = Random.Range(0, 10);
            if (sale == 10)
            {
                c.Sale = true;
                c.Price = c.Price / 2;
            }
            

            //set random position inside area
            var randomPos = transform.position = Random.insideUnitSphere * 20;
            randomPos = new Vector3(randomPos.x, 0.5f, randomPos.z);
            cloth.transform.position = randomPos;



            Clothing.Add(cloth.transform);

        }


        foreach (Transform tr in Clothing)
        {
            var clothingscript = tr.GetComponent<ClothingPiece>();
            if (clothingscript.Jeans)
            {
                clothingscript.Sale = false;
                clothingscript.Price = 300;
                break;
            }


        }
        

	}

	/*
	void Start () {





		foreach (Transform tr in transform)
		{
			Clothing.Add(tr);
		}

		clothingCount = Clothing.Count;
		var threehundreddollarPair = Random.Range(1, clothingCount);

		foreach (Transform cloth in Clothing)
		{
			var c = cloth.transform.gameObject.AddComponent<ClothingPiece>();
			c.Price = Random.Range(minPrice, maxPrice);
		}

		var target = Clothing[threehundreddollarPair].GetComponent<ClothingPiece>();
		target.Price = 300;


	}
	*/
	void Update () {
		
	}
}
