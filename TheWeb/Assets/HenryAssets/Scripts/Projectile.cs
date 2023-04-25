using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsHost);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x < -150 || screenPos.x > Screen.width + 150 || screenPos.y < -150 || screenPos.y > Screen.height + 150) {
            Destroy(this.gameObject);
            gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }

     private void OnColliderEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "projectile") {
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(this.gameObject);
            gameObject.GetComponent<NetworkObject>().Despawn();
        }

        if (collision.gameObject.tag == "ant") {
           collision.gameObject.BroadcastMessage("TakeDamage", 1);
        }
    }
}
