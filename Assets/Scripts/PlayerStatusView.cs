using TMPro;
using UnityEngine;

public class PlayerStatusView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI spText;
    [SerializeField]private PlayerStatus _playerStatus;

    void Update()
    {
        if(_playerStatus!=null){
        lifeText.text = "Life:" + _playerStatus.Life.ToString();
        spText.text = "SP:" + _playerStatus.SpecialPoint.ToString();
        }
    }
}