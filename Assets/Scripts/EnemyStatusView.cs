using TMPro;
using UnityEngine;

public class EnemyStatusView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField]private EnemyStatus _enemyStatus;

    

    void Update()
    {
        
        lifeText.text = "Life:" + _enemyStatus.Life.ToString();
       
    }
}