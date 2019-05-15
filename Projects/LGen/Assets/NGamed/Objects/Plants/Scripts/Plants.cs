using System.Collections.Generic;
using UnityEngine;

public abstract class Plants : MonoBehaviour {
	public int width = 1;
	public int depth = 1;

	public float space = 1.0f;

	public int generations = 1;


	protected List<Plant> plants;


	private void Awake() {
		plants = new List<Plant>(width * depth);
	}


	protected virtual void Start() {
		float startX = -0.5f * space * (width - 1);
		float startY = 0.0f;
		float startZ = -0.5f * space * (depth - 1);
		
		for(int v = 0; v < width; v += 1) {
			for(int u = 0; u < depth; u += 1) {
				float x = startX + u * space;
				float y = startY;
				float z = startZ + v * space;

				Vector3 position = new Vector3(x, y, z);
				Quaternion rotation = Quaternion.identity;

				Plant plant = growPlant(position, rotation, transform);
				
				plant.name = "Plant (" + u + ", " + v + ")";

				plants.Add(plant);
			}
		}
	}


	protected abstract Plant growPlant(Vector3 position, Quaternion rotation, Transform parent);
}
