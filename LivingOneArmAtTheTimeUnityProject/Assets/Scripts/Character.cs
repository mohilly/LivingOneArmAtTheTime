using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

    [Header("Name")]
    public string name;

    [Header("3S")]
    public float speed = 4; //0-10 (km/h)
    public float stamina = 5; //0-10 (rank), 5 is "normal"
    public float strength = 5; //0-10, the bigger the number the slower the balance will be moved down, this will be conntected to the calculation of how fast balance 

    [Header("Balance")]
    public float balance = 10; //0-10, if under 5 then character has a bigger chance to fall or drop items // MAYBE PUT 0-1


    //-----------------------------------------------------------------------//

    //default constructor for person without disability 
    Character()
    {
        name = "TestCharacter";

        speed = 4;
        stamina = 5;
        strength = 5; // this will be conntected to the calculation of how fast balance 

        balance = 10;
    }
    
    // constructor for making characters
    public Character(string _Name, float _Speed, float _Stamina, float _Strength) 
    {
        name = _Name;

        speed = _Speed;
        stamina = _Stamina;
        strength = _Strength; // this will be conntected to the calculation of how fast balance

        balance = 10;        
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
    public float strengthSet(float _Strength)
    {
        strength = _Strength;
        return strength;
    }
    public float balanceSet(float _Balance)
    {
        balance = _Balance;
        return balance;
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
    public float strengthGet()
    {
        return strength;
    }
    public float balanceGet()
    {
        return balance;
    }

    //Deconstructor
    ~Character(){}
}


