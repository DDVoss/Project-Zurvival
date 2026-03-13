using UnityEngine;
public class ZombieHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    private float _currentHealth;
    private ZombieContext _context;

    public void Initialize(ZombieContext context)
    {
        _context = context;
        _currentHealth = maxHealth;
        _context.Health = _currentHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _context.Health = _currentHealth;
        _context.IsHit = true;

        if (_currentHealth <= 0)
        {
            _context.Health = 0;
            Destroy(gameObject);
        }
    }
}