using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int maxHP = 100;
    [SerializeField] bool character;
    private int currentHealth { get; set; }

    private void Awake()
    {
        currentHealth = maxHP;
    }
    
    public void takeDmg(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");

        if(currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }

    public int getDamage() { return this.damage; }
    public int getCurrentHealth() { return this.currentHealth; }
    public int getMaxHP() { return this.maxHP; }
    public bool getIsCharacter() { return this.character; }
}
