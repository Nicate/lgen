using System.Collections.Generic;
using UnityEngine;

public class TreePlants : Plants {
	public TreePlant treePlantPrefab;

	public string production1 = "S";
	public string production2 = "B";


	protected override Plant growPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = createPlant(position, rotation, parent);
		
		updatePlant(plant);

		return plant;
	}
	
	private void updatePlant(Plant plant) {
		LSystem system = createSystem();

		Debug.Log(system.generateSequence(generations));

		plant.interpret(system);
	}


	private Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		return Instantiate(treePlantPrefab, position, rotation, parent);
	}

	private LSystem createSystem() {
		LSystem system = new LSystem();

		system.addVariable("S");
		system.addVariable("T");
		system.addVariable("K");
		system.addVariable("B");
		system.addVariable("L");
		system.addVariable("[");
		system.addVariable("]");

		system.setAxiom("S");

		system.setProduction("S", production1);
		system.setProduction("B", production2);

		return system;
	}


	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			foreach(Plant plant in plants) {
				updatePlant(plant);
			}
		}
	}
}
