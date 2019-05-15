﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plants : MonoBehaviour {
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


	protected List<Plant> plants;


	protected virtual void Awake() {
		plants = new List<Plant>(width * depth);
	}
	
	protected virtual void Start() {
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
	
	protected virtual void Update() {
		if(debugSystem) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				foreach(Plant plant in plants) {
					updatePlant(plant);
				}
			}
		}
	}


	private Plant growPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = createPlant(position, rotation, parent);
		
		updatePlant(plant);

		return plant;
	}
	
	private void updatePlant(Plant plant) {
		LSystem system = createSystem();

		if(debugSystem) {
			system.setAxiom(debugAxiom);

			foreach(DebugProduction debugProduction in debugProductions) {
				system.setProduction(debugProduction.variable, debugProduction.production);
			}
		}

		string sequence = system.generateSequence(generations);

		if(debugSystem) {
			Debug.Log(sequence);
		}

		plant.interpret(system);
	}


	protected abstract Plant createPlant(Vector3 position, Quaternion rotation, Transform parent);
	protected abstract LSystem createSystem();
}
