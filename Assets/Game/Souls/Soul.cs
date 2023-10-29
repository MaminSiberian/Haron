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
            Debug.Log("�������� ���������� �� �������, ������� ����������� ����� ������� ������");
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
}


