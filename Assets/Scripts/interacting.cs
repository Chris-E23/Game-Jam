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
    public TMP_Text interactionText;
    int lunchboxesstolenfrom;
    public GameObject NPChandler;
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
                bribebutton.gameObject.SetActive(true);
                if (hit.collider.gameObject.GetComponent<NPCStorage>().ingang == false)
                    looking(hit, "What do you want?");
                else
                    looking(hit, "What's up man!");
            }



        }
        else if (Physics.Raycast(ray, out hit, 7f))
        {
            
            if (hit.collider.tag == "trash")
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "Throw away your trash";
            }
            if (Input.GetKey(KeyCode.E)){
                switch (hit.collider.gameObject.tag)
                {

                    case null:
                        break;
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
                        gameController.instance.currenthungervalue += 20;
                        gameController.instance.money -= 10f;
                        holding = true;

                        break;
                    case "trash":
                        originalrot = hit.collider.transform.rotation;
                        Destroy(foodTray);
                        Cursor.lockState = CursorLockMode.None;
                        Camera.main.transform.LookAt(hit.collider.transform);
                        holding = false;
                        break;
                    case "lunchbox":
                       
                            if(hit.collider.gameObject.GetComponent<lunchbox>().money > 0)
                            {
                                gameController.instance.money += 5f;
                                hit.collider.gameObject.GetComponent<lunchbox>().money -= 5;

                            }


                      
                        break;

                }

                    




            }
            if (hit.collider.tag == "lunchbox")
            {
                if(hit.collider.gameObject.GetComponent<lunchbox>().money > 0)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Steal $5";

                }
                
            }
            else if (hit.collider.tag == "lunchlady")
            {
                if (hit.collider.gameObject.GetComponent<lunchbox>().money > 0)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Get Lunch";

                }

            }
            else
            {

                interactionText.gameObject.SetActive(false);
            }
           
            

        }

    }

    public void looking(RaycastHit hit, string message)
    {
        
        hit.collider.transform.LookAt(this.transform);
        gameController.instance.interactiontxt.text = message;
        Camera.main.transform.LookAt(hit.collider.transform);
        interactionScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        person = hit.collider.gameObject;
        gameController.instance.moneytheyhave.text = "Money they have: " + hit.collider.GetComponent<NPCStorage>().money.ToString();
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerMovementTutorial>().enabled = false;
        
        if (person.gameObject.GetComponent<NPCStorage>().ingang == true)
        {
            bribebutton.gameObject.SetActive(false);


        }
        else
        {

            bribebutton.gameObject.SetActive(true);
        }
    }
   
    public void bribe()
    {


        if (person.tag == "bully")
        {
            gameController.instance.interactiontxt.text = "Are you kidding me?";


        }
        else if (person.tag == "teacher")
        {
            if (gameController.instance.money < 10)
            {
                gameController.instance.interactiontxt.text = "You don't have $10";
                bribebutton.gameObject.SetActive(false);
                closebutton.gameObject.SetActive(true);


            }
            else
            {
                gameController.instance.currentparentsatisfaction += 10;
                gameController.instance.money -= 10;

            }


            
           
            

        }

        if(gameController.instance.money >= person.GetComponent<NPCStorage>().money)
        {
            

                gameController.instance.interactiontxt.text = "Bribe me to do what?";
                bribebutton.gameObject.SetActive(false);
                closebutton.gameObject.SetActive(false);
                joingangbutton.gameObject.SetActive(true);
            



        }
    }

    
    public void joingang()
    {
        bribebutton.gameObject.SetActive(false);
        closebutton.gameObject.SetActive(true);
        joingangbutton.gameObject.SetActive(false);
        gameController.instance.interactiontxt.text = "Okay";
        gameController.instance.money -= person.GetComponent<NPCStorage>().money / 2 ;
        person.GetComponent<NPCStorage>().money += person.GetComponent<NPCStorage>().money / 2;
        person.GetComponent<NPCStorage>().ingang = true;
        gameController.instance.moneytheyhave.text = "Money they have: " + person.GetComponent<NPCStorage>().money;

        NPChandler.GetComponent<nameGenerator>().addToGang(person.name);
    }

    

}
