using System.Collections;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private SoulsInfo _soulData;

    private int _index;
    private int _marinaId;
    private Color _color;
    public SpriteRenderer _renderer;
    [Range(9, 14)]public float minCooldown;
    [Range(15, 30)]public float maxCooldown;
    public float moveSpeed;
    private float cooldown;
    public AudioClip[] FX;
    private AudioSource _source;
    private float currentCooldown;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        currentCooldown = cooldown;
        cooldown = Random.Range(minCooldown, maxCooldown);
        if(_soulData != null )
        {
            _index = _soulData.Index;
            _marinaId = _soulData.Marinaid;
            _color = _soulData.color;
        }
        else
        {
            Debug.Log("Занесите информацию об объекте, который отобразился белым красным цветом");
            _renderer.color = Color.red;
            return;
        }
        
    }

    private void Update()
    {
        if(currentCooldown < 0 && !_source.isPlaying)
        {
            _source.PlayOneShot(FX[Random.Range(0, FX.Length - 1)]);
            currentCooldown = cooldown;
        }
        else if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public int GetIndex() { return _index; }
    public int GetMarinaId() { return _marinaId; }

   
    public SoulsInfo GetSoulsInfo() { return _soulData; }
    public void SetSoulsInfo(SoulsInfo info) { _soulData = info; }

    public void StartMarinaMove(Transform pos)
    {
        StartCoroutine(MarinaMove(pos));
    }

    private IEnumerator MarinaMove(Transform pos)
    {
        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, pos.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
            if(transform.position == pos.position)
            {
                yield return new WaitForSeconds(2f);
                Destroy(gameObject);
                break;
            }
        }
        
    }
}


