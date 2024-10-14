using System;

public class CharacterState
{
    private float health = 100;

    public float Health
    {
        get => health;
        private set
        {
            health = value;
            if (health < 0)
            {
                health = 0;
                OnDamageTake?.Invoke(health);
            }
        }
    }

    public Action<float> OnDamageTake { get; set; }
    public Action OnDeath { get; set; }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        OnDamageTake?.Invoke(health);

        if (Health == 0) OnDeath?.Invoke();
    }

    ~CharacterState()
    {
        OnDamageTake = null;
        OnDeath = null;
    }
}