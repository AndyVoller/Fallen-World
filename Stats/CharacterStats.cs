using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected List<Stat> stats = new List<Stat>();

    [Header("Main stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int maxMagicka;
    [SerializeField] protected int maxStamina;
    [Space(10)]
    [SerializeField] private int expForDefeat;

    public int CurrentHealth { get; protected set; }
    public int CurrentMagicka { get; protected set; }
    public int CurrentStamina { get; protected set; }

    public int HealthRegen { get; set; }
    public int MagickaRegen { get; set; }
    public int StaminaRegen { get; set; }

    protected IStatsUI statsUI;

    private static PlayerStats playerStats;

    void Start()
    {
        GetStartStatsValues();
        statsUI = new CharacterStatsUI();
        statsUI.UpdateUI(this);
        StartCoroutine("RegenStats");
        playerStats = FindObjectOfType<PlayerStats>();
    }

    protected virtual void GetStartStatsValues()
    {
        CurrentHealth = maxHealth;
        CurrentMagicka = maxMagicka;
        CurrentStamina = maxStamina;

        HealthRegen = 3;
        MagickaRegen = 10;
        StaminaRegen = 5;
        expForDefeat = 100;
    }

    #region GetSetMethods

    // --------------------- Get methods -----------------------------

    public Stat GetStat(string statName)
    {
        foreach (Stat stat in stats)
        {
            if (stat.GetStatName() == statName)
            {
                return stat;
            }
        }
        return null;
    }

    public List<Stat> GetStats()
    {
        return stats;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetMaxMagicka()
    {
        return maxMagicka;
    }

    public int GetMaxStamina()
    {
        return maxStamina;
    }

    // ------------------- Set methods -----------------------------

    public void SetMaxHealth(int amount)
    {
        maxHealth += amount;
        statsUI.UpdateHealthBar(this);
    }

    public void SetMaxMagicka(int amount)
    {
        maxMagicka += amount;
        statsUI.UpdateMagickaBar(this);
    }

    public void SetMaxStamina(int amount)
    {
        maxStamina += amount;
        statsUI.UpdateStaminaBar(this);
    }

    #endregion

    protected IEnumerator RegenStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CurrentHealth += HealthRegen;
            CurrentMagicka += MagickaRegen;
            CurrentStamina += StaminaRegen;
            ClampCurrentStats();
            statsUI.UpdateUI(this);
        }
    }

    protected void ClampCurrentStats()
    {
        CurrentHealth = CurrentHealth > maxHealth ? maxHealth : CurrentHealth;
        CurrentMagicka = CurrentMagicka > maxMagicka ? maxMagicka : CurrentMagicka;
        CurrentStamina = CurrentStamina > maxStamina ? maxStamina : CurrentStamina;
    }

    public virtual bool UseMagicka(int amount)
    {
        if (CurrentMagicka < amount)
            return false;
        CurrentMagicka -= amount;
        statsUI.UpdateMagickaBar(this);
        return true;
    }

    public virtual bool UseStamina(int amount)
    {
        if (CurrentStamina < amount)
            return false;
        CurrentStamina -= amount;
        statsUI.UpdateStaminaBar(this);
        return true;
    }

    public virtual void TakeDamage(Damage damage)
    {
        int damageAmount = CalculateReceivedDamage(damage);
        CurrentHealth -= damageAmount;
        statsUI.UpdateHealthBar(this);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    protected int CalculateReceivedDamage(Damage damage)
    {
        if (damage.DamageElement == Element.Other)      // No defence against "Other" damage
            return damage.DamageAmount;

        Stat defStat = stats.Find(x => x.GetStatType() == Stat.StatType.Defence && x.GetElement() == damage.DamageElement);
        if (defStat == null)
        {
            Debug.LogWarning("Can't find stat");
            return 0;
        }

        return Mathf.Clamp(damage.DamageAmount - defStat.GetValue(), 1, int.MaxValue);
    }

    protected virtual void Die()
    {
        playerStats.AddExperience(expForDefeat);

        IEnemy enemy = GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.Die();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
