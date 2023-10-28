using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private Slider healthBar;

    private void OnEnable()
    {
        transform.SetParent(GameplayUI.enemyHP);
    }
}
