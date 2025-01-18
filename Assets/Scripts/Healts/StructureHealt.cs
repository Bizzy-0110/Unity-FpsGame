using UnityEngine;

public class StructureHealt : HealthManager
{
    public override void onDeath()
    {
        showDestroyEffect(); // mostro l'effetto di distruzione
        Destroy(gameObject); // elimino la struttura
    }

    public override void onHealtUpdate()
    {
        return; // non faccio nulla
    }

}
