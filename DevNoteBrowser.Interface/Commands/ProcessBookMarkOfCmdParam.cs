using LogApplication.Common.Commands;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.COMMANDS
{

    /// <summary>
    /// CmdParam is carrier object , in its payload is hidden the true class to pass
    /// </summary>
    public class ProcessBookMarkOfCmdParam : NativeActivity<CmdParam>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }
        public OutArgument<string> CommandName { get; set; }
        //public OutArgument<object> CommandParam { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            //create waiting or listener here...
            context.CreateBookmark(this.BookmarkName.Get(context), new BookmarkCallback(OnReadComplete));
        }

        void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
        {
            //
            //string bname = context.GetValue(this.BookmarkName);
            var input0 = state as CmdParam;
            context.SetValue(this.Result, input0);
            context.SetValue(this.CommandName, input0.CommandName);
            Console.WriteLine(input0.ToString());

        }
    }
}
