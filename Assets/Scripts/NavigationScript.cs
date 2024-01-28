using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    [SerializeField] private List<NavMeshAgent> agents;
    [SerializeField] private Transform endPos;

	private void Update()
	{
		CheckInput();
	}

	private void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200))
				SendAllAgents(hit.point);
		}
	}

	private void SendAllAgents(Vector3 position)
	{
		SendAgentsToPosition(position);
        endPos.position = position;
		agents.ForEach(a => StartCoroutine(CheckReachedEndPos(a)));
	}

	private void SendAgentsToPosition(Vector3 position)
	{
		agents.ForEach(a => a.SetDestination(position));
	}

	private IEnumerator CheckReachedEndPos(NavMeshAgent agent)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (agent.remainingDistance < 1)
            {
                Debug.Log($"{agent} has reached its destination.");
                break;
            }
        }
    }
}
