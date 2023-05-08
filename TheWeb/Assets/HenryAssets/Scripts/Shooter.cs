using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Shooter : NetworkBehaviour
{
    // Start is called before the first frame update
    public int reloadCD = 300;
    private int reload = 0;
    public GameObject projectile;
    public float projVel = 10f;
    public Transform spawnPos;
    public bool tutorial = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsHost) {
            return;
        }
        if (Input.GetButton("Shoot") && reload == 0) {
            reload = reloadCD;
            Debug.Log("SHOOTING");
            ShootBullet();
        }
        if (reload > 0) {
            reload--;
        }
        if (Input.GetButton("Shoot")) {
            Debug.Log("Trying to Shoot");
        }
    }

    void ShootBullet() {
        GameObject bullet = (GameObject)Instantiate(projectile, spawnPos.position, transform.rotation);
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projVel);
        // Debug.Log(transform.up);
    }

    void ShootBullet_Tutorial() {
        GameObject bullet = (GameObject)Instantiate(projectile, spawnPos.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projVel);
        // Debug.Log(transform.up);
    }
}
