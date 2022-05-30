using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject characters;
    public CharacterManager characterManager;

    private void Awake()
    {
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();
    }

    private void Update()
    {
        //switching between main and spaced item
        if (Input.GetKeyDown(KeyCode.Space)) { itemSpace(); }
    }

    //THIS CODE NEEDS TO BE IN A SPECIAL SCRIPT ATTACHED TO INTERACTABLE OBJECTS OK
    #region - Items - 
    public void itemCarry()
    {
        if (characterManager.itemMainCarry && characterManager.itemMainCarry) { characterManager.canCarryItems = false; } //if character has both slots filled with items then they cannot carry more
        else { characterManager.canCarryItems = true; } //if any of these or if both are false, then player can carry more items

        if (characterManager.canCarryItems)
        {
            if (characterManager.itemMainCarry && !characterManager.itemSpacedCarry)
            {
                //CREATE VISUAL TEXT TO SAY PLAYER NEEDS TO SPACE ITEMS
            }
            else if (characterManager.itemMainCarry && !characterManager.itemSpacedCarry)
            {
                //then player can pick up stuff
                OnMouseDown();
            }
        }
        else
        {
            //create error to show on the screen saying "I cannot carry more items"
        }
    }

    //function to switch from Main to Spaced and vice versa
    public void itemSpace()
    {
        if (characterManager.itemMainCarry || characterManager.itemSpacedCarry)
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
        if (!characterManager.itemMainCarry && !characterManager.itemSpacedCarry)
        {
            //do nothing
        }
    }

    public void itemPlace()
    {
        //function to place an object down from MAIN slot
    }

    public void OnMouseDown()
    {
        //By pressing left click character picks up items AND PUTS THEM TO MAIN ONLY
        if (Input.GetMouseButtonDown(0)) //Debug.Log("Pressed left click.");
        {
            if (!characterManager.itemMainCarry)
            {
                GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
                this.transform.position = characterManager.ItemDestinationMain.position;
                this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform;
                characterManager.itemMainCarry = true;
            } else if (characterManager.itemMainCarry)
            {
                //TELL PLAYER TO SPACE
            }
        }

        //By pressing right click character drops items from MAIN ONLY
        if  (Input.GetMouseButtonDown(1))
        {
            if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform)
            {
                this.transform.parent = null; // removing parent
                GetComponent<Rigidbody>().useGravity = true;

                if (characterManager.itemMainCarry && !characterManager.itemSpacedCarry)
                {
                    //CREATE VISUAL TEXT TO SAY PLAYER NEEDS TO SPACE ITEMS
                }
                characterManager.itemMainCarry = false;
            }
        }
    }
    #endregion
}
