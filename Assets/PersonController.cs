using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour {
    public ShackerController player;
    public float TriggerRateClose = 0.1f;
    public float TriggerRateMed = 0.05f;
    public float TriggerRateFar = 0.01f;


    private GameObject TriggerCloseGameObject;
	public SphereCollider TriggerClose;
	public float TriggerCloseRadius = 2;

	private GameObject TriggerMedGameObject;
	public SphereCollider TriggerMed;
	public float TriggerMedRadius = 5;

	private GameObject TriggerFarGameObject;
	public SphereCollider TriggerFar;
	public float TriggerFarRadius = 7;


	public Vector3 Destination;
	public int WanderDistanceMin = 3;
	public int WanderDistanceMax = 20;
	private Transform tr;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;

	public enum TriggerType { Close,Med,Far }

	void Start () {

        player = GameObject.FindWithTag("Player").transform.GetComponent<ShackerController>();
       


        TriggerRateClose = 1.1f;
        TriggerRateMed = 0.1f;
        TriggerRateFar = 0.05f;

        tr = this.transform;

        if (!player.intromode) {

            SetupTrigger(TriggerType.Close, TriggerClose, TriggerCloseRadius);
            SetupTrigger(TriggerType.Med, TriggerMed, TriggerMedRadius);
            SetupTrigger(TriggerType.Far, TriggerFar, TriggerFarRadius);
        }

		navMeshAgent = tr.GetComponent<NavMeshAgent>();
		Destination = tr.position;
		InvokeRepeating("Wander", 0, 3);
	}

    void SetupTrigger(TriggerType tt, SphereCollider c, float radius) {



        var g = new GameObject
        {
            layer = 2,
            name = this.name + "_Trigger"
        };

        g.transform.position = tr.position;
        g.transform.SetParent(tr);
        if (tt == TriggerType.Close)
            TriggerCloseGameObject = g;

        if (tt == TriggerType.Med)
            TriggerCloseGameObject = g;

        if (tt == TriggerType.Far)
            TriggerCloseGameObject = g;

        var trigScript = g.AddComponent<TriggerHeart>() as TriggerHeart;

        var trigger = g.AddComponent<SphereCollider>() as SphereCollider;
        c = g.transform.GetComponent<SphereCollider>();
        c.radius = radius;
        c.isTrigger = true;


        if (tt == TriggerType.Close)
        {
            TriggerClose = g.transform.GetComponent<SphereCollider>();
            trigScript.rate = TriggerRateClose;

        }

        if (tt == TriggerType.Med)
        {
            TriggerMed = g.transform.GetComponent<SphereCollider>();

            trigScript.rate = TriggerRateMed;
        }

        if (tt == TriggerType.Far)
        {
            TriggerFar = g.transform.GetComponent<SphereCollider>();

            trigScript.rate = TriggerRateFar;
        }
	}

	void Update () {

        if (player.intromode)
            return;
        TriggerClose.radius = TriggerCloseRadius;
		TriggerMed.radius = TriggerMedRadius;
		TriggerFar.radius = TriggerFarRadius;
	}

	void Wander()
	{
		var dist = Random.Range(WanderDistanceMin, WanderDistanceMax);

		Destination = tr.position + Random.insideUnitSphere * dist;
		navMeshAgent.SetDestination(Destination);
	}
}
