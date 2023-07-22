using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interacting : MonoBehaviour
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
            if (Input.GetKey(KeyCode.E)){
                switch (hit.collider.gameObject.tag)
                {
                    case "principal":
                        looking(hit, "Get back to class");
                        break;
                    case "teacher":
                        looking(hit, "Welcome to Class");
                        break;
                    case "bully":
                        looking(hit, "I oughta kick your butt");
                        break;
                    case "lunchlady":
                        looking(hit, "Here's your food!");
                        Instantiate(foodTray, hand.transform.position, hand.transform.rotation, hand.transform);
                        holding = true;
                        break;
                    case "trash":
                        Destroy(foodTray);
                        interactionScreen.SetActive(true);
                        gameController.instance.interactiontxt.text = "Throw away your trash";
                        Cursor.lockState = CursorLockMode.None;
                        Camera.main.transform.LookAt(hit.collider.transform);
                        Instantiate(foodTray, hand.transform.position, hand.transform.rotation, hand.transform);
                        holding = false;
                        break;

                }

                    




            }
           
            

        }

    }

    public void looking(RaycastHit hit, string message)
    {

        hit.collider.transform.LookAt(this.transform);
        gameController.instance.interactiontxt.text = message;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        Camera.main.transform.LookAt(hit.collider.transform);
        interactionScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        person = hit.collider.gameObject;
    }
    public void closeInteraction()
    {
        interactionScreen.SetActive(false);
        player.gameObject.GetComponent<PlayerController>().enabled = true;
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void bribe()
    {


        if (person.tag == "bully")
        {
            gameController.instance.interactiontxt.text = "Are you kidding me?";


        }
    }

}
