using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QrCodeReader : MonoBehaviour
{
    public Camera cam;
    private BarcodeReader barCodeReader;

    FrameCapturer m_pixelCapturer;
    Color32[] framebuffer;
    public Text text;
    public Text textFrameCount;

    // Use this for initialization
    void Start()
    {
        barCodeReader = new BarcodeReader();
        Resolution currentResolution = Screen.currentResolution;
        m_pixelCapturer = cam.GetComponent<FrameCapturer>();
    }

    void Update()
    {

        Resolution currentResolution = Screen.currentResolution;
        //Debug.LogWarning("Update");
        try
        {
            //framebuffer = m_pixelCapturer.m_lastCapturedColors;
            //var frames = m_pixelCapturer.frames;

            if (m_pixelCapturer.frames == null)
            {
                //Debug.LogWarning("return");
                return;
            }
            //Debug.Log("0");
            if (m_pixelCapturer.frames.Count == 0)
                return;
            //Debug.Log("1");
            //if (framebuffer.Length == 0)
            //{
            //    //Debug.LogWarning("return");
            //    return;
            //}
            textFrameCount.text = m_pixelCapturer.frames.Count.ToString();
            var frame = m_pixelCapturer.frames.Dequeue();
            //var data = barCodeReader.Decode(frame, currentResolution.width, currentResolution.height);
            var height = m_pixelCapturer.m_Height;
            var width = m_pixelCapturer.m_Width;
            var data = barCodeReader.Decode(frame, width, height);
            if (data != null)
            {
                // QRCode detected.
                Debug.Log(data);
                Debug.Log("QR: " + data.Text);
                text.text = data.Text;

                //OnQrCodeRead(new QrCodeReadEventArgs() { text = data.Text });
            }
            

            //var data = barCodeReader.Decode(framebuffer, currentResolution.width, currentResolution.height);
            //if (data != null)
            //{
            //    // QRCode detected.
            //    Debug.Log(data);
            //    Debug.Log("QR: " + data.Text);

            //    //OnQrCodeRead(new QrCodeReadEventArgs() { text = data.Text });
            //}
            //Debug.Log("end decode");

        }
        catch (Exception e)
        {
            Debug.LogError("Error reading QR");
            Debug.LogError(e.Message);
        }

        // skip 1 frame each time 
        // solves GetPixels() blocks for ReadPixels() to complete
        // https://medium.com/google-developers/real-time-image-capture-in-unity-458de1364a4c
        m_pixelCapturer.m_shouldCaptureOnNextFrame = true;

    }
}