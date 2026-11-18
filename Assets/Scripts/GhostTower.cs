using UnityEngine;

public class GhostTower : MonoBehaviour
{
    SpriteRenderer sr;
    SpriteRenderer rangeSr;
    static Sprite circleSprite;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0.4f;
        sr.color = c;

        GameObject go = new GameObject("RangeIndicator");
        go.transform.SetParent(transform, false);
        rangeSr = go.AddComponent<SpriteRenderer>();
        rangeSr.sprite = GetCircleSprite();
        rangeSr.sortingLayerID = sr.sortingLayerID;
        rangeSr.sortingOrder = sr.sortingOrder - 1;   // atrás da torre fantasma
    }

    // range = raio horizontal em unidades do mundo; yRatio = cellSize.y / cellSize.x
    public void ShowRange(float range, float yRatio)
    {
        Vector3 p = transform.lossyScale;
        rangeSr.transform.localScale = new Vector3(
            2f * range / p.x,
            2f * range * yRatio / p.y,
            1f);
    }

    public void SetValid(bool valid)
    {
        sr.color = valid ? new Color(0.85f, 1f, 0.85f, 0.55f) : new Color(1f, 0.75f, 0.72f, 0.55f);
        rangeSr.color = valid ? new Color(0.78f, 0.95f, 0.82f, 0.9f) : new Color(1f, 0.72f, 0.68f, 0.9f);
    }

    static Sprite GetCircleSprite()
    {
        if (circleSprite != null) return circleSprite;

        int size = 256;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        tex.wrapMode = TextureWrapMode.Clamp;
        Color[] px = new Color[size * size];
        float r = size / 2f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float d = Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), new Vector2(r, r)) / r;
                float edge = Mathf.Clamp01((1f - d) * r);                 // borda suave de 1 px
                float a = Mathf.Lerp(0.07f, 0.38f, Mathf.InverseLerp(0.965f, 0.99f, d)) * edge;
                px[y * size + x] = new Color(1, 1, 1, d <= 1f ? a : 0f);
            }
        }

        tex.SetPixels(px);
        tex.Apply();
        circleSprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        return circleSprite;
    }
}