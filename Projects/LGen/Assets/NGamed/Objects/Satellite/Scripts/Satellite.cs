using System.IO;
using UnityEngine;

public class Satellite : MonoBehaviour {
	[Header("Camera")]
	public Camera satelliteCamera;

	[Header("Shader")]
	public Shader replacementShader;
	public string replacementTag = "";


	private Texture2D texture;
	private Rect rectangle;

	private float responsive;
	private float unresponsive;
	private float background;


	private void Awake() {
		// Only render when we tell it to.
		satelliteCamera.enabled = false;

		int width = satelliteCamera.targetTexture.width;
		int height = satelliteCamera.targetTexture.height;

		texture = new Texture2D(width, height, TextureFormat.RGB24, false);
		rectangle = new Rect(0, 0, width, height);

		responsive = 0.0f;
		unresponsive = 0.0f;
		background = 1.0f;
	}


	public void scan() {
		satelliteCamera.RenderWithShader(replacementShader, replacementTag);

		RenderTexture.active = satelliteCamera.targetTexture;
		texture.ReadPixels(rectangle, 0, 0);
		RenderTexture.active = null;
	}

	public void evaluate() {
		int responsiveArea = 0;
		int unresponsiveArea = 0;
		int backgroundArea = 0;

		for(int y = (int) rectangle.yMin; y < (int) rectangle.yMax; y += 1) {
			for(int x = (int) rectangle.xMin; x < (int) rectangle.xMax; x += 1) {
				Color color = texture.GetPixel(x, y);

				// Specifically look for green, black, and white pixels. This is not the most
				// straightforward but allows us to visually inspect the render texture better.
				if(color.g > 0.5f && color.b < 0.5f) {
					responsiveArea += 1;
				}
				else if(color.r < 0.5f && color.g < 0.5f && color.b < 0.5f) {
					unresponsiveArea += 1;
				}
				else {
					backgroundArea += 1;
				}
			}
		}

		int totalArea = responsiveArea + unresponsiveArea + backgroundArea;

		responsive = responsiveArea / (float) totalArea;
		unresponsive = unresponsiveArea / (float) totalArea;
		background = backgroundArea / (float) totalArea;
	}


	public void export(string fileName) {
		byte[] bytes = texture.EncodeToPNG();

		File.WriteAllBytes(fileName, bytes);
	}


	public float getResponsive() {
		return responsive;
	}

	public float getUnresponsive() {
		return unresponsive;
	}

	public float getBackground() {
		return background;
	}
}
