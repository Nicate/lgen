using UnityEngine;

public class SimpleTrees : Trees {
	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = base.createPlant(position, rotation, parent);

		plant.name = "Simple Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		PlantSystem system = base.createSystem();
		
		system.setProduction("T", "TR[KTL]BR[KTL]BR[KTL]B");
		system.setProduction("B", "BTB");

		system.setName("Simple Tree");

		return system;
	}
}
