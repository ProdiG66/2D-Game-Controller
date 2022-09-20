using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine.UI;

public class BoomEffect : MonoBehaviour {
	[SerializeField]
	private Material _ripplemat;

	[SerializeField]
	private Texture texture;

	private bool rippling;
	private RawImage ri;

	private Vector2 position;

	public Material ripplemat {
		get => _ripplemat;
		set => _ripplemat = value;
	}

	private void Start() {
		rippling = false;
		ri = GetComponent<RawImage>();
	}

	private void Update() {
		if (ripplemat.GetFloat("TimeStep") < 1 && rippling) {
			ri.material = ripplemat;
			ri.texture = texture;
			ri.color = Vector4.one;
			ripplemat.SetFloat("TimeStep",
				ripplemat.GetFloat("TimeStep") + ripplemat.GetFloat("Speed") * Time.deltaTime);
			ripplemat.SetVector("FocalPoint",
				new Vector4((position.x - transform.position.x) / 18 + 0.5f,
					(position.y - transform.position.y) / 10 + 0.5f, 0, 0));
		}
		else {
			ripplemat.SetFloat("TimeStep", 0);
			rippling = false;
			ripplemat.SetVector("FocalPoint", new Vector4(1, 1));
		}
	}


	public void SetFocus(Vector2 pos) {
		ripplemat.SetVector("FocalPoint",
			new Vector4((pos.x - transform.position.x) / 18 + 0.5f, (pos.y - transform.position.y) / 10 + 0.5f, 0, 0));
		position = pos;
	}

	public void Boom(Vector2 position, float speed = 1, float magnituted = -0.5f, float size = 0.11f) {
		rippling = true;
		SetFocus(position);
		ripplemat.SetFloat("Speed", speed);
		ripplemat.SetFloat("Magnification", magnituted);
		ripplemat.SetFloat("Size", size);
	}
#if UNITY_EDITOR
	[SerializeField]
	private Transform player;

	[SerializeField]
	private Vector3 offset;

	[Button("Boom")]
	public void Boom() {
		Boom(player.position + offset);
	}
#endif
}