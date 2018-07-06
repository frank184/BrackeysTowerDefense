using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameOver = false;

    private void Update()
    {
        if (!gameOver)
            if (Player.Lives <= 0)
                EndGame();
    }

    private void EndGame()
    {
        gameOver = true;
        Debug.Log("GAME OVER");
    }
}
