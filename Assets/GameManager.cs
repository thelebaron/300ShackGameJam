using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject Player;
    private ShackerController shacker;

    public Transform Heart;
    private Animator heartAnimator;
    public float StressLevel;
    public float ActualStressMult;

    //UI
    public Image UISaleLabel;
    public Text UIPrice;
    public Text UISale;
    public bool Sale;
    public bool ToggleScale;
    public Button UILatestChatty;
    public Button PhoneBig;
    public Image PhoneBigImage;
    public Text AnnoyingWinText;
    public Text AnnoyingLoseText;

    public AudioSource sfxChachingSrc;
    public AudioSource musicSrc;
    public AudioSource grunt;
    public bool audioEnabled = true;

    public bool showPhone = false;
    public bool SetWinCondition;
    public bool SetLoseCondition;

    public List<GameObject> peoplePrefab = new List<GameObject>();

    public void TogglePhoneVisibility() {
        showPhone = !showPhone;

        if (showPhone)
        {
            PhoneBig.interactable = true;
            PhoneBigImage.enabled = true;
        }
        else
        {
            PhoneBig.interactable = false;
            PhoneBigImage.enabled = false;
        }

    }
    public void ClickPhone(){

        if (audioEnabled)
        {
            var random = UnityEngine.Random.Range(-10.0f, 10.0f);
            if(random > 6)
                grunt.Play();
        }

        if(shacker.Stress >0)
        {
            shacker.Stress -= 25;
            if (shacker.Stress < 0)
                shacker.Stress = 0;
        }
        


    }

    public void ToggleAudio() { 
        audioEnabled = !audioEnabled;

        if (!audioEnabled)
        {
            musicSrc.Stop();
        }
        if (audioEnabled)
        {
            musicSrc.Play();
        }
    }

	void Start () {

        sfxChachingSrc = GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player");
        shacker = Player.transform.GetComponent<ShackerController>();

        if (Heart == null)
            Debug.LogError("Missing UI object Heart");
        heartAnimator = Heart.GetComponent<Animator>();



    }
	


	void Update () {

        PricelabelUpdate();
        PulseLabel();

        shacker.Stress += 0.15f; // auto tick up just being in the world 
        StressLevel = shacker.Stress / 100;
        ActualStressMult = 0.15f + StressLevel * 1.25f;
        heartAnimator.speed = ActualStressMult;

        if (showPhone && shacker.Stress>2)
            shacker.Stress -= 0.75f;



        DoEndConditions();
    }

    public void DoEndConditions()
    {
        /*
        if(shacker.has300DollarJeans)
            AnnoyingWinText.enabled = true;
        else
            AnnoyingLoseText.enabled = true;
            */
        if (SetWinCondition)
            AnnoyingWinText.enabled = true;
        if (SetLoseCondition)
            AnnoyingLoseText.enabled = true;
    }

    public void PulseLabel()
    {
        if (ToggleScale)
        {
            StartCoroutine(PulseSaleTagImage());

            sfxChachingSrc.time = 1.75f;
            if(audioEnabled)
                sfxChachingSrc.Play();
            ToggleScale = !ToggleScale;



        }
    }

    public IEnumerator PulseSaleTagImage()
    {
        UISaleLabel.transform.localScale = new Vector3(4, 4, 1);
        yield return new WaitForSeconds(0.05f);
        UISaleLabel.transform.localScale = new Vector3(3, 3, 1);
        yield return null;
    }

    private void PricelabelUpdate()
    {
        if (Sale)
            UISale.text = "SALE";
        else
            UISale.text = "";
    }
}
