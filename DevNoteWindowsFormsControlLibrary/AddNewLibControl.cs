using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogApplication.Common.Config;
using System.IO;
using Common;
using BaiCrawler.DAL;
using DevNote.Interface.Models;
using BaiTextFilterClassLibrary;

namespace DevNoteWindowsFormsControlLibrary
{
    public partial class AddNewLibControl : UserControl
    {
        public AddNewLibControl()
        {
            InitializeComponent();
        }


        private void ButtonSave_Click(object sender, EventArgs e)
        {
           


            //step# 31 validate entry         

            List<TextBox> paramPath = new List<TextBox> { txtDomain, txtDept, txtDept, txtFile, txtFile };
            foreach (var text in paramPath)
            {
                if (string.IsNullOrEmpty(text.Text))
                {
                    //MessageBox.Show(text.Tag + " is required.");
                    MessageBox.Show(text.Tag + " is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }




            //step# 32 update if existing
            ConfigManager config = new ConfigManager();
            //    <add key="DevNoteDesignerLibrary" value="D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNoteDesignerLibrary" />
            var root = config.GetValue("DevNoteDesignerLibrary");


            //save 
            var filePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}", root, txtDomain.Text, txtDept.Text, txtFile.Text, numVersion.Value.ToString(), txtFile.Text + ".xaml");
            var filePathJS = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}", root, txtDomain.Text, txtDept.Text, txtFile.Text, numVersion.Value.ToString(), txtFile.Text + ".js");


            Console.WriteLine(filePath);

            var folder = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            //step# 33  step #30 save to workflow
            //GlobalDef.FrontWF.Save();
            //GlobalDef.FrontWF.CurrentFilePath = filePath;
            //GlobalDef.FrontWF.Save();

            //save using template...
            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);

            var xamlFolder = string.Format("{0}\\XAML", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var xamlFile = Path.Combine(xamlFolder, "SaveTemplate.xaml.xml");
            //1. read script file
            // var fileName = FilePath;//saveFileDialog1.FileName;
            var xamlFileContent = File.ReadAllText(xamlFile);

            xamlFileContent = xamlFileContent.Replace("##DisplayName##", txtFile.Text);

            //copy js file to lib folder
            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptTestPath = Path.Combine(codeceptjsFolder, "latest_test.js");

            //filePathJS = codeceptTestPath;
             File.Copy(codeceptTestPath, filePathJS, true);

            //path will be change by ImportJSFile method later, making this as redundant file(back up file)
            xamlFileContent = xamlFileContent.Replace("##JSFile##", filePathJS);
            File.WriteAllText(filePath, xamlFileContent);

            if (!File.Exists(filePath))
            {
                LogApplication.Agent.LogError("Failed saving ... n/ " + filePath);
                MessageBox.Show("Failed saving ... n/ " + filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            using (MyDBContext thisDb = new MyDBContext())
            {


                var profile = thisDb.WFProfiles.FirstOrDefault(p => p.Tag.ToLower() == txtTag.Text.ToLower());

                if (profile != null) //do update
                {
                    profile.Domain = txtDomain.Text;
                    profile.Department = txtDept.Text;
                    profile.Tag = txtTag.Text;
                    profile.SourcePath = filePath;
                    profile.VersionNo = Convert.ToInt32(numVersion.Value);
                    profile.Created = DateTime.Now;
                }

                else//do Insert
                {
                    profile = new WFProfile
                    {
                        Domain = txtDomain.Text,
                        Department = txtDept.Text,
                        Tag = txtTag.Text,
                        SourcePath = filePath,
                        VersionNo = Convert.ToInt32(numVersion.Value),
                        Created = DateTime.Now


                    };


                    thisDb.WFProfiles.Add(profile);

                }
                thisDb.SaveChanges();
                lblOutput.Text = profile.Name + Environment.NewLine + profile.SourcePath;


                //step# 35 IMPORT JSFile to WF folder
                var newProfile = ImportJSFile(profile);

                //step# 36 fix variable naming
                FixVariableS(newProfile);

            }

            //lblOutput.Text = 
            // this.Close();
        }

        WFProfile ImportJSFile(WFProfile profile)
        {
            var xmlFile = profile.SourcePath;
            //1. read script file
            // var fileName = FilePath;//saveFileDialog1.FileName;
            var xmlFileContent = File.ReadAllText(xmlFile);


            var JsFileDictionary = new List<string>();
            var NewJsFileList = new List<string>();


            string[] delimeter = new string[] { "JSFullFIlePath=\"" };
            string[] split = xmlFileContent.Split(delimeter, StringSplitOptions.None);

            var splitList = split.ToList();

            if (splitList.Count > 1)
            {

                for (int i = 1; i < splitList.Count; i++)
                {
                    var value = splitList[i].Split('"').First();
                    //JSFilePaths.Add(value);


                    //simulate test MESSAGE
                    //var key = Path.GetFileNameWithoutExtension(value);
                    var content = value;
                    JsFileDictionary.Add(content);
                }

            }//end SPLIT

            var MyDir = Path.GetDirectoryName(profile.SourcePath);
            int scriptCnt = 0;

            //step# 35.1 save SCRIPT to its new folder "Script_"
            foreach (var src in JsFileDictionary)
            {
                var fName = Path.GetFileName(src);

                string thisDir = Path.Combine(MyDir, "Script_" + scriptCnt.ToString());

                if (!Directory.Exists(thisDir))
                    Directory.CreateDirectory(thisDir);

                var newFile = Path.Combine(thisDir, fName);

                if (newFile.ToLower().Trim() != src.ToLower().Trim())
                {

                    if (File.Exists(newFile))
                        File.Delete(newFile);

                    File.Copy(src, newFile);
                }

                scriptCnt += 1;
                NewJsFileList.Add(newFile);
            }

            //step# 35.2 change WF file 
            //reconstruct the split file
            StringBuilder newScript = new StringBuilder("");
            if (splitList.Count > 1)
            {
                var _JSFullFIlePath = "JSFullFIlePath=\"";
                newScript.Append(splitList[0]);

                for (int i = 1; i < splitList.Count; i++)
                {
                    //...JSFullFIlePath=" 
                    newScript.Append(_JSFullFIlePath);//+ NewJsFileList[i-1];

                    //...JSFullFIlePath=D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNote.Main\bin\Debug\Katalon\blast_test2.js">
                    newScript.Append(splitList[i].Replace(JsFileDictionary[i - 1], NewJsFileList[i - 1]));

                }
            }//end reconstruct

            LogApplication.Agent.LogWarn(newScript.ToString());
            File.WriteAllText(profile.SourcePath, newScript.ToString());
            return profile;

        }//end IMPORT


        void FixVariableS(WFProfile profile)
        {
            var xmlFile = profile.SourcePath;
            //1. read script file
            // var fileName = FilePath;//saveFileDialog1.FileName;
            var xmlFileContent = File.ReadAllText(xmlFile);




            //List<string> JSFilePaths = new List<string>();
            var JsFileDictionary = new Dictionary<string, string>();


            string[] delimeter = new string[] { "JSFullFIlePath=\"" };
            string[] split = xmlFileContent.Split(delimeter, StringSplitOptions.None);

            var splitList = split.ToList();

            if (splitList.Count > 1)
            {

                for (int i = 1; i < splitList.Count; i++)
                {
                    var value = splitList[i].Split('"').First();
                    //JSFilePaths.Add(value);


                    //simulate test MESSAGE
                    var key = Path.GetFileNameWithoutExtension(value);
                    key = (i).ToString() + "." + key;
                    var content = value;
                    JsFileDictionary.Add(key, content);
                }
            }

            var ds = JsFileDictionary.ToList<KeyValuePair<string, string>>();
            ListOfVariablesPerFile = new List<string>();
            inputCount = 0;
            //NewScript = new StringBuilder("");

            foreach (var f in JsFileDictionary)
            {
                LoadDataFromFile(f.Value);
            }
        }


        List<string> ListOfVariablesPerFile;


        int inputCount;
        //StringBuilder NewScript;

        private List<string> LoadDataFromFile(string path)
        {

            var jsFileContent = File.ReadAllText(path);


            //NewScript = new StringBuilder("");
            ListOfVariablesPerFile = new List<string>();
            // ListOfVariables = new List<string>();

            if (File.Exists(path))
            {
                // FileName.Text = Path.GetFileName(path);
                // TextArea.Text = File.ReadAllText(path);
            }
            else
                return ListOfVariablesPerFile;

            //int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {



                var expressions = line.Split(new string[] { Keywords.declareVariable }, StringSplitOptions.None);
                //I.say('DECLARE');var
                //TIP: we only allow one varible declare per action line OR we only covert the first var
                if (expressions.Length > 1)
                {                    //X='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}
                    var expression = expressions[1].Split(';').First();

                    //x ='123'
                    //x
                    var xName = expression.Split('=').First().Trim();
                    if (!ListOfVariablesPerFile.Contains(xName))
                    {

                        inputCount++;
                        ListOfVariablesPerFile.Add(xName);

                        var inputVar = xName + inputCount.ToString();
                        //line = line.Replace(xName, inputVar);
                        jsFileContent = jsFileContent.Replace(xName, inputVar);

                    }

                    System.Console.WriteLine(line);


                }


            }


            file.Close();
            System.Console.WriteLine("There were {0} lines.", inputCount);
            // Suspend the screen.  
            // System.Console.ReadLine();

            File.WriteAllText(path, jsFileContent);

            return ListOfVariablesPerFile;

        }

        bool validateProfilePath()
        {



            return true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        void CreateDefaultWorkFlowFile(string scriptFile)
        {

        }

        void CreateXAML()
        {

        }

        public  event EventHandler GoToLibrary;

        public  void OnGoToLibrary()
        {
            if (GoToLibrary != null)
                GoToLibrary(this, EventArgs.Empty);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //
            OnGoToLibrary();
        }
    }
}
