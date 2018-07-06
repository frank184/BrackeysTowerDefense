using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyUI : MonoBehaviour {

    private void Update()
    {
        GetComponent<Text>().text = "$" + Player.Money;
    }
}
