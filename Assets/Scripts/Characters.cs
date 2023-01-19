using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters 
{

    private int currentHealth, maxHealth;

    private int attack, defense, velocity;

    public bool alive = true;

    public string name;

    public Characters(string _name,int _health,int _attack,int _defense,int _velocity)
    {

        name = _name;
        maxHealth = _health;
        currentHealth = _health;
        attack = _attack;
        defense = _defense;
        velocity = _velocity;

    }

    //GETTERS
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetAttack()
    {
        return attack;
    }
    public int GetDefense()
    {
        return defense;
    }
    public int GetVelocity()
    {
        return velocity;
    }

    //SETTERS
    public void SetCurrentHealth(int _currentHealth)
    {
        currentHealth = _currentHealth;
    }
    public void SetMaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
    }
    public void SetAttack(int _attack)
    {
        attack = _attack;
    }
    public void SetDefense(int _defense)
    {
        defense = _defense;
    }
    public void SetVelocity(int _velocity)
    {
        velocity = _velocity;
    }


}
