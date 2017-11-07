using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour {

	public ShackerController Shacker;
    public AudioSource win;
    public AudioSource lose;
    public GameManager gameManager;


    // Use this for initialization
    void Start () {

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        Shacker = GameObject.FindWithTag("Player").GetComponent<ShackerController>();

	}
	
	// Update is called once per frame
	void Update () {


		if(Shacker.Stress > 100)
        {
            Shacker.Stress = 100;
            gameManager.SetLoseCondition = true;
            lose.Play();
            Shacker.Death();

        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Shacker.gameObject)
		{
			if (Shacker.has300DollarJeans)
			{
                //win
                gameManager.SetWinCondition = true;
                win.Play();

            }
			else
			{
                //fail
                gameManager.SetLoseCondition = true;
                lose.Play();
            }

		}
	}
}
