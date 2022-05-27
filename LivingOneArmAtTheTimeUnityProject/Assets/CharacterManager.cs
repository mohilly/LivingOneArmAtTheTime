using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{   
    Character Armstrong;
    Character Dahy;
    Character Dexter;

    bool right = false;
    bool left = false;
    bool forward = false;
    bool backward = false;



    // Start is called before the first frame update
    void Start()
    {
        //Character Stats Allocation
        Armstrong.nameSet("Armstrong");
        Armstrong.speedSet(10);
        Armstrong.staminaSet(5);
        Armstrong.balanceSet(10);
        Armstrong.strengthSet(7);

        Dahy.nameSet("Dahy");
        Dahy.speedSet(3);
        Dahy.staminaSet(10);
        Dahy.balanceSet(10);
        Dahy.strengthSet(10);

        Dexter.nameSet("Dexter");
        Dexter.speedSet(3);
        Dexter.staminaSet(10);
        Dexter.balanceSet(10);
        Dexter.strengthSet(10);

    }

    // Update is called once per frame
    void Update()
    {
        if (right&&left) //if player presses both left and right at the same time
        {
            right = false;
            left = false;
        }

        if (forward && backward) //if player presses both forward and backward at the same time
        {
            forward = false;
            backward = false;
        }

        switch (right)
        {
            case true:
                // code block
                break;
            case false:
                // code block
                break;
            default:
                // code block
                break;
        }

        switch (left)
        {
            case true:
                // code block
                break;
            case false:
                // code block
                break;
            default:
                // code block
                break;
        }

        switch (forward)
        {
            case true:
                // code block
                break;
            case false:
                // code block
                break;
            default:
                // code block
                break;
        }

        switch (backward)
        {
            case true:
                // code block
                break;
            case false:
                // code block
                break;
            default:
                // code block
                break;
        }

    }

}
