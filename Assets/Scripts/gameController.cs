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
    private float money; 
    public static gameController instance;
    public int classtime = 100;
    public TMP_Text interactiontxt;
    public TMP_Text doorText;
    public TMP_Text eventTxt;
    public GameObject lunch;
    public GameObject classroom;
    public GameObject recess;
    public TMP_Text timer;
    public GameObject screen;
    public float classTime = 120;
    public float timeRemaining;
    public Slider parentsatis;
    public TMP_Text parenttxt;
    public Slider hungerslider;
    public TMP_Text hungertxt;
    
    public void Awake()
    {
        instance = this;
    }
    public enum gamestate
    {
        lunchtime,
        classtime,
        recess


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

    }

    
    void Update()
    {
        timer.text = "Time: " + classtime;
        time();
        if(timeRemaining == 0)
        {
            switch (state)
            {
                case gamestate.classtime:
                    state = gamestate.lunchtime;
                    timeRemaining = classTime;
                    break;



            }

        }
        moneytxt.text = "Money: " + money; 
        if(state == gamestate.classtime)
        {
            eventTxt.text = "Get to class";
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(true);
        }
        else if(state == gamestate.lunchtime)
        {
            eventTxt.text = "Lunch Time!!!";
            lunch.gameObject.SetActive(true);
            classroom.gameObject.SetActive(false);

        }
        else if (state == gamestate.recess)
        {
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(true);
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
    public void addMoney(float moneyamount)
    {
        money += moneyamount;

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
}
