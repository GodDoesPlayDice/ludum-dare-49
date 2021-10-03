using UnityEngine;

public class PlotDrawer : MonoBehaviour
{
    [SerializeField]
    private Texture2D texture;

    [SerializeField]
    private int sizeX = 150;

    [SerializeField]
    private int step = 5;

    [SerializeField]
    private float verticalStep = 100f;

    [SerializeField]
    private float verticalMargin = 5f;

    [SerializeField]
    private Color blankColor = new Color(1f, 1f, 1f, 1f);

    [SerializeField]
    private Color lineColor;

    [SerializeField]
    private PlotValuesHolder valuesHolder;

    private float plotXMultiplier;

    private void Start()
    {
        valuesHolder.SetValuesCount(sizeX / step);
        plotXMultiplier = texture.width / (float) sizeX;
    }

    public void AddAndDraw(ResourceChangedEP param)
    {
        valuesHolder.Add(param.value);
        RedrawTexture();
    }

    private void RedrawTexture()
    {
        var pixels = texture.GetPixels();

        SetPixelsToColor(pixels, blankColor);
        texture.SetPixels(pixels);

        var values = valuesHolder.GetValues();
        for (int i = 0; i < values.Count - 1; i ++)
        {

            Vector2 v1 = new Vector2(i * step, values[i]);
            Vector2 v2 = new Vector2((i + 1) * step, values[i + 1]);

            var plotYMinMax = CalcPlotYMinMax();

            DrawValueLinesMeasureLines(plotYMinMax);

            DrawLine(PlotPosToTexturePos(v1, plotYMinMax), PlotPosToTexturePos(v2, plotYMinMax), lineColor);
            //Debug.Log("DrawLine " + v1 + " " + v2);
        }
        texture.Apply();
    }

    public void SetPixelsToColor(Color[] pixels, Color color)
    {
        for (var i = 0; i < pixels.Length; ++i)
        {
            pixels[i] = color;
        }
    }

    public void DrawLine(Vector2 p1, Vector2 p2, Color col)
    {
        Debug.Log("DrawLine" + p1 + " " + p2);
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        float ctr = 0;

        while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y)
        {
            t = Vector2.Lerp(p1, p2, ctr);
            ctr += frac;
            texture.SetPixel((int)t.x, (int)t.y, col);
            
        }
    }

    private Vector2 PlotPosToTexturePos(Vector2 plotPos, Vector2 plotYMinMax)
    {
        Vector2 result = new Vector2(plotPos.x * plotXMultiplier, Remap(plotYMinMax.x, plotYMinMax.y, verticalMargin, texture.height - verticalMargin, plotPos.y));
        return result;
    }

    private Vector2 CalcPlotYMinMax()
    {
        var plotMin = Mathf.Floor(valuesHolder.GetMinValue() / verticalStep) * verticalStep;
        var plotMax = Mathf.Ceil(valuesHolder.GetMaxValue() / verticalStep) * verticalStep;
        return new Vector2(plotMin, plotMax);
    }

    private void DrawValueLinesMeasureLines(Vector2 plotYMinMax)
    {
        var count = (plotYMinMax.y - plotYMinMax.x) / verticalStep + 1;
        for (int i = 0; i < count; i ++)
        {
            for(int x = 0; x < texture.width; x ++)
            {
                var texturePos = PlotPosToTexturePos(new Vector2(x, plotYMinMax.x + i * verticalStep), plotYMinMax);
                texture.SetPixel((int)texturePos.x, (int)texturePos.y, Color.black);
            } 
        }
    }

    private float Remap(float x1, float x2, float y1, float y2, float value)
    {
        var normal = Mathf.InverseLerp(x1, x2, value);
        return Mathf.Lerp(y1, y2, normal);
    }
}
