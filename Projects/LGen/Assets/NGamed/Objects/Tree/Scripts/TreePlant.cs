using UnityEngine;

// "Tree" already exists somewhere within Unity.
public class TreePlant : Plant {
	[Header("Interpretation")]
	public Limb trunkPrefab;
	public Limb ringPrefab;
	public Limb knotPrefab;
	public Limb budPrefab;
	public Limb leafPrefab;
	
	[Header("Evaluation")]
	public Satellite satellitePrefab;
	

	private Satellite satellite;


	protected override void Awake() {
		base.Awake();

		satellite = Instantiate(satellitePrefab, transform);
	}


	public Satellite getSatellite() {
		return satellite;
	}
	
	
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


	public override void evaluate() {
		satellite.scan();
		satellite.evaluate();

		TreeSystem system = getSystem() as TreeSystem;
		
		system.setResponsiveness(satellite.getResponsive());
		system.setUnresponsiveness(satellite.getUnresponsive());
		system.setEmptiness(satellite.getBackground());
	}
}
