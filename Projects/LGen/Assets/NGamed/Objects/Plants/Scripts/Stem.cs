using UnityEngine;

public class Stem : Part {
	public Part basePart;
	public Part capPart;

	public float length = 1.0f;
	public float radius = 1.0f;

	public float attenuation = 1.0f;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		float stemLength = length;
		float stemRadius = radius * Mathf.Pow(attenuation, -depth * (branch + 1));

		transform.localPosition = new Vector3(0.0f, length, 0.0f);

		basePart.transform.localScale = new Vector3(stemRadius, stemLength, stemRadius);
		capPart.transform.localScale = new Vector3(stemRadius, stemRadius, stemRadius);

		return true;
	}
}
