using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nameGenerator : MonoBehaviour
{
   
    public List<NPCStorage> lunchpeople = new List<NPCStorage>();
    public List<NPCStorage> classroom = new List<NPCStorage>();
    public List<NPCStorage> recess = new List<NPCStorage>();
    private  List<string> names = new List<string>();
    private  List<float> money = new List<float>();
    public TMP_Text nametxt;
    public gameController player;
    void Start()
    {
        names.Add("Chris");
        names.Add("Bob");
        names.Add("Mike");
        names.Add("Gus");
        names.Add("Walter");
        names.Add("Flynn");
        names.Add("Skyler");
        names.Add("Akrit");
        names.Add("Billy");
        names.Add("Mika");
        names.Add("Jones");
        names.Add("Billy");
        names.Add("Edwin");
        names.Add("Jolie");
        names.Add("Christina");
        
        money.Add(10);
        money.Add(20);
        money.Add(30);
        money.Add(25);
        money.Add(200);
        money.Add(10);
        money.Add(40);
        money.Add(4);
        money.Add(70);
        money.Add(15);
        money.Add(3);
        money.Add(9);
        money.Add(2);
        money.Add(300);
        money.Add(100);

        respawn();

        
    }
    public void respawn()
    {
       
        for (int i = 0; i < 15; i++)
        {
           
                classroom[i].name = names[i];
                classroom[i].money = money[i];

            
                lunchpeople[i].name = names[i];
                lunchpeople[i].money = money[i];

            
           

        }
    }

   
    
}