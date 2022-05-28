using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject activeCharacter;
    int numberCharacter = 1;
    string tagCharacter = "Tag_Armstrong";

    public bool controlArmstrong = false;
    public bool controlDahy = false;
    public bool controlDexter = false;

    Character Armstrong;
    Character Dahy;
    Character Dexter;

    public CharacterController controller;
    float characterRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ///Debug.Log(tagCharacter);
        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();

        Armstrong = new Character("Armstrong", 10, 5, 7);
        Dahy = new Character("Dahy", 5, 10, 3);
        Dexter = new Character("Dexter", 7, 5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        numCharSet();
        switch (numberCharacter)
        {
            case 1:
                Debug.Log("Armstrong active");
                tagCharacter = "Tag_Armstrong";
                controlArmstrong = true;
                controlDahy = false;
                controlDexter = false;
                break;
            case 2:
                Debug.Log("Dahy active");
                tagCharacter = "Tag_Dahy";
                controlArmstrong = false;
                controlDahy = true;
                controlDexter = false;
                break;
            case 3:
                Debug.Log("Dexter active");
                tagCharacter = "Tag_Dexter";
                controlArmstrong = false;
                controlDahy = false;
                controlDexter = true;
                break;
        }

        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();

        //Input
        float ADinput = Input.GetAxis("Horizontal");
        float WSinput = Input.GetAxis("Vertical");

        // pressing A or D turns player
        characterRotation = ADinput;
        Vector3 turning = new Vector3(0f, characterRotation, 0f); 

        //Character always goes forward based on the way they are facing
        Vector3 moving = activeCharacter.transform.rotation * transform.forward * WSinput * Time.deltaTime; 

        controller.Move(moving);
        controller.transform.Rotate(turning);
    }

    /*Getting the input from alphanumeric or kepad*/
    int numCharSet() 
    {
        if      (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { return numberCharacter = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { return numberCharacter = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { return numberCharacter = 3; }
        else { return numberCharacter; }
    }
}
