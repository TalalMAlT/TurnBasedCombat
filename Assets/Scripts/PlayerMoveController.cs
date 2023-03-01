using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveController : MonoBehaviour
{

    [SerializeField] private RectTransform playerRectTransform;

    // physical inputs
    void Update()
    {
        var position = playerRectTransform.anchoredPosition;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += 1.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= 1.0f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            position.y += 1.0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= 1.0f;
        }
        playerRectTransform.anchoredPosition = position;
    }

   // public class PlayerCollision : MonoBehaviour
    //{
        private void OnTriggerEnter2D(Collider2D col)
        
        {
            if (col.gameObject.CompareTag("Enemy1"))
            {
                SceneManager.LoadScene("MainGame");
            }
        }


    //}
}