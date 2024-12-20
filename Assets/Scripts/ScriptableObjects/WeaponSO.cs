using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]

public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab; // riferimento al prefab dell'arma

    public int MagazineSize = 10; // quantit� di munizioni
    public bool canZoom=false; // flag che indica se l'arma pu� mirare
    public float ZoomFOV; // fov dell'eventuale zoom
    public float ZoomRotationSpeed = 0.3f; // sensibilit� visuale mestre si usa lo zoom

    public bool isAutomatic=false; // indica se l'arma � automatica
    public int Damage = 1; // danno per colpo
    public float FireRate = 0.5f;
    public GameObject hitVFXPrefab; // effetto collisione colpo


}
