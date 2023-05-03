using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebEdge : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject node1;
    private GameObject node2;
    private bool nodeSet;

    public bool buildable = false;
    
    void Start()
    {
        // updateTransform();
        // Debug.Log("wtf!!");
    }

    // Update is called once per frame
    
    public void SetNodes(GameObject n1, GameObject n2) {
        node1 = n1;
        node2 = n2;
        nodeSet = true;
    }

    public (GameObject, GameObject) nodes(){
        if (!nodeSet) {
            Debug.Log("nodes not set");
        }
        return (node1, node2);
    }
}
