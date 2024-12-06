using UnityEngine;

public class HealtManager : MonoBehaviour
{
    [SerializeField] int startHP = 100;
    [SerializeField] int maxHP = 100;
    [SerializeField] int regenHP = 0;

    private int healt;

    private void Start()
    {
        healt = startHP;
    }

    public void TakeDamage(int damage)
    {
        healt -= damage;

        if (healt <= 0) // nascondo il robot se finisce la vita
        {
            gameObject.SetActive(false);
        }

    }
}
