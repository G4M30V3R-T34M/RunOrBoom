using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] SOVariable newGunVariable;
    [SerializeField] GunSO defaultGunSO;

    private Gun playerGun;

    private void Awake() => playerGun = GetComponent<Gun>();

    public void ChangeWeapon() => playerGun.ChangeGun((GunSO)newGunVariable.GetValue());

    public void ResetWeapon() => playerGun.ChangeGun(defaultGunSO);

}
