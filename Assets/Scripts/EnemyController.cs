using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private TurnController turnController;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private EnemyStatus enemyStatus;
    void update(){
        ChooseAction();
    }
    void ChooseAction()
    {
        if(turnController.IsPlayerTurn){
        }
        else{
            float r = UnityEngine.Random.Range(0, 1.0f);
        if (r < 0.6f)
        {
            Attack();
        }
        else
        {
            Defend();
        }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        enemyStatus.IsDefending= false;
        if(playerStatus.IsDefending==true){
        }
        else{
        playerStatus.Damage(1);
        }
        turnController.Increment();
    }
    
    void Defend()
    {
        Debug.Log("Defend");
        enemyStatus.IsDefending = true;
        turnController.Increment();
    }
}