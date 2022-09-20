using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProdiG.Classes {
	[Serializable]
	public class PlayerInputs {
		public Vector2 move;
		public bool jumpDown;
		public bool jumpHeld;
		public bool dashDown;
		public bool attackDown;

		[HideInInspector]
		private InputAction _attack;

		[HideInInspector]
		private InputAction _dash;

		[HideInInspector]
		private InputAction _jump;

		[HideInInspector]
		private InputAction _move;

		private PlayerControls _playerControls;

		public void Awake() {
			_playerControls = new PlayerControls();
			_move = _playerControls.Gameplay.Move;
			_jump = _playerControls.Gameplay.Jump;
			_dash = _playerControls.Gameplay.Dash;
			_attack = _playerControls.Gameplay.Attack;
		}

		public void OnEnable() {
			_playerControls.Enable();
		}

		public void OnDisable() {
			_playerControls.Disable();
		}

		public void Update() {
			jumpDown = _jump.WasPressedThisFrame();
			jumpHeld = _jump.IsPressed();
			dashDown = _dash.WasPressedThisFrame();
			attackDown = _attack.WasPressedThisFrame();
			move = _move.ReadValue<Vector2>();
		}
	}
}