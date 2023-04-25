using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntHealth : NetworkBehaviour
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
        Debug.Log("health decremented");
        if (currHealth <= 0) {

            gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }


}

