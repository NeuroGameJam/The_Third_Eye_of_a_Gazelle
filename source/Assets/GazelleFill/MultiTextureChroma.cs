using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MultiTextureChroma : MonoBehaviour {
	public float invert;
	public Color colorA;
	public RenderTexture textureA;
	public Color colorB;
	public RenderTexture textureB;
	public Color colorC;
	public RenderTexture textureC;
	public Color colorIn;
	public Color colorOut;
	private Material mat;

	void Start () {
		mat = new Material (Shader.Find ("Chroma/Multicolor"));
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		//colorOut = new Color (0.5f+((Mathf.Cos(Time.time*0.5f)+1)*0.25f), 0.5f+((Mathf.Cos(Time.time*1f)+1)*0.25f), 0.5f+((Mathf.Cos(Time.time*1.5f)+1)*0.25f), 1f);
		colorOut = Color.white;
		mat.SetColor ("_ColorA", colorA);
		mat.SetColor ("_ColorB", colorB);
		mat.SetColor ("_ColorC", colorC);

		mat.SetColor ("_ColorLineIn", colorIn);
		mat.SetColor ("_ColorLineOut", colorOut);

		mat.SetTexture ("_TextureA", textureA);
		mat.SetTexture ("_TextureB", textureB);
		mat.SetTexture ("_TextureC", textureC);

		mat.SetFloat ("_Invert", invert);

		Graphics.Blit (source, destination, mat);		
	}
}
