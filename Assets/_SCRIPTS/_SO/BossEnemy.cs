using UnityEngine;

[CreateAssetMenu(fileName = "BossName", menuName = "Scriptable Objects/BossEnemy")]
public class BossEnemy : ScriptableObject
{
    public string _name;

    public float _health;
    public float _damage;
    public float _attackCooldown;
}
