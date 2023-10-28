using UnityEngine;
using UnityEngine.UI;

public class UiHUD : MonoBehaviour
{

    public Text text;
    public Image HealthBar;
    private void Start()
    {
        
        
    }

    public void UpdateCountSouls(int count)
    {
        text.text = "Count: " + count;
    }

    public void UpdateHealthInfo(int hp, int maxHp)
    {
        HealthBar.fillAmount = hp / maxHp;
    }
}
