using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField]int _maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = _maxHealth;
    }
    public void getDamage(int amount){
        currentHealth -= amount;
        if (currentHealth < 0){
            
        }
    }
    public void getHeal(int amount)
    {
        Mathf.Clamp(currentHealth, -50, _maxHealth);
        currentHealth += amount;
    }

}
