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

    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private EnemyStatus _enemyStatus;
    [SerializeField] private GameObject buttonRoot;
    [SerializeField] private EnemyController _enemycontroller;
    enum MainGameState
    {
        PlayerActionWait,
        PlayerActionDone,
        EnemyActionWait,
        EnemyActionDone,
    }

    private MainGameState _mainGameState;
    private int _stateCounter;

    // Start is called before the first frame update
    void Start()
    {
        _playerStatus.Life =  GameParameters.Instance.playerLife;
         _playerStatus.SpecialPoint =  GameParameters.Instance.playerSpecialPoint;

        ChangeState(MainGameState.PlayerActionWait);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_mainGameState)
        {
            case MainGameState.PlayerActionWait:
                UpdatePlayerActionWait();
                break;
            case MainGameState.PlayerActionDone:
                UpdatePlayerActionDone();
                break;
            case MainGameState.EnemyActionWait:
                UpdateEnemyActionWait();
                break;
            case MainGameState.EnemyActionDone:
                UpdateEnemyActionDone();
                break;
        }
        _stateCounter++;
    }

    void UpdatePlayerActionWait()
    {
        if (_stateCounter == 1)
        {
            Debug.Log("Start UpdatePlayerActionWait");
            buttonRoot.SetActive(true);
          

        
        }
    }

    void UpdatePlayerActionDone()
    {
        if (_stateCounter == 1)
        {
            Debug.Log("Start UpdatePlayerActionDone");
            //timeRemaining=3;
            //if(timeRemaining > 0){
            //timeRemaining -= Time.deltaTime;
            //}
            buttonRoot.SetActive(false);
            ChangeState(MainGameState.EnemyActionWait);

        }
    }

    void UpdateEnemyActionWait()
    {
        if (_stateCounter == 1)
        {
            Debug.Log("Start UpdateEnemyActionWait");
            _enemycontroller.ChooseAction();
            ChangeState(MainGameState.EnemyActionDone);

        }
    }

    void UpdateEnemyActionDone()
    {
        if (_stateCounter == 1)
        {
            float timeRemaining=1.0f;
            if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            }
            Debug.Log("Start UpdateEnemyActionDone");
        }
        
        if (_enemyStatus.IsDead)
        {
            GoToEnding();
            GameParameters.Instance.currentLevel+=1;
            Debug.Log("current level = "+GameParameters.Instance.currentLevel);
            GameParameters.Instance.playerLife = _playerStatus.Life;
        }
        else
        {
            ChangeState(MainGameState.PlayerActionWait);
        }
    }

    void ChangeState(MainGameState state)
    {
        _mainGameState = state;
        _stateCounter = 0;
    }

    public void Punch()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
       
            if (r < 0.67f)
            {
                Debug.Log("Punch");
                const int punchPower = 1;
                if (_enemyStatus.IsDefending == true)
                {
                }
                else
                {
                    _enemyStatus.Damage(punchPower);
                    
                }
            }
            else
            {
                Debug.Log("Punch Missed");
            }
             SP_check();
              ChangeState(MainGameState.PlayerActionDone);

    }
    void SP_check(){
        if(_playerStatus.SpecialPoint < 4){
                _playerStatus.SpecialPoint+=1;
              }
    
    }

    public void Kick()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
        
            if (r < 0.33f)
            {
                Debug.Log("Kick");
                _playerStatus.IsDefending = false;
                const int kickPower = 2;
                if (_enemyStatus.IsDefending == true)
                {
                    _enemyStatus.Damage(1);
                }
                else
                {
                    _enemyStatus.Damage(kickPower);
                }
            }
            else
            {
                Debug.Log("Kick Missed");
            }
              SP_check();
              ChangeState(MainGameState.PlayerActionDone);
    }

    public void Defend()
    {
      
        Debug.Log("Defend");
        _playerStatus.IsDefending = true;
            SP_check();
          ChangeState(MainGameState.PlayerActionDone);
        
            
    }

    public void Special()
    {
        if(_playerStatus.SpecialPoint==4){
        Debug.Log("Special");
        _enemyStatus.Damage(2);
        _playerStatus.SpecialPoint-=4;
        Debug.Log("Sp = "+_playerStatus.SpecialPoint);
        }
        else{
            Debug.Log("Points are not enough :)");
        }
        _playerStatus.IsDefending = false;
        ChangeState(MainGameState.PlayerActionDone);
    }

    public void GoToEnding()
    {
        GameParameters.Instance.playerLife = _playerStatus.Life;
        SceneManager.LoadScene("Move");
      GameParameters.Instance.playerSpecialPoint= _playerStatus.SpecialPoint;

    }
}