using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public GameObject closebutton;
    public GameObject bribebutton;
    public GameObject joingangbutton;
    public GameObject okbutton;
    Quaternion originalrot;

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
                        originalrot = hit.collider.transform.rotation;
                        looking(hit, "Get back to class");
                        break;
                    case "teacher":
                        originalrot = hit.collider.transform.rotation;
                        looking(hit, "Welcome to Class");
                        break;
                    case "bully":
                        originalrot = hit.collider.transform.rotation;
                        looking(hit, "I oughta kick your butt");
                        break;
                    case "lunchlady":
                        originalrot = hit.collider.transform.rotation;
                        looking(hit, "Here's your food!");
                        Instantiate(foodTray, hand.transform.position, hand.transform.rotation, hand.transform);
                        holding = true;
                        break;
                    case "trash":
                        originalrot = hit.collider.transform.rotation;
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
        player.gameObject.GetComponent<PlayerController>().cameramove = false;
        Camera.main.transform.LookAt(hit.collider.transform);
        interactionScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        person = hit.collider.gameObject;
        gameController.instance.moneytheyhave.text = "Money they have: " + hit.collider.GetComponent<NPCStorage>().money.ToString();
        

        
    }
    public void closeInteraction()
    {
        interactionScreen.SetActive(false);
        player.gameObject.GetComponent<PlayerController>().cameramove = true;
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        person.transform.rotation = originalrot;
    }
    public void bribe()
    {


        if (person.tag == "bully")
        {
            gameController.instance.interactiontxt.text = "Are you kidding me?";


        }
        else if (person.tag == "teacher")
        {
            gameController.instance.bribefield.gameObject.SetActive(true);
           
            

        }

        if(gameController.instance.money >= person.GetComponent<NPCStorage>().money)
        {
            

                gameController.instance.interactiontxt.text = "Bribe me to do what?";
                bribebutton.gameObject.SetActive(false);
                closebutton.gameObject.SetActive(false);
                joingangbutton.gameObject.SetActive(true);
            



        }
    }

    public void bribeteacher()
    {
        float money = float.Parse(gameController.instance.bribefield.text);

        if (money > gameController.instance.money)
        {
            gameController.instance.interactiontxt.text = "You don't even have that kind of money";
            bribebutton.gameObject.SetActive(false);
            closebutton.gameObject.SetActive(false);


        }

    }
    public void joingang()
    {
        bribebutton.gameObject.SetActive(true);
        closebutton.gameObject.SetActive(true);
        joingangbutton.gameObject.SetActive(false);
        gameController.instance.interactiontxt.text = "Okay";
        gameController.instance.money -= person.GetComponent<NPCStorage>().money / 2 ;

    }

    

}
