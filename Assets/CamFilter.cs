using UnityEngine;
using System.Collections;

public class CamFilter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Texture2D LT2d = new Texture2D(src.width, src.height);
        RenderTexture.active = src;
        LT2d.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        LT2d.Apply();

        Color[] Lpix =LT2d.GetPixels(0, 0, LT2d.width, LT2d.height);
        for (int i = 0; i < Lpix.Length; i++)
        {
            
            Lpix[i].r = 0;
            
        }

        Texture2D Atext = new Texture2D(LT2d.width, LT2d.height);
        Atext.SetPixels(Lpix);
		Atext.Apply ();

		Graphics.Blit(Atext, dest);
    }
}
