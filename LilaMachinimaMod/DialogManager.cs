using System.IO;
using UnityEngine;

namespace LilaMachinimaMod
{
    internal class DialogManager
    {
        private const string dialogFilename = "mods/dialog.txt"; // TODO: Change hardcoded path
        readonly DialogParser _dialogParser;
        readonly TextManager _textManager;

        public DialogManager(DialogParser dialogParser, TextManager textManager)
        {
            _dialogParser = dialogParser;
            _textManager = textManager;
        }

        public void LaunchDialog()
        {
            GenerateChunksFromDialogFile();
            Print();
        }

        private void Print()
        {
            _textManager.GoTo("Header1"); // TODO: Change hardcoded header
        }

        private void GenerateChunksFromDialogFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), dialogFilename);
            var text = File.ReadAllText(path);
            _dialogParser.GenerateChunks(new TextAsset(text));
        }
    }
}
