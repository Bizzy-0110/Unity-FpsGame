using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem shotEffect; // effetto di sparo

    public void Shoot(WeaponSO weaponConf)
    {
        // eseguo l'effetto di sparo
        shotEffect.Play();

        RaycastHit hit; // raggio invisibile

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)) // lancio il raggio invisibile e controllo se ha colpito qualcosa
        {
            Instantiate(weaponConf.hitVFXPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            // Debug.Log(hit.collider.gameObject.name); // mostro a console il nome dell' oggettro colpito

            HealthManager healtManage = hit.collider.GetComponent<HealthManager>(); // ottengo l'HealtManager dell'oggetto colpito

            healtManage?.TakeDamage(weaponConf.Damage); // se l'oggetto ha lo script healt manager gli infliggo il danno

        }
    }
}
