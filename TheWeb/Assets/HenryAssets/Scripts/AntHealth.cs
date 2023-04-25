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

    public void takeDamage(int amount)
    {
        currHealth -= amount;
        StartCoroutine("Hurt");
        Debug.Log("health decremented");
        if (currHealth <= 0) {
            Destroy(gameObject);
            this.GetComponent<NetworkObject>().Despawn();
        }
    }

    IEnumerator Hurt() {
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(2).gameObject.SetActive(false);
    }


}

