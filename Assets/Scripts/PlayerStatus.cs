using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int Life;
    public int SpecialPoint;
    public bool Defend;
    public bool IsDead => Life <= 0;
    
    public bool IsEnableToUseSpecial => SpecialPoint >= 0;

    public void Damage(int damage)
    {
        Life = Mathf.Max(0, Life - damage);
    }
    public bool IsDefending {
        get { return Defend; }
        set { Defend = value;}
    }
    public void ConsumeSpecialPoint(int amount)
    {
        Debug.Assert(IsEnableToUseSpecial);
        SpecialPoint = Mathf.Max(0, SpecialPoint - amount); 
    }

}