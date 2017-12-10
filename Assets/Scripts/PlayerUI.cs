using UnityEngine;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	RectTransform thrusterFuelFill;

	private PlayerController controller;

	[SerializeField]
	GameObject pauseMenu;

	public void SetPlayerController(PlayerController _controller) {
		controller = _controller;
	}

	void Start()  {
		PauseMenu.isOn = false;
	}

	void SetFuelAmount(float _amount) {
		thrusterFuelFill.localScale = new Vector3 (1f, _amount, 1f);
	}

	void Update() {
		SetFuelAmount(controller.GetThrusterFuelAmount ());

		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePauseMenu ();
		}
	}

	void TogglePauseMenu(){
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.isOn = pauseMenu.activeSelf;
	}
}
