using UnityEngine;
using TMPro;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private bool isTicking = false;
    private float timer = 0f;
    private float time;

    private void Awake()
    {
        text.enabled = false;
    }
    private void Update()
    {
        if (!isTicking)
        {
            text.enabled = false;
            return;
        }
        Tick();
    }
    public void SendMessage(string message, float time = 20f)
    {
        text.text = message;
        this.time = time;
        timer = 0f;
        isTicking = true;
        text.enabled = true;
    }
    private void Tick()
    {
        if (timer >= time)
        {
            timer = 0f;
            isTicking = false;
            text.enabled = false;
        }
        else
            timer += Time.deltaTime;
    }
}
