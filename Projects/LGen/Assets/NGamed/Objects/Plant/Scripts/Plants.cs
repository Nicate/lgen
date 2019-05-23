using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Plants : MonoBehaviour, IEnumerable<Plant> {
	[Header("Forest")]
	public int width = 1;
	public int depth = 1;
	
	[Space]
	public float spacing = 1.0f;

	[Header("L-System")]
	public int generations = 1;

	[Space]
	public bool debugSystem = false;

	public string debugAxiom = "";
	public List<DebugProduction> debugProductions = new List<DebugProduction>();
	
	[Serializable]
	public struct DebugProduction {
		public string variable;
		public string production;
	}

	[Header("Fitness")]
	public bool debugSatellite = false;
	public string debugExportDirectory = "";


	protected List<Plant> plants;


	protected virtual void Awake() {
		plants = new List<Plant>(width * depth);
	}

	protected virtual void Start() {
		if(debugSystem) {
			growPlants();
		}
	}

	protected virtual void Update() {
		if(debugSystem) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				updatePlants();
			}
		}

		if(debugSatellite) {
			if(Input.GetKeyDown(KeyCode.Z)) {
				foreach(Plant plant in plants) {
					Debug.LogFormat("Plant: {0}\nReponsiveness: {1}\nUnresponsiveness: {2}\nEmptiness: {3}", plant.name, plant.getResponsiveness(), plant.getUnresponsiveness(), plant.getEmptiness());
				}
			}

			if(Input.GetKeyDown(KeyCode.X)) {
				foreach(Plant plant in plants) {
					plant.getSatellite().export(Path.Combine(debugExportDirectory, plant.name + ".png"));
				}
			}
		}
	}


	public int getNumberOfPlants() {
		return plants.Count;
	}

	public Plant[] getPlants() {
		return plants.ToArray();
	}

	public IEnumerator<Plant> GetEnumerator() {
		foreach(Plant plant in plants) {
			yield return plant;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	public Plant this[int key] {
		get {
			return plants[key];
		}
	}


	public void growPlants() {
		float startX = -0.5f * spacing * (width - 1);
		float startY = 0.0f;
		float startZ = -0.5f * spacing * (depth - 1);
		
		for(int v = 0; v < width; v += 1) {
			for(int u = 0; u < depth; u += 1) {
				float x = startX + u * spacing;
				float y = startY;
				float z = startZ + v * spacing;

				Vector3 position = new Vector3(x, y, z);
				Quaternion rotation = Quaternion.identity;

				Plant plant = growPlant(position, rotation, transform);
				
				plant.name += " (" + u + ", " + v + ")";

				plants.Add(plant);
			}
		}
	}

	private Plant growPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = createPlant(position, rotation, parent);
		
		updatePlant(plant);

		return plant;
	}


	public void updatePlants() {
		foreach(Plant plant in plants) {
			updatePlant(plant);
		}
	}
	
	private void updatePlant(Plant plant) {
		LSystem system = createSystem();

		if(debugSystem) {
			system.setAxiom(debugAxiom);

			foreach(DebugProduction debugProduction in debugProductions) {
				system.setProduction(debugProduction.variable, debugProduction.production);
			}
		}

		system.generateSequence(generations);

		if(debugSystem) {
			Debug.Log(system);
		}

		plant.interpret(system);

		plant.evaluate();
	}


	protected abstract Plant createPlant(Vector3 position, Quaternion rotation, Transform parent);
	protected abstract LSystem createSystem();
}
