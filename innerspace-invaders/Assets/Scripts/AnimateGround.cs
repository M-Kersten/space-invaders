using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AnimateGround : MonoBehaviour
{
    private Renderer renderer;

    [SerializeField]
    private float speed;

    private float currentOffset;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        currentOffset += Time.deltaTime * speed;
        renderer.material.mainTextureOffset = new Vector2(0, currentOffset);
    }
}
