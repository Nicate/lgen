using UnityEngine;

public class Knot : Limb {
	public float bend = 0.0f;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		transform.localRotation = Quaternion.Euler(bend, 0.0f, 0.0f);

		return true;
	}
}
