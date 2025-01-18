using UnityEngine;

public abstract class HealthManager : MonoBehaviour
{
    
    [SerializeField] protected  bool invincible = false; // Se vero, non può subire danni

    // regenerazione
    [SerializeField] protected bool regenerate = false; // Se vero, la salute si rigenera
    [SerializeField] protected int regenHP = 0; // Quantità di salute rigenerata ogni volta
    [SerializeField] protected float regenDelay = 5f; // Tempo di ritardo per la rigenerazione (in secondi)

    [SerializeField] protected int startHP = 100; // Salute iniziale
    [SerializeField] protected int maxHP = 100; // Salute massima

    [SerializeField] protected GameObject destroyEffect; // Effetto visivo alla distruzione

    protected int currentHealth;
    private float timeSinceLastRegen = 0f; // Tempo trascorso dall'ultima rigenerazione

    private void Start()
    {
        currentHealth = startHP;
    }

    private void Update()
    {
        if (!regenerate) return; // Se non rigenera, non fare nulla

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
        currentHealth += regenHP; // Aggiungi la salute rigenerata

        onHealtUpdate(); // Esegui l'aggiornamento della salute

        // Assicurati che la salute non superi il massimo
        if (currentHealth > maxHP)
            currentHealth = maxHP;
    }

    
    public virtual void TakeDamage(int damage)
    {
        if (invincible) return; // Se invincibile, non subire danni

        currentHealth -= damage; // Sottrai i danni

        onHealtUpdate(); // Esegui l'aggiornamento della salute

        if (currentHealth <= 0) // Se la salute arriva a zero o meno, disabilita l'oggetto
        {
            onDeath();
            currentHealth = 0; // Imposta la salute a zero (evita valori negativi)
        }
        
    }

    // Funzioni astratte che devono essere implementate nelle classi figlie
    public abstract void onDeath(); // funzione eseguita una volta essaurita la vita
    public abstract void onHealtUpdate(); // funzione eseguita quando la salute viene modificata

    protected void showDestroyEffect() // Mostra l'effetto di distruzione se impostato
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
