using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntMove : NetworkBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector3 relPos;
    private Vector2 moveX, moveY;
    private Vector2 rotatedMove;
    float mouseAng;

    void Start(){
        var anim = GetComponentInChildren<Animator>();
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

    void AntFixedUpdate() {

    }

    void SpiderFixedUpdate() {
        Move();
        Rotate();
    }

    void AntUpdate() {
        ProcessInputs();   
    }

    void SpiderUpdate() {
        ProcessInputs();   
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
        
        
        rb.velocity = moveDirection * moveSpeed;
        
    }

    void Rotate() 
    {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        relPos = mouseWorldPos - transform.position;
        mouseAng = Mathf.Atan2(relPos.y, relPos.x) * (180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, mouseAng);

    }

    bool IsAnt() {
        return IsOwner && !IsHost;
    }
}