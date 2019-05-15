using UnityEngine;

public class Leaf : Part {
	public float length;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);
		
		Vector3 orientation = transform.rotation * Vector3.up;
		Quaternion rotation = Quaternion.FromToRotation(orientation, Vector3.up);

		transform.rotation = rotation * transform.rotation;
		transform.localScale = new Vector3(length, length, length);

		return false;
	}
}
