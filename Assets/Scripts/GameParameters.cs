using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;
    
    //please add parameters which you want to share
    public int playerLife;
    public int playerSpecialPoint;
    public int currentLevel;
    
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        new GameObject("GameParametersInstance", typeof(GameParameters));
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}