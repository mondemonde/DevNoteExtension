using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Player.Models
{
    public class EventScriptFile
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string Content { get; set; }
        public ObservableCollection<string> Variables { get; set; }
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
