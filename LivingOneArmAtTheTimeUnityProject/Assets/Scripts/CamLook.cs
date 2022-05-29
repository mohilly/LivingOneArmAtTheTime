using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    CharacterManager characterManager;
    [SerializeField] GameObject CharactersGO;
    public Transform character2Follow;
    //public Transform targetCharacter; //TESTING

    public Vector3 actCharPos;
    public Quaternion actCharRot;
    public Vector3 camOffset = new Vector3(0f, 1.75f, -1f);
    public Vector3 camOffsetCurrent;

    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float smoothSpeed = 0.125f;
    Quaternion targetRotation;

    private void Awake()
    {
        characterManager = CharactersGO.GetComponent<CharacterManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //camOffset = transform.position - characterManager.activeCharacter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //targetCharacter = characterManager.activeCharacter.transform;

        actCharPos = characterManager.activeCharacter.transform.position;
        actCharRot = characterManager.activeCharacter.transform.rotation;


        // PART FOR MOUSE MOVEMENT
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, - 45f, 45f); //-45f, 45f OR actCharRot.y * (- 45f), actCharRot.y * 45f
      
        yRotation += mouseX;                            //SOLUTION 1, NO GO
        yRotation = Mathf.Clamp(yRotation, - 45f, 45f); //SOLUTION 4, NO GO
      
        targetRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothSpeed);
      
        character2Follow = characterManager.activeCharacter.transform;
        
        camOffsetCurrent = camOffset;

        //camOffsetCurrent += new Vector3(0,,0);
        //SOLUTION 3, NO GO, Tried to sort out X distance based on quaternion rotation of the object
        //camOffsetCurrent.x = camOffset.x - actCharRot.y;
        //camOffsetCurrent.y = camOffset.y * actCharRot.y;
        //camOffsetCurrent.z = -1;        

        //Vector3 newCamPos = actCharPos + camOffsetCurrent; // should I replace "actCharPos" with "character2Follow.position"
        Vector3 newCamPos = camOffsetCurrent; // should I replace "actCharPos" with "character2Follow.position"
        //Vector3 newCamPos = actCharRot.y * actCharPos + camOffset; //SOLUTION 2, NO GO, Internet said you need to multiply quaternion rotation of the character with its position for camera to stay behind, but it just weirldy leaves camera on the spot and rotates odd
        
        transform.localPosition = newCamPos;
        //transform.rotation = actCharRot * Quaternion.Euler(camOffsetCurrent.x, camOffsetCurrent.z, 0f);

        //transform.position = targetCharacter.position + camOffsetCurrent; // SOLUTION 5, NO GO, DOES THE SAME THING - TESTING
        //transform.LookAt(actCharPos); // SOLUTION 6, NO GO, THEN ROTATION DOESN'T WORK ON MOUSE INPT
        //transform.Translate(actCharPos, Space.Self); //SOLUTION 7, NO GO, DOES SOME WEIRD SHIT
    }



    Vector3 getRotation() 
    {

        Vector3 ans;
        float radius = 2;

        float theta = (2 * Mathf.PI) ;

        float x = radius * Mathf.Sin(theta);
        float z = radius * Mathf.Cos(theta);

        ans = new Vector3(x, transform.position.y, -z);

        return ans;
    
    }

    private void LateUpdate()
    {

    }
}
