using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private TurnController turnController;
    [SerializeField] private PlayerStatusView playerStatusView;
    [SerializeField] private EnemyStatusView enemyStatusView;

    [SerializeField]private PlayerStatus _playerStatus;
    [SerializeField]private EnemyStatus _enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (turnController.Turn > 10)
        if (_enemyStatus.IsDead)
        {
            GoToEnding();
        }
    }

    public void Punch()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
        if(turnController.IsPlayerTurn){
        if(r < 0.67f){
        Debug.Log("Punch");
        const int punchPower = 1;
        if(_enemyStatus.IsDefending==true){
        }
        else{
        _enemyStatus.Damage(punchPower);
        }
        }else{
        Debug.Log("Punch Missed");}
        turnController.Increment();
        }
    }
    
    public void Kick()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
        if(turnController.IsPlayerTurn){
        if(r < 0.33f){
        Debug.Log("Kick");
        _playerStatus.IsDefending=false;
        const int kickPower = 2;
        if(_enemyStatus.IsDefending==true){
        _enemyStatus.Damage(1);
        }
        else{
        _enemyStatus.Damage(kickPower);
        }
        }
        else{
        Debug.Log("Kick Missed");}
        turnController.Increment();
        }
    }
    
    public void Defend()
    {
        if(turnController.IsPlayerTurn){
        Debug.Log("Defend");
        _playerStatus.IsDefending=true;
        turnController.Increment();
        }
    }
    
    public void Special()
    {
        if(turnController.IsPlayerTurn){
        Debug.Log("Special");
        _playerStatus.IsDefending=false;
        turnController.Increment();
        }    
    }

    public void GoToEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}
