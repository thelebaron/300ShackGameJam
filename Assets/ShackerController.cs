using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShackerController : MonoBehaviour {

    public float Stress = 0;
    public enum StressLevel { Normal, Fortressofsolitude }
    public StressLevel CurrentStressLevel;
    public Text StressUI;
    public Transform clickFeedbackRMB;
    public Transform clickFeedbackLMB;
    private Animator animator;
	private Transform tr;
	private Ray ray;
	private RaycastHit raycastHitClick;
	private Vector3 clickLocation;
	private NavMeshAgent navMeshAgent;
    private Fader faderRMB;
    private Fader faderLMB;
    public ClothingPiece ClothingPiece;
    public float ClothingPrice;
    public bool has300DollarJeans = false;
    public AudioSource jeansclap;
    public bool dead;
    public bool intromode = false;
    public Scene gamescene;
    public Scene introscene;


    void Start () {

		tr = this.transform;
		navMeshAgent = tr.GetComponent<NavMeshAgent>();
        faderRMB = clickFeedbackRMB.GetComponent<Fader>();
        faderLMB = clickFeedbackLMB.GetComponent<Fader>();

        if (clickFeedbackRMB == null || clickFeedbackLMB == null)
            Debug.Log("Missing feedback, not fatal but not good");
	}
	
	void Update () {

		QueryMouseClick();

        if (intromode)
            return;
        UpdateUI();
        UpdateStress();
        CheckWinCondition();
	}

    private void CheckWinCondition()
    {
        //if(has300DollarJeans)

    }

    private void UpdateStress()
    {
        if (Stress < 0.1f)
            return;

        if(CurrentStressLevel == StressLevel.Normal)
            Stress -= 0.05f;
        if (CurrentStressLevel == StressLevel.Fortressofsolitude)
            Stress -= 0.15f;
    }

    private void UpdateUI()
    {
        //var rounded = Stress = Mathf.Round(Stress * 100f) / 100f;
        StressUI.text = Stress.ToString("f1");
    }

    void ResetFadeRMB() {
        if (clickFeedbackRMB == null)
            return;
        var offset = new Vector3(clickLocation.x, 0.2f, clickLocation.z);
        clickFeedbackRMB.position = offset;
        faderRMB.Alpha = 1;

    }
    void ResetFadeLMB()
    {
        if (clickFeedbackLMB == null)
            return;
        var offset = new Vector3(clickLocation.x, 0.2f, clickLocation.z);
        clickFeedbackLMB.position = offset;
        faderLMB.Alpha = 1;

    }
    public void Death()
    {
        var rb = GetComponent<Rigidbody>();
        if (!dead)
            rb.AddExplosionForce(555, tr.position, 5);
        dead = true;
        Destroy(navMeshAgent);

    }

    void QueryMouseClick()
    {


        if (Input.GetButtonDown("Submit") && !intromode)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetButtonDown("Submit") && intromode)
        {
            //Scene scene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(scene.name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetButtonDown("Cancel") && intromode)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
        if (Input.GetButtonDown("Cancel") && !intromode) {
            SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
        }

        if (dead)
            return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(ray, out raycastHitClick, 100))
            {
                clickLocation = raycastHitClick.point;
                navMeshAgent.destination = clickLocation;
                ResetFadeRMB();
                /*
				if (hit.collider.CompareTag("Enemy"))
				{
					targetedEnemy = hit.transform;
					enemyClicked = true;
				}
				*/

                clickLocation = raycastHitClick.point;
                //else
                //{
                //walking = true;
                //enemyClicked = false;
                //navMeshAgent.Resume();
                //}
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (intromode)
                return;
            if (Physics.Raycast(ray, out raycastHitClick, 100))
            {
                if (has300DollarJeans)
                    return;

                var checkClothing = raycastHitClick.collider.transform.GetComponent<ClothingPiece>();
                var dist = Vector3.Distance(tr.position, raycastHitClick.collider.transform.position);
                if (checkClothing != null && dist < 2)
                {
                    ClothingPiece = checkClothing;

                    ClothingPrice = ClothingPiece.Price;

                    ClothingPiece.PulseClothingPrice();

                    if (ClothingPiece.Price == 300)
                    {
                        has300DollarJeans = true;
                        jeansclap.Play();
                    }
                }


                clickLocation = raycastHitClick.point;
                ResetFadeLMB();
                clickLocation = raycastHitClick.point;
            }
        }


	}
}
