using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interactable : MonoBehaviour
{
    public GameObject characters;
    public CharacterManager characterManager;

    public bool isBeingCarried = false;
    public bool canBePickedUp = false;

    //THESE BOOLS ARE NOT BEING USED CURRENTLY, REDUNDANT, THINK ABOUT REMOVING THEM. SOUNDS LIKE A THING ON A CHARACTER.CS
    //BOOL TO FETCH FROM A CURRENTLY ACTIVE CHARACTER.....
    public bool canCarryItems_i = true;
    public bool itemMainCarry_i = false;
    public bool itemSpcdCarry_i = false;

    public TMP_Text txt_warning;

    public float invalidHeight_i = 1.5f;

    private void Awake()
    {
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();

        txt_warning = GameObject.Find("Text (TMP) Ingame Warning").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        actCMSGet_i(characterManager); //fetching currently active stats for Carry Bools
        invalidHeight_i = characterManager.invalidHeight;
    }
    private void Update()
    {
        actCMSGet_i(characterManager); //fetching currently active stats for Carry Bools

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            itemSpace();
        }

        itemCarry();
        OnMouseDown();

        invalidHeight_i = characterManager.invalidHeight;

        //If character is carrying something in spaced, then balance needs to be kept, that is it is decreased
        if (itemSpcdCarry_i) { characterManager.need2KeepBalance = true; }
        else if (!itemSpcdCarry_i) { characterManager.need2KeepBalance = false; }
        if (characterManager.currentBalance <=0 && this.transform.position == characterManager.ItemDestinationSpaced.position) 
        {
            itemDrop();
            actCMSSet_iS(false);
            isBeingCarried = false;
        }

        if (characterManager.currentStaminaGet() <= 0)
        {
            txt_warning.text = "I am tired. I need to stop for a couple of seconds..";
            StartCoroutine(RemoveSubtitles());
        }


    }
    #region - Items - 
    public void itemCarry()
    {
        if (itemMainCarry_i && itemSpcdCarry_i) { actCMSSet_iC(false); } //if character has both slots filled with items then they cannot carry more
        else { actCMSSet_iC(true); } //if any of these or if both are false, then player can carry more items
    }
    /// <summary>
    /// Switching between main and spaced item
    /// </summary>
    public void itemSpace()
    {
        if (itemMainCarry_i || itemSpcdCarry_i)
        { 
            if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform)
            {
                GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
                this.transform.position = characterManager.ItemDestinationSpaced.position;
                this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemSpaced).transform;
                this.transform.localPosition = new Vector3(0, 0, 0); // WANTED TO RESET THE POSITION TO 0,0,0 SO IT IS IN THE DEAD CENTER OF THAT POSITION //no work
            }
            else if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemSpaced).transform)
            {
                GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall          
                this.transform.position = characterManager.ItemDestinationMain.position;
                this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform;
                this.transform.localPosition = new Vector3(0, 0, 0); // WANTED TO RESET THE POSITION TO 0,0,0 SO IT IS IN THE DEAD CENTER OF THAT POSITION // no work
            }

            if ((itemMainCarry_i && !itemSpcdCarry_i) || (itemSpcdCarry_i && !itemMainCarry_i))
            {
                actCMSSet_iM(!itemMainCarry_i);
                actCMSSet_iS(!itemSpcdCarry_i);
            }
            else if (itemMainCarry_i && itemSpcdCarry_i)
            {
                actCMSSet_iM(true);
                actCMSSet_iS(true);
            }
        }

        if (!itemMainCarry_i && !itemSpcdCarry_i)
        {
            //do nothing
        }
    }
    //USE LATER
    public void itemPlace()
    {
        //function to place an object down from MAIN slot
    }
    #endregion

    public void OnMouseDown()
    {
        //By pressing left click character picks up items AND PUTS THEM TO MAIN ONLY
        if (Input.GetMouseButtonDown(0) && !isBeingCarried && canBePickedUp) //&& (playerCasting.toTarget <= 2)) //Debug.Log("Pressed left click."); //IF ITEM IS NOT BEING CARRIED CURRENTLY.
        {
            if (invalidHeight_i < this.transform.position.y) // if this item's y value (height) is bigger then current characters max height value, that character cannot reach that item
            { 
                txt_warning.text = "This item is too high for me to reach!\nMaybe my roomate can reach it.";
                StartCoroutine(RemoveSubtitles());
            } 
            else 
            {
                if (canCarryItems_i)
                {
                    if ((!itemMainCarry_i && itemSpcdCarry_i) || (!itemMainCarry_i && !itemSpcdCarry_i))
                    {
                        itemPickUp();
                        actCMSSet_iM(true);
                        isBeingCarried = true;
                    }
                    else if (itemMainCarry_i && !itemSpcdCarry_i) //iF character is carrying something in the man slot, but spaced is free
                    {
                        txt_warning.text = "I cannot carry another item in my main hand. \n(Press [SPACE] to move item to the secondary place)";
                        StartCoroutine(RemoveSubtitles());
                    }
                }
                else
                { 
                    txt_warning.text = "I cannot carry any more items!";
                    StartCoroutine(RemoveSubtitles());
                }
            }
        }

        //By pressing right click character drops items from MAIN ONLY
        if (Input.GetMouseButtonUp(1))
        {
            //if (this.transform.parent == GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform)
            if (itemMainCarry_i) // if item is in the main carry slot AND if this item is at the currently active destination
            {
                if (this.transform.position == characterManager.ItemDestinationMain.position)
                {
                    itemDrop();
                    actCMSSet_iM(false);
                    isBeingCarried = false;
                }
                else if (this.transform.position == characterManager.ItemDestinationSpaced.position)
                {
                    isBeingCarried = true;
                    txt_warning.text = "I have put this item into my seconadry place";
                    StartCoroutine(RemoveSubtitles());
                }
            }
            if (itemSpcdCarry_i) // if item is in the main carry slot AND if this item is at the currently active destination
            {
               if (this.transform.position == characterManager.ItemDestinationMain.position)
               {
               }
               else if (this.transform.position == characterManager.ItemDestinationSpaced.position)
               {
                    txt_warning.text = "I must have item in my main hand to put it down deliberately.\n(Press [SPACE] to move item from secondary place)";
                    StartCoroutine(RemoveSubtitles());
                    isBeingCarried = true;
               }
            }
        }
    }

    void itemPickUp()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false; // making sure the object character picks up does not fall
        this.transform.position = characterManager.ItemDestinationMain.position;
        this.transform.parent = GameObject.FindGameObjectWithTag(characterManager.tagItemMain).transform;
        txt_warning.text = "I have picked up an item.";
        StartCoroutine(RemoveSubtitles());
    }

    void itemDrop()
    {
        this.transform.forward = new Vector3 (5, 5, 5);
        this.transform.parent = null; // removing parent
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        txt_warning.text = "I have dropped the item.";
        StartCoroutine(RemoveSubtitles());
    }

    public void actCMSGet_i(CharacterManager characterManager)
    {
        bool[] actCMSGetArray = { true, false, false };
        actCMSGetArray = characterManager.itemCarryArrGet();

        canCarryItems_i = actCMSGetArray[0];
        itemMainCarry_i = actCMSGetArray[1];
        itemSpcdCarry_i = actCMSGetArray[2];
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

    IEnumerator RemoveSubtitles()
    {
        yield return new WaitForSeconds(3);
        txt_warning.text = " ";
    }
}
