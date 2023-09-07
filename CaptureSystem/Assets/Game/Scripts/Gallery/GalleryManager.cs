// ----- C#
using System.IO;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForGallery
{
    public class GalleryManager : MonoBehaviour
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const int WIDTH_RESOLUTION  = 256;
        private const int HEIGHT_RESOLUTION = 256;

        private string _photoDirectoryPath = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetPhotoDirectoryPath(string path)
        {
            if (_photoDirectoryPath != null)
                return;

            _photoDirectoryPath = path;
        }

        public List<Sprite> LoadPhotoGroup()
        {
            if (_photoDirectoryPath == null)
                return null;

            List<Sprite> result         = new List<Sprite>(); 
            string[]     photoFilePaths = Directory.GetFiles(_photoDirectoryPath);

            for (int i = 0; i < photoFilePaths.Length; i++)
            {
                var photoPath  = photoFilePaths[i];
                var photoBytes = System.IO.File.ReadAllBytes(photoPath);
                var extension  = Path.GetExtension(photoPath).ToLower();

                if (extension == ".png")
                {
                    Texture2D texture = new Texture2D(WIDTH_RESOLUTION, HEIGHT_RESOLUTION);
                    texture.LoadImage(photoBytes);

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    result.Add(sprite);
                }
            }

            return result;
        }
    }
}