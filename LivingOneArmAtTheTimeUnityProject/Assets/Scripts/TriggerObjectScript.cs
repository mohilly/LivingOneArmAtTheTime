using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectScript : MonoBehaviour
{
    /// <summary>
    /// Keeping tracks of what objects entered thetrigger
    /// Trigger Object Script (TOS)
    /// </summary>
    public bool cakeTOS = false;
    public bool cupsTOS = false;
    public bool drinksTOS = false;
    public bool pizzaTOS = false;
    public bool presentTOS = false;

    [SerializeField] bool cup01 = false;
    [SerializeField] bool cup02 = false;
    [SerializeField] bool cup03 = false;
    [SerializeField] bool drink01 = false;
    [SerializeField] bool drink02 = false;
    [SerializeField] bool present01 = false;
    [SerializeField] bool present02 = false;
    [SerializeField] bool present03 = false;
    [SerializeField] bool pizza01 = false;
    [SerializeField] bool pizza02 = false;

    public bool allObjectsTOS = false;

    void Update()
    {
        if(cup01 && cup02 && cup03) { cupsTOS = true; } else { cupsTOS = false; }
        if(drink01 && drink02) { drinksTOS = true; } else { drinksTOS = false; }
        if(pizza01 && pizza02) { pizzaTOS = true;} else { pizzaTOS = false; }
        if(present01 && present02 && present03) { presentTOS = true;} else { presentTOS = false; }

        //IF ALL OBJECTS ARE TRUE THEN SET THE ULTIMATE BOOL TO TRUE
        if(cakeTOS && cupsTOS && drinksTOS && pizzaTOS && presentTOS) { allObjectsTOS = true; } else { allObjectsTOS = false; }
    }

    void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.name)
        {
            case "CAKE":
                cakeTOS = true;
                break;
            case "CUP 01":
                cup01 = true;
                break;
            case "CUP 02":
                cup02 = true;
                break;
            case "CUP 03":
                cup03 = true;   
                break;
            case "BOTTLE":
                drink01 = true;
                break;
            case "JUICE":
                drink02 = true;
                break;
            case "PIZZA BOX 01":
                pizza01 = true;
                break;
            case "PIZZA BOX":
                pizza02 = true;
                break;
            case "PRESENT 01":
                present01 = true;
                break;
            case "PRESENT 02":
                present02 = true;
                break;
            case "PRESENT 03":
                present03 = true;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "CAKE":
                cakeTOS = false;
                break;
            case "CUP 01":
                cup01 = false;
                break;
            case "CUP 02":
                cup02 = false;
                break;
            case "CUP 03":
                cup03 = false;
                break;
            case "BOTTLE":
                drink01 = false;
                break;
            case "JUICE":
                drink02 = false;
                break;
            case "PIZZA BOX 01":
                pizza01 = false;
                break;
            case "PIZZA BOX":
                pizza02 = false;
                break;
            case "PRESENT 01":
                present01 = false;
                break;
            case "PRESENT 02":
                present02 = false;
                break;
            case "PRESENT 03":
                present03 = false;
                break;
        }
    }
}
