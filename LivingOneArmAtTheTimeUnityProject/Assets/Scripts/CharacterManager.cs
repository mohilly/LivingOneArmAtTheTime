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
        //ASK YOURSELF, DO I NEED THIS SHIT
        tagItemMain = "Tag_ItemMainArmstrong";
        tagItemSpaced = "Tag_ItemSpacedArmstrong";

        //3 bools: Can Carry items, item is in Main, item is Spaced
        itemCarryArrayArmstrong = new bool[] { true, false, false };
        itemCarryArrayDexter = new bool[] { true, false, false };
        itemCarryArrayDahy = new bool[] { true, false, false };
        //Debug.Log(itemCarryArrayArmstrong[0]);

        actSSSSet(Armstrong);
        //actCMSSet(Armstrong);
        actCMSSet();
        actCMSGet();
        actCMSUpdate();

        //activeSpeedStaminaStrength = new float[3] { Armstrong.speedGet(), Armstrong.staminaGet(), Armstrong.strengthGet() };
        //itemCarryArr = new bool[3] { canCarryItems, itemMainCarry, itemSpacedCarry };
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

        //DOES THIS ITEM DESTINATION MUST BE HERE? LIKE IN THAT WAY, OR SHOULD WE SET IT IN A FUNCTION THAT IS BEING CALLED?
        ItemDestinationMain = GameObject.FindGameObjectWithTag(tagItemMain).transform;
        ItemDestinationSpaced = GameObject.FindGameObjectWithTag(tagItemSpaced).transform;

        //actCMSUpdate(Armstrong);
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
                //actCMSSet(Armstrong);
                //actCMSUpdate(Armstrong);
                actSSSSet(Armstrong);
                break;
            case 2:
                tagCharacter = "Tag_Dahy";
                tagItemMain = "Tag_ItemMainDahy";
                tagItemSpaced = "Tag_ItemSpacedDahy";
                controlArmstrong = false;
                controlDahy = true;
                controlDexter = false;
                //actCMSSet(Dahy);
                //actCMSUpdate(Dahy);
                actSSSSet(Dahy);
                break;
            case 3:
                tagCharacter = "Tag_Dexter";
                tagItemMain = "Tag_ItemMainDexter";
                tagItemSpaced = "Tag_ItemSpacedDexter";
                controlArmstrong = false;
                controlDahy = false;
                controlDexter = true;
                //actCMSSet(Dexter);
                //actCMSUpdate(Dexter);
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

    /// <summary>
    /// Setting up bools: canCarryItems, itemMainCarry and itemSpacedCarry. <br/>
    /// canCarryItems - can a character carry an item? <br/>
    /// itemMainCarry - is item being carried in the main slot? <br/>
    /// itemSpacedCarry - is item being carried in the spaced slot?
    /// </summary>
    /// <param name="character"></param>
    public void actCMSSet() // parameter Character character
    {
        //canCarryItems_CM = character.canCarryItemsGet();
        //itemMainCarry_CM = character.itemMainCarryGet();
        //itemSpcdCarry_CM = character.itemSpacedCarryGet();
        
        //canCarryItems_CM = actCMSGet()[0];
        //itemMainCarry_CM = actCMSGet()[1];
        //itemSpcdCarry_CM = actCMSGet()[2];
        //
        //bool[] actCMS = { canCarryItems_CM, itemMainCarry_CM, itemSpcdCarry_CM };
        //itemCarryArr = actCMS;

        itemCarryArr = actCMSGet();

        //Debug.Log("actCMSSet is being called in CharacterManager.cs, and itemMainCarry_CM = " + itemMainCarry_CM); // this is called so many times it is evident it is overwritten by some shit
    }

    /// <summary>
    /// Update (Set) Carry, Main and Spaced for the current character
    /// </summary>
    /// <param name="character"></param>
    public void actCMSUpdate() // parameter Character character
    {
        itemCarryArr = actCMSGet();

        //TO BE UPDATED....TO TAKE CURRENTLY ACTIVE STATS ONLY. NOT OF A CHARACTER.
        //character.canCarryItemsSet(itemCarryArr[0]);
        //character.itemMainCarrySet(itemCarryArr[1]);
        //character.itemSpacedCarrySet(itemCarryArr[2]);
        //Debug.Log("Updating character carry bools in actCMSUpdate. itemCarryArr = { " + itemCarryArr[0]+ ", " + itemCarryArr[1] + ", " + itemCarryArr[2]+ " }"); // BEING CALLED
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
        //Debug.Log("actCMSUpdate_CMm is being called in CharacterManager.cs and itemCarryArr[1] = " + actCMS_m); // BEING CALLED
    }

    public void actCMSUpdate_CMs(bool spaced)
    {
        itemSpcdCarry_CM = spaced;

        bool actCMS_s = itemSpcdCarry_CM;
        itemCarryArr[2] = actCMS_s;
    }

    public bool [] itemCarryArrGet()
    {
        //Debug.Log("itemCarryArrGet function in CharacterManager.cs called");
        return itemCarryArr;
    }

   public bool [] actCMSGet()
   {
        //Debug.Log("actCMSGet function in CharacterManager.cs called");
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
