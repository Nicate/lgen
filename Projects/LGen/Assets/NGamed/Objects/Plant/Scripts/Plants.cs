using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Plants : MonoBehaviour {
	[Header("Plants")]
	public int width = 1;
	public int depth = 1;
	
	[Space]
	public float spacing = 1.0f;

	[Header("L-Systems")]
	public int generations = 1;

	[Header("Evolution")]
	public int evolutions = 0;

	[Header("Debugging")]
	public bool debug = false;

	[Space]
	public string exportDirectory = "";

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
	private List<LSystem> systems;

	private int evolution;


	protected virtual void Awake() {
		plants = new List<Plant>(width * depth);
		systems = new List<LSystem>(width * depth);

		evolution = 0;
	}

	protected virtual void Start() {
		grow();
		evolve();
	}

	protected virtual void Update() {
		if(debug) {
			if(Input.GetKeyDown(KeyCode.P)) {
				foreach(Plant plant in plants) {
					Debug.Log(plant);
				}
			}

			if(Input.GetKeyDown(KeyCode.X)) {
				foreach(Plant plant in plants) {
					plant.getSatellite().export(Path.Combine(exportDirectory, plant.name + ".png"));
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Space)) {
				updatePlants();
			}
			
			if(Input.GetKeyDown(KeyCode.Return)) {
				evolvePlants();
			}
		}
	}


	private void grow() {
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

				Plant plant = createPlant(position, rotation, transform);
				plant.name += " (" + u + ", " + v + ")";
				plants.Add(plant);

				LSystem system = createSystem();
				system.setName(system.getName() + " (" + u + ", " + v + ")");
				systems.Add(system);

				updatePlant(plant, system);
			}
		}
	}

	protected abstract Plant createPlant(Vector3 position, Quaternion rotation, Transform parent);
	protected abstract LSystem createSystem();


	public void updatePlants() {
		for(int index = 0; index < plants.Count; index += 1) {
			Plant plant = plants[index];
			LSystem system = systems[index];

			updatePlant(plant, system);
		}
	}
	
	private void updatePlant(Plant plant, LSystem system) {
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

		LSystem[] originalSystems = new LSystem[systems.Count];

		for(int index = 0; index < systems.Count; index += 1) {
			originalSystems[index] = systems[index].copy();
		}
		
		LSystem[] evolvedSystems = evolveSystems(originalSystems, plants.ToArray(), evolution);

		systems.Clear();
		systems.AddRange(evolvedSystems);

		updatePlants();
	}

	protected abstract LSystem[] evolveSystems(LSystem[] systems, Plant[] plants, int evolution);
}
