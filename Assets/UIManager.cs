using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text countSouls;

    public void UpdateSoulsCount(string text)
    {
        countSouls.text = "Count: " + text; 
    }
}
