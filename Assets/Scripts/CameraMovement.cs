using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public bool onDownValise;
    public bool onTopValise;
    public bool offValise = true;

    public Vector3 positionValiseDown;
    public Vector3 positionValiseTop;
    public Vector3 positionInitial;

    public Vector3 rotationValiseDown;
    public Vector3 rotationValiseTop;
    public Vector3 rotationInitial;

    void Start()
    {
        
    }

    public void MovementToValiseDown(float duration)
    {
        onTopValise = false;
        offValise = false;

        StartCoroutine(ToValiseDown(duration));
    }

    public void MovementToValiseTop(float duration)
    {
        onDownValise = false;
        offValise = false;

        StartCoroutine(ToValiseTop(duration));
    }


    public void MovementToInitial(float duration)
    {
        onTopValise = false;
        onDownValise = false;

        StartCoroutine(ToInitial(duration));
    }

    IEnumerator ToValiseDown(float duration)
    {
        float timeStamp = 0f;
        Vector3 positionActuelle = transform.position;
        Quaternion rotationActuelle = transform.rotation;

        while (timeStamp < duration)
        {
            
            transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(rotationValiseDown),
            timeStamp / (duration)
            );

            transform.position = Vector3.Lerp(
            positionActuelle,
            positionValiseDown,
            timeStamp  / (duration)
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(rotationValiseDown);
        transform.position = positionValiseDown;

        onDownValise = true;

    }

    IEnumerator ToValiseTop(float duration)
    {
        float timeStamp = 0f;
        Vector3 positionActuelle = transform.position;
        Quaternion rotationActuelle = transform.rotation;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(rotationValiseTop),
            timeStamp / (duration)
            );

            transform.position = Vector3.Lerp(
            positionActuelle,
            positionValiseTop,
            timeStamp / (duration)
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(rotationValiseTop);
        transform.position = positionValiseTop;

        onTopValise = true;

    }

    IEnumerator ToInitial(float duration)
    {
        float timeStamp = 0f;
        Vector3 positionActuelle = transform.position;
        Quaternion rotationActuelle = transform.rotation;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(rotationInitial),
            timeStamp / (duration)
            );

            transform.position = Vector3.Lerp(
            positionActuelle,
            positionInitial,
            timeStamp / (duration)
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(rotationInitial);
        transform.position = positionInitial;

        offValise = true;

    }

}
