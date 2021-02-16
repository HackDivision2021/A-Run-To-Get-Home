using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/***
 * This class is for enemy use
 */

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public float range = 20f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player is in range, then move enemy towards player
        if(Vector3.Distance(player.position, transform.position) <= range)
        {
            navMeshAgent.SetDestination(player.position);
        }

    }
}
