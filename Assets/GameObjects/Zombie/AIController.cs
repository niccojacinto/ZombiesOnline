using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : Character {

    [SerializeField]
    float searchRadius = 10.0f;

    NavMeshAgent agent;

	public override void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        agent.SetDestination(FindClosestPlayer());
    }

    Vector3 FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDist = searchRadius;
        // Debug.Log(players.Length);
        Vector3 closestPlayer = Vector3.zero;
        
        foreach(GameObject player in players)
        {
            float distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distFromPlayer < searchRadius && distFromPlayer < closestDist)
            {
                closestDist = distFromPlayer;
                closestPlayer = player.transform.position;
            }
        }
        return closestPlayer;
    }
}
