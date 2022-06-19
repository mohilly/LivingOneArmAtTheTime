using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject characters;
    public CharacterManager characterManager;

    public TMP_Text txt_speedNum;

    float speedUI = 0f;
    float staminaUI = 0f;
    float balanceUI = 10f;

    public TMP_Text txt_staminaNumCur;
    public TMP_Text txt_balanceNumCur;

    /// <summary>
    /// TO-DO List THINGIES
    /// Items in the Livingroom (LR) ture only when all items of the same category are in the box.
    /// </summary>
    /// 

    public GameObject triggerBox;
    public TriggerObjectScript triggerObjectScript;

    public TMP_Text txt_TickCake;
    public TMP_Text txt_TickCups;
    public TMP_Text txt_TickDrinks;
    public TMP_Text txt_TickPizza;
    public TMP_Text txt_TickPresents;

    public bool cakeLR      = false;
    public bool cupsLR      = false;
    public bool drinksLR    = false;
    public bool pizzaLR     = false;
    public bool presnetLR   = false;

    private void Awake()
    { 
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();

        triggerBox = GameObject.FindGameObjectWithTag("Tag_Trigger");
        triggerObjectScript = triggerBox.GetComponent<TriggerObjectScript>();
    }

    void Start()
    {              
        speedUI    = characterManager.actSSSGet()[0];
        staminaUI  = characterManager.actSSSGet()[1];

        //changing text on the UI
        txt_speedNum.text    = speedUI.ToString();

        txt_staminaNumCur.text = staminaUI.ToString();
        txt_balanceNumCur.text = balanceUI.ToString();
    }

    void Update()
    {
        speedUI = characterManager.actSSSGet()[0];
        staminaUI = characterManager.actSSSGet()[1];

        //changing text on the UI
        txt_speedNum.text = speedUI.ToString();

        //txt_speedNumCur.text = characterManager. ; //to update later if needed
        txt_staminaNumCur.text = characterManager.currentStamina.ToString();
        txt_balanceNumCur.text = characterManager.currentBalance.ToString();

        //Updating variables to see if all items are present there
        cakeLR =   triggerObjectScript.cakeTOS;
        cupsLR =   triggerObjectScript.cupsTOS;
        drinksLR = triggerObjectScript.drinksTOS;
        pizzaLR =  triggerObjectScript.pizzaTOS;
        presnetLR = triggerObjectScript.presentTOS;

        if (cakeLR)     { txt_TickCake.text     = "_____"; }        else { txt_TickCake.text      = ""; }
        if (cupsLR)     { txt_TickCups.text     = "_______";}       else { txt_TickCups.text      = ""; }
        if (drinksLR)   { txt_TickDrinks.text   = "________";}      else { txt_TickDrinks.text    = ""; }
        if (pizzaLR)    { txt_TickPizza.text    = "_______";}       else { txt_TickPizza.text     = ""; }
        if (presnetLR)  { txt_TickPresents.text = "__________";}    else {  txt_TickPresents.text = ""; }
    }
}
