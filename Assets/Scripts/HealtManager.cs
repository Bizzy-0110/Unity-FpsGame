using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int startHP = 100; // Salute iniziale
    [SerializeField] int maxHP = 100; // Salute massima
    [SerializeField] int regenHP = 1; // Quantità di salute rigenerata ogni volta
    [SerializeField] float regenDelay = 1f; // Tempo di ritardo per la rigenerazione (in secondi)
    [SerializeField] GameObject destroyEffect;

    private int health;
    private float timeSinceLastRegen = 0f; // Tempo trascorso dall'ultima rigenerazione

    private void Start()
    {
        health = startHP;
    }

    private void Update()
    {
        // Aumenta il tempo trascorso dal momento dell'ultimo aggiornamento
        timeSinceLastRegen += Time.deltaTime;

        // Se il tempo trascorso è maggiore o uguale al ritardo di rigenerazione, rigenera la salute
        if (timeSinceLastRegen >= regenDelay)
        {
            Regenerate();
            timeSinceLastRegen = 0f; // Reset del timer
        }
    }

    private void Regenerate()
    {
        health += regenHP; // Aggiungi la salute rigenerata

        // Assicurati che la salute non superi il massimo
        if (health > maxHP)
            health = maxHP;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Sottrai i danni

        if (health <= 0) // Se la salute arriva a zero o meno, disabilita l'oggetto
        {
            SelfDestruction();
            health = 0; // Imposta la salute a zero (evita valori negativi)
        }
        
    }

    public void SelfDestruction()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // elimino dalla memoria il nemico
        
        // TODO: pool Enemies
       // gameObject.SetActive(false); // Disabilita l'oggetto (oppure aggiungi una logica per morte/animazione)
    }

    public int GetHealth()
    {
        return health;
    }
}
