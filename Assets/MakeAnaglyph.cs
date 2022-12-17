using UnityEngine;
using System.Collections;

public class MakeAnaglyph : MonoBehaviour {

	public Camera cameraL;
	public Camera cameraR;
	RenderTexture LT;
	RenderTexture RT;
	Texture2D LT2d, RT2d;
	Color[] Apix;
	Texture2D Atext;

	// Use this for initialization
	void Start () {
		
		LT = new RenderTexture(1024, 512, 16, RenderTextureFormat.ARGB32);
		RT = new RenderTexture(1024, 512, 16, RenderTextureFormat.ARGB32);
		cameraR.targetTexture = RT;
		cameraL.targetTexture = LT;

		LT2d = new Texture2D(cameraL.targetTexture.width, cameraL.targetTexture.height);
		RT2d = new Texture2D(cameraL.targetTexture.width, cameraL.targetTexture.height);
		Atext = new Texture2D(RT.width, RT.height);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{



		if (cameraL.targetTexture.IsCreated() && cameraR.targetTexture.IsCreated())
		{
			
			RenderTexture.active = cameraL.targetTexture;
			LT2d.ReadPixels(new Rect(0, 0, cameraL.targetTexture.width, cameraL.targetTexture.height), 0, 0);
			LT2d.Apply();

			RenderTexture.active = cameraR.targetTexture;
			RT2d.ReadPixels(new Rect(0, 0, cameraR.targetTexture.width, cameraR.targetTexture.height), 0, 0);
			RT2d.Apply();

			RenderTexture.active = null;

			Color[] Rpix = RT2d.GetPixels(0, 0, RT2d.width, RT2d.height);
			Color[] Lpix = LT2d.GetPixels(0, 0, LT2d.width, LT2d.height);

			if (Apix == null)
			{
				Apix = new Color[Rpix.Length];
			}

			for (int i = 0; i < Rpix.Length; i++)
			{
				//Apix [i].a = Rpix [i].a;
				Apix[i].r = 0.299f * Lpix[i].r + 0.587f * Lpix[i].g + 0.114f * Lpix[i].b;
				Apix[i].g = Rpix[i].g;
				Apix[i].b = Rpix[i].b;
			}

			if (Atext != null) {
				Atext.SetPixels (Apix);
				Atext.Apply ();

				Graphics.Blit (Atext, dest);
			} else Graphics.Blit (src, dest);
			//Display.GetComponent<Renderer> ().material.mainTexture = Anaglyph;
		}

	}
}
