using UnityEngine;

public class TreePlant : Plant {
	public Part basePrefab;
	public Part capPrefab;
	public Part budPrefab;
	public Part leafPrefab;


	private Part currentParentPart;


	protected override void interpret(string variable) {
		Debug.Log(variable);
	}
}
