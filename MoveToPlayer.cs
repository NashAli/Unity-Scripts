using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public Transform goal;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
}
