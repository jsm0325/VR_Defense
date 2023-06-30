using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Monster", fileName = "MonsterData")]

public class MonsterScriptable : ScriptableObject
{
    public GameObject[] dropItem;
    public int coin = 0;

    public int HP = 0;
    public float moveSpeed = 0;

    public bool stun = false;
}
