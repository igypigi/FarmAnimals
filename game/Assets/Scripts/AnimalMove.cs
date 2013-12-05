using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour {
	private static NavMeshAgent agent;
	private static GameObject player;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.Find ("Player");
		moveToPoint = GameObject.Find ("AnimalBarnSpot").transform.position;
	}

	// If animals should move this variable should be set
	private static Vector3 moveToPoint;

	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(gameObject.transform.position, moveToPoint);
		if (distance > 0.2) {
			// If distance is larger than 10.0 than run else walk
			if (distance > 10.0) {
				agent.speed = 7;
				animation.Play("run", PlayMode.StopAll);
			} else {
				agent.speed = 4;
				animation.Play("walk", PlayMode.StopAll);
			}
			agent.SetDestination(moveToPoint);
		} else {
			animation.Play("idle", PlayMode.StopAll);
		}
	}
}