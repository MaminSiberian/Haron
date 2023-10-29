using System.Collections;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SoulsInfo _soulData;

    private int _index;
    private int _marinaId;
    private Color _color;
    [Range(9, 14)]public float minCooldown;
    [Range(15, 30)]public float maxCooldown;
    public float moveSpeed;
    private float cooldown;
    public AudioClip[] FX;
    private AudioSource _source;
    private float currentCooldown;
    [SerializeField] private Animator _anim;
    public Vector3 _startPos;

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
        
        _startPos = transform.position;

        
    }
    private void OnEnable()
    {
        LevelDirector.OnRespawn += Reset;
    }
    private void OnDisable()
    {
        LevelDirector.OnRespawn -= Reset;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) _spriteRenderer.flipX = true;
        if (Input.GetAxis("Horizontal") > 0) _spriteRenderer.flipX = false;

        if (currentCooldown < 0 && !_source.isPlaying)
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


    public void Reset()
    {
        transform.position = _startPos;

    }
    public SoulsInfo GetSoulsInfo() { return _soulData; }
    public void SetSoulsInfo(SoulsInfo info) { _soulData = info; }

    public void StartMarinaMove(Transform pos)
    {
        StartCoroutine(MarinaMove(pos));
    }
    public Animator GetAnim()
    {
        return _anim;
    }

    private IEnumerator MarinaMove(Transform pos)
    {
        _anim.SetBool("Idle", true);
        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, pos.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
            float distance = Vector2.Distance(transform.position, pos.position);
            if(distance <= 0.3f)
            {
                yield return new WaitForSeconds(2f);
                Destroy(gameObject);
                break;
            }
        }
        
    }
}


