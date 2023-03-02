using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    [SerializeField] Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x < playerPosition.localPosition.x)
        {
                    SceneManager.LoadScene(nextLevelName);
        }
    }

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Debug.Log("next level");
    //    SceneManager.LoadScene("Move" + nextLevel);
    //}
}
