using UnityEngine;

// "Tree" already exists somewhere within Unity.
public class TreePlant : Plant {
	[Header("Tree")]
	public Limb trunkPrefab;
	public Limb ringPrefab;
	public Limb knotPrefab;
	public Limb budPrefab;
	public Limb leafPrefab;


	protected override void interpret(string variable) {
		if(variable == "T") {
			growLimb(trunkPrefab);
		}
		else if(variable == "R") {
			growLimb(ringPrefab);
		}
		else if(variable == "K") {
			growLimb(knotPrefab);
		}
		else if(variable == "B") {
			growLimb(budPrefab);
		}
		else if(variable == "L") {
			growLimb(leafPrefab);
		}
		else if(variable == "[") {
			startBranch();
		}
		else if(variable == "]") {
			stopBranch();
		}
	}
}
