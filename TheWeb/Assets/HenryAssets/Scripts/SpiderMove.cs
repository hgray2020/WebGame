using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpiderMove : NetworkBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField]private bool onNetwork;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector3 relPos;
    private Vector2 moveX, moveY;
    private Vector2 rotatedMove;
    private float mouseAng;
    private float rot;
    private bool onNode = false;
    private bool onEdge = false;
    private GameObject webNode;
    private GameObject webEdge;
    private Vector2 closestPosOnEdge;
    [SerializeField] private float rotSpeed = 100;

    private Animator animator;

    void Start(){
        animator = gameObject.GetComponentInChildren<Animator>();
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
        if (move != 0) {
            animator.SetBool("Walk", true);
        } else {
            animator.SetBool("Walk", false);
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
        if (!onNetwork) {
            return true;
        }
        return IsOwner && IsHost;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "web_edge") {
            Debug.Log("edge");
            webEdge = other.gameObject;
            onEdge = true;
            closestPosOnEdge = other.ClosestPoint(new Vector2(transform.GetChild(2).position.x, transform.GetChild(2).position.y));
            
        }
        if (other.gameObject.tag == "web_node") {
            onNode = true;
            webNode = other.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "web_node") {
            onNode = false;
        } 
        if (other.gameObject.tag == "web_edge") {
            onEdge = false;
        }
    }

    public bool isOnWebNode() {
        return onNode;
    }
    
    public bool isOnWebEdge() {
        return onEdge;
    }

    public Vector2 closestPos() {
        return closestPosOnEdge;
    }

    public GameObject currWebNode() {
        return webNode;
    }
    
    public GameObject currWebEdge() {
        return webEdge;
    }
}