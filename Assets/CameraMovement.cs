using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Vector3 positionValise;
    private Vector3 positionInitial;

    public Vector3 rotationValise;
    private Vector3 rotationInitial;

    void Start()
    {
        positionInitial = transform.position;
        rotationInitial = transform.rotation.eulerAngles;
    }

    public void MovementToValise(float duration)
    {
        StartCoroutine(InitialToValise(duration));
    }

    IEnumerator InitialToValise(float duration)
    {
        float timeStamp = 0f;

        while (timeStamp < duration)
        {
            
            transform.rotation = Quaternion.Lerp(
            Quaternion.Euler(rotationInitial),
            Quaternion.Euler(rotationValise),
            timeStamp / (duration)
            );

            transform.position = Vector3.Lerp(
            positionInitial,
            positionValise,
            timeStamp  / (duration)
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(rotationValise);
        transform.position = positionValise;

    }

}
