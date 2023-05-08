using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Audio;

public class Shooter : NetworkBehaviour
{
    // Start is called before the first frame update
    public int reloadCD = 300;
    private int reload = 0;
    public GameObject projectile;
    public float projVel = 10f;
    public Transform spawnPos;
    public AudioSource shootSFX;

    private Animator animator; 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsHost) {
            return;
        }
        if (Input.GetButton("Shoot") && reload == 0) {
            reload = reloadCD;
            ShootBullet();
        }
        if (reload > 0) {
            reload--;
        }
    }

    void ShootBullet() {
        if (shootSFX.isPlaying == false){
            shootSFX.Play();
        }
        GameObject bullet = (GameObject)Instantiate(projectile, spawnPos.position, transform.rotation);
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projVel);
        StartCoroutine("Shoot");
        // Debug.Log(transform.up);
    }

    IEnumerator Shoot() {
        animator.SetBool("Shoot", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Shoot", false);
    } 
}
