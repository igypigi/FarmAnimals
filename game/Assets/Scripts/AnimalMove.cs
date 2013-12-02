using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour {
	
	private static NavMeshAgent agent;
	public Vector3 moveToPoint;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find("Player");
		Vector3 position = player.transform.position;
		agent.SetDestination(position);
	
	}
}
