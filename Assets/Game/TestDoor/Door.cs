using UnityEditor.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    private Animator _anim;

    private void Start()
    {
        isOpen = false;
        _anim = GetComponent<Animator>();
    }

    public void Open()
    {
        if( !isOpen )
        {
            _anim.SetBool("Open", true);
            isOpen = true;
        }
    }
    



}
