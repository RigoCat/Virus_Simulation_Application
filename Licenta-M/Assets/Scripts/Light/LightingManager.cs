using UnityEngine;

namespace MF
{
	public class LightingManager : MonoBehaviour
	{
		[SerializeField] private Light DirectionalLight;
		[SerializeField] private LightingPreset Preset;
		private TimeManager timeManager;

		private void Awake()
		{
			timeManager = FindObjectOfType<TimeManager>();
		}

		private void Update()
		{
			if (Preset == null)
				return;

			var time = timeManager.TimeModel.Hours + timeManager.TimeModel.Minutes / 60f + timeManager.TimeModel.Seconds / 3600f;
			UpdateLighting(time / 24f);
		}

		private void UpdateLighting(float timePercent)
		{
			//Set ambient and fog
			RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
			RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

			//If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
			if (DirectionalLight != null)
			{
				DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

				DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 110, 170f, 0));
			}

		}

		//Try to find a directional light to use if we haven't set one
		private void OnValidate()
		{
			if (DirectionalLight != null)
				return;

			//Search for lighting tab sun
			if (RenderSettings.sun != null)
			{
				DirectionalLight = RenderSettings.sun;
			}
			//Search scene for light that fits criteria (directional)
			else
			{
				Light[] lights = GameObject.FindObjectsOfType<Light>();
				foreach (Light light in lights)
				{
					if (light.type == LightType.Directional)
					{
						DirectionalLight = light;
						return;
					}
				}
			}
		}
	}
}