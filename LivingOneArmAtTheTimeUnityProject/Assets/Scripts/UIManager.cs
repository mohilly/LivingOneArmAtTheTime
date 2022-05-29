using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
   // AAAHHH THIS DOESN'T WORK
   public GameObject characters;
   public CharacterManager characterManager;

    public TMP_Text txt_speedNum;
    public TMP_Text txt_staminaNum;
    public TMP_Text txt_balanceNum;
    public TMP_Text txt_strengthNum;
    public TMP_Text txt_itemsNum;

    float speedUI = 0f;
    float staminaUI = 0f;
    float strengthUI = 0f;
    float balanceUI = 0f;
    int itemsUI = 0;

    private void Awake()
    { 
        // AAAHHH THIS DOESN'T WORK!!!!!
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();
    }

    void Start()
    {              
        //Debug.Log(characterManager.actSSSGet()[0]);
      
        //DON'T DO IT IN START - BC IT IS NOT BEING INITIALISED BC ALL IS HAPPENING IN START
        //characterManager.actSSSGet();
        speedUI    = characterManager.actSSSGet()[0];
        staminaUI  = characterManager.actSSSGet()[1];
        strengthUI = characterManager.actSSSGet()[2];
        
        //changing text on the UI
        txt_speedNum.text    = speedUI.ToString();
        txt_staminaNum.text  = staminaUI.ToString();
        txt_strengthNum.text = strengthUI.ToString();
        
        //Need to update this values according to Character Manager
        txt_balanceNum.text = balanceUI.ToString();
        txt_strengthNum.text = itemsUI.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        speedUI = characterManager.actSSSGet()[0];
        staminaUI = characterManager.actSSSGet()[1];
        strengthUI = characterManager.actSSSGet()[2];


        //changing text on the UI
        txt_speedNum.text = speedUI.ToString();
        txt_staminaNum.text = staminaUI.ToString();
        txt_strengthNum.text = strengthUI.ToString();

        //Need to update this values according to Character Manager
        txt_balanceNum.text = balanceUI.ToString();
        txt_strengthNum.text = itemsUI.ToString();
    }
}