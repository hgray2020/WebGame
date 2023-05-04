using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Graph
{
    public List<Vertex> AdjacencyList { get; set; }
    public Graph()
    {
        AdjacencyList = new List<Vertex>();
    }

    /// <summary>
    /// Adds a new vertex to the graph
    /// </summary>
    /// <param name="newVertex">Name of the new vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool AddVertex(GameObject newVertex)
    {
        // We will keep the implementation simple and focus on the concepts
        // Ignore duplicate vertices.
        if (AdjacencyList.Find(v => v.Name == newVertex) != null) return true;

        // Add vertex to the graph
        AdjacencyList.Add(new Vertex(newVertex));
        return true;
    }

    public bool IsEmpty() {
        return AdjacencyList.Count == 0;
    }

    /// <summary>
    /// Adds a new edge between two given vertices in the graph
    /// </summary>
    /// <param name="v1">Name of the first vertex</param>
    /// <param name="v2">Name of the second vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool AddAnEdge(GameObject v1, GameObject v2)
    {
        // We will keep the implementation simple and focus on the concepts
        // Do not worry about handling invalid indexes or any other error cases.
        // We will assume all vertices are valid and already exist.

        // Add vertex v2 to the edges of vertex v1
        AdjacencyList.Find(v => v.Name == v1).Edges.Add(v2);

        // Add vertex v1 to the edges of vertex v2
        AdjacencyList.Find(v => v.Name == v2).Edges.Add(v1);

        return true;
    }

    /// <summary>
    /// Removes an edge between two given vertices in the graph
    /// </summary>
    /// <param name="v1">Name of the first vertex</param>
    /// <param name="v2">Name of the second vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool RemoveAnEdge(GameObject v1, GameObject v2)
    {
        // We will keep the implementation simple and focus on the concepts
        // Do not worry about handling invalid indexes or any other error cases.
        // We will assume all vertices are valid and already exist.

        // Remove vertex v2 to the edges of vertex v1
        AdjacencyList.Find(v => v.Name == v1).Edges.Remove(v2);

        // Remove vertex v1 to the edges of vertex v2
        AdjacencyList.Find(v => v.Name == v2).Edges.Remove(v1);

        return true;
    }

    #region " DFS Traversal "

    /// <summary>
    /// Recursively traverse the graph and return an array of vertex names
    /// </summary>
    /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
    /// <returns>Returns array of GameObjects</returns>
    public List<GameObject> DFSRecursive(GameObject startVertex)
    {
        Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
        if (start == null) return null;

        // List to keep track of the result
        List<GameObject> result = new List<GameObject>();

        // Lookup for keep track of visited nodes
        HashSet<GameObject> visited = new HashSet<GameObject>();

        DFSR(start, result, visited);
        return result;
    }

    private void DFSR(Vertex startVertex, List<GameObject> result, HashSet<GameObject> visited)
    {
        if (startVertex == null || visited.Contains(startVertex.Name)) return;

        //Add the vertex to the visited list
        result.Add(startVertex.Name);

        // Mark the vertex as visited
        visited.Add(startVertex.Name);

        // Traverse through the neighbors of the vertex
        foreach (var neighbor in startVertex.Edges)
        {
            // If the neighbor vertex is not visited already, perform DFS on the neighbor vertex
            if (!visited.Contains(neighbor))
            {
                DFSR(AdjacencyList.Find(v => v.Name == neighbor), result, visited);
            }
        }
    }

    /// <summary>
    /// Iteratively traverse the graph and return an array of vertex names
    /// </summary>
    /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
    /// <returns>Returns array of GameObjects</returns>
    public List<GameObject> DFSIterative(GameObject startVertex)
    {
        Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
        if (start == null) return null;

        List<GameObject> result = new List<GameObject>();
        HashSet<GameObject> visited = new HashSet<GameObject>();
        Stack<Vertex> stack = new Stack<Vertex>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (visited.Contains(current.Name)) continue;
            result.Add(current.Name);
            visited.Add(current.Name);

            foreach (var neighbor in current.Edges)
            {
                if (!visited.Contains(neighbor))
                {
                    stack.Push(AdjacencyList.Find(v => v.Name == neighbor));
                }
            }
        }
        return result;
    }

    #endregion

    /// <summary>
    /// Iteratively traverse the graph and return an array of vertex names
    /// </summary>
    /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
    /// <returns>Returns array of GameObjects</returns>
    public List<GameObject> BFSTraversal(GameObject startVertex)
    {
        Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
        if (start == null) return null;

        List<GameObject> result = new List<GameObject>();
        HashSet<GameObject> visited = new HashSet<GameObject>();
        Queue<Vertex> queue = new Queue<Vertex>();
        queue.Enqueue(start);

        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            // If current vertex is already visited, move to the next vertex in the queue
            if(visited.Contains(current.Name)) continue;

            result.Add(current.Name);
            visited.Add(current.Name);

            foreach (var neighbor in current.Edges)
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(AdjacencyList.Find(v => v.Name == neighbor));
                }
            }
        }

        return result;

    }
}

public class Vertex
{
    /// <summary>
    /// Name of the vertex
    /// </summary>
    public GameObject Name { get; set; }

    /// <summary>
    /// All the edges connected to the given vertex
    /// </summary>
    public List<GameObject> Edges { get; set; }
    public IDictionary<GameObject, float> Costs { get; set; }

    public Vertex(GameObject node)
    {
        this.Name = node;
        this.Edges = new List<GameObject>();
        this.Costs = new Dictionary<GameObject, float>();
    }
}



public class WebSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    private Graph webGraph;
    private int cooldownMax = 30;
    private int cooldown = 0;
    [SerializeField]private GameObject node;
    public GameObject spider;
    [SerializeField]private GameObject webEdge;
    [SerializeField]private GameObject buildEdgeFab;
    private GameObject buildEdge;
    public Transform webSpawnPoint;
    public bool initialized = false;
    private Vector3 closestPoint;
    public SpiderMove sm;
    private bool building = false;
    private GameObject buildFrom;
    private bool buildFromEdge = true;
    private GameInventory spiderInv;
    private bool initInv = false;
    [SerializeField]private GameObject[] buildables;
    void Start()
    {
        webGraph = new Graph();
        buildEdge = (GameObject)Instantiate(buildEdgeFab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) {
            return;
        }
        if (!initInv) {
            GameObject tmp = GameObject.FindWithTag("spider_inv");
            if (tmp == null) {
                return;
            }
            initInv = true;
            spiderInv = tmp.GetComponent<GameInventory>();
            
        }
        
        if (Input.GetButtonDown("WebBuild") && cooldown == 0 && spiderInv.coins > 0) {
            
            cooldown = cooldownMax;
            Debug.Log(spiderInv.GetSelected());
            if (spiderInv.GetSelected() == 0) {
                Debug.Log("starting web");
                if (webGraph.IsEmpty()) {
                    spiderInv.CoinChange(-1);
                    Debug.Log(spiderInv.coins);
                    GameObject newNode = (GameObject)Instantiate(node, spider.transform.position, Quaternion.identity);
                    newNode.GetComponent<NetworkObject>().Spawn(true);
                    webGraph.AddVertex(newNode);
                    newNode.transform.parent = transform;
                    building = true;
                    buildFrom = newNode;
                } else {
                    if (!building) {
                        Debug.Log("building web" + sm.isOnWebEdge() + ", " + sm.isOnWebNode());
                        if (sm.isOnWebNode() || sm.isOnWebEdge()) {
                            
                            building = true;
                            if (sm.isOnWebNode()) {
                                buildFrom = sm.currWebNode();
                            }
                            Debug.Log("building");
                            Debug.Log(buildFrom);
                            if (sm.isOnWebEdge() && !sm.isOnWebNode()) {
                                buildFrom = sm.currWebEdge();
                                buildFromEdge = true;
                                closestPoint = new Vector3(sm.closestPos().x, sm.closestPos().y, transform.position.z);
                                GameObject newNode = (GameObject)Instantiate(node, closestPoint, Quaternion.identity);
                                newNode.GetComponent<NetworkObject>().Spawn(true);
                                newNode.transform.parent = transform;
                                webGraph.AddVertex(newNode);
                                buildFrom = newNode;
                                (GameObject node1, GameObject node2) = sm.currWebEdge().GetComponent<WebEdge>().nodes();
                                webGraph.RemoveAnEdge(node1, node2);
                                webGraph.AddVertex(newNode);
                                webGraph.AddAnEdge(node1, newNode);
                                webGraph.AddAnEdge(node2, newNode);
                            }
                        }
                        
                    } else {
                        
                        Debug.Log("hmm");
                        GameObject newNode = null;
                        if (!sm.isOnWebNode()) {
                            newNode = (GameObject)Instantiate(node, webSpawnPoint.position, Quaternion.identity);
                            newNode.GetComponent<NetworkObject>().Spawn(true);
                            newNode.transform.parent = transform;
                            webGraph.AddVertex(newNode);
                            if (sm.isOnWebEdge()) {
                                closestPoint = new Vector3(sm.closestPos().x, sm.closestPos().y, transform.position.z);
                                newNode.transform.position = closestPoint;
                                (GameObject node1, GameObject node2) = sm.currWebEdge().GetComponent<WebEdge>().nodes();
                                webGraph.RemoveAnEdge(node1, node2);
                                webGraph.AddVertex(newNode);
                                webGraph.AddAnEdge(node1, newNode);
                                webGraph.AddAnEdge(node2, newNode);
                            }
                        } else {
                            newNode = sm.currWebNode();
                        }
                        if (newNode == buildFrom) {
                            Debug.Log("building to same node");
                            return;
                        }
                        
                        Vector2 n1 = new Vector2(newNode.transform.position.x, newNode.transform.position.y);
                        Vector2 n2 = new Vector2(buildFrom.transform.position.x, buildFrom.transform.position.y);
                        float z = newNode.transform.position.z;
                        Vector2 midpoint = (n1 + n2) / 2;
                        float length = Vector2.Distance(n1, n2);
                    
                        float angle = Mathf.Atan2(n1.y - n2.y, n1.x - n2.x) * (180 / Mathf.PI) ;
                        // Debug.Log(length);
                        // Debug.Log(angle);
                        Debug.Log(midpoint);
                        
                        
                        GameObject edge = (GameObject)Instantiate(webEdge, new Vector3(midpoint.x, midpoint.y, z), Quaternion.Euler(0, 0, angle));
                        edge.GetComponent<NetworkObject>().Spawn(true);
                        edge.transform.localScale = new Vector3(length, 0.1f, 1);
                        edge.transform.parent = transform;

                        
                        webGraph.AddAnEdge(newNode, buildFrom);
                        building = false;
                        edge.GetComponent<WebEdge>().SetNodes(newNode, buildFrom);
                        buildFromEdge = false;
                        spiderInv.CoinChange(-1);
                    }
                    
                }
            } else {
                if (sm.isOnWebNode() || sm.isOnWebEdge() && !sm.currWebEdge().GetComponent<WebEdge>().buildable) {
                    GameObject toSpawn = buildables[spiderInv.GetSelected() - 1];
                    bool bought = spiderInv.Purchase();
                    if (bought) {
                        GameObject spawned = (GameObject)Instantiate(toSpawn, sm.currWebEdge().GetComponent<WebEdge>().transform.position, Quaternion.identity);
                        spawned.GetComponent<NetworkObject>().Spawn(true);
                        spawned.transform.parent = sm.currWebEdge().GetComponent<WebEdge>().transform;
                        if ((spiderInv.GetSelected() - 1) == 0) {
                            sm.currWebEdge().GetComponent<SpriteRenderer>().color = Color.green;
                        } else {
                            sm.currWebEdge().GetComponent<SpriteRenderer>().color = Color.black;
                        }
                        sm.currWebEdge().GetComponent<WebEdge>().buildable = true;
                    }
                } 
            }
            
        }
        
    }

    void FixedUpdate() {
        if (!initialized) {
            return;
        }
        if (cooldown > 0) {
            cooldown--;
        }
        if (building) {
            buildEdge.SetActive(true);
            Vector2 n1 = new Vector2(webSpawnPoint.position.x, webSpawnPoint.position.y);
            Vector2 n2 = new Vector2(buildFrom.transform.position.x, buildFrom.transform.position.y);
            float z = buildFrom.transform.position.z;
            Vector2 midpoint = (n1 + n2) / 2;
            float length = Vector2.Distance(n1, n2);
        
            float angle = Mathf.Atan2(n1.y - n2.y, n1.x - n2.x) * (180 / Mathf.PI);
            buildEdge.transform.position = new Vector3(midpoint.x, midpoint.y, z);
            buildEdge.transform.localScale = new Vector3(length, 0.1f, 1);
            buildEdge.transform.rotation = Quaternion.Euler(0, 0, angle);
        } else {
            buildEdge.SetActive(false);
        }
    }

    
}
