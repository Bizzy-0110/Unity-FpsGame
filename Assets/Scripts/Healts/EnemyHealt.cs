using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyHealt : HealthManager
{
    public override void onHealtUpdate()
    {
        return; // non fa nulla
    }

    public override void onDeath() // metodo che viene chiamato quando la salute arriva a zero
    {   
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // elimino dalla memoria il nemico
        // TODO: pool Enemies
    }
}
