using UnityEngine;

public class Player : MonoBehaviour {
    public static int Lives;
    private int startLives = 20;

    public static float Money;
    private int startMoney = 800;

    private void Start()
    {
        Lives = startLives;
        Money = startMoney;
    }
}
