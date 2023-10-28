using UnityEngine.Events;
using UnityEngine.Rendering;

public static class UIManager
{
    public static UnityEvent<int, int> onChangedHP;
    
    public static UnityEvent<int> onChangeCountSouls;

    public static void SendCountSouls(int count)
    {
        onChangeCountSouls?.Invoke(count);
    }

    public static void SendHP(int hp, int maxhp)
    {
        onChangedHP?.Invoke(hp, maxhp);
    }
    
}
