using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_T1 : MonoBehaviour
{

    public GameObject second_target;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        second_target.gameObject.SetActive(true);
    }
}
