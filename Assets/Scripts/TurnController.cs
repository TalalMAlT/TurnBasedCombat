using TMPro;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    private int _turn = 0;

    public int Turn => _turn;
    public bool IsPlayerTurn => _turn % 2 == 0;

    public void Clear()
    {
        SetTurn(0);
    }

    public void SetTurn(int turn)
    {
        _turn = turn;
        turnText.text = "Turn " + (_turn + 1);
    }

    public void Increment()
    {
        SetTurn(_turn + 1);
    }

    private void Awake()
    {
        Clear();
    }
}