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
    public TMP_InputField bribefield;
    public float hungermaxvalue;
    public float currenthungervalue;
    int classes = 0;
    public days daystatus;
    public GameObject NPChandler;
    public void Awake()
    {
        instance = this;
        daystatus = days.monday;
        bribefield.gameObject.SetActive(false);
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
        currentparentsatisfaction = parentsatisfactionmax;
        
    }

    
    void Update()
    {
        parentsatis.value = currentparentsatisfaction;

        if(state == gamestate.newday)
        {

            switch (daystatus)
            {
                case days.monday:
                    daystatus = days.tuesday;
                    currentparentsatisfaction -= 20;
                    break;
                case days.tuesday:
                    daystatus = days.wednesday;
                    currentparentsatisfaction -= 40;
                    break;
                case days.wednesday:
                    daystatus = days.thursday;
                    currentparentsatisfaction -= 30;
                    break;
                case days.thursday:
                    daystatus = days.friday;
                    currentparentsatisfaction -= 40;
                    break;



            }

        }
        if(money == 0)
        {

            //game end



        }
        if(currentparentsatisfaction == 0)
        {

            //gameend

        }
        if(currenthungervalue == 0)
        {


            //game end 
        }
       
        timer.text = "Time: " + (int)timeRemaining;

        if (classes > 2)
        {

            state = gamestate.newday;


        }
        if(state == gamestate.classtime || state == gamestate.lunchtime || state == gamestate.recess)
        {
            time();
        }
        
        if(timeRemaining <= 0)
        {
           
            
                switch (state)
                {

                    case gamestate.classtime:
                        state = gamestate.lunchtime;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.GetComponent<PlayerController>().teleport(classteleport);
                        classes++;
                        break;
                    case gamestate.lunchtime:
                        state = gamestate.recess;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.GetComponent<PlayerController>().teleport(lunchteleport);
                        break;
                    case gamestate.recess:
                        state = gamestate.classtime;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.GetComponent<PlayerController>().teleport(recessteleport);
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
            recesstime();
        }

        if (screen.activeInHierarchy)
        {
           this.gameObject.GetComponent<PlayerController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            parentsatis.gameObject.SetActive(false);
            parenttxt.gameObject.SetActive(false);
            hungerslider.gameObject.SetActive(false);
            hungertxt.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.GetComponent<PlayerController>().enabled = true;
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

    }
     IEnumerator MyCouroutine()
    {
        yield return new WaitForSeconds(4f);
        beginningText.gameObject.SetActive(false);

    }
    public void closeInteraction()
    {
        interactionScreen.SetActive(false);
        player.gameObject.GetComponent<PlayerController>().cameramove = true;
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void recesstime()
    {
        interactionScreen.SetActive(true);
        interactiontxt.text = "It's recess time, your bully is going to go after you to steal your money, last the period in order to keep your money.";
           


    }
    
}
