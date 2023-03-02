using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
     if(GameParameters.Instance.currentLevel==1){
        Debug.Log("Worked");
         enemy.SetActive(false);

     }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
