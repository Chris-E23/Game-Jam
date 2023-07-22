using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCStorage : MonoBehaviour
{
    public string name;
    public TMP_Text nametxt;
    public float money; 


    void Start()
    {
        



    }

    void Update()
    {

        nametxt.text = name; 



    }
    public void setName(string name_)
    {

        name = name_;

    }
}
