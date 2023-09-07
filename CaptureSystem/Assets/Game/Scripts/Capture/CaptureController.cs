// ----- C#
using System.IO;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUI;

namespace InGame.ForCapture
{
    public class CaptureController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Camera _captureCamera = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Camera
        private Camera      _captureRnderCamera = null;
        
        // ----- UI
        private CaptureView _captureView        = null;

        // ----- Capture Info
        private string      _capturePicturePath = null;
        private string      _direction          = null;
        private Texture2D   _capturePicture     = null;
        private int         _widthResolution    = 256;
        private int         _heightResolution   = 256;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void SetCaptureView(CaptureView captureView)
        {
            if (_captureView != null) return;

            _captureView = captureView;
        }

        public void SetCaptureCamera(Camera captureCamera) 
        {
            if (_captureRnderCamera != null) return;

            _captureRnderCamera = captureCamera;
        }

        public void SetCaptureInfo() 
        {
            _capturePicturePath = Application.dataPath + "/CaptureImage/";
            _direction          = Path.Combine(Application.persistentDataPath, "CaptureImage");
        }

        public Texture2D Capture()
        {
            _captureView.ShowBlackOut();

            if (_capturePicturePath == null)
                return null;

            if (!Directory.Exists(_capturePicturePath)) Directory.CreateDirectory(_capturePicturePath);
            
            var captureFiles  = Directory.GetFiles(_capturePicturePath);
            var fileCount     = captureFiles.Length / 2;
            var renderTexture = new RenderTexture(_widthResolution, _heightResolution, 24);
            var writeTexture  = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);

            _captureCamera.targetTexture = renderTexture;
            _captureCamera.Render();

            RenderTexture.active = renderTexture;
            writeTexture.ReadPixels(new Rect(0, 0, _widthResolution, _heightResolution), 0, 0);
            writeTexture.Apply();

            Sprite captureSprite = Sprite.Create(writeTexture, new Rect(0, 0, _widthResolution, _heightResolution), Vector2.one * 0.5f);
            byte[] bytes         = captureSprite.texture.EncodeToPNG();
            string fileName      = $"CaptureSystem_{fileCount}.png";

            File.WriteAllBytes(Path.Combine(_capturePicturePath, fileName), bytes);

            return writeTexture;
        }

        /*
            var captureTexture = startParam as Texture2D;
            var rect = new Rect(0, 0, captureTexture.width, captureTexture.height);
            var captureSprite = Sprite.Create(captureTexture, rect, new Vector2(0.5f, 0.5f));
            */
        // ----- Private
        //private void _

    }
}