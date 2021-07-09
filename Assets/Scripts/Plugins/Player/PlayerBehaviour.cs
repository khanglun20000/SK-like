using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float maxHealth,currentHealth;
    public float maxShield, currentShield;
    public float maxMana, currentMana;
    private float lastDamagedTime = 0;
    private readonly float regenerationDelay = 5f, regenerationRate = 2f;
    public static bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentMana = maxMana;
        InvokeRepeating(nameof(RegenerateShield), 0, regenerationRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentShield >= maxShield)
        {
            currentShield = maxShield;
        }
        if(currentMana>= maxMana)
        {
            currentMana = maxMana;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMana = maxMana;
        }
    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            lastDamagedTime = Time.time;
            if (currentShield > 0)
            {
                currentShield -= damage;
            }
            else
            {
                currentHealth -= damage;
            }
        }
    }
    public void RegenerateShield()
    {
        if(lastDamagedTime >= 0 && Time.time - lastDamagedTime >= regenerationDelay && currentShield<maxShield)
        {
            currentShield += 10;
        }
    }
}
