using UnityEngine;

public class Player : MonoBehaviour {
    public static int Lives;
    private int startLives = 20;

    public static int Money;
    private int startMoney = 400;

    private void Start()
    {
        Lives = startLives;
        Money = startMoney;
    }
}
