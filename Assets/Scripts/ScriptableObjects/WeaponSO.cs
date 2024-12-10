using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]

public class WeaponSO : ScriptableObject
{
    public int Damage = 1; // danno per colpo
    public float FireRate = 0.5f;
    public GameObject hitVFXPrefab; // effetto collisione colpo


}
