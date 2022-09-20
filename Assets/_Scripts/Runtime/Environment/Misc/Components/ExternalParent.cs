using NaughtyAttributes;
using UnityEngine;

public class ExternalParent : MonoBehaviour {
	public bool position;
	public bool rotation;
	public bool scale;

	[SerializeField]
	private Transform parent;

	[SerializeField]
	[ShowIf("position")]
	private Vector3 posOffset;

	[SerializeField]
	[ShowIf("rotation")]
	private Vector3 rotOffset;

	[SerializeField]
	[ShowIf("scale")]
	private Vector3 scaleOffset;

	private void Update() {
		if (position) transform.position = parent.position + posOffset;
		if (rotation) transform.eulerAngles = parent.eulerAngles + rotOffset;
		if (scale) transform.localScale = parent.localScale + scaleOffset;
	}
}