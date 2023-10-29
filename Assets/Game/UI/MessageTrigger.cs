using UI;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIDirector.SendMessage(Messages.randomLines[Random.Range(0, Messages.randomLines.Count)], 10f);
            gameObject.SetActive(false);
        }
    }
}
