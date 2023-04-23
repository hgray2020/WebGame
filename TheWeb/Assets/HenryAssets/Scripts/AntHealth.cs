using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currHealth;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currHealth -= amount;
        print("health decrmented");
        if (currHealth <= 0) {
            print("health fully depleted");
            Destroy(gameObject);
        }
    }


}

