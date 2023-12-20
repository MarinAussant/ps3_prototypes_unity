using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using UnityEngine.UIElements;

public class LevelInventaireManager : MonoBehaviour
{

    [SerializeField] public GameObject invObjectPrefab;

    public ScriptableInv[] inventaireList;
    public List<ScriptableInvDrag> inventaire;
    public List<ScriptableInvDrag> inventairePlace;

    public List<GameObject> listCartes;

    private TouchManager touchManager;

    public int level = 0;

    private GameObject isSelecting;
    private GameObject colorSelecting;
    private bool is3D;

    public GameObject infoUiArea;
    public TextMeshProUGUI infoText;
    public UnityEngine.UI.Image infoImage;
    public TextMeshProUGUI infoTitre;
    public GameObject verifButton;
    public SpriteRenderer spriteSombre;
    //Medaille
    public GameObject bienJoueMedaille;
    public UnityEngine.UI.Image medailleAffiche;
    public SpriteRenderer medaille1;
    public SpriteRenderer medaille2;
    public SpriteRenderer medaille3;

    public GameObject bandeArea;
    public TextMeshProUGUI titreNiveau;

    public GameObject SlideLeftButton;
    public GameObject SlideRightButton;

    private bool canTouch;


    // Start is called before the first frame update
    void Start()
    {
        level = 0;

        is3D = false;
        canTouch = true;

        //SlideLeftButton.SetActive(false);
        //SlideRightButton.SetActive(false);

        inventairePlace = new List<ScriptableInvDrag>();
        inventaire = new List<ScriptableInvDrag>();
        foreach (ScriptableInvDrag invObject in inventaireList[0].objectList)
        {
            inventaire.Add(invObject);
        }

        touchManager = FindAnyObjectByType<TouchManager>();

        loadInventaire(0);

        infoUiArea.SetActive(false);
        activateUI(false);
        bienJoueMedaille.SetActive(false);
        medaille1.color = new Color(255, 255, 255, 0);
        medaille2.color = new Color(255, 255, 255, 0);
        medaille3.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isSelecting);

        if (isSelecting && !is3D)
        {
            if (touchManager.canSwipeVertical)
            {
                touchManager.canSwipeVertical = false;
            }
            if (TouchPosition().y > -5.15)
            {
                StartDrag();
                is3D = true;

            }
        }

        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began && canTouch)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.farClipPlane)), out var info))
                {
                    if (info.collider.gameObject.tag == "CaseInventaire")
                    {
                        if(colorSelecting)
                        {
                            colorSelecting.GetComponent<SpriteRenderer>().color = new Color(1, 0.93f, 0.83f, 1);
                        }
                        isSelecting = info.collider.gameObject;
                        colorSelecting = info.collider.gameObject;
                        isSelecting.GetComponent<SpriteRenderer>().color = new Color(1, 0.778712f, 0.6084906f, 1);
                        //StartDrag();

                    }
                }
            }
        }

        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended && canTouch)
            {
                if (isSelecting)
                {
                    
                    if (touchManager.lastTouchIsClick())
                    {
                        touchManager.canTouch = false;
                        touchManager.startTouchPosition = new Vector2(0,-42);
                        canTouch = false;
                        infoText.text = isSelecting.GetComponent<individualInvElement>().scriptableData.infoText;
                        infoImage.sprite = isSelecting.GetComponent<individualInvElement>().scriptableData.infoImageRef;
                        infoTitre.text = isSelecting.GetComponent<individualInvElement>().scriptableData.objectName;
                        infoUiArea.SetActive(true);
                    }
                    if(is3D) {
                        is3D = false;
                    }
                    else
                    {
                        isSelecting = null;
                    }

                }

                if (!touchManager.canSwipeVertical)
                {
                    touchManager.canSwipeVertical = true;
                }
            }
        }

       
        
        GameObject[] listReceptacle = GameObject.FindGameObjectsWithTag("Receptacle");
        bool estComplet = true;
        foreach (GameObject receptacle in GameObject.FindGameObjectsWithTag("Receptacle"))
        {
            if (receptacle.GetComponent<ObjectContainer>().objects.Count == 0)
            {
                estComplet = false;
            }
        }
        if (level == 3)
        {
            estComplet = false;
        }

        if (estComplet)
        {
            verifButton.SetActive(true);
        }
        else
        {
            verifButton.SetActive(false);
        }
        

    }

    public void Verif()
    {
        Draggable[] listObjetPlace = FindObjectsOfType<Draggable>();
        int compteur = 0;
        int reachScore = GameObject.FindGameObjectsWithTag("Receptacle").Length;
        Debug.Log("REACH SCORE : "+reachScore);

        foreach (Draggable draggable in listObjetPlace)
        {
            if (draggable.VerifyDraggable())
            {
                compteur++;
            }
        }

        if (compteur == reachScore)
        {
            foreach (Draggable draggable in listObjetPlace)
            {
                Destroy(draggable.transform.parent.gameObject);
            }
            NextLevel();
        }
    }

    public void loadInventaire(int numNiveau)
    {

        float offset = 0;

        foreach (ScriptableInvDrag invObject in inventaire)
        {
            GameObject tempAffichage = Instantiate(invObjectPrefab, transform);
            tempAffichage.transform.position += new Vector3(offset, 0, 0);
            SpriteRenderer spriteRenderer = tempAffichage.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = invObject.imageRef;
            tempAffichage.GetComponent<BoxCollider>().size = new Vector3(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y, 1);
            tempAffichage.GetComponent<individualInvElement>().invElementNum = (int) offset;
            tempAffichage.GetComponent<individualInvElement>().scriptableData = invObject;
            offset += 1;
        }
    }

    public void SlideRight()
    {
        canTouch = false;
        isSelecting = null;
        StartCoroutine(SmoothSlideRight());
    }

    public void SlideLeft()
    {
        canTouch = false;
        isSelecting = null;
        StartCoroutine(SmoothSlideLeft());
    }

    IEnumerator SmoothSlideRight()
    {
        float timeStamp = 0f;
        Vector3 actualPosition = transform.position;
        Vector3 destination = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);

        while (timeStamp < 0.2)
        {

            transform.position = Vector3.Lerp(
            actualPosition,
            destination,
            timeStamp / 0.2f
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.position = destination;
        canTouch = true;
    }

    IEnumerator SmoothSlideLeft()
    {
        float timeStamp = 0f;
        Vector3 actualPosition = transform.position;
        Vector3 destination = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);

        while (timeStamp < 0.2)
        {

            transform.position = Vector3.Lerp(
            actualPosition,
            destination,
            timeStamp / 0.2f
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.position = destination;
        canTouch = true;
    }

    public void NextLevel()
    {

        level += 1;

        bienJoueMedaille.SetActive(true);
        GameObject laMap = GameObject.FindGameObjectsWithTag("Map")[0];
        GameObject newMap = null;
        if (level < listCartes.Count)
        {
            newMap = Instantiate(listCartes[level], transform.parent);
            newMap.transform.eulerAngles = new Vector3(0, 0, newMap.transform.eulerAngles.z);
            newMap.transform.position = laMap.transform.position;
        }
        

        switch (level)
        {
            case 0:
                break;

            case 1:
                newMap.transform.position = new Vector3(newMap.transform.position.x,newMap.transform.position.y,newMap.transform.position.z);
                medailleAffiche.sprite = medaille1.sprite;
                medaille1.color = new Color(255,255,255,255);
                titreNiveau.text = "Arromanches #1";
                break;

            case 2:
                newMap.transform.position = new Vector3(newMap.transform.position.x - 0.3f, newMap.transform.position.y, newMap.transform.position.z);
                medailleAffiche.sprite = medaille2.sprite;
                medaille2.color = new Color(255, 255, 255, 255);
                titreNiveau.text = "Arromanches #2";
                break;
            case 3:
                medailleAffiche.sprite = medaille3.sprite;
                medaille3.color = new Color(255, 255, 255, 255);
                titreNiveau.text = "Fin !";
                break;
        }

        Invoke("desafficheBienJoue", 5f);

        Destroy(laMap);

        inventaire.Clear();
        if (level < inventaireList.Length)
        {
            foreach (ScriptableInvDrag invObject in inventaireList[level].objectList)
            {
                inventaire.Add(invObject);
            }
            
        }
        ReloadInv();


        PagesContainer containerDePage = FindAnyObjectByType<PagesContainer>();
        if (containerDePage != null)
        {
            containerDePage.NextLevel();
        }
    }
    public void desafficheBienJoue()
    {
        bienJoueMedaille.SetActive(false);
    }

    public void StartDrag()
    {
        if (isSelecting)
        {
            inventaire.Remove(isSelecting.GetComponent<individualInvElement>().scriptableData);
            inventairePlace.Add(isSelecting.GetComponent<individualInvElement>().scriptableData);

            ReloadInv();
            
            GameObject draggingObject = Instantiate(isSelecting.GetComponent<individualInvElement>().scriptableData.objectDraggable, transform.parent);
            draggingObject.transform.eulerAngles = new Vector3(180,0,0);
            draggingObject.GetComponentInChildren<Draggable>().isDragging = true;
            draggingObject.GetComponentInChildren<Draggable>().attachScriptableDrag = isSelecting.GetComponent<individualInvElement>().scriptableData;
        }
    }

    public void EndDrag(bool isOnReceptacle, GameObject leDraggable)
    {
        if (!isOnReceptacle && !leDraggable.GetComponent<Draggable>().isPlaced)
        {
            
            inventairePlace.Remove(leDraggable.GetComponent<Draggable>().attachScriptableDrag);
            inventaire.Add(leDraggable.GetComponent<Draggable>().attachScriptableDrag);
            Destroy(leDraggable.transform.parent.gameObject);
            isSelecting = null;

            ReloadInv();
        }
        if (is3D)
        {
            is3D = false;
        }

    }

    public void Dismiss(GameObject leDraggable)
    {
        inventairePlace.Remove(leDraggable.GetComponent<Draggable>().attachScriptableDrag);
        inventaire.Add(leDraggable.GetComponent<Draggable>().attachScriptableDrag);
        Destroy(leDraggable.transform.parent.gameObject);
        isSelecting = null;

        ReloadInv();
    }

    public void ReloadInv()
    {
        //GameObject tempIsSelected = isSelecting;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //isSelecting = tempIsSelected;

        if (inventaire.Count > 0)
        {
            float offset = 0;
            foreach (ScriptableInvDrag invObject in inventaire)
            {
                GameObject tempAffichage = Instantiate(invObjectPrefab, transform);
                tempAffichage.transform.position += new Vector3(offset, 0, 0);
                SpriteRenderer spriteRenderer = tempAffichage.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = invObject.imageRef;
                tempAffichage.GetComponent<BoxCollider>().size = new Vector3(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y, 1);
                tempAffichage.GetComponent<individualInvElement>().invElementNum = (int)offset;
                tempAffichage.GetComponent<individualInvElement>().scriptableData = invObject;
                offset += 1;
            }
        }

    }

    public void activateUI(bool etat)
    {
        if (etat)
        {
            spriteSombre.color = new Color(1, 1, 1, 0.45f);
        }
        else
        {
            spriteSombre.color = new Color(1, 1, 1, 0);
        }
        bandeArea.SetActive(etat);
        SlideLeftButton.SetActive(etat);
        SlideRightButton.SetActive(etat);
        if (etat)
        {
            ReloadInv();
        }
        else
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
    }

    public void desactivateInformation()
    {
        infoUiArea.SetActive(false);
        canTouch = true;
        touchManager.canTouch = true;
    }

    public Vector3 TouchPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.nearClipPlane + 7.5f));
    }
}
