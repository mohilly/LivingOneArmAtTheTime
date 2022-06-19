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
        if (other.gameObject.name == "CAKE")         { cakeTOS   = true; }
        if (other.gameObject.name == "CUP 01")       { cup01     = true; }
        if (other.gameObject.name == "CUP 02")       { cup02     = true; }
        if (other.gameObject.name == "CUP 03")       { cup03     = true; }
        if (other.gameObject.name == "BOTTLE")       { drink01   = true; }
        if (other.gameObject.name == "JUICE")        { drink02   = true; }
        if (other.gameObject.name == "PIZZA BOX 01") { pizza01   = true; }
        if (other.gameObject.name == "PIZZA BOX 02") { pizza02   = true; }
        if (other.gameObject.name == "PRESENT 01")   { present01 = true; }
        if (other.gameObject.name == "PRESENT 02")   { present02 = true; }
        if (other.gameObject.name == "PRESENT 03")   { present03 = true; }
    }

    void OnTriggerExit(Collider other)
    {       
        if (other.gameObject.name == "CAKE")         { cakeTOS   = false; }
        if (other.gameObject.name == "CUP 01")       { cup01     = false; }
        if (other.gameObject.name == "CUP 02")       { cup02     = false; }
        if (other.gameObject.name == "CUP 03")       { cup03     = false; }
        if (other.gameObject.name == "BOTTLE")       { drink01   = false; }
        if (other.gameObject.name == "JUICE")        { drink02   = false; }
        if (other.gameObject.name == "PIZZA BOX 01") { pizza01   = false; }
        if (other.gameObject.name == "PIZZA BOX 02") { pizza02   = false; }
        if (other.gameObject.name == "PRESENT 01")   { present01 = false; }
        if (other.gameObject.name == "PRESENT 02")   { present02 = false; }
        if (other.gameObject.name == "PRESENT 03")   { present03 = false; }
    }
}
