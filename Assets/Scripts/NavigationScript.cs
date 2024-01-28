using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent[] agents;
    [SerializeField] private Transform endPos;

    private void Start()
    {
        agents[0].SetAreaCost(5, 1);
        agents[1].SetAreaCost(4, 1);
        foreach(NavMeshAgent agent in agents)
            agent.SetDestination(endPos.position);
        StartCoroutine(CheckWinner());
    }

    private void Update()
    {
        //if (agents[0].remainingDistance < 0.2f && !hasWon)
        //{
        //    Debug.Log($"{agents[0]} wins");
        //    hasWon = true;
        //}
        //else if (agents[1].remainingDistance < 0.2f && !hasWon)
        //{
        //    Debug.Log($"{agents[1]} wins");
        //    hasWon = true;
        //}
    }

    private IEnumerator CheckWinner()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (agents[0].remainingDistance < 0.2f)
            {
                Debug.Log($"{agents[0]} wins");
                break;
            }
            else if (agents[1].remainingDistance < 0.2f)
            {
                Debug.Log($"{agents[1]} wins");
                break;
            }
        }
    }
}
