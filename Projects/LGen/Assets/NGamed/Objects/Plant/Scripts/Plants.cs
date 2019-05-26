using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plants : MonoBehaviour {
	[Header("Plants")]
	public int width = 1;
	public int depth = 1;
	
	[Space]
	public float spacing = 1.0f;

	[Header("Systems")]
	public int generations = 1;

	[Header("Evolution")]
	public int evolutions = 0;

	[Header("Debugging")]
	public bool debug = false;

	[Space]
	public KeyCode logKey = KeyCode.L;
	public KeyCode updateKey = KeyCode.Space;
	public KeyCode evolveKey = KeyCode.Return;

	[Space]
	public bool overrideSystems = false;

	public string overrideAxiom = "";
	public List<OverrideProduction> overrideProductions = new List<OverrideProduction>();
	
	[Serializable]
	public struct OverrideProduction {
		public string variable;
		public string production;
	}

	
	private List<Plant> plants;
	private List<PlantSystem> systems;

	private int evolution;


	protected virtual void Awake() {
		plants = new List<Plant>(width * depth);
		systems = new List<PlantSystem>(width * depth);

		evolution = -1;
	}

	protected virtual void Start() {
		grow();
		evolve();
	}

	protected virtual void Update() {
		if(debug) {
			if(Input.GetKeyDown(logKey)) {
				foreach(PlantSystem system in systems) {
					Debug.Log(system);
				}
			}
			
			if(Input.GetKeyDown(updateKey)) {
				updatePlants();
			}
			
			if(Input.GetKeyDown(evolveKey)) {
				evolvePlants();
			}
		}
	}


	protected Plant[] getPlants() {
		return plants.ToArray();
	}
	
	protected PlantSystem[] getSystems() {
		return systems.ToArray();
	}


	private void grow() {
		evolution = 0;

		float startX = transform.position.x - 0.5f * spacing * (width - 1);
		float startY = transform.position.y;
		float startZ = transform.position.z - 0.5f * spacing * (depth - 1);
		
		for(int v = 0; v < depth; v += 1) {
			for(int u = 0; u < width; u += 1) {
				float x = startX + u * spacing;
				float y = startY;
				float z = startZ + v * spacing;

				Vector3 position = new Vector3(x, y, z);
				Quaternion rotation = Quaternion.identity;

				Plant plant = createPlant(position, rotation, transform);
				plant.name += " (" + u + ", " + v + ")";
				plants.Add(plant);

				PlantSystem system = createSystem();
				system.setName(system.getName() + " (" + u + ", " + v + ")");
				systems.Add(system);

				updatePlant(plant, system);
			}
		}
	}

	protected abstract Plant createPlant(Vector3 position, Quaternion rotation, Transform parent);
	protected abstract PlantSystem createSystem();


	public void updatePlants() {
		for(int index = 0; index < plants.Count; index += 1) {
			Plant plant = plants[index];
			PlantSystem system = systems[index];

			updatePlant(plant, system);
		}
	}
	
	private void updatePlant(Plant plant, PlantSystem system) {
		if(overrideSystems) {
			system = system.copy();

			system.setAxiom(overrideAxiom);

			foreach(OverrideProduction debugProduction in overrideProductions) {
				system.setProduction(debugProduction.variable, debugProduction.production);
			}
		}

		system.generateSequence(generations);

		if(overrideSystems) {
			Debug.Log(system);
		}

		plant.interpret(system);
		plant.evaluate();
	}


	private void evolve() {
		for(int generation = 0; generation < evolutions; generation += 1) {
			evolvePlants();
		}
	}
	
	public void evolvePlants() {
		evolution += 1;

		PlantSystem[] originalSystems = new PlantSystem[systems.Count];

		for(int index = 0; index < systems.Count; index += 1) {
			originalSystems[index] = systems[index].copy();
		}
		
		PlantSystem[] evolvedSystems = evolveSystems(originalSystems, evolution);

		systems.Clear();
		systems.AddRange(evolvedSystems);

		updatePlants();
	}

	protected abstract PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution);
}
