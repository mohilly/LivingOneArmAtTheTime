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

    // Start is called before the first frame update
    void Start()
    {
        ///Debug.Log(tagCharacter);
        activeCharacter = GameObject.FindGameObjectWithTag(tagCharacter);
        controller = activeCharacter.GetComponent<CharacterController>();

        Armstrong = new Character("Armstrong", 10, 5, 7);
        Dahy = new Character("Dahy", 5, 10, 3);
        Dexter = new Character("Dexter", 7, 5, 10);

        //Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moving = transform.right * x + transform.forward * z; // change so character cannot go left and right, but rather rotate to those directions

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
