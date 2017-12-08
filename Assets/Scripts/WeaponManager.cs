using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

	[SerializeField]
	private string weaponLayerName = "Weapon";

	[SerializeField]
	private Transform weaponHolder;

	[SerializeField]
	private PlayerWeapon primaryWeapon;

	private PlayerWeapon currentWeapon;
	private WeaponGFX currentGFX;

	void Start () {
		EquipWeapon (primaryWeapon);
	}

	void EquipWeapon(PlayerWeapon _weapon) {
		currentWeapon = _weapon;

		GameObject _weaponIns = (GameObject)Instantiate (_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
		_weaponIns.transform.SetParent (weaponHolder);

		currentGFX = _weaponIns.GetComponent<WeaponGFX> ();
		if (currentGFX == null) {
			Debug.LogError ("No WeaponGFX component on the weapon object:" + _weaponIns.name);
		}

		if (isLocalPlayer) {
			Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
		}
	}

	public PlayerWeapon GetCurrentWeapon () {
		return currentWeapon;
	}

	public WeaponGFX GetCurrentGFX () {
		return currentGFX;
	}
}