using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanManager : MonoBehaviour
{
    //Raciones
    private int triad;

    //Flores

    private int snowFlower;
    private int waterBubble;
    private int bulbClaw;

    //Personajes

    List<Characters> characters;

    //Walk Counter

    private int stepCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        Characters test = new Characters("Pablonsky", 990, 4, 5, 6);

        characters = new List<Characters>
        {
            new Characters("Satz", 30, 4, 5, 6),
            new Characters("Kiev", 30, 4, 5, 6)
        };
        AddCharacter(test);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            stepCounter++;
            Debug.Log(stepCounter);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        { 
            if(stepCounter >= 3)
            {
                Eat(triad, characters.Count);
                Debug.Log(characters[0].name + " : " + characters[0].GetCurrentHealth());
                stepCounter = 0;
            }
        }
    }

    void AddCharacter(Characters _character)
    {
        characters.Add(_character);
    }

    void Eat(int numofportions,int numofcharacters)
    {

        if(numofportions >= numofcharacters)
        {

            int eatenPortions = Mathf.Abs(numofportions - numofcharacters);

            triad -= eatenPortions;

        }
        else
        {
            int diff = Mathf.Abs(numofportions - numofcharacters);

            //HIT a cada uno por la diferencia
            foreach(Characters character in characters)
            {
                if(!character.alive)
                {
                    continue;
                }
                Hit(character, diff);

            }
        }

    }

    void Hit(Characters character ,int damage)
    {
        damage += Random.Range(0, 3);
        int tempcurrenthealth = character.GetCurrentHealth() - damage;
        character.SetCurrentHealth(tempcurrenthealth);
        if(character.GetCurrentHealth()<= 0)
        {
            Die(character);
        }
    }

    void Die(Characters _character)
    {
        _character.SetCurrentHealth(0);
        _character.alive = false;
    }

    void CraftRations()
    {
        snowFlower--;
        waterBubble--;
        bulbClaw--;
        triad++;
    }

}
