using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    


   
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        
        
    }

    
    void Update()
    {
        

            moveToDoor();
        
        //checkDoor();

        
    }

    private void moveToDoor()
    {

        agent.SetDestination(target.position);

    }
}
