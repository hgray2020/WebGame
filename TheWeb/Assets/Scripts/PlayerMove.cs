using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMove : NetworkBehaviour
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
    private float rot;

    void Start(){
        var anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!IsOwner) {
            return;
        }
        
        if (IsHost) {
            SpiderUpdate();
        } else {
            AntUpdate();
        }
    }
    
    void FixedUpdate()
    {
        if (!IsOwner) {
            return;
        }

        if (IsHost) {
            SpiderFixedUpdate();
        } else {
            AntFixedUpdate();
        }
    }

    void AntFixedUpdate() {
        Move();
    }

    void SpiderFixedUpdate() {
        Move();
        Rotate();
    }

    void AntUpdate() {
        AntProcessInputs();   
    }

    void SpiderUpdate() {
        SpiderProcessInputs();   
    }
    
    void SpiderProcessInputs()
    {
        rot = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(0, move, 0);
        
        if (moveDirection.magnitude > 1) {
            moveDirection = moveDirection.normalized;
        }
    }

    void AntProcessInputs()
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
        rotatedMove = transform.TransformDirection(moveDirection);
        rb.velocity = rotatedMove * moveSpeed;
        
    }

    void Rotate() 
    {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        relPos = mouseWorldPos - transform.position;
        mouseAng = Mathf.Atan2(relPos.y, relPos.x) * (180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, mouseAng);

    }
}