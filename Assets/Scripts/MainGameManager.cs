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
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdatePlayerActionWait");
        }
    }

    void UpdatePlayerActionDone()
    {
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdatePlayerActionDone");
        }
    }

    void UpdateEnemyActionWait()
    {
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdateEnemyActionWait");
        }
    }

    void UpdateEnemyActionDone()
    {
        if (_stateCounter == 0)
        {
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
        if (turnController.IsPlayerTurn)
        {
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

            turnController.Increment();
        }
    }

    public void Kick()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
        if (turnController.IsPlayerTurn)
        {
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

            turnController.Increment();
        }
    }

    public void Defend()
    {
        if (turnController.IsPlayerTurn)
        {
            Debug.Log("Defend");
            _playerStatus.IsDefending = true;
            turnController.Increment();
        }
    }

    public void Special()
    {
        if (turnController.IsPlayerTurn)
        {
            Debug.Log("Special");
            _playerStatus.IsDefending = false;
            turnController.Increment();
        }
    }

    public void GoToEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}