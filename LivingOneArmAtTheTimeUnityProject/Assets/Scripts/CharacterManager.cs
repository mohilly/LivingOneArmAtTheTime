using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject activeCharacter;
    public CharacterController controller;
    public Camera mainCam;

    Character Armstrong;
    Character Dahy;
    Character Dexter;

    int numberCharacter = 1;
    public string tagCharacter = "Tag_Armstrong";

    public bool controlArmstrong    = false;
    public bool controlDahy         = false;
    public bool controlDexter       = false;

    float characterRotation = 0f;

    float WSinput;
    float ADinput;

    Vector3 turning;
    Vector3 moving;

    float[] activeSpeedStaminaStrength;
    float activeSpeed;
    float activeStamina;
    float activeStrength;

    private void Awake()
    {
        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();
        mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();

        Armstrong   = new Character("Armstrong", 10, 5, 7);
        Dahy        = new Character("Dahy", 1, 10, 3);
        Dexter      = new Character("Dexter", 7, 5, 10);

        activeSpeedStaminaStrength = new float [3] { Armstrong.speedGet(), Armstrong.staminaGet(), Armstrong.strengthGet()};
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        numCharSet();
        switch (numberCharacter)
        {
            case 1:
                //Debug.Log("Armstrong active");
                tagCharacter = "Tag_Armstrong";
                controlArmstrong = true;
                controlDahy = false;
                controlDexter = false;
                actSSSSet(Armstrong);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                break;
            case 2:
                //Debug.Log("Dahy active");
                tagCharacter = "Tag_Dahy";
                controlArmstrong = false;
                controlDahy = true;
                controlDexter = false;
                actSSSSet(Dahy);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                break;
            case 3:
                //Debug.Log("Dexter active");
                tagCharacter = "Tag_Dexter";
                controlArmstrong = false;
                controlDahy = false;
                controlDexter = true;
                actSSSSet(Dexter);
                //Debug.Log("Speed: " + activeSpeedStaminaStrength[0] + ", Stamina " + activeSpeedStaminaStrength[1] + ", Strength: " + activeSpeedStaminaStrength[2]);
                //Debug.Log("Speed: " + actSSSGet()[0] + ", Stamina " + actSSSGet()[1] + ", Strength: " + actSSSGet()[2]);
                break;
        }

        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();
        mainCam.transform.SetParent(activeCharacter.transform); 
        
        // LOOK UP ON LINE ABOVE = CHANGE ITEMS PARENT TO THE EMPTY TRANSFORM OBJECT ALREADY ATTACHED TO PLAYER'S (HAND, LAP ETC), THEN CHANGE POSITION OF PICKED ITEM TO (0,0,0,) TO BE IN CENTER 

        characterMove();
    }

    /*Getting the input from alphanumeric or kepad*/
    int numCharSet() 
    {
             if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { return numberCharacter = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { return numberCharacter = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { return numberCharacter = 3; }
        else { return numberCharacter; }
    }

    public Vector3 actCharPosGet()
    {
        return activeCharacter.transform.position;
    }

    //Setting the Speed, Stamina and Strength of an active character
    public float[] actSSSSet(Character character)
    {
        activeSpeed = character.speedGet();
        activeStamina = character.staminaGet();
        activeStrength = character.strengthGet();
        
        float[] actSSS = {activeSpeed, activeStamina, activeStrength};
        activeSpeedStaminaStrength = actSSS;

        return activeSpeedStaminaStrength;
    }

    //Getting the active Speed, Stamina and Strength
    public float[] actSSSGet()
    {
        return activeSpeedStaminaStrength;
    }

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

        controller.Move(moving);
        controller.transform.Rotate(turning);
    }
}
