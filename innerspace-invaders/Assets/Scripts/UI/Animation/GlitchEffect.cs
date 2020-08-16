using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
	public Texture2D displacementMap;
	public Shader Shader;

	[SerializeField] 
	private Player player;

	[SerializeField] 
	private EnemyGrid gridManager;
	
	[Header("Glitch Intensity")]
	[SerializeField]
	[Range(0, 1)]
	private float intensity;
	[SerializeField]
	[Range(0, 1)]
	private float flipIntensity;
	[SerializeField]
	[Range(0, 1)]
	private float colorIntensity;
	[SerializeField]
	private bool flickering;
	[SerializeField]
	private float flickerSpeed;
	[SerializeField]
	private float flickerDuration;
	[SerializeField]
	private Vector2 distortion;
	
	private Material material;
	private float flickerTimer;
	private float flickeringTimer;
	
	// cache the shader strings for efficiency
	private static readonly int FilterRadius = Shader.PropertyToID("filterRadius");
	private static readonly int Direction = Shader.PropertyToID("direction");
	private static readonly int FlipDown = Shader.PropertyToID("flip_down");
	private static readonly int FlipUp = Shader.PropertyToID("flip_up");
	private static readonly int Displace = Shader.PropertyToID("displace");

	void Start()
	{
		material = new Material(Shader);
		
		player.HealtChanged += FlickerScreen;
		gridManager.EnemyKilled += ChangeColor;
		
		material.SetFloat(FlipUp, 0);
		material.SetFloat(FlipDown, 1);
		material.SetFloat("scale", distortion.y);
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ColorIntensity", colorIntensity);
		material.SetTexture("_DispTex", displacementMap);
	}

	private void OnDestroy()
	{
		player.HealtChanged -= FlickerScreen;
		gridManager.EnemyKilled -= ChangeColor;
	}

	private void ChangeColor()
	{
		material.SetFloat(FilterRadius, Random.Range(-3f, 3f) * colorIntensity);
		material.SetVector(Direction, Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
	}

	private void FlickerScreen(int amount)
	{
		flickering = true;
		AudioManager.Instance.PlayClip(1);
	}

	private void Update()
	{
		if (!flickering)
			return;

		flickeringTimer += Time.deltaTime;
		if (flickeringTimer > flickerSpeed)
		{
			material.SetFloat(FlipDown, 1 - Random.Range(0, 1f) * flipIntensity);
			material.SetFloat(FlipUp, Random.Range(0, 1f) * flipIntensity);
			material.SetFloat(Displace, Random.Range(0, distortion.x));
			flickeringTimer = 0;
		}

		flickerTimer += Time.deltaTime;
		if (flickerTimer > flickerDuration)
		{
			material.SetFloat(FlipUp, 0);
			material.SetFloat(FlipDown, 1);
			material.SetFloat(Displace, 0);
			flickering = false;
			flickerTimer = 0;
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}
}
