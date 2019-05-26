using UnityEngine;

public class Trees : Plants {
	[Header("Trees")]
	public TreePlant treePrefab;


	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = Instantiate(treePrefab, position, rotation, parent);

		plant.name = "Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		PlantSystem system = new PlantSystem();

		system.setName("Tree");

		system.addVariable("T");
		system.addVariable("R");
		system.addVariable("K");
		system.addVariable("B");
		system.addVariable("L");
		system.addVariable("[");
		system.addVariable("]");

		system.setAxiom("T");

		system.setProduction("T", "TR[KTL]BR[KTL]BR[KTL]B");
		system.setProduction("B", "BTB");

		return system;
	}


	protected override PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution) {
		return systems;
	}
}
