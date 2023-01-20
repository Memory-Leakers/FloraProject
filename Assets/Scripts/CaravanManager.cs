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

    //Hours
    private float gametime = 0.0f;
    private float gametimeinminutes = 0.0f;
    private float gametimeinhours = 0.0f;
    private float gametimeScale = 0.0f;

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

        triad = 10;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameTime();
        Debug.Log("Time in minutes : " + gametimeinminutes);
        Debug.Log("Time in hours : " +  gametimeinhours);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayGameTime();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StopGameTime();
        }


        if(gametimeinhours >= 24.0f)
        {
            Eat(triad, characters.Count);
            Debug.Log(characters[0].name + " : " + characters[0].GetCurrentHealth());
            stepCounter = 0;

            gametime = 0.0f;

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

            
            int numofrationseaten = numofcharacters - Random.Range(0, 1);

            Debug.Log("Eaten " + numofrationseaten + "Portions !!!!!!!!!!!!!!!!!!!!!!!!");

            triad -= numofrationseaten;

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

    void PlayGameTime()
    {
        gametimeScale = 3600.0f;
    }

    void UpdateGameTime()
    {
        gametime += Time.deltaTime * gametimeScale;
        gametimeinminutes  = gametime / 60;
        gametimeinhours = gametimeinminutes / 60;
    }

    void StopGameTime()
    {
        gametimeScale = 0.0f;
    }

}
