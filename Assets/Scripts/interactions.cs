using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactions : MonoBehaviour
{
   
    private Camera cam;
 
    public GameObject player;
    public GameObject interactionScreen;
    public GameObject viewpoint;
    private GameObject person;
    public GameObject foodTray;
    public GameObject hand;
    private bool holding; 
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
      
        interactionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       

        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject.tag == "door")
            {

                if (Input.GetKey(KeyCode.E))
                {

                    Vector3 teleportPosition = hit.collider.transform.position + hit.collider.transform.forward * 5;
                    transform.position = teleportPosition;

                }
                



            }
            if (hit.collider.gameObject.tag == "door")
            {
                gameController.instance.doorText.gameObject.SetActive(true);



            }
            else
            {

                gameController.instance.doorText.gameObject.SetActive(false);
            }






        }
        else
        {

            gameController.instance.doorText.gameObject.SetActive(false);
        }
        basicInteractions();
      

    }



    public void basicInteractions()
    {


        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = cam.transform.position;
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 7f, LayerMask.GetMask("child")))
        {
            if (Input.GetKey(KeyCode.E))
            {
                looking(hit, "What do you want?");
            }



        }
        else if (Physics.Raycast(ray, out hit, 7f))
        {
            if (Input.GetKey(KeyCode.E) && hit.collider.gameObject.tag == "principal")
            {
                looking(hit, "Get back to class");

              
            }
            else if(Input.GetKey(KeyCode.E) && hit.collider.gameObject.tag == "teacher"){

                if (gameController.instance.state == gameController.gamestate.classtime)
                {
                    looking(hit, "Welcome to Class");

                }
               

            }
            else if (Input.GetKey(KeyCode.E) && hit.collider.gameObject.tag == "bully")
            {
                    looking(hit, "I oughta kick your butt");
               
            }
            else if (Input.GetKey(KeyCode.E) && hit.collider.gameObject.tag == "lunchlady")
            {
                looking(hit, "Here's your food!");
                Instantiate(foodTray, hand.transform.position, hand.transform.rotation, hand.transform);
                holding = true;
            }
            else if (Input.GetKey(KeyCode.E) && hit.collider.gameObject.tag == "trash")
            {
                Destroy(foodTray);
                interactionScreen.SetActive(true);
                gameController.instance.interactiontxt.text = "Throw away your trash";
                Cursor.lockState = CursorLockMode.None;
                Camera.main.transform.LookAt(hit.collider.transform);
                Instantiate(foodTray, hand.transform.position, hand.transform.rotation, hand.transform);
                holding = false;
            }
            

        }

    }

    public void looking(RaycastHit hit, string message)
    {

        hit.collider.transform.LookAt(this.transform);
        gameController.instance.interactiontxt.text = message;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Camera.main.transform.LookAt(hit.collider.transform);
        interactionScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        person = hit.collider.gameObject;
    }
    public void closeInteraction()
    {
        interactionScreen.SetActive(false);
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void bribe()
    {


       if(person.tag == "bully")
       {
            gameController.instance.interactiontxt.text = "Are you kidding me?";


        }
    }
    
}
