using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebEdge : MonoBehaviour
{
    // Start is called before the first frame update
    private float length = 0;
    private Vector2 midpoint;
    private float angle;
    private float z;
    private bool nodeSet;
    void Start()
    {
        // updateTransform();
        // Debug.Log("wtf!!");
    }

    // Update is called once per frame
    
    public void SetNodes(GameObject node1, GameObject node2) {
        Debug.Log(node1.transform.position);
        Debug.Log(node2.transform.position);
        Vector2 n1 = new Vector2(node1.transform.position.x, node1.transform.position.y);
        Vector2 n2 = new Vector2(node2.transform.position.x, node2.transform.position.y);
        Debug.Log(n1);
        Debug.Log(n2);
        z = node1.transform.position.z;
        midpoint = (n1 + n2) / 2;
        length = Vector2.Distance(n1, n2);
       
        angle = Mathf.Atan2(n1.y - n2.y, n1.x - n2.x) * (180 / Mathf.PI) ;
        // Debug.Log(length);
        // Debug.Log(angle);
        Debug.Log(midpoint);
        transform.position = new Vector3(midpoint.x, midpoint.y, z);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(length, 0.1f, 1);
    }

    
}
