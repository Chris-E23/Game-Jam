using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class gameController : MonoBehaviour
{

    public TMP_Text moneytxt;
    public TMP_Text beginningText;
    public gamestate state = gamestate.classtime;
    public float money; 
    public static gameController instance;
    public TMP_Text interactiontxt;
    public TMP_Text doorText;
    public TMP_Text eventTxt;
    public GameObject lunch;
    public GameObject classroom;
    public GameObject recess;
    public TMP_Text timer;
    public GameObject screen;
    public float classTime = 2;
    public float timeRemaining;
    public Slider parentsatis;
    public TMP_Text parenttxt;
    public Slider hungerslider;
    public TMP_Text hungertxt;
    public GameObject player;
    public GameObject interactionScreen;
    public GameObject viewpoint;
    public TMP_Text moneytheyhave;
    public Transform classteleport;
    public Transform lunchteleport;
    public Transform recessteleport;
    public float parentsatisfactionmax = 100;
    public float currentparentsatisfaction;
    public float hungermaxvalue;
    public float currenthungervalue;
    int classes = 0;
    public days daystatus;
    public GameObject gangmembertext;
    public GameObject NPChandler;
    public GameObject EndScreen;
    bool inInteraction;
    public Camera cam;
    public TMP_Text daytext;
    public GameObject bribebutton;
    public GameObject launchattackbutton;
    public bool attackgoing;
    bool pause;
    public GameObject bully;

    public void Awake()
    {
        instance = this;
        daystatus = days.monday;
        

    }
    public enum gamestate
    {
        lunchtime,
        classtime,
        recess,
        newday

    }
    public enum days
    {
        monday,
        tuesday,
        wednesday,
        thursday,
        friday

    }
    
    void Start()
    {
        money = 20;
        state = gamestate.classtime;
        timeRemaining = classTime;
        screen.SetActive(true);
        parentsatis.maxValue = parentsatisfactionmax;
        hungerslider.maxValue = hungermaxvalue;
        cam = Camera.main;
        daytext.text = "Monday";
        pause = true;
    }

    
    void Update()
    {

        if (screen.activeInHierarchy)
        {
            cam.gameObject.GetComponent<camController>().enabled = false;
            player.gameObject.GetComponent<PlayerMovementTutorial>().enabled = false;


        }
        
       if(NPChandler.gameObject.GetComponent<nameGenerator>().ganglist.Count >= 5)
        {
            gangmembertext.gameObject.SetActive(true);


        }
        
        
        
        
        parentsatis.value = currentparentsatisfaction;
        hungerslider.value = currenthungervalue;
        
        
        
        
        
        
        if(state == gamestate.newday)
        {
            if(daystatus == days.monday)
            {
                daystatus = days.tuesday;
                currentparentsatisfaction -= 20;
                state = gamestate.classtime;
                classes = 0;
                daytext.text = "Tuesday";
            }
            else if(daystatus == days.tuesday)
            {
                daystatus = days.wednesday;
                currentparentsatisfaction -= 40;
                state = gamestate.classtime;
                classes = 0;
                daytext.text = "Wednesday";
            }
            else if (daystatus == days.wednesday)
            {

                daystatus = days.thursday;
                currentparentsatisfaction -= 30;
                state = gamestate.classtime;
                classes = 0;
                daytext.text = "Thursday";
            }
            else if (daystatus == days.thursday)
            {
                daystatus = days.friday;
                currentparentsatisfaction -= 40;
                state = gamestate.classtime;
                classes = 0;
                daytext.text = "Friday";
            }
           

        }
        if(money == 0)
        {

            //game end
            EndScreen.gameObject.SetActive(true);


        }
        if(currentparentsatisfaction == 0)
        {
            EndScreen.gameObject.SetActive(true);
            //gameend

        }
        if(currenthungervalue == 0)
        {

            EndScreen.gameObject.SetActive(true);
            //game end 
        }
       
        timer.text = "Time: " + (int)timeRemaining;

        if (classes >= 2)
        {

            state = gamestate.newday;


        }
        if((state == gamestate.classtime || state == gamestate.lunchtime || state == gamestate.recess) && !pause)
        {
            time();
        }
        
        if(timeRemaining <= 0)
        {
                switch (state)
                {

                    case gamestate.classtime:
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.transform.position = lunchteleport.position;
                        state = gamestate.lunchtime;
                        timeRemaining = classTime;
                        classes++;
                        changeposition();
                        break;
                    case gamestate.lunchtime:
                        state = gamestate.recess;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.transform.position = recessteleport.position;
                        changeposition();
                        recesstime();

                    break;
                    case gamestate.recess:
                        state = gamestate.classtime;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        changeposition();
                        player.transform.position = classteleport.position;
                        player.transform.position = classteleport.position;
                        break;



                }

            
            

        }
        moneytxt.text = "Money: " + money; 
        if(state == gamestate.classtime)
        {
            eventTxt.text = "Get to class";
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(true);
            recess.gameObject.SetActive(false);
        }
        else if(state == gamestate.lunchtime)
        {
            eventTxt.text = "Lunch Time!!!";
        
            lunch.gameObject.SetActive(true);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(false);
        }
        else if (state == gamestate.recess)
        {
            eventTxt.text = "Recess";
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(true);
           
        }

        if (screen.activeInHierarchy)
        {
           
            Cursor.lockState = CursorLockMode.None;
            parentsatis.gameObject.SetActive(false);
            parenttxt.gameObject.SetActive(false);
            hungerslider.gameObject.SetActive(false);
            hungertxt.gameObject.SetActive(false);
        }
        else
        {
            
            parentsatis.gameObject.SetActive(true);
            parenttxt.gameObject.SetActive(true);
            hungerslider.gameObject.SetActive(true);
            hungertxt.gameObject.SetActive(true);
            StartCoroutine(MyCouroutine());
            timer.gameObject.SetActive(true);

        }


    }
   
    public void time()
    {
        timeRemaining -= 1 * Time.deltaTime;


    }
    public void close()
    {

        screen.SetActive(false);
        interactionScreen.SetActive(false);
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    
        cam.gameObject.GetComponent<camController>().enabled = true;
        player.gameObject.GetComponent<PlayerMovementTutorial>().enabled = true;
        pause = false;
    }
     IEnumerator MyCouroutine()
    {
        yield return new WaitForSeconds(4f);
        beginningText.gameObject.SetActive(false);

    }
    
    public void recesstime()
    {
            pause = true;
            interactionScreen.SetActive(true);
            interactiontxt.text = "It's recess time, your bully is going to go after you to steal your money, last the period in order to keep your money.";
            bribebutton.gameObject.SetActive(false);
            cam.gameObject.GetComponent<camController>().enabled = false;
            player.gameObject.GetComponent<PlayerMovementTutorial>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
      
           if(daystatus == days.friday)
            {

                launchattackbutton.gameObject.SetActive(true);

            }


    }

    public void attack()
    {
        attackgoing = true;


    }
    public void changeposition()
    {
        for (int i = 0; i < 15; i++) {

            NPChandler.GetComponent<nameGenerator>().classroom[i].GetComponent<positioning>().changeposition();

    }
        bully.gameObject.GetComponent<positioning>().changeposition();
    }
    
}
