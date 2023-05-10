using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntMove : NetworkBehaviour
{
    
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
    public bool isFlying = false;
    public bool isFire = false;
    public bool isSoldier = false;
    public bool isNormal = false;
    private int type;
    public int[] damages = {2, 6, 8, 4};
    [SerializeField]private float[] moveSpeeds = {2, 0.8f, 0.4f, 1};
    private int damage;
    float mouseAng;
    private float baseScale;
    
    public Animator animator;

    SpriteRenderer web;

    void Start(){
        if (isFlying) {
            type = 0;
        } else if (isFire) {
            type = 1;
        } else if (isSoldier) {
            type = 2;
        } else if (isNormal) {
            type = 3;
        }
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Walk", true);
        rb = GetComponent<Rigidbody2D>();
        speed = moveSpeeds[type];
        damage = damages[type];
        baseScale = transform.localScale.x;
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
        if (offEdge && offNode) {
            speed = moveSpeeds[type];
        }
        float mag = transform.position.x -  eggs.transform.position.x;
        float xdif = 1;
        if (mag != 0) {
            xdif = mag / Mathf.Abs(mag);
        }

        transform.localScale = new Vector3(baseScale * xdif, transform.localScale.y, transform.localScale.z);
    }

    bool IsAnt() {
        return !IsHost;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnServerRpc() {
        Debug.Log("despawn rpc");
        gameObject.GetComponent<NetworkObject>().Despawn();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "egg") {
            other.gameObject.BroadcastMessage("eggsGetHit", damage);
            DespawnServerRpc();
        }
            if (other.tag == "web_edge" || other.tag == "web_node") {
                if (other.tag == "web_edge") {
                    offEdge = false;
                }
                if (other.tag == "web_node") {
                    offNode = false;
                }
            }
            if (other.tag == "spike" && !isFlying) {
                this.gameObject.BroadcastMessage("takeDamage", 4);
            } else if (other.tag == "slime" && !isFlying) {
                this.gameObject.BroadcastMessage("takeDamage", 2);
                StartCoroutine("Stuck");
            } else if (other.tag == "web_edge") {
                StartCoroutine("Slowed");
            }
    }

    IEnumerator Stuck() {
        speed = 0f;
        transform.GetChild(3).gameObject.SetActive(true);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(2f);
        speed = moveSpeeds[type];
        transform.GetChild(3).gameObject.SetActive(false);
        animator.SetBool("Walk", true);
    }

    IEnumerator Slowed() {
        speed = moveSpeeds[type] * 0.5f;
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        speed = moveSpeeds[type];
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "web_edge") {
            offNode = true;
        }
        if (other.tag == "web_node") {
            offNode = true;
        }

        if (isFire && (other.tag == "slime" || other.tag == "spike")) {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}