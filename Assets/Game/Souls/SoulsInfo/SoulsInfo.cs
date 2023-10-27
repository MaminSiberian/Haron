using UnityEngine;

[CreateAssetMenu(fileName = "SoulsInfo", menuName = "ScriptableObjects/SoulsInfo", order = 1)]
public class SoulsInfo : ScriptableObject
{
    [SerializeField] private int _index;
    [SerializeField] private int _marinaId;
    [SerializeField] private Color _color;
    [SerializeField] private int _dialogieId;

    public int Index => _index;
    public int Marinaid => _marinaId;
    public Color color => _color;

    public int dialogieId => _dialogieId;



}
