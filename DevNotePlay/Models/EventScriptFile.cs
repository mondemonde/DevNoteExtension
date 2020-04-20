using System.IO;

namespace Player.Models
{
    public class EventScriptFile
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string Content { get; set; }
        public string FileNameWithExtension
        {
            get
            {
                return Path.GetFileName(SourcePath);
            }
        }
        public string ParentFolder
        {
            get
            {
                return Path.GetFileName(Path.GetDirectoryName(SourcePath));
            }
        }
    }
}
