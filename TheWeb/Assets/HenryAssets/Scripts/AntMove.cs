using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntMove : NetworkBehaviour
{
    public float moveSpeed = 5;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector3 relPos;
    private Vector2 moveX, moveY;
    private Vector2 rotatedMove;
    float mouseAng;

    void Start(){
        var anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsAnt()) {
            return;
        }
        ProcessInputs();
    }
    
    void FixedUpdate()
    {
        if (!IsAnt()) {
            return;
        }
        Move();
    }

       
    
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector2(moveX, moveY);
        if (moveDirection.magnitude > 1 ) {
            moveDirection = moveDirection.normalized;
        }
    }
    
    void Move()
    {
        // MoveServerRpc(moveDirection * moveSpeed);
        rb.velocity = moveDirection * moveSpeed;
    }

    bool IsAnt() {
        return !IsHost;
    }

    // [ServerRpc(RequireOwnership = false)]
    // public void MoveServerRpc(Vector2 move) {
    //     Debug.Log(move);
    //     rb.velocity = move;
    // }
}