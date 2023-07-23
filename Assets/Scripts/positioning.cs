using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positioning : MonoBehaviour
{
    public Transform lunchposition;
    public Transform recesspositon;
    public Transform classposition;
    
    void Start()
    {
        if(this.gameObject.tag == "bully")
        {
            this.gameObject.GetComponent<AIController>().enabled = false;


        }
    }

   
    void Update()
    {
        
       
    }
    public void changeposition()
    {

        if (gameController.instance.state == gameController.gamestate.classtime)
        {
            this.transform.position = classposition.position;



        }
        else if (gameController.instance.state == gameController.gamestate.lunchtime)
        {
            this.transform.position = lunchposition.position;

        }
        else if (gameController.instance.state == gameController.gamestate.recess)
        {
            this.transform.position = recesspositon.position;

        }

    }
}
