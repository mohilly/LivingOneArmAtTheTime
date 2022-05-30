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

    int numberCharacter = 1;
    public string tagCharacter = "Tag_Armstrong";
    public string tagItemMain = "Tag_ItemMainArmstrong";
    public string tagItemSpaced = "Tag_ItemSpacedArmstrong";

    public bool controlArmstrong = false;
    public bool controlDahy = false;
    public bool controlDexter = false;

    float characterRotation = 0f;
    public bool switching = false;

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
    [Header("Action")]
    public bool isMoving = false;
    public bool isCaryingItem = false;
    public bool isSpacedItem = false; // second position of the item that imapcts the balance
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
    #region - Items -
    [Header("Items")]
    public Transform ItemDestinationMain;
    public Transform ItemDestinationSpaced;

    public bool canCarryItems = true;
    public bool itemMainCarry = false;
    public bool itemSpacedCarry = false;

    #endregion
    private void Awake()
    {
        characterActiveSet();
        mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();

        Armstrong = new Character("Armstrong", 7, 5, 7);
        Dahy = new Character("Dahy", 2, 10, 3);
        Dexter = new Character("Dexter", 5, 5, 10);

        activeSpeedStaminaStrength = new float[3] { Armstrong.speedGet(), Armstrong.staminaGet(), Armstrong.strengthGet() };

        gravityDirection = Vector3.down;

        tagCharacter = "Tag_Armstrong";
        tagItemMain = "Tag_ItemMainArmstrong";
        tagItemSpaced = "Tag_ItemSpacedArmstrong";
    }
    void Start()
    {
        currentStamina = actSSSGet()[1];
        ItemDestinationMain = GameObject.FindGameObjectWithTag(tagItemMain).transform;
        ItemDestinationSpaced = GameObject.FindGameObjectWithTag(tagItemSpaced).transform;
    }

    // Update is called once per frame
    void Update()
    {
        numCharSet();
        characterSwitch();
        if (switching) { currentStamina = actSSSGet()[1]; switching = false; }

        characterActiveSet();
        mainCam.transform.SetParent(activeCharacter.transform);

        // LOOK UP ON LINE ABOVE = CHANGE ITEMS PARENT TO THE EMPTY TRANSFORM OBJECT ALREADY ATTACHED TO PLAYER'S (HAND, LAP ETC), THEN CHANGE POSITION OF PICKED ITEM TO (0,0,0,) TO BE IN CENTER 

        calculateGravity();
        characterMove();
        characterStamina();

        if(Input.GetKeyDown(KeyCode.Escape)) { } //make sure to add EXITING OF THE GAME AND TERMINATING THE PROGRAM

        ItemDestinationMain = GameObject.FindGameObjectWithTag(tagItemMain).transform;
        ItemDestinationSpaced = GameObject.FindGameObjectWithTag(tagItemSpaced).transform;
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
                //Debug.Log("Armstrong active");
                tagCharacter = "Tag_Armstrong";
                tagItemMain = "Tag_ItemMainArmstrong";
                tagItemSpaced = "Tag_ItemSpacedArmstrong";
                controlArmstrong = true;
                controlDahy = false;
                controlDexter = false;
                actSSSSet(Armstrong);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                break;
            case 2:
                //Debug.Log("Dahy active");
                tagCharacter = "Tag_Dahy";
                tagItemMain = "Tag_ItemMainDahy";
                tagItemSpaced = "Tag_ItemSpacedDahy";
                controlArmstrong = false;
                controlDahy = true;
                controlDexter = false;
                actSSSSet(Dahy);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                break;
            case 3:
                //Debug.Log("Dexter active");
                tagCharacter = "Tag_Dexter";
                tagItemMain = "Tag_ItemMainDexter";
                tagItemSpaced = "Tag_ItemSpacedDexter";
                controlArmstrong = false;
                controlDahy = false;
                controlDexter = true;
                actSSSSet(Dexter);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                //Debug.Log("Speed: " + actSSSGet()[0] + ", Stamina " + actSSSGet()[1] + ", Strength: " + actSSSGet()[2]);
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

    #region - SETUP of Speed, Stamina, Strength -
    //Setting the Speed, Stamina and Strength of an active character
    public float[] actSSSSet(Character character)
    {
        activeSpeed = character.speedGet();
        activeStamina = character.staminaGet();
        activeStrength = character.strengthGet();

        float[] actSSS = { activeSpeed, activeStamina, activeStrength };
        activeSpeedStaminaStrength = actSSS;

        return activeSpeedStaminaStrength;
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
