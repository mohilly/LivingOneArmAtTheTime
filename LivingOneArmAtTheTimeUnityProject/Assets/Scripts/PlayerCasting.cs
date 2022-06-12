using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float distanceFromTarget;
    public float toTarget;

    [SerializeField] private string interactableTag = "Tag_Interactable";
    [SerializeField] private Material materialSelected;
    [SerializeField] private Material materialDefault;

    public Transform _selection;
    public Transform selection;
    //public Renderer selectionRenderer;

    public GameObject characters;
    public CharacterManager characterManager;

    private void Awake()
    {
        characters = GameObject.FindGameObjectWithTag("Tag_Character");
        characterManager = characters.GetComponent<CharacterManager>();

    }

    void Update()
    {
        //if (selectionRenderer != null) 
        {
            //Debug.Log("Renderer exists");
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = materialDefault;
                selectionRenderer.GetComponent<Interactable>().canBePickedUp = false; //interactable object cannnot be picked up when not looked at
                _selection = null;
            }

            //raycasting from camera  // maybe I don't need this?
            //var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit,characterManager.invalidDistance))
            {
                selection = Hit.transform;

                if (selection.CompareTag("Tag_Interactable"))
                {
                    toTarget = Hit.distance;
                    distanceFromTarget = toTarget;

                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = materialSelected;
                        selectionRenderer.GetComponent<Interactable>().canBePickedUp = true; //interactable object can be picked up only when looked at
                    }

                    _selection = selection;
                }
            }
        } //else if (selectionRenderer == null) { Debug.Log("Object has no renderer"); }

    }
}
