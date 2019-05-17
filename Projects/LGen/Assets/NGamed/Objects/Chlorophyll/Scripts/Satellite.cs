using System.IO;
using UnityEngine;

public class Satellite : MonoBehaviour {
	public MaterialReplacer target;

	public string exportFileName = "";


	private Camera satelliteCamera;

	private Texture2D texture;
	private Rect rectangle;


	private void Awake() {
		satelliteCamera = GetComponent<Camera>();

		// Only render when we tell it to.
		satelliteCamera.enabled = false;
	}

	private void Start() {
		int width = satelliteCamera.targetTexture.width;
		int height = satelliteCamera.targetTexture.height;

		texture = new Texture2D(width, height, TextureFormat.RGB24, false);
		rectangle = new Rect(0, 0, width, height);
	}


	public void scan() {
		target.replace();
		satelliteCamera.Render();
		target.restore();

		RenderTexture.active = satelliteCamera.targetTexture;
		texture.ReadPixels(rectangle, 0, 0);
		RenderTexture.active = null;
	}

	public void evaluate() {
		int responsiveArea = 0;
		int unresponsiveArea = 0;

		for(int y = (int) rectangle.yMin; y < (int) rectangle.yMax; y += 1) {
			for(int x = (int) rectangle.xMin; x < (int) rectangle.xMax; x += 1) {
				Color color = texture.GetPixel(x, y);

				// Specifically looks for green pixels, this is not the most straightforward but allows us to visually inspect the render texture better.
				if(color.g > 0.5f && color.b < 0.5f) {
					responsiveArea += 1;
				}
				else {
					unresponsiveArea += 1;
				}
			}
		}

		int totalArea = responsiveArea + unresponsiveArea;

		float responsive = responsiveArea / (float) totalArea;
		float unresponsive = unresponsiveArea / (float) totalArea;

		Debug.LogFormat("Reponsive: {0}\nUnresponsive: {1}", responsive, unresponsive);
	}

	public void export() {
		if(exportFileName.Length > 0) {
			byte[] bytes = texture.EncodeToPNG();

			File.WriteAllBytes(exportFileName, bytes);
		}
	}


	private void Update() {
		if(Input.GetKeyDown(KeyCode.S)) {
			scan();
		}
		else if(Input.GetKeyDown(KeyCode.E)) {
			evaluate();
		}
		else if(Input.GetKeyDown(KeyCode.X)) {
			export();
		}
	}
}
