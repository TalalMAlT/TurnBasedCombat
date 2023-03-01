using UnityEngine;

public class EnemyStatus: MonoBehaviour
{
    [SerializeField]public int Life;
    public bool Defend;
   
    
    public bool IsDead => Life <= 0;
    public bool IsDefending {
        get { return Defend; }
        set { Defend = value;}
    }

    public void Damage(int damage)
    {
        Life = Mathf.Max(0, Life - damage);
    }

}