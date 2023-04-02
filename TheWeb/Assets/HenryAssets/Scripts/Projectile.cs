using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(screenPos);
        if (screenPos.x < -50 || screenPos.x > Screen.width + 50 || screenPos.y < -50 || screenPos.y > Screen.height + 50) {
            Destroy(this.gameObject);
        }
    }
}
