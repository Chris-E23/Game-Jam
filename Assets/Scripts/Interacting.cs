using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CorruptElementary
{
    public class Interacting : MonoBehaviour
    {

        private Camera cam;
        public GameObject player;
        public GameObject interactionScreen;
        public GameObject viewpoint;
        [HideInInspector] public GameObject person;
        public GameObject foodTray;
        public GameObject foodTrayHolder;
        public GameObject hand;
        private bool holding;
        public GameObject closebutton;
        public GameObject bribebutton;
        public GameObject joingangbutton;
        public GameObject okbutton;
        public TMP_Text interactionText;
        int lunchboxesstolenfrom;

        public GameObject NPChandler;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;

            interactionScreen.SetActive(false);
            bribebutton.gameObject.SetActive(false);
            foodTrayHolder = null;
        }

        // Update is called once per frame
        void Update()
        {

            basicInteractions();



            if (!interactionScreen.activeInHierarchy)
            {
                bribebutton.gameObject.SetActive(false);
            }


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

                    if (hit.collider.gameObject.GetComponent<NPCStorage>().ingang == false)
                        looking(hit, "What do you want?");
                    else
                        looking(hit, "What's up man!");
                }



            }
            else if (Physics.Raycast(ray, out hit, 7f))
            {
                if (hit.collider.tag != "Default")
                {

                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (hit.collider.tag == "principal")
                        {

                            lookingNPC(hit, "Get back to class");
                        }
                        else if (hit.collider.tag == "teacher")
                        {

                            lookingNPC(hit, "Welcome to Class");
                        }
                        else if (hit.collider.tag == "bully")
                        {
                            lookingNPC(hit, "I oughta kick your butt");


                        }
                        else if (hit.collider.tag == "lunchlady")
                        {
                            if (foodTrayHolder == null)
                            {
                                lookingNPC(hit, "Here's your food");
                                GameController.instance.currenthungervalue += 20;
                                GameController.instance.money -= 10f;
                                foodTrayHolder = Instantiate(foodTray, hand.transform.position, hand.transform.rotation,
                                    hand.transform);

                            }
                            else
                            {

                                lookingNPC(hit, "Throw away your trash before getting more! IDIOT!");

                            }

                        }
                        else if (hit.collider.tag == "trash" && foodTrayHolder != null)
                        {

                            Destroy(foodTrayHolder);
                            Camera.main.transform.LookAt(hit.collider.transform);

                        }
                        else if (hit.collider.tag == "lunchbox")
                        {
                            if (hit.collider.gameObject.GetComponent<Lunchbox>().money > 0)
                            {
                                GameController.instance.money += 5f;
                                hit.collider.gameObject.GetComponent<Lunchbox>().money -= 5;

                            }

                        }
                        else if (hit.collider.tag == "skipbutton")
                        {
                            GameController.instance.timeRemaining = 0;
                            interactionText.gameObject.SetActive(false);
                        }

                    }

                    if (hit.collider.tag == "lunchbox")
                    {
                        if (hit.collider.gameObject.GetComponent<Lunchbox>().money > 0)
                        {
                            interactionText.gameObject.SetActive(true);
                            interactionText.text = "Steal $5";

                        }

                    }
                    else if (hit.collider.tag == "lunchlady")
                    {

                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "Get Lunch";



                    }
                    else if (hit.collider.tag == "trash")
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "Throw away trash";


                    }
                    else if (hit.collider.tag == "skipbutton")
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "Skip forward to next period";


                    }
                    else
                    {
                        interactionText.text = "";

                    }




                }
            }
            else
            {

                interactionText.text = "";
            }

        }

        public void looking(RaycastHit hit, string message)
        {

            hit.collider.transform.LookAt(this.transform);
            GameController.instance.interactiontxt.text = message;
            Camera.main.transform.LookAt(hit.collider.transform);
            interactionScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            person = hit.collider.gameObject;

            if (person.GetComponent<NPCStorage>() != null)
                GameController.instance.moneytheyhave.text =
                    "Money they have: " + hit.collider.GetComponent<NPCStorage>().money.ToString();

            cam.gameObject.GetComponent<CamController>().enabled = false;
            player.gameObject.GetComponent<PlayerController>().enabled = false;

            if (GameController.instance.state == GameController.gamestate.lunchtime)
            {
                bribebutton.gameObject.SetActive(true);


            }
            else
            {
                bribebutton.gameObject.SetActive(false);
                if (foodTrayHolder != null)
                {
                    Destroy(foodTrayHolder);
                }
            }

            if (person.gameObject.GetComponent<NPCStorage>().ingang == true)
            {
                bribebutton.gameObject.SetActive(false);


            }

        }

        public void lookingNPC(RaycastHit hit, string message)
        {
            hit.collider.transform.LookAt(this.transform);
            GameController.instance.interactiontxt.text = message;
            Camera.main.transform.LookAt(hit.collider.transform);
            interactionScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            person = hit.collider.gameObject;
            cam.gameObject.GetComponent<CamController>().enabled = false;
            player.gameObject.GetComponent<PlayerController>().enabled = false;
            bribebutton.gameObject.SetActive(false);

            if (person.tag == "teacher")
            {

                bribebutton.gameObject.SetActive(true);
            }
        }

        public void bribe()
        {


            if (person.tag == "bully")
            {
                GameController.instance.interactiontxt.text = "Are you kidding me?";


            }
            else if (person.tag == "teacher")
            {
                if (GameController.instance.money < 10)
                {
                    GameController.instance.interactiontxt.text = "You don't have $10";
                    bribebutton.gameObject.SetActive(false);
                    closebutton.gameObject.SetActive(true);


                }
                else
                {
                    GameController.instance.interactiontxt.text = "Don't tell anybody";
                    GameController.instance.currentparentsatisfaction += 10;
                    GameController.instance.money -= 10;

                }






            }

            if (GameController.instance.money >= person.GetComponent<NPCStorage>().money)
            {


                GameController.instance.interactiontxt.text = "Bribe me to do what?";
                bribebutton.gameObject.SetActive(false);
                closebutton.gameObject.SetActive(false);
                joingangbutton.gameObject.SetActive(true);




            }
            else
            {
                GameController.instance.interactiontxt.text = "You don't have enough money! LOL! BROKE!";
                bribebutton.gameObject.SetActive(false);
                closebutton.gameObject.SetActive(true);



            }
        }


        public void joingang()
        {
            bribebutton.gameObject.SetActive(false);
            closebutton.gameObject.SetActive(true);
            joingangbutton.gameObject.SetActive(false);
            GameController.instance.interactiontxt.text = "Okay";
            GameController.instance.money -= GameController.instance.money / 2;
            person.GetComponent<NPCStorage>().money += GameController.instance.money / 2;
            person.GetComponent<NPCStorage>().ingang = true;
            GameController.instance.moneytheyhave.text = "Money they have: " + person.GetComponent<NPCStorage>().money;
            GameController.instance.peopleingangtext.text += "\n" + person.GetComponent<NPCStorage>().name;
        }



    }
}
