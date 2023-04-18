using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public void ShakeCamera(float durationTime2, float magnitude2){
        StartCoroutine(ShakeMe(durationTime2, magnitude2));
    }

    public IEnumerator ShakeMe(float durationTime, float magnitude){
        Vector3 origPos = transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < durationTime){
            float sX = Random.Range(-1f, 1f) * magnitude;
            float sY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3((origPos.x+sX), (origPos.y+sY), origPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = origPos;
    }
}
