using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject characters;
    public CharacterManager characterManager;

    public bool isBeingCarried = false;

    //THESE BOOLS ARE NOT BEING USED CURRENTLY, REDUNDANT, THINK ABOUT REMOVING THEM. SOUNDS LIKE A THING ON A CHARACTER.CS
    //BOOL TO FETCH FROM A CURRENTLY ACTIVE CHARACTER.....
    //BUT THEN THE CURRENTLY ACTIVE CHARACTER WOULD HAVE TO FETCH THIS BACK AGAIN EVERY TIME YOU FUCKING UPDATE THE BOOL IN HERE.
    //SO MAYBE DELETE THESE
    public bool canCarryItems_i = true;
    public bool itemMainCarry_i = false;
    public bool itemSpcdCarry_i = false;

    private void Awake()
    {
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();
    }
    private void Start()
    {
        actCMSGet_i(characterManager); //fetching currently active stats for Carry Bools
    }

    private void Update()
    {
        actCMSGet_i(characterManager); //fetching currently active stats for Carry Bools

        //switching between main and spaced item
        if (Input.GetKeyDown(KeyCode.Space)) { itemSpace(); }
        itemCarry();
        OnMouseDown();
        //OnMouseUp();
    }

    //THIS CODE NEEDS TO BE IN A SPECIAL SCRIPT ATTACHED TO INTERACTABLE OBJECTS OK
    #region - Items - 
    public void itemCarry()
    {
        if (canCarryItems_i && itemSpcdCarry_i) { actCMSSet_iC(true); } //if character has both slots filled with items then they cannot carry more
        else { characterManager.actCMSUpdate_CMc(true); } //if any of these or if both are false, then player can carry more items
    }

    //function to switch from Main to Spaced and vice versa //FIX LATER
    public void itemSpace()
    {
        if (itemMainCarry_i || itemSpcdCarry_i)
        { 
            if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform)
            {
                GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
                this.transform.position = characterManager.ItemDestinationSpaced.position;
                this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemSpaced).transform;
            }
            else if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemSpaced).transform)
            {
                GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
                this.transform.position = characterManager.ItemDestinationMain.position;
                this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform;
            }
        }
        if (!itemMainCarry_i && !itemSpcdCarry_i)
        {
            //do nothing
        }
    }

    //FIX/USE LATER
    public void itemPlace()
    {
        //function to place an object down from MAIN slot
    }

    public void OnMouseDown()
    {
        //By pressing left click character picks up items AND PUTS THEM TO MAIN ONLY
        if (Input.GetMouseButtonDown(0) && !isBeingCarried) //Debug.Log("Pressed left click."); //IF ITEM IS NOT BEING CARRIED CURRENTLY.
        {
            if (canCarryItems_i)
            {
                if (itemMainCarry_i && !itemSpcdCarry_i) //iF character is carrying something in the man slot, but spaced is free
                {
                    //CREATE VISUAL TEXT TO SAY PLAYER NEEDS TO SPACE ITEMS
                    //Debug.Log("Already carrying an item in main hand! Space it up!"); // Works!
                }
                else if ((!itemMainCarry_i && itemSpcdCarry_i) || (!itemMainCarry_i && !itemSpcdCarry_i))
                {
                    //then player can pick up stuff
                    //OnMouseDown();
                    // the ITEM IS PICKED.

                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
                    this.transform.position = characterManager.ItemDestinationMain.position;
                    this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform;
                    actCMSSet_iM(true);
                    isBeingCarried = true;
                }
            }
            else
            {
                //create error to show on the screen saying "I cannot carry more items"
            }
        }

        //By pressing right click character drops items from MAIN ONLY
        if (Input.GetMouseButtonUp(1))
        {
            //if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform)
            if (itemMainCarry_i)
            {

                this.transform.parent = null; // removing parent
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                Debug.Log("Right click pressed");
                isBeingCarried = false;
                itemMainCarry_i = false;
            }

            if (!itemMainCarry_i && itemSpcdCarry_i)
            {
                //CREATE VISUAL TEXT TO SAY PLAYER NEEDS TO SPACE ITEMS
                //ITEM IS DROPPED THEN.
            }
        }
    }

    public void OnMouseUp()
    {

    }
    #endregion

    public void actCMSGet_i(CharacterManager characterManager)
    {
        canCarryItems_i = characterManager.actCMSGet()[0];
        itemMainCarry_i = characterManager.actCMSGet()[1];
        itemSpcdCarry_i = characterManager.actCMSGet()[2];
    }

    public void actCMSSet_iC(bool carry)
    {
        characterManager.actCMSUpdate_CMc(carry);
    }

    public void actCMSSet_iM(bool main)
    {
        characterManager.actCMSUpdate_CMm(main);
    }

    public void actCMSSet_iS(bool spaced)
    {
        characterManager.actCMSUpdate_CMs(spaced);
    }
}
