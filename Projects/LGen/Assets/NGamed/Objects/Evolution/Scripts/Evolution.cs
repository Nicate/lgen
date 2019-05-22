using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Evolution : MonoBehaviour {
	[Header("Evolution")]
	public Plants plantsPrefab;

	[Space]
	public bool debugEvolution = false;


	private Plants plants;


	protected virtual void Awake() {
		plants = Instantiate(plantsPrefab, transform);
	}

	protected virtual void Start() {
		plants.growPlants();
	}
	
	protected virtual void Update() {
		if(debugEvolution) {
			if(Input.GetKeyDown(KeyCode.Return)) {
				evolvePlants(plants.getPlants());
			}
		}
	}


	protected virtual void evolvePlants(Plant[] plants) {
		LSystem[] systems = new LSystem[plants.Length];
		
		for(int index = 0; index < plants.Length; index += 1) {
			systems[index] = plants[index].getSystem();
		}

		systems = evolve(systems);

		for(int index = 0; index < plants.Length; index += 1) {
			Plant plant = plants[index];
			LSystem system = systems[index];

			// TODO What about updatePlant()?
			plant.interpret(system);
			plant.evaluate();
		}
	}

	protected abstract LSystem[] evolve(LSystem[] systems);
}
