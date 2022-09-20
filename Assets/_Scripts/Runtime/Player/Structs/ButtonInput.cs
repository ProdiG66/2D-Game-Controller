using System;
using UnityEngine;

namespace ProdiG.Structs {
	//Prototype Input
	[Serializable]
	public struct ButtonInput {
		public bool nonAnalog;

		[SerializeField]
		private float _actuation;

		public bool isPressed => actuation > 0;

		public float actuation {
			get => _actuation;
			set => _actuation = value;
		}
	}
}