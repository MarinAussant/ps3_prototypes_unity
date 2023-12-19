using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    public bool onDownValise = false;
    public bool onTopValise = false;
    public bool offValise = true;

    public Vector3 positionValiseDown;
    public Vector3 positionValiseTop;
    public Vector3 positionInitial;

    public Vector3 rotationValiseDown;
    public Vector3 rotationValiseTop;
    public Vector3 rotationInitial;

    public LevelInventaireManager levelInventaireManager;

    public Button enterValiseButton;
    public Button getCarnetButton;

    //private Button activeButton;

    public AudioClip soundWallet;
    public AudioClip soundToMap;
    public AudioClip soundToCarnet;

    public AudioSource audioSource;

    private void Start()
    {
        offValise = true;
        getCarnetButton.gameObject.SetActive(false);

        audioSource.volume = 0f;
    }

    public void MovementToValiseDown(float duration)
    {
        //enterValiseButton.interactable = false;
        enterValiseButton.gameObject.SetActive(false);

        onTopValise = false;
        offValise = false;

        audioSource.volume = 0.2f;
        audioSource.clip = soundToCarnet;
        audioSource.Play();

        levelInventaireManager.activateUI(false);

        StartCoroutine(ToValiseDown(duration));
    }

    public void MovementToValiseTop(float duration)
    {
        //getCarnetButton.interactable = false;
        getCarnetButton.gameObject.SetActive(false);

        onDownValise = false;
        offValise = false;

        audioSource.volume = 0.2f;
        audioSource.clip = soundToMap;
        audioSource.Play();

        StartCoroutine(ToValiseTop(duration));
    }


    public void MovementToInitial(float duration)
    {
        //getCarnetButton.interactable = false;
        getCarnetButton.gameObject.SetActive(false);

        onTopValise = false;
        onDownValise = false;

        /*audioSource.volume = 0.1f;
        audioSource.clip = soundWallet;
        audioSource.Play();*/

        levelInventaireManager.activateUI(false);

        StartCoroutine(ToInitial(duration));
    }

    public void TakeCarnet()
    {
        getCarnetButton.gameObject.SetActive(false);
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

        //getCarnetButton.interactable = true;
        enterValiseButton.gameObject.SetActive(false);
        getCarnetButton.gameObject.SetActive(true);

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

        levelInventaireManager.activateUI(true);

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

        //enterValiseButton.interactable = true;
        getCarnetButton.gameObject.SetActive(false);
        enterValiseButton.gameObject.SetActive(true);

        offValise = true;

    }
}
