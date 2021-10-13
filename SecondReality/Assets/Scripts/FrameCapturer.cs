using Moments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCapturer : MonoBehaviour
{
    //test ---------------
    public RawImage rawImage;
    public Text textInfo;
    public Text textInfo2;
    //------------------

    // Script Inputs
    public bool m_shouldCaptureOnNextFrame = false;
    public Color32[] m_lastCapturedColors;


    void Start()
    {
        m_Frames = new Queue<RenderTexture>();
        Init();
        State = RecorderState.Recording;
        StartCoroutine(PreProcess());
    }

    void OnPostRender()
    {

        //if (m_shouldCaptureOnNextFrame && Time.time > _time + 1f)
        //{
        //    Debug.LogWarning("capture");
        //    Resolution res = Screen.currentResolution;
        //    m_lastCapturedColors = GetRenderedColors();
        //    m_shouldCaptureOnNextFrame = false;
        //    _time = Time.time;
        //}



        //if (Time.time > _time + 2f)
        //{
        //    UnityEngine.Rendering.AsyncGPUReadback.Request(temp, (req) =>
        //    {

        //        m_centerPixTex.LoadRawTextureData(req.GetData<uint>());
        //        m_centerPixTex.Apply();

        //        m_lastCapturedColors = m_centerPixTex.GetPixels32();

        //        _time = Time.time;
        //        //System.IO.File.WriteAllBytes(path, ImageConversion.EncodeToPNG(newTex));
        //        //Debug.Log("ok");
        //    });
        //}

    }


    public enum RecorderState
    {
        Recording,
        Paused,
        PreProcessing
    }
    public RecorderState State { get; private set; }
    int m_MaxFrameCount = 20;
    float m_Time;
    float m_TimePerFrame;
    Queue<RenderTexture> m_Frames;
    RenderTexture m_RecycledRenderTexture;
    ReflectionUtils<Recorder> m_ReflectionUtils;

    //[SerializeField, Moments.Min(8)]
    public int m_Width = 200;

   // [SerializeField, Moments.Min(8)]
    public int m_Height = 200;

    //[SerializeField, Moments.Min(0.1f)]
    float m_BufferSize = 1f;

    //[SerializeField, Range(1, 30)]
    int m_FramePerSecond = 5;

    [SerializeField]
    bool m_AutoAspect = false;

    public Queue<Color32[]> frames;

    RenderTexture rt;

    Texture2D texture2DCroped;

    Color32[] frame;

    Color32[] pixels;

    [SerializeField]
    private Image _frameViewer;

    void Init()
    {
        m_Width = Screen.width/3;
        m_Height = m_Width;
        _frameViewer.GetComponent<RectTransform>().sizeDelta = new Vector2(m_Width-100, m_Height-100);


        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(m_Width, m_Height);

        State = RecorderState.Paused;
        ComputeHeight();
        m_MaxFrameCount = Mathf.RoundToInt(m_BufferSize * m_FramePerSecond);
        m_TimePerFrame = 1f / m_FramePerSecond;
        m_Time = 0f;

        texture2DCroped = new Texture2D(m_Width, m_Height);
        Debug.Log("m_MaxFrameCount " + m_MaxFrameCount);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (State != RecorderState.Recording)
        {
            Graphics.Blit(source, destination);
            return;
        }

        m_Time += Time.unscaledDeltaTime;

        if (m_Time >= m_TimePerFrame)
        {
            // Limit the amount of frames stored in memory
            if (m_Frames.Count >= m_MaxFrameCount)
            {
                //Debug.Log("m_Frames.Dequeue();");
                m_RecycledRenderTexture = m_Frames.Dequeue();
                m_RecycledRenderTexture.Release();
            }


            textInfo2.text = m_Frames.Count.ToString();

            m_Time -= m_TimePerFrame;

            // Frame data
            rt = m_RecycledRenderTexture;
            m_RecycledRenderTexture = null;

            if (rt == null)
            {
                //rt = new RenderTexture(m_Width, m_Height, 0, RenderTextureFormat.ARGB4444);
                rt = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB4444);
                rt.wrapMode = TextureWrapMode.Clamp;
                rt.filterMode = FilterMode.Point;
                rt.anisoLevel = 0;

            }

            Graphics.Blit(source, rt);
            //Graphics.CopyTexture(source, 0, 0, 0, 0, m_Width, m_Height, rt, 0, 0, 0, 0);


            m_Frames.Enqueue(rt);
            //Debug.LogWarning("add frame");
        }

        Graphics.Blit(source, destination);
    }


    IEnumerator PreProcess()
    {
        frames = new Queue<Color32[]>();

        // Get a temporary texture to read RenderTexture data
        Texture2D temp = new Texture2D(m_Width, m_Height, TextureFormat.ARGB4444, false);
        temp.hideFlags = HideFlags.HideAndDontSave;
        temp.wrapMode = TextureWrapMode.Clamp;
        temp.filterMode = FilterMode.Bilinear;
        temp.anisoLevel = 0;

        // Process the frame queue
        while (State == RecorderState.Recording)
        {
            if (m_Frames.Count > 0)
            {
                if (frames.Count >= m_MaxFrameCount)
                {
                    frames.Dequeue();
                }
                var realesedFrame = m_Frames.Dequeue();
                frame = ToColorFrame(realesedFrame, temp);
                realesedFrame.Release();
                frames.Enqueue(frame);

                //Debug.LogWarning("frame added");
            }
            yield return null;
        }
    }

    Color32[] ToColorFrame(RenderTexture source, Texture2D target)
    {
        RenderTexture.active = source;
        //target.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        target.ReadPixels(new Rect(Screen.width/2 - m_Width/2, Screen.height / 2 - m_Height / 2, m_Width, m_Height), 0, 0);
        Debug.Log((Screen.width / 2 - m_Width / 2).ToString() + "x" + (Screen.height / 2 - m_Height / 2).ToString()+"("+m_Width+","+m_Height+")");
        target.Apply();

        //pixels = target.GetPixels(0, 0, m_Width, m_Height, 0);
        pixels = target.GetPixels32();


        texture2DCroped.SetPixels32(pixels);
        texture2DCroped.Apply();

        textInfo.text = target.width + " " + target.height;
        //rawImage.texture = target;
        rawImage.texture = texture2DCroped;

        RenderTexture.active = null;

        //return target.GetPixels32();
        return pixels;

        //return target.GetPixels(0, 0, m_Width, m_Height).se;   
    }

    public void ComputeHeight()
    {
        if (!m_AutoAspect)
            return;

        m_Height = Mathf.RoundToInt(m_Width / GetComponent<Camera>().aspect);
    }

    private Color32[] GetPixels32(ref Texture2D source, int x, int y, int blockWidth, int blockHeight)
    {
        return source.GetPixels32();
    }
}