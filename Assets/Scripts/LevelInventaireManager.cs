using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInventaireManager : MonoBehaviour
{

    public ScriptableInv[] inventaireList;

    [SerializeField] private GameObject invObjectPrefab;


    // Start is called before the first frame update
    void Start()
    {
        loadInventaire(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadInventaire(int numNiveau)
    {
        foreach (ScriptableInvDrag invObject in inventaireList[numNiveau].objectList)
        {
            GameObject tempAffichage = Instantiate(invObjectPrefab, transform);
            tempAffichage.GetComponent<SpriteRenderer>().sprite = invObject.imageRef;
        }
      
    }
    

}
