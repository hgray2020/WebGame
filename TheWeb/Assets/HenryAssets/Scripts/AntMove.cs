using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntMove : NetworkBehaviour
{
    public float moveSpeed = 5;
    private float speed;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector3 relPos;
    private Vector2 moveX, moveY;
    private Vector2 rotatedMove;
    private bool offNode = true;
    private bool offEdge = true;
    float mouseAng;

    void Start(){
        var anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = moveSpeed;
    }

    void Update()
    {
        if (!IsAnt()) {
            return;
        }
        GameObject eggs = GameObject.FindWithTag("egg");
        if (eggs == null) {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, eggs.transform.position, speed * Time.deltaTime);
        Debug.Log(offEdge +", " + offNode);
        if (offEdge && offNode) {
            speed = moveSpeed;
        }
    }
    
    

       
    
    

    bool IsAnt() {
        return !IsHost;
    }

    // [ServerRpc(RequireOwnership = false)]
    // public void MoveServerRpc(Vector2 move) {
    //     Debug.Log(move);
    //     rb.velocity = move;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "egg") {
            other.gameObject.BroadcastMessage("eggsGetHit", 5);
            Destroy(gameObject);
        }
        if (other.tag == "web_edge" || other.tag == "web_node") {
            speed = moveSpeed * 0.5f;
            if (other.tag == "web_edge") {
                offEdge = false;
            }
            if (other.tag == "web_node") {
                offNode = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "web_edge") {
            offNode = true;
        }
        if (other.tag == "web_node") {
            offNode = true;
        }
    }

}