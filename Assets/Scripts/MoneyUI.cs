using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyUI : MonoBehaviour {

    private void Update()
    {
        GetComponent<Text>().text = string.Format("${0:F0}", Player.Money);
    }
}
