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
              ChangeState(MainGameState.PlayerActionDone);

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
              ChangeState(MainGameState.PlayerActionDone);
    }

    public void Defend()
    {
      
        Debug.Log("Defend");
        _playerStatus.IsDefending = true;
          ChangeState(MainGameState.PlayerActionDone);
            
    }

    public void Special()
    {
        
        Debug.Log("Special");
        _playerStatus.IsDefending = false;
        ChangeState(MainGameState.PlayerActionDone);
    }

    public void GoToEnding()
    {
        GameParameters.Instance.playerLife = _playerStatus.Life;
        SceneManager.LoadScene("Ending");
        
    }
}