using System;
using UnityEngine;

namespace ProdiG.Classes {
	public class PlayerEvents {
		public delegate void AttackedDelegate();

		public delegate void DashingChangedDelegate(bool setBool, Vector2 setVector2);

		public delegate void DoubleJumpedDelegate();

		public delegate void GroundedChangedDelegate(bool setBool, float setFloat);

		public delegate void JumpedDelegate();

		public AttackedDelegate _attackedDelegate;
		public DashingChangedDelegate _dashingChangedDelegate;
		public DoubleJumpedDelegate _doubleJumpedDelegate;
		public GroundedChangedDelegate _groundedChangedDelegate;
		public JumpedDelegate _jumpedDelegate;

		public PlayerEvents() {
			_groundedChangedDelegate = InvokeGroundedChanged;
			_dashingChangedDelegate = InvokeDashingChanged;
			_jumpedDelegate = InvokeJumped;
			_doubleJumpedDelegate = InvokeDoubleJumped;
			_attackedDelegate = InvokeAttacked;
		}

		public event Action<bool, float> GroundedChanged;
		public event Action<bool, Vector2> DashingChanged;
		public event Action Jumped;
		public event Action DoubleJumped;
		public event Action Attacked;

		public void InvokeGroundedChanged(bool setBool, float setFloat) {
			GroundedChanged?.Invoke(setBool, setFloat);
		}

		public void InvokeDashingChanged(bool setBool, Vector2 setVector2) {
			DashingChanged?.Invoke(setBool, setVector2);
		}

		public void InvokeJumped() {
			Jumped?.Invoke();
		}

		public void InvokeDoubleJumped() {
			DoubleJumped?.Invoke();
		}

		public void InvokeAttacked() {
			Attacked?.Invoke();
		}
	}
}