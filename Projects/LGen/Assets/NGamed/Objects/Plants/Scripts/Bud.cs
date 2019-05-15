using UnityEngine;

public class Bud : Part {
	public float radius;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);
		
		transform.localScale = new Vector3(radius, radius, radius);

		return false;
	}
}
