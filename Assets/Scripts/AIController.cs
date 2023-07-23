using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private GameObject player;


   
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        player = gameController.instance.player;

    }

    
    void Update()
    {

        if (gameController.instance.state == gameController.gamestate.recess && this.gameObject.tag == "bully")
        {

            this.gameObject.GetComponent<AIController>().enabled = true;
            this.gameObject.GetComponent<AIController>().target = player.transform;
            this.gameObject.GetComponent<AIController>().agent = this.gameObject.GetComponent<NavMeshAgent>();

        }
        else
        {
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;


        }
        moveToDoor();
        
        //checkDoor();

        
    }

    private void moveToDoor()
    {

        agent.SetDestination(target.position);

    }
}
