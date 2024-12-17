using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]

public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab; // riferimento al prefab dell'arma

    public bool isAutomatic=false; // indica se l'arma è automatica
    public int Damage = 1; // danno per colpo
    public float FireRate = 0.5f;
    public GameObject hitVFXPrefab; // effetto collisione colpo


}
