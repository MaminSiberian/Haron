

using UI;
using UnityEngine;

public class Marina : MonoBehaviour
{
    public int index;
    public int countSouls;
    public Transform souldelivered;
    public Transform posForSoulDelivired;
    [SerializeField] private Transform target;
    private GameplayUI gameplayUI;
    private const string HelpShop = "Нажми Space, чтобы открыть магазин";
    private bool canOpenShop;

    private void Awake()
    {
        if (index == 1) LevelDirector.SetStartPier(target);
        canOpenShop = false;
    }

    private void Start()
    {
        gameplayUI = FindObjectOfType<GameplayUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameplayUI.ShowHelpText(HelpShop, true);
            canOpenShop = true;
            UIDirector.SendMessage("Чтобы взять или сдать призрака нажмите E", 5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameplayUI.ShowHelpText(HelpShop, false);
            
            canOpenShop = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canOpenShop)
        {
            UIDirector.OpenShop();
            gameplayUI.ShowHelpText(HelpShop, false);
        }
    }
}
