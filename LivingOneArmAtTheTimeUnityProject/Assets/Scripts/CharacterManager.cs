using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region - Characters -
    [Header("Characters")]
    public GameObject activeCharacter;
    public CharacterController controller;
    public Camera mainCam;

    Character Armstrong;
    Character Dahy;
    Character Dexter;

    public int numberCharacter = 1;
    public string tagCharacter = "Tag_Armstrong";
    public string tagItemMain = "Tag_ItemMainArmstrong";
    public string tagItemSpaced = "Tag_ItemSpacedArmstrong";

    float characterRotation = 0f;
    public bool switching = false;



    //SPAGEHTTTIIIO
    //public bool instantatedDahy = false;
    //public bool instantatedDexter = false;

    #endregion
    #region - Movement input floats and vectors - 
    [Header("Movement")]
    float WSinput;
    float ADinput;

    Vector3 turning;
    Vector3 moving;
    #endregion
    #region - Speed, Stamina, Strength -
    [Header("3S")]
    float[] activeSpeedStaminaStrength;
    float activeSpeed;
    float activeStamina;
    float activeStrength;
    #endregion
    public float armstrongMaxHeight;
    public float armstrongMaxDistance;
    public float dahyMaxHeight;
    public float dahyMaxDistance;
    public float dexterMaxHeight;
    public float dexterMaxDistance;

    public float invalidHeight = 1.5f;
    public float invalidDistance = 2f;

    #region - Action -
    [Header("Carrying and Balance Action")]
    public bool isMoving = false;

    public bool need2KeepBalance = false; // false by default, triggered by certain types of action
    public float currentBalance = 10;
    public float currentStamina;
    #endregion
    #region - Gravity - 
    [Header("Gravity")]
    public float gravity;
    public float currentGravity;
    public float constantGravity;
    public float maxGravity;

    private Vector3 gravityDirection;
    private Vector3 gravityMovement;
    #endregion
    #region - Armstrong - 
    [Header("Armstrong stats")]
    public bool controlArmstrong = false;
    /// <summary>
    /// 3 bools: Can Carry items, item is in Main, item is Spaced
    /// </summary>
    public bool[] itemCarryArrayArmstrong;
    #endregion
    #region - Dahy - 
    [Header("Dahy stats")]
    public bool controlDahy = false;
    /// <summary>
    /// 3 bools: Can Carry items, item is in Main, item is Spaced
    /// </summary>
    public bool[] itemCarryArrayDahy;
    #endregion
    #region - Dexter - 
    [Header("Dexter stats")]
    public bool controlDexter = false;
    /// <summary>
    /// 3 bools: Can Carry items, item is in Main, item is Spaced
    /// </summary>
    public bool[] itemCarryArrayDexter;
    #endregion

    #region - Items -
    [Header("Items")]
    public Transform ItemDestinationMain;
    public Transform ItemDestinationSpaced;

    public bool canCarryItems_CM = true; //current character can carry items
    public bool itemMainCarry_CM = false; //current character is not carrying item in main position
    public bool itemSpcdCarry_CM = false; // second position of the item that imapcts the balance, current character is not carrying item in spaced position

    public bool[] itemCarryArr = { true, false, false};
    #endregion
    private void Awake()
    {
        characterActiveSet();
        mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();

        Armstrong = new Character("Armstrong", 7, 5, 7);
        Dahy = new Character("Dahy", 2, 10, 3);
        Dexter = new Character("Dexter", 5, 5, 10);

        gravityDirection = Vector3.down;

        tagCharacter = "Tag_Armstrong";
        tagItemMain = "Tag_ItemMainArmstrong";
        tagItemSpaced = "Tag_ItemSpacedArmstrong";

        //3 bools: Can Carry items, item is in Main, item is Spaced
        itemCarryArrayArmstrong = new bool[] { true, false, false };
        itemCarryArrayDexter = new bool[] { true, false, false };
        itemCarryArrayDahy = new bool[] { true, false, false };

        actSSSSet(Armstrong);
        actCMSSet();
        actCMSGet();
        actCMSUpdate();

        armstrongMaxHeight = 1.4f;
        armstrongMaxDistance = 1f;

        dahyMaxHeight = 3f;
        dahyMaxDistance = 1.5f;

        dexterMaxHeight = 1.8f;
        dexterMaxDistance = 1.2f;
    }

    void Start()
    {
        currentStamina = actSSSGet()[1];
        ItemDestinationMain = GameObject.FindGameObjectWithTag(tagItemMain).transform;
        ItemDestinationSpaced = GameObject.FindGameObjectWithTag(tagItemSpaced).transform;
    }
    void Update()
    {
        numCharSet();
        characterSwitch();
        if (switching) 
        { 
            currentStamina = actSSSGet()[1];
            actCMSSet();
            switching = false; 
        }

        characterActiveSet();
        mainCam.transform.SetParent(activeCharacter.transform);

        // LOOK UP ON LINE ABOVE = CHANGE ITEMS PARENT TO THE EMPTY TRANSFORM OBJECT ALREADY ATTACHED TO PLAYER'S (HAND, LAP ETC), THEN CHANGE POSITION OF PICKED ITEM TO (0,0,0,) TO BE IN CENTER 

        calculateGravity();
        characterMove();
        characterStamina();

        if(Input.GetKeyDown(KeyCode.Escape)) { } //make sure to add EXITING OF THE GAME AND TERMINATING THE PROGRAM
               
        ItemDestinationMain = GameObject.FindGameObjectWithTag(tagItemMain).transform;
        ItemDestinationSpaced = GameObject.FindGameObjectWithTag(tagItemSpaced).transform;

        actCMSUpdate();
    }

    #region - Active Character - 
    /*Getting the input from alphanumeric or kepad*/
    int numCharSet()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { switching = true; return numberCharacter = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { switching = true; return numberCharacter = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { switching = true; return numberCharacter = 3; }
        else { return numberCharacter; }
    }

    public void characterSwitch()
    {
        switch (numberCharacter)
        {
            case 1:
                tagCharacter = "Tag_Armstrong";
                tagItemMain = "Tag_ItemMainArmstrong";
                tagItemSpaced = "Tag_ItemSpacedArmstrong";
                controlArmstrong = true;
                controlDahy = false;
                controlDexter = false;
                invalidHeight = armstrongMaxHeight;
                invalidDistance = armstrongMaxDistance;
                actSSSSet(Armstrong);
                break;
            case 2:
                tagCharacter = "Tag_Dahy";
                tagItemMain = "Tag_ItemMainDahy";
                tagItemSpaced = "Tag_ItemSpacedDahy";
                controlArmstrong = false;
                controlDahy = true;
                controlDexter = false;
                invalidHeight   = dahyMaxHeight;
                invalidDistance = dahyMaxDistance;
                actSSSSet(Dahy);
                break;
            case 3:
                tagCharacter = "Tag_Dexter";
                tagItemMain = "Tag_ItemMainDexter";
                tagItemSpaced = "Tag_ItemSpacedDexter";
                controlArmstrong = false;
                controlDahy = false;
                controlDexter = true;
                invalidHeight   = dexterMaxHeight;
                invalidDistance = dexterMaxDistance;
                actSSSSet(Dexter);
                break;
        }
    }

    public Vector3 actCharPosGet()
    {
        return activeCharacter.transform.position;
    }

    public void characterActiveSet()
    {
        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();
    }
    #endregion

    #region - SETUP of Stats -
    /// <summary>
    /// Setting the Speed, Stamina and Strength of an active character. <br/>
    /// Setting up floats: activeSpeed, activeStamina and activeStrength. <br/>
    /// </summary>
    /// <param name="character"></param>
    public void actSSSSet(Character character)
    {
        activeSpeed = character.speedGet();
        activeStamina = character.staminaGet();
        activeStrength = character.strengthGet();

        float[] actSSS = { activeSpeed, activeStamina, activeStrength };
        activeSpeedStaminaStrength = actSSS;
    }

    //Getting the active Speed, Stamina and Strength
    public float[] actSSSGet()
    {
        return activeSpeedStaminaStrength;
    }

    public void characterStamina()
    {
        if (isMoving) { characterStaminaDecrease(); }
        else if (!isMoving) { characterStaminaIncrease(); }
    }
    #endregion

    #region - SET, GET & UPDATE of Carry, Main, Spaced (CMS)
    /// <summary>
    /// Setting up bools: canCarryItems, itemMainCarry and itemSpacedCarry. <br/>
    /// canCarryItems - can a character carry an item? <br/>
    /// itemMainCarry - is item being carried in the main slot? <br/>
    /// itemSpacedCarry - is item being carried in the spaced slot?
    /// </summary>
    /// <param name="character"></param>
    public void actCMSSet() // parameter Character character
    {

        itemCarryArr = actCMSGet();
    }

    /// <summary>
    /// Update (Set) Carry, Main and Spaced for the current character
    /// </summary>
    /// <param name="character"></param>
    public void actCMSUpdate() // parameter Character character
    {
        itemCarryArr = actCMSGet();
    }

    public void actCMSUpdate_CMc(bool carry)
    {
        canCarryItems_CM = carry;

        bool actCMS_c = canCarryItems_CM;
        itemCarryArr[0] = actCMS_c;
    }

    public void actCMSUpdate_CMm(bool main)
    {
        itemMainCarry_CM = main;

        bool actCMS_m = itemMainCarry_CM;
        itemCarryArr[1] = actCMS_m;
    }

    public void actCMSUpdate_CMs(bool spaced)
    {
        itemSpcdCarry_CM = spaced;

        bool actCMS_s = itemSpcdCarry_CM;
        itemCarryArr[2] = actCMS_s;
    }

    public bool [] itemCarryArrGet()
    {
        return itemCarryArr;
    }

   public bool [] actCMSGet()
   {
        bool [] actCMSGetArray = { true, false, false };

        switch (numberCharacter)
        {
            case 1:
                //Armstrong
                actCMSGetArray =  itemCarryArrayArmstrong;
                break;
            case 2:
                //Dahy
                actCMSGetArray =  itemCarryArrayDahy;
                break;
            case 3:
                //Dexter
                actCMSGetArray = itemCarryArrayDexter;
                break;
        }

        itemCarryArr =  actCMSGetArray;
        return itemCarryArr;
    }

    #endregion

    #region - Move, Stamina change -
    void characterMove()
    {
        //Input
        WSinput = Input.GetAxis("Vertical");
        ADinput = Input.GetAxis("Horizontal");

        //pressing A or D turns player
        characterRotation = ADinput;
        turning = new Vector3(0f, characterRotation, 0f);

        //Character always goes forward based on the way they are facing
        //NEED TO UPDATE THIS SO IT TAKES THE SPEED, AND INCREASES TO IT.
        moving = activeCharacter.transform.rotation * transform.forward * WSinput * actSSSGet()[0] * Time.deltaTime;

        if (WSinput != 0) { isMoving = true; } // Debug.Log("Character is moving");  confirmed it works
        else { isMoving = false; } // Debug.Log("Character is still")confirmed it works

        controller.Move(moving + gravityMovement);
        controller.transform.Rotate(turning);
    }

    void characterStaminaDecrease()
    {
        if (currentStamina > 0)
        { currentStamina -= 1f * Time.deltaTime; }
        else if (currentStamina <= 0)
        { currentStamina = 0; }
        //Debug.Log(currentStamina);
    }

    void characterStaminaIncrease()
    {
        if (currentStamina <= actSSSGet()[1])
        { currentStamina += 1f * Time.deltaTime; }
        if (currentStamina > actSSSGet()[1])
        { currentStamina = actSSSGet()[1]; }
        //Debug.Log("Increasing stamina to " + currentStamina);
    }
    #endregion

    #region - Gravity - 
    private bool isGrounded()
    {
        return controller.isGrounded;
    }
    private void calculateGravity()
    {
        if (isGrounded())
        {
            currentGravity = constantGravity;
        }
        else
        {
            if (currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMovement = gravityDirection * -currentGravity; //holding direction
    }
    #endregion
}
