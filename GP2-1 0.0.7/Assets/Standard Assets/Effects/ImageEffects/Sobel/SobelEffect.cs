using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/GlitchEffect")]
	public class SobelEffect : ImageEffectBase
	{

		// Called by camera to apply image effect
		void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, material);
		}
	}
}