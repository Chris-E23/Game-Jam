using UnityEngine;
using UnityEngine.AI;
public class positioning : MonoBehaviour
{
    public Transform lunchposition;
    public Transform recesspositon;
    public Transform classposition;
    private GameObject player;
    public GameObject bully; 
    void Start()
    {
        bully = gameController.instance.bully;
        player = gameController.instance.player;
        if (this.gameObject.tag == "bully")
        {
            _player = GameController.instance.player;
            if (this.gameObject.CompareTag("bully"))
            {
                this.gameObject.GetComponent<AIController>().enabled = true;


            }
        }


        void Update()
        {


        }

        public void changeposition()
        {
            switch (GameController.instance.state)
            {
                case GameController.gamestate.classtime:
                    this.transform.position = classposition.position;
                    this.transform.rotation = classposition.rotation;
                    break;
                case GameController.gamestate.lunchtime:
                    this.transform.position = lunchposition.position;
                    this.transform.rotation = lunchposition.rotation;
                    break;
                case GameController.gamestate.recess:
                    this.transform.position = recesspositon.position;
                    this.transform.rotation = recesspositon.rotation;
                    break;
            }
        }

        public void resetRotation()
        {
           
            this.transform.rotation = classposition.rotation;
            bully.gameObject.GetComponent<NavMeshAgent>().enabled = false;

        }
        else if (gameController.instance.state == gameController.gamestate.lunchtime)
        {
          
            this.transform.rotation = lunchposition.rotation;
            bully.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (gameController.instance.state == gameController.gamestate.recess)
        {
         
            this.transform.rotation = recesspositon.rotation;
            bully.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        }
    }

}
