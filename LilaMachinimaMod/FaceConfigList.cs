namespace LilaMachinimaMod
{
    [System.Serializable]
    internal class FaceConfig
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

    [System.Serializable]
    internal class FaceConfigList
    {
        public FaceConfig[] CustomFaces { get; set;}
    }
}
