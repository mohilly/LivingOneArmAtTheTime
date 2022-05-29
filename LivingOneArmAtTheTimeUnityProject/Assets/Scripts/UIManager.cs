using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject characters;
    public CharacterManager characterManager;

    public TMP_Text txt_speedNum;
    public TMP_Text txt_staminaNum;
    public TMP_Text txt_balanceNum;
    public TMP_Text txt_strengthNum;
    public TMP_Text txt_itemsNum;

    private void Awake()
    {
        characterManager = characters.GetComponent<CharacterManager>();
    }
    void Start()
    {
        characterManager.actSSSGet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
