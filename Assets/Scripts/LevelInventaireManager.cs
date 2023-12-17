using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInventaireManager : MonoBehaviour
{

    public ScriptableInv[] inventaireList;

    [SerializeField] private GameObject invObjectPrefab;

    private List<ScriptableInvDrag> inventaire;
    private TouchManager touchManager;

    private GameObject isSelecting;


    // Start is called before the first frame update
    void Start()
    {
        isSelecting = null;
        inventaire = new List<ScriptableInvDrag>();
        touchManager = FindAnyObjectByType<TouchManager>();
        loadInventaire(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.farClipPlane)), out var info))
                {
                    if (info.collider.gameObject.tag == "CaseInventaire")
                    {
                        isSelecting = info.collider.gameObject;
                        
                    }
                }
            }
        }

        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (isSelecting)
                {
                    if (touchManager.lastTouchIsClick())
                    {
                        Debug.Log(isSelecting.GetComponent<individualInvElement>().invElementNum);
                    }
                }

                isSelecting = null;
            }
        }
    }

    public void loadInventaire(int numNiveau)
    {

        float offset = 0;

        foreach (ScriptableInvDrag invObject in inventaireList[numNiveau].objectList)
        {
            inventaire.Add(invObject);
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
}
