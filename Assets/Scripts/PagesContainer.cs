using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PagesContainer : MonoBehaviour
{

    // Detection swipe
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    //Rotation Pages
    public Transform pivotPoint;

    public int level;

    // Gestion pages
    public GameObject[] pagesList;
    public GameObject[] pagesList2;
    public GameObject[] pagesList3;

    private GameObject[] actualList;

    private GameObject[] actualPages;
    private GameObject[] nextPages;
    private int[] numPages;

    private Vector3 frontPageOffset = new Vector3(0, -0.008f, -0.025f);

    private bool focusRight;
    public float timeToAction;
    public float offsetFocus;
    private bool canSwipe = true;

    private TouchManager touchManager;

    public AudioClip soundPage1;
    public AudioClip soundPage2;
    public AudioClip soundPage3;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        level = FindAnyObjectByType<LevelInventaireManager>().level;
        audioSource.volume = 0.5f;

        touchManager = FindAnyObjectByType<TouchManager>();
        transform.position = new Vector3(transform.position.x, -4.01f, transform.position.z + 0.1f);

        focusRight = true;

        actualPages = new GameObject[2];
        nextPages = new GameObject[2];
        numPages = new int[2];
        numPages[1] = 1;


        LoadPages(level);
        /*
        if (pagesList[1])
        {
            nextPages[1] = Instantiate(pagesList[1]);
            nextPages[1].transform.SetParent(transform, false);
            nextPages[1].transform.position = new Vector3(transform.position.x + 1f + frontPageOffset.x, transform.position.y + frontPageOffset.y, transform.position.z + frontPageOffset.z);
            nextPages[1].transform.rotation = transform.rotation;
            nextPages[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        actualPages[1] = Instantiate(pagesList[0]);
        actualPages[1].transform.SetParent(transform, false);
        actualPages[1].transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        actualPages[1].transform.rotation = transform.rotation;
        actualPages[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
        */

    }

    // Update is called once per frame
    void Update()
    {

        // Swipe
        if (canSwipe)
        {
            if (touchManager.isSwipeLeft)
            {
                canSwipe = false;
                if (focusRight)
                {
                    TurnRightPage();
                    StartCoroutine(focusOnOtherPage(new Vector3(transform.parent.position.x + offsetFocus, transform.parent.position.y, transform.parent.position.z), timeToAction));
                    focusRight = false;
                }
                else
                {
                    StartCoroutine(focusOnOtherPage(new Vector3(transform.parent.position.x - offsetFocus, transform.parent.position.y, transform.parent.position.z), timeToAction));
                    focusRight = true;
                }
            }

            if (touchManager.isSwipeRight)
            {
                canSwipe = false;
                if (focusRight)
                {
                    StartCoroutine(focusOnOtherPage(new Vector3(transform.parent.position.x + offsetFocus, transform.parent.position.y, transform.parent.position.z), timeToAction));
                    focusRight = false;
                }
                else
                {
                    TurnLeftPage();
                    StartCoroutine(focusOnOtherPage(new Vector3(transform.parent.position.x - offsetFocus, transform.parent.position.y, transform.parent.position.z), timeToAction));
                    focusRight = true;
                }
            }
        }
    }

    public void NextLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        level = FindAnyObjectByType<LevelInventaireManager>().level;
        LoadPages(level);
    }

    public void LoadPages(int numInv)
    {
       
        Array.Clear(nextPages,0,nextPages.Length);
        Array.Clear(actualPages, 0, nextPages.Length);
        Array.Clear(numPages, 0, nextPages.Length);

        switch (numInv)
        {
            case 0:
                actualList = pagesList;
                break;

            case 1:
                actualList = pagesList2;
                break;

            case 2:
                actualList = pagesList3;
                break;
        }
        if (actualList[1])
        {
            nextPages[1] = Instantiate(actualList[1]);
            nextPages[1].transform.SetParent(transform, false);
            nextPages[1].transform.position = new Vector3(transform.position.x + 1f + frontPageOffset.x, transform.position.y + frontPageOffset.y, transform.position.z + frontPageOffset.z);
            nextPages[1].transform.rotation = transform.rotation;
            nextPages[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        actualPages[1] = Instantiate(actualList[0]);
        actualPages[1].transform.SetParent(transform, false);
        actualPages[1].transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        actualPages[1].transform.rotation = transform.rotation;
        actualPages[1].transform.localRotation = Quaternion.Euler(0, 180, 0);

        numPages[1] = 1;
    }
    public void TurnRightPage()
    {

        if (actualPages[1] != null)
        {

            if (nextPages[0] != null)
            {
                Destroy(nextPages[0]);
            }

            //La page de gauche devient la prochaine page de gauche (s'il y a une page de gauche)
            if (actualPages[0] != null)
            {
                nextPages[0] = actualPages[0];
                nextPages[0].transform.position = new Vector3(pivotPoint.position.x - 1f + frontPageOffset.x, pivotPoint.position.y + frontPageOffset.y - 0.993f, pivotPoint.position.z + frontPageOffset.z - 0.025f);
            }

            PlayRandomSound();

            //On tourne la page
            StartCoroutine(RightPageAnimation(timeToAction / 2, actualPages[1]));
            //actualPages[1].transform.RotateAround(transform.position, transform.up, 180);

            //Cette page devient la page de gauche
            actualPages[0] = actualPages[1];
            numPages[0] = numPages[1];

            //La prochain page de droite devient la page de droite (s'il y a une prochaine page de droite)
            if (nextPages[1])
            {

                actualPages[1] = nextPages[1];
                actualPages[1].transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                numPages[1] = numPages[1] + 1;

                if (numPages[1] < actualList.Length)
                {
                    nextPages[1] = Instantiate(actualList[numPages[1]]);
                    nextPages[1].transform.SetParent(transform, false);
                    nextPages[1].transform.position = new Vector3(transform.position.x + 1f + frontPageOffset.x, transform.position.y + frontPageOffset.y, transform.position.z + frontPageOffset.z);
                    nextPages[1].transform.rotation = transform.rotation;
                    nextPages[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    nextPages[1] = null;
                }
            }
            else
            {
                actualPages[1] = null;
            }
        }


    }

    public void TurnLeftPage()
    {

        if (actualPages[0] != null)
        {

            if (nextPages[1] != null)
            {
                Destroy(nextPages[1]);
            }

            if (actualPages[1] != null)
            {
                nextPages[1] = actualPages[1];
                nextPages[1].transform.position = new Vector3(transform.position.x + 1f + frontPageOffset.x, transform.position.y + frontPageOffset.y, transform.position.z + frontPageOffset.z);
            }

            PlayRandomSound();

            //On tourne la page
            StartCoroutine(LeftPageAnimation(timeToAction / 2, actualPages[0]));
            //actualPages[0].transform.RotateAround(transform.position, transform.up, -180);

            //Cette page devient la page de droite
            actualPages[1] = actualPages[0];
            numPages[1] = numPages[0];

            //La prochain page de droite devient la page de droite (s'il y a une prochaine page de droite)
            if (nextPages[0])
            {

                actualPages[0] = nextPages[0];
                actualPages[0].transform.position = new Vector3(pivotPoint.position.x - 1f, pivotPoint.position.y - 0.993f, pivotPoint.position.z - 0.025f);
                numPages[0] = numPages[0] - 1;

                if (numPages[0] > 1)
                {
                    nextPages[0] = Instantiate(actualList[numPages[0] - 2]);
                    nextPages[0].transform.SetParent(transform, false);
                    nextPages[0].transform.position = new Vector3(pivotPoint.position.x - 1f + frontPageOffset.x, pivotPoint.position.y + frontPageOffset.y - 0.993f, pivotPoint.position.z + frontPageOffset.z - 0.025f);
                    nextPages[0].transform.rotation = transform.rotation;
                }
                else
                {
                    nextPages[0] = null;
                }


            }
            else
            {
                actualPages[0] = null;
            }

        }
    }


    public void PlayRandomSound()
    {
        AudioClip[] soundList = {soundPage1, soundPage2, soundPage3};

        audioSource.clip = soundList[UnityEngine.Random.Range(0, 2)];
        audioSource.Play();
    }

    IEnumerator focusOnOtherPage(Vector3 destination, float duration)
    {

        float timeStamp = 0f;

        while (timeStamp < duration)
        {

            transform.parent.position = Vector3.Lerp(
            transform.parent.position,
            destination,
            timeStamp / (duration * 2)
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.parent.position = destination;
        canSwipe = true;

    }

    IEnumerator RightPageAnimation(float duration, GameObject page)
    {

        float timeStamp = 0f;

        while (timeStamp < duration)
        {

            page.transform.RotateAround(pivotPoint.position, transform.up, 180 / (duration / Time.deltaTime));

            yield return null;

            timeStamp += Time.deltaTime;
        }

        page.transform.position = new Vector3(pivotPoint.position.x - 1f, pivotPoint.position.y - 0.993f, pivotPoint.position.z - 0.025f);
        page.transform.rotation = transform.rotation;

    }

    IEnumerator LeftPageAnimation(float duration, GameObject page)
    {

        float timeStamp = 0f;

        while (timeStamp < duration)
        {

            page.transform.RotateAround(pivotPoint.position, transform.up, -180 / (duration / Time.deltaTime));

            // Attendez la frame suivante
            yield return null;

            timeStamp += Time.deltaTime;
        }

        //transform.rotation = endRotation;
        page.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        page.transform.rotation = transform.rotation;
        page.transform.localRotation = Quaternion.Euler(0, 180, 0);

    }

}
