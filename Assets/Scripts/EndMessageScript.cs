using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndMessageScript : MonoBehaviour
{
    [SerializeField] NavMeshAgent[] agents;
    bool winnerDecided = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == agents[0].gameObject)
        {
            if (agents[0].remainingDistance < 0.2f && !winnerDecided)
            {
                Debug.Log("Capsule man won");
                winnerDecided = true;
            }
        }
        else if (collision.collider.gameObject == agents[1].gameObject)
        {
            if (agents[1].remainingDistance < 0.2f && !winnerDecided)
            {
                Debug.Log("Capsule man won");
                winnerDecided = true;
            }
        }
    }
}
