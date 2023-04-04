using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpiderMove : NetworkBehaviour
{
    [SerializeField]private float moveSpeed;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector3 relPos;
    private Vector2 moveX, moveY;
    private Vector2 rotatedMove;
    private float mouseAng;
    private float rot;
    [SerializeField] private float rotSpeed = 100;

    void Start(){
        var anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsSpider()) {
            return;
        }
        ProcessInputs();
    }
    
    void FixedUpdate()
    {
        if (!IsSpider()) {
            return;
        }
        Move();
        Rotate();
    }

    
    
    void ProcessInputs()
    {
        rot = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(0, move, 0);
        
        if (moveDirection.magnitude > 1) {
            moveDirection = moveDirection.normalized;
        }
    }
    
    void Move()
    {
        rotatedMove = transform.TransformDirection(moveDirection);
        rb.velocity = rotatedMove * moveSpeed;
        rb.angularVelocity = -1 * rotSpeed * rot;
    }

    void Rotate() 
    {
        // mouseScreenPos = Input.mousePosition;
        // mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        // relPos = mouseWorldPos - transform.position;
        // mouseAng = Mathf.Atan2(relPos.y, relPos.x) * (180 / Mathf.PI) - 90;
        // transform.rotation = Quaternion.Euler(0, 0, mouseAng);

    }

    bool IsSpider() {
        return IsOwner && IsHost;
    }
}