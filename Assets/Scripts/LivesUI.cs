using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesUI : MonoBehaviour
{

    private void Update()
    {
        GetComponent<Text>().text = Player.Lives + " LIVES";
    }
}
