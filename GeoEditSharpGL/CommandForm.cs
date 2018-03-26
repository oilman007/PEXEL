using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Globalization;






namespace GeoEdit
{
    public partial class CommandForm : Form
    {
        public CommandForm()
        {
            InitializeComponent();
        }

        private void CommandForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



        public string Code
        {
            set
            {
                this.richTextBox_commands.Text = value;
            }
            get
            {
                return this.richTextBox_commands.Text;
            }
        }



        public event EventHandler ExecuteEvent;





        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ExecuteEvent != null)
                ExecuteEvent(sender, e);
        }















        public void Execute(Project project)
        {
            string code = this.richTextBox_commands.Text;
            code = 
            @"using System; 
            using System.Windows.Forms; 
            using GeoEdit; 
            using System.Drawing;
            namespace MyNamespace
            { 
               public class MyClass 
               { 
                    public MyClass(Project p) { project = p; }
                    public Project project {set;get;}
                    public void Func()
                    { 
                       " + code + @"
                    } 
               } 
            }";
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters compParameters = new CompilerParameters();
            compParameters.ReferencedAssemblies.Add("System.dll");
            compParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            compParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            CompilerResults res = codeProvider.CompileAssemblyFromSource(compParameters, code);
            if (res.Errors.HasErrors)
            {
                string lcErrorMsg = "";
                lcErrorMsg = res.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < res.Errors.Count; x++)
                    lcErrorMsg = lcErrorMsg + "\r\nLine: " + res.Errors[x].Line.ToString() + " - " + res.Errors[x].ErrorText;
                MessageBox.Show(lcErrorMsg + "\r\n\r\n" + code, "Compiler Demo");
                return;
            }

            this.richTextBox_log.Text += this.richTextBox_commands.Text;
            this.richTextBox_commands.Text = string.Empty;


            object myClass =
                res.CompiledAssembly.CreateInstance("MyNamespace.MyClass", false, BindingFlags.CreateInstance, null,
                                                    new object[] { project }, CultureInfo.CurrentCulture, null);

            myClass.GetType().GetMethod("Func").Invoke(myClass, new object[] { });
            //UpdateProject(CurrProject);
        }







    }

}
