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
    [SerializeField] private GameObject _idle;
    [SerializeField] private GameObject _punch;
    [SerializeField] private GameObject _kick;
    [SerializeField] private GameObject _special;
    [SerializeField] private GameObject _defend;
    enum MainGameState
    {
        PlayerActionWait,
        PlayerActionDone,
        EnemyActionWait,
        EnemyActionDone,
    }

    private MainGameState _mainGameState;
    private MainGameState _nextGameState;
    private int _stateCounter;
    private float stateTimer;

    // Start is called before the first frame update
    void Start()
    {
        _playerStatus.Life =  GameParameters.Instance.playerLife;
         _playerStatus.SpecialPoint =  GameParameters.Instance.playerSpecialPoint;

        ChangeState(MainGameState.PlayerActionWait);
    }

    void SetAnimation(string animationName)
    {
        _idle.SetActive(false);
        _punch.SetActive(false);
        _idle.SetActive(false);
        _special.SetActive(false);
        _defend.SetActive(false);
        Debug.Log("Anim : " + animationName);
        switch(animationName)
        {
            case "idle":
                _idle.SetActive(true);
                break;
            case "punch":
                _punch.SetActive(true);
                break;
            case "kick":
                _kick.SetActive(true);
                break;
            case "defend":
                _defend.SetActive(true);
                break;
            case "special":
                _special.SetActive(true);
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(_mainGameState != _nextGameState)
        {
            _mainGameState = _nextGameState;
            _stateCounter = 0;
            stateTimer = 0;
        }

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
    }

    void LateUpdate()
    {
        _stateCounter++;
    }

    void UpdatePlayerActionWait()
    {
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdatePlayerActionWait");
            buttonRoot.SetActive(true);
          

        
        }
    }

    void UpdatePlayerActionDone()
    {
        Debug.Log("UpdatePlayerActionDone " + _stateCounter);
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdatePlayerActionDone");
            stateTimer = 2;
        }
        stateTimer -= Time.deltaTime;
        if(stateTimer > 0)
             return;

        SetAnimation("idle");
        ChangeState(MainGameState.EnemyActionWait);
    }

    void UpdateEnemyActionWait()
    {
        if (_stateCounter == 0)
        {
            Debug.Log("Start UpdateEnemyActionWait");
            _enemycontroller.ChooseAction();
            ChangeState(MainGameState.EnemyActionDone);

        }
    }

    void UpdateEnemyActionDone()
    {
        if (_stateCounter == 0)
        {
            float timeRemaining=1.0f;
            while(timeRemaining > 0){
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
        _nextGameState = state;
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
                    SetAnimation("punch");

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
                    _idle.SetActive(false);
                    _kick.SetActive(true);

                }
                else
                {
                    _enemyStatus.Damage(kickPower);
                    SetAnimation("kick");

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
         SetAnimation("defend");

          ChangeState(MainGameState.PlayerActionDone);
        
            
    }

    public void Special()
    {
        if(_playerStatus.SpecialPoint==4){
        Debug.Log("Special");
        _enemyStatus.Damage(2);
        _playerStatus.SpecialPoint-=4;
        Debug.Log("Sp = "+_playerStatus.SpecialPoint);
        SetAnimation("special");
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
      SwitchLevel();
      GameParameters.Instance.playerSpecialPoint= _playerStatus.SpecialPoint;

    }
    void SwitchLevel(){
         switch (GameParameters.Instance.currentLevel)
        {
            case 0 or 1:
                SceneManager.LoadScene("Move");
                break;
            case 2 or 3 or 4:
                SceneManager.LoadScene("Move 1");
                break;
            case 5 or 6 or 7:
                SceneManager.LoadScene("Move 2");
                break;    
        }

    }
}