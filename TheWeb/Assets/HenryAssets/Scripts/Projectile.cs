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
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(screenPos);
        if (screenPos.x < -50 || screenPos.x > Screen.width + 50 || screenPos.y < -50 || screenPos.y > Screen.height + 50) {
            Destroy(this.gameObject);
        }
    }

     private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "projectile") {
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(GetComponent<Rigidbody>());
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (collision.tag == "Ant_Player") {
            var healthComponent = collision.GetComponent<AntHealth>();
            if(healthComponent != null) {
                healthComponent.TakeDamage(1);
                print("took 1 ant health");
            }
        }
    }
}
