using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public int reloadCD = 300;
    private int reload = 0;
    public KeyCode shootKey = KeyCode.Space;
    public GameObject projectile;
    public float projVel = 10f;
    public Transform spawnPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(shootKey) && reload == 0) {
            reload = reloadCD;
            ShootBullet();
        }
        if (reload > 0) {
            reload--;
        }
    }

    void ShootBullet() {
        GameObject bullet = (GameObject)Instantiate(projectile, spawnPos.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projVel);
        Debug.Log(transform.up);
    }
}