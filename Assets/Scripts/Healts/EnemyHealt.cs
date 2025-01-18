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
        showDestroyEffect(); // mostro l'effetto di distruzione

        Destroy(gameObject); // elimino dalla memoria il nemico
        // TODO: pool Enemies
    }
}
