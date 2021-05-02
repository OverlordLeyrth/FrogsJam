using UnityEngine;

public class Fading : MonoBehaviour
{
    private SpriteRenderer sr_;
    [HideInInspector] public float alpha;
    [HideInInspector] public bool startFade;
    public float delta;

    private void Awake()
    {
        sr_ = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color color = sr_.color;
        color.a = alpha;
        sr_.color = color;

        if (startFade)
        {
            alpha += delta;
        }
    }
}
