using UI;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    private Animator _anim;
    private bool canOpen;

    private void Start()
    {
        isOpen = false;
        _anim = GetComponent<Animator>();
        canOpen = false;
    }

    

    public void Open()
    {
        if( !isOpen  && LevelDirector.keysCounter > 0)
        {
            _anim.SetBool("Open", true);
            isOpen = true;
            UIDirector.SendMessage(Messages.doorOpened, 4f);
            LevelDirector.WasteKey();
            Destroy(this);
        }
        else if(!isOpen && LevelDirector.keysCounter <= 0)
        {
            UIDirector.SendMessage(Messages.doorNotOpened, 3f);
        }

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canOpen)
        {
            Open();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpen = false;
        }
    }





}
