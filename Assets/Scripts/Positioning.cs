using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CorruptElementary
{

    public class Positioning : MonoBehaviour
    {
    

    public Transform lunchposition;
    public Transform recesspositon;
    public Transform classposition;
    private GameObject player;
    public GameObject bully;

        void Start()
        {
<<<<<<< HEAD
            this.gameObject.GetComponent<AIController>().enabled = true;


=======
            bully = GameController.instance.bully;
            player = GameController.instance.player;
            if (this.gameObject.tag == "bully")
            {
                player = GameController.instance.player;
                if (this.gameObject.CompareTag("bully"))
                {
                    this.gameObject.GetComponent<AIController>().enabled = true;


                }
            }
>>>>>>> 68b003a (cvgbnmhjyhftdjyhrfdtxmgjchm,)
        }
    }

<<<<<<< HEAD
   
    void Update()
    {
        
       
    }
    public void changeposition()
    {

        if (gameController.instance.state == gameController.gamestate.classtime)
=======
        void Update()
>>>>>>> 68b003a (cvgbnmhjyhftdjyhrfdtxmgjchm,)
        {
            this.transform.position = classposition.position;
            this.transform.rotation = classposition.rotation;


        }
        else if (gameController.instance.state == gameController.gamestate.lunchtime)
        {
            this.transform.position = lunchposition.position;
            this.transform.rotation = lunchposition.rotation;
        }
        else if (gameController.instance.state == gameController.gamestate.recess)
        {
            this.transform.position = recesspositon.position;
            this.transform.rotation = recesspositon.rotation;
            
         
        }

    }

    public void resetRotation()
    {
        if (gameController.instance.state == gameController.gamestate.classtime)
        {
           
            this.transform.rotation = classposition.rotation;
            bully.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            if (GameController.instance.state == GameController.gamestate.lunchtime)
            {

                this.transform.rotation = lunchposition.rotation;
                bully.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
            else if (GameController.instance.state == GameController.gamestate.recess)
            {


                this.transform.rotation = recesspositon.rotation;
                bully.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }


        }
       

    }
}



