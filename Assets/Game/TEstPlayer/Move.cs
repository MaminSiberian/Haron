using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(h, v);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
