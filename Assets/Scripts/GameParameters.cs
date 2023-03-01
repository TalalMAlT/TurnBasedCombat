using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    //please add parameters which you want to share
    public int playerLife=4;
    public int playerSpecialPoint=4;
    public int currentLevel=0;

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