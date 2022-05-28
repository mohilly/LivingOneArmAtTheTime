using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    public string name;

    [Header("Moving")]
    public bool isMoving = false; // default standing in place
    public float speed = 4; //0-10 (km/h)
    public float stamina = 5; //0-10 (rank), 5 is "normal"

    [Header("Balance")]
    public bool need2KeepBalance = false; // false by default, triggered by certain types of action
    public float balance = 10; //0-10, if under 5 then character has a bigger chance to fall or drop items // MAYBE PUT 0-1
    public float strength = 5; //0-10, the bigger the number the slower the balance will be moved down, this will be conntected to the calculation of how fast balance 

    //-----------------------------------------------------------------------//

    //default constructor for person without disability 
    Character()
    {
        name = "TestCharacter";

        isMoving = false;
        speed = 4;
        stamina = 5;

        need2KeepBalance = false; // false by default, triggered by certain types of action
        balance = 1;
        strength = 5; // this will be conntected to the calculation of how fast balance 
    }
    
    // constructor for making characters
    public Character(string _Name, float _Speed, float _Stamina, float _Strength) 
    {
        name = _Name;
        speed = _Speed;
        stamina = _Stamina;

        need2KeepBalance = false; // false by default, triggered by certain types of action
        balance = 10;
        strength = _Strength; // this will be conntected to the calculation of how fast balance 
    }

    //Setters
    public string nameSet(string _Name)
    {
        name = _Name;
        return name;
    }
    public float speedSet(float _Speed)
    {
        speed = _Speed;
        return speed;
    }
    public float staminaSet(float _Stamina)
    {
        stamina = _Stamina;
        return stamina;
    }
    public float balanceSet(float _Balance)
    {
        balance = _Balance;
        return balance;
    }
    public float strengthSet(float _Strength)
    {
        strength = _Strength;
        return strength;
    }

    //Getters
    public string nameGet()
    {
        return name;
    }
    public float speedGet()
    {
        return speed;
    }
    public float staminaGet()
    {
        return stamina;
    }
    public float balanceGet()
    {
        return balance;
    }
    public float strengthGet()
    {
        return strength;
    }

    //Deconstructor
    ~Character(){}
}


