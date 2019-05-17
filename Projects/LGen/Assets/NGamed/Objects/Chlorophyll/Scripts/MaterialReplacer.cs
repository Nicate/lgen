using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReplacer : MonoBehaviour {
	public ReplacementMaterial[] replacementMaterials;

	[Serializable]
	public struct ReplacementMaterial {
		public Material original;
		public Material replacement;
	}


	private Dictionary<Material, Material> originalMaterialsMap;
	private Dictionary<Material, Material> replacementMaterialsMap;


	private void Awake() {
		originalMaterialsMap = new Dictionary<Material, Material>(replacementMaterials.Length);
		replacementMaterialsMap = new Dictionary<Material, Material>(replacementMaterials.Length);

		foreach(ReplacementMaterial replacementMaterial in replacementMaterials) {
			originalMaterialsMap.Add(replacementMaterial.replacement, replacementMaterial.original);
			replacementMaterialsMap.Add(replacementMaterial.original, replacementMaterial.replacement);
		}
	}


	public void replace() {
		foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>()) {
			if(replacementMaterialsMap.ContainsKey(renderer.sharedMaterial)) {
				renderer.sharedMaterial = replacementMaterialsMap[renderer.sharedMaterial];
			}
		}
	}

	public void restore() {
		foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>()) {
			if(originalMaterialsMap.ContainsKey(renderer.sharedMaterial)) {
				renderer.sharedMaterial = originalMaterialsMap[renderer.sharedMaterial];
			}
		}
	}
}
