﻿using BaiCrawler.DAL;
using BaiTextFilterClassLibrary;
using Common;
using DevNote.Interface.Models;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevNoteWindowsFormsControlLibrary
{
    public partial class DevNoteLibForm : Form
    {
       

        private void DevNoteLibForm_Load(object sender, EventArgs e)
        {
            // this.wFProfilesTableAdapter.Fill(this.baiDataSet1.WFProfiles);
            // this.wFProfilesTableAdapter.Fill(this.baiDataSet1.WFProfiles);


          MyDB = new MyDBContext();
          ReloadProfiles();
           
        }


        ScintillaNET.Scintilla TextArea;
        public DevNoteLibForm()
        {
            InitializeComponent();

            #region scintilla
            // CREATE CONTROL
            TextArea = new ScintillaNET.Scintilla();
            TextPanel.Controls.Add(TextArea);

            // BASIC CONFIG
            TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            //TextArea.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            //InitDragDropFile();

            #endregion

         
        }


        private void WFProfilesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.wFProfilesBindingSource.EndEdit();
            MyDB.SaveChanges();
            //this.tableAdapterManager.UpdateAll(this.baiDataSet);

        }

        #region Numbers, Bookmarks, Code Folding


        //  ScintillaNET.Scintilla TextArea;
        void loadDefault()
        {
            // DEFAULT FILE
            //LoadDataFromFile("../../MainForm.cs");

            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\JS\";
            var FullFileName = folder + "Temp.js";
            LoadDataFromFile(FullFileName);

        }

        List<string> _listOfVariables;
        List<string> ListOfVariables
        {
            get
            {
                if (_listOfVariables == null)
                    _listOfVariables = new List<string>();

                return _listOfVariables;
            }
            set
            {
                _listOfVariables = value;
            }
        }

        private void LoadDataFromFile(string path)
        {

            // ListOfVariables = new List<string>();

            if (File.Exists(path))
            {
                FileName.Text = Path.GetFileName(path);
                TextArea.Text = File.ReadAllText(path);
            }
            else
                return;

            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);



            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                counter++;

                var expressions = line.Split(new string[] { Keywords.declareVariable }, StringSplitOptions.None);
                //I.say('DECLARE');var
                //TIP: we only allow one varible declare per action line OR we only covert the first var
                if (expressions.Length > 1)
                {
                    //X='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}
                    var expression = expressions[1].Split(';').First();

                    //x ='123'
                    //x
                    var xName = expression.Split('=').First().Trim();
                    if (!ListOfVariables.Contains(xName))
                        ListOfVariables.Add(xName);
                    else
                    {
                        //remove declartion
                        //var1='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}

                        // action.Script = Keywords.useVariable + expressions[1];

                    }

                   dgVariableColumn.DataSource = ListOfVariables;

                    //'123'
                    //var xValue = expression.Split('=').Last().Trim();
                }


            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            // System.Console.ReadLine();

        }
        private void InitColors()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));

        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }


        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 10;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.CaretForeColor = Color.LimeGreen;


            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);


            TextArea.Lexer = Lexer.Cpp;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }


        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {

            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        MyDBContext MyDB;





        Dictionary<string, string> JsFileDictionary;

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                var xmlFile = sourcePathTextBox.Text;
                //1. read script file
                // var fileName = FilePath;//saveFileDialog1.FileName;
                var xmlFileContent = File.ReadAllText(xmlFile);              



                //List<string> JSFilePaths = new List<string>();
                JsFileDictionary = new Dictionary<string, string>();

                //var activities =
                //   WorkflowInspectionServices.GetActivities(root).ToList();//.GetEnumerator();

                //foreach (var activity in activities)
                //{
                //    if(activity is CodeCeptActivity)
                //    {

                //        var code = activity as CodeCeptActivity;

                //        ActivityContext context = new ActivityContext();

                //        code.JSFullFIlePath..Get(ActivityContext )

                //        JSFilePaths.Add(code.JSFullFIlePath.);
                //    }
                //}
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

                lboxJSFiles.DataSource = ds;//new BindingSource(JsFileDictionary, null);
                lboxJSFiles.DisplayMember = "key";
                lboxJSFiles.ValueMember = "value";

                //load bindingsource parmeter
                wFProfileParametersBindingSource.DataSource = null;
                //using (MyDBContext thisDb = new MyDBContext())
                //{               

                //}
                int currentId = Convert.ToInt32(idTextBox.Text);
                var profileParams = MyDB.WFProfileParameters.Where(p => p.WFProfileId == currentId).ToList();

                wFProfileParametersBindingSource.DataSource = profileParams;

                ListOfVariables = new List<string>();
                foreach (var dic in ds)
                {

                    var filePath = dic.Value; //((KeyValuePair<string, string>)lboxJSFiles.SelectedItem).Value;
                    LoadDataFromFile(filePath);
                }

                ListOfVariables.Insert(0, "");
                //dgVariableColumn.DataSource = ListOfVariables;
                foreach (string item in ListOfVariables)
                {
                   //TODO tsComboBoxMapper.ComboBox.Items.Add(item);

                }




            }
        }

        private void LboxJSFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lboxJSFiles.SelectedIndex > -1)
            {
                var filePath = ((KeyValuePair<string, string>)lboxJSFiles.SelectedItem).Value;
                LoadDataFromFile(filePath);
            }
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            wFProfileParametersBindingSource.EndEdit();

            var id = Convert.ToInt32(idTextBox.Text);
            //var file = MyDB.WFProfiles.First(w => w.Id == id);

            var forDelete = MyDB.WFProfileParameters.Where(p => p.WFProfileId == id).ToList();
            MyDB.WFProfileParameters.RemoveRange(forDelete);

            MyDB.SaveChanges();
            //foreach(var p in forDelete)
            //{
            //    var del = MyDB.WFProfileParameters.First(x => x.Id == p.Id);
            //    MyDB.WFProfileParameters.Remove()
            //}

            foreach (var item in wFProfileParametersBindingSource.List)
            {
                var param = (WFProfileParameter)item;
                param.WFProfileId = Convert.ToInt32(idTextBox.Text);
                MyDB.WFProfileParameters.Add(param);

            }


            //save params..
            MyDB.SaveChanges();
        }

        private void ToolStripButtonDeleteParam_Click(object sender, EventArgs e)
        {

        }

        void ReloadProfiles()
        {
            var profiles = MyDB.WFProfiles.ToList();


            //wFProfilesBindingSource.DataMember = null;
            wFProfilesBindingSource.DataSource = profiles;

            var profileParams = MyDB.WFProfileParameters.ToList();

            // wFProfileParametersBindingSource.DataMember = null;
            wFProfileParametersBindingSource.DataSource = profileParams;
        }

        private void BindingNavigatorDoDeleteItem_Click(object sender, EventArgs e)
        {
            //delete
           if( MessageBox.Show("Continue Delete?","Warning!",MessageBoxButtons.YesNo
               ,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                WFProfile current = (WFProfile)wFProfilesBindingSource.Current;
                MyDB.WFProfiles.Attach(current);
                MyDB.WFProfiles.Remove(current);
                MyDB.SaveChanges();
                ReloadProfiles();
            }

        }

        private void WFProfilesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {

          //  WFProfile oldProfile = (WFProfile)wFProfilesBindingSource.Current;

            //update the new
            wFProfilesBindingSource.EndEdit();     
            WFProfile current =(WFProfile)wFProfilesBindingSource.Current;


            var tagName = current.Tag.ToLower().Trim();

            var duplicate = MyDB.WFProfiles.FirstOrDefault(p => p.Tag.ToLower() == tagName);
            if(duplicate!=null)
            {
                //MessageBox.Show("Event Tag is already existing.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                MyDB.Entry(current).State = System.Data.Entity.EntityState.Modified;
                //MyDB.WFProfiles.Attach(current);
                MyDB.SaveChanges();

            }
            else
            {

                //MyDB.WFProfiles.Add(current);//Attach(current);
                MyDB.Entry(current).State = System.Data.Entity.EntityState.Added;
                MyDB.SaveChanges();
            }

            ReloadProfiles();



        }

        public event EventHandler PlayWithChrome;
        /// <summary>
        /// Raises the PlayWithChrome event
        /// </summary>
        protected virtual void OnPlayWithChrome(EventArgs e)
        {
            if (PlayWithChrome != null)
            {
                PlayWithChrome(this, e);
            }
        }
        private void toolStripPlayChrome_Click(object sender, EventArgs e)
        {
            var content = TextArea.Text;
            File.WriteAllText(FileEndPointManager.DefaultPlayJsFile, content);
            OnPlayWithChrome(e);

        }
    }
}
