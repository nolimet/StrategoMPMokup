using UnityEngine;
using System.Collections;

public class NavTarget : MonoBehaviour {


    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;

    [SerializeField]
    bool canAlwaysSee;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

	/*void Update () {
        if (canAlwaysSee||agent.destination != target.position && Vector3.Distance(target.position, transform.position) < 15f)
        {
            RaycastHit hit;
            if (canAlwaysSee || Physics.Raycast(transform.position, target.position - transform.position, out hit, 100))
                if (canAlwaysSee || hit.transform.gameObject.tag == "Player")
                    agent.SetDestination(target.position);
        }
	}*/
}
