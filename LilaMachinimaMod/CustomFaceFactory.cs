using System;
using System.Reflection;
using UnityEngine;
using System.IO;
using static FaceSetter;
using Newtonsoft.Json;
using MelonLoader;

namespace LilaMachinimaMod
{
    internal class CustomFaceFactory
    {
        private const string _gameObjectToCloneFromName = "Stranger";
        private const string _facesArrayPath = "Face/Faces";
        private const float _facesXGap = 10f;
        private const float _facesXOffset = 50f;
        private Transform? _lastNonCustomFaceTransform;
        private int _originalFacesCount;

        public void CreateCustomFacesFromFile(string path)
        {
            try
            {
                string jsonContent = File.ReadAllText(path);
                FaceConfigList? faceConfigList = JsonConvert.DeserializeObject<FaceConfigList>(jsonContent) ?? throw new Exception("Error when deserializing custom faces from json file");
                foreach (var faceConfig in faceConfigList.CustomFaces)
                {
                    Melon<LilaMachinimaMod>.Logger.Msg($"name: {faceConfig.Name}, path: {faceConfig.ImagePath}");
                    InstantiateCustomFace(faceConfig.Name, faceConfig.ImagePath);
                }
            }
            catch (Exception e)
            {
                Melon<LilaMachinimaMod>.Logger.Error("Error when creating custom faces: ", e);
            }
        }

        public GameObject InstantiateCustomFace(string name, string pathToImage)
        {
            var facesTransform = GameObject.Find("DontDestroyOnLoad").transform.Find($"{_facesArrayPath}");
            SetLastNonCustomFacePosition(facesTransform);
            var faceGameObject = InstantiateStrangerCloneWithName(name, facesTransform);
            SetFaceSpriteFromPath(faceGameObject, pathToImage);
            AddCustomFaceToFaceSetter(faceGameObject);
            SortFacesTransform(facesTransform);
            return faceGameObject;
        }

        void SetLastNonCustomFacePosition(Transform facesTransform)
        {
            if (_lastNonCustomFaceTransform == null)
            {
                _originalFacesCount = facesTransform.childCount;
                _lastNonCustomFaceTransform = facesTransform.GetChild(facesTransform.childCount-1);
            }
        }

        private void SortFacesTransform(Transform facesTransform)
        {
            if(_lastNonCustomFaceTransform == null)
            {
                throw new Exception("Last non custom face transform is null!");
            }

            for (int i = _originalFacesCount; i < facesTransform.childCount; i++)
            {
                var face = facesTransform.GetChild(i).gameObject;

                // Calculate new position
                float xPosition = _lastNonCustomFaceTransform.position.x + _facesXGap * i + _facesXOffset; 

                face.transform.position = new Vector3(xPosition, _lastNonCustomFaceTransform.position.y, _lastNonCustomFaceTransform.position.z);
            }
        }

        /// <summary>
        /// Finds the FaceSetter object and adds Face component from the custom face game object to the faces dictionary
        /// </summary>
        /// <param name="customFaceGameObject"></param>
        private void AddCustomFaceToFaceSetter(GameObject customFaceGameObject)
        {
            var faceSetter = GameObject.Find("GameManager").GetComponent<FaceSetter>();
            Type exampleType = typeof(FaceSetter);
            FieldInfo facesFieldInfo = exampleType.GetField("faces", BindingFlags.NonPublic | BindingFlags.Instance);
            var dict = (StringFaceDictionary)facesFieldInfo.GetValue(faceSetter);
            var customFaceFace = customFaceGameObject.GetComponent<Face>();
            dict.Add(customFaceGameObject.name, customFaceFace);
            facesFieldInfo.SetValue(faceSetter, dict);
        }

        /// <summary>
        /// Sets the sprite of the face object to the image from the given path
        /// </summary>
        /// <param name="faceGameObject"></param>
        /// <param name="path"></param>
        private void SetFaceSpriteFromPath(GameObject faceGameObject, string path)
        {
            var spriteRenderer = GetFaceSpriteRenderer(faceGameObject);
            var renderer = GetFaceRenderer(faceGameObject);
            var texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, File.ReadAllBytes(path));
            Vector2 rendererDimensions = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
            float pixelsPerUnit = texture.width / rendererDimensions.x;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            spriteRenderer.sprite = sprite;
        }

        /// <summary>
        /// Instantiates a clone of the Stranger object with the given name
        /// </summary>
        /// <param name="name">Name for the new gameObject</param>
        /// <param name="facesTransform">Transform of faces object in the game, which contains all faces.</param>
        /// <returns>Clone of the Stranger object</returns>
        /// <exception cref="Exception"></exception>
        private GameObject InstantiateStrangerCloneWithName(string name, Transform facesTransform)
        {
            var strangerObject = facesTransform
                .Find($"{_gameObjectToCloneFromName}").gameObject ?? throw new Exception($"{_gameObjectToCloneFromName} object not found!");
            var clone = UnityEngine.Object.Instantiate(strangerObject, facesTransform);
            clone.name = name;
            clone.transform.Find(_gameObjectToCloneFromName).name = name;
            return clone;
        }

        private SpriteRenderer GetFaceSpriteRenderer(GameObject face)
        {
            return face.transform.Find(face.name).GetComponent<SpriteRenderer>();
        }

        private Renderer GetFaceRenderer(GameObject face)
        {
            return face.transform.Find(face.name).GetComponent<Renderer>();
        }
    }
}
