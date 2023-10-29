using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private SoulsInfo _soulData;

    private int _index;
    private int _marinaId;
    private Color _color;
    public SpriteRenderer _renderer;

    private void Start()
    {
        
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
        
    }

    public int GetIndex() { return _index; }
    public int GetMarinaId() { return _marinaId; }

   
    public SoulsInfo GetSoulsInfo() { return _soulData; }
    public void SetSoulsInfo(SoulsInfo info) { _soulData = info; }
}


