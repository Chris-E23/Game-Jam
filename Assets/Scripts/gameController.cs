using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    
    void Start()
    {
        money = 20;
        state = gamestate.classtime;
        StartCoroutine(MyCouroutine());
        

    }

    
    void Update()
    {
        moneytxt.text = "Money: " + money; 
        if(state == gamestate.classtime)
        {
            eventTxt.text = "Get to class";
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(true);
        }
        else if(state == gamestate.lunchtime)
        {
            lunch.gameObject.SetActive(true);
            classroom.gameObject.SetActive(false);

        }
        else if (state == gamestate.recess)
        {
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(true);
        }




    }
    public void addMoney(float moneyamount)
    {
        money += moneyamount;

    }
     IEnumerator MyCouroutine()
    {
        yield return new WaitForSeconds(4f);
        beginningText.gameObject.SetActive(false);

    }
}
