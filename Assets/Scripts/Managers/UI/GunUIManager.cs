using FeTo.SOArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunUIManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] Image gunIcon;
    [SerializeField] TextMeshProUGUI ammoText;

    [Header("Variables")]
    [SerializeField] SOVariable newGunSo;
    [SerializeField] IntVariable currentAmmo;

    [Header("Default values")]
    [SerializeField] GunSO defaultGun;

    private readonly string infinityStr = "Inf";

    private GunSO currentWeapon;

    public void GunPicked()
    {
        currentWeapon = (GunSO)newGunSo.GetValue();
        UpdateGunUI();
    }

    public void UpdateGunUI()
    {
        gunIcon.sprite = currentWeapon.gunPickUpSprite;
        ammoText.SetText(currentWeapon.isDefault ? infinityStr : currentWeapon.ammunition.ToString());
    }

    public void UpdateAmmo() => ammoText.SetText(currentAmmo.GetValue().ToString());

    public void ResetGun()
    {
        currentWeapon = defaultGun;
        UpdateGunUI();
    }
}
