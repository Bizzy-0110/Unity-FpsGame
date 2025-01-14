using UnityEngine;

public class PlayerHealt : MonoBehaviour
{

    [SerializeField] int startingHealt = 5;

    [SerializeField] int currrentHealt;

    private void Awake()
    {
        currrentHealt = startingHealt;

    }

    public void TakeDamage(int damage)
    {
        currrentHealt -= damage;

        if (currrentHealt <= 0)
        {
            Destroy(gameObject); // temporaneo
        }
    }
}
