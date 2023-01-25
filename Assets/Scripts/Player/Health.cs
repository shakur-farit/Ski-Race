using UnityEngine;

/// <summary>
/// Class of health counting.
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [Space(10)]
    [Tooltip("Current health.")]
    [SerializeField] private int currentHealth;
    [Tooltip("Maximum health.")]
    [SerializeField] private int maximumHealth;

    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public int MaximumHealth { get { return maximumHealth; } }

    /// <summary>
    /// Change number of current health
    /// </summary>
    /// <param name="changingValue"></param>
    public void ChangeCurrentHealth(int changingValue)
    {
        CurrentHealth += changingValue;

        if (CurrentHealth > maximumHealth)
            CurrentHealth = maximumHealth;
       
    }
}
