using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Primitives;
//using SharpGL.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;

//using Ionic.Zip;

using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Globalization;

using System.Web;

using System.Threading.Tasks;
using System.Threading;



namespace GeoEdit
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            CreateMenuItems();
            CreateTreeView();
            CreateMainOpenGLControl();
            ShowGridLines = toolStripButton_gridLines.Checked;
            CurrProject = new Project();
            ProjectTreeViewSelectedNode = new TreeNode();
            GenerateModTitle();
            AddNewModel();
        }


        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }


        void newMenu_Click(object sender, EventArgs e)
        {
            New();
        }

        void openMenu_Click(object sender, EventArgs e)
        {
            Open();
        }

        void saveMenu_Click(object sender, EventArgs e)
        {
            Save();
        }

        void saveAsMenu_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        void recentFilesMenu_Click(object sender, EventArgs e)
        {
            string filename = ((ToolStripMenuItem)sender).Name;
            OpenRecentFile(filename);
        }

        void close_Event(object sender, FormClosingEventArgs e)
        {
            if (OkToContinue())
            {
                //WriteSettings();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
            


        //void ReadSettings();
        //void WriteSettings();


        void New()
        {
            if (OkToContinue())
            {
                UpdateProject(new Project());
                SetCurrentFile(string.Empty);
            }
        }


        void Open()
        {
            if (OkToContinue())
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = String.Format("Project Files (*.{0})|*.{0}", defaultExt);
                //dialog.Filter = String.Format("Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", defaultExt);
                dialog.Multiselect = false;
                dialog.ShowDialog();
                string filename = dialog.FileName;
                if (filename != null && filename != string.Empty)
                    LoadFile(filename);
            }
        }







        const string defaultExt = "pxl";




        bool Save()
        {
            if (curfile == null || curfile == string.Empty)
                return SaveAs();
            else
                return SaveFile(curfile);
        }
        



        bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = String.Format("Project Files (*.{0})|*.{0}", defaultExt);
            dialog.DefaultExt = defaultExt;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == null || filename == string.Empty)
                return false;
            return SaveFile(filename);
        }


        void OpenRecentFile(string filename)
        {
            if (OkToContinue())
            {
                LoadFile(filename);
            }
        }


        bool modified = false;

        bool OkToContinue()
        {
            if (modified)
            {
                string caption = "Project Closing";
                string message = "The document has been modified.\n" + "Do you want to save your changes?";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    return Save();
                else if (result == DialogResult.Cancel)
                    return false;
            }
            return true;
        }


        bool LoadFile(string filename)
        {
            if (!ReadFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        bool SaveFile(string filename)
        {
            if (!WriteFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        void SetCurrentFile(string filename)
        {
            curfile = filename;
            string shownName = "Untitled";
            if (curfile != null && curfile != string.Empty)
            {
                shownName = StrippedName(curfile);
                recentFiles.Remove(curfile);
                //
                recentFiles.Reverse();
                recentFiles.Add(curfile);
                recentFiles.Reverse();
                //
                UpdateRecentFile();
            }
            this.Text = shownName;
        }


        void UpdateRecentFile()
        {
            List<string> temp = new List<string>();
            foreach (string filename in recentFiles)
                if (File.Exists(filename))
                    temp.Add(filename);
            recentFiles = temp;
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                if (i < recentFiles.Count())
                {
                    recentFilesFileMenu[i].Name = recentFiles[i];
                    recentFilesFileMenu[i].Text = "&" + (i + 1).ToString() + " " + StrippedName(recentFiles[i]);
                    recentFilesFileMenu[i].Visible = true;
                }
                else
                    recentFilesFileMenu[i].Visible = false;
            }
            separatorDownFileMenu.Visible = (recentFiles.Count != 0);
        }


        string StrippedName(string fullFileName)
        {
            FileInfo fileinfo = new FileInfo(fullFileName);
            return fileinfo.Name;
        }


        List<string> recentFiles = new List<string>();
        string curfile;


        bool WriteFile(string filename)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool ok = this.CurrProject.Save(filename);
            string msg;
            if (ok)
            {
                msg = "File Saved Successfully!";
            }
            else
            {
                msg = "File Saving Error! Abort operation.";
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
            return ok;
        }




        bool ReadFile(string filename)
        {
            Project temp = new Project();
            Cursor.Current = Cursors.WaitCursor;
            bool ok = temp.Load(filename);
            string msg;
            if (ok)
            {
                msg = "File Loaded Successfully!";
                UpdateProject(temp);
            }
            else
            {
                msg = "File Loading Error! Abort operation.";
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
            return ok;
        }






        ToolStripMenuItem newMenu, openMenu, saveMenu, saveAsMenu, exitMenu;
        ToolStripSeparator separatorUpFileMenu, separatorDownFileMenu;
        const int maxRecentFiles = 5;
        ToolStripMenuItem[] recentFilesFileMenu;

        //ToolStripMenuItem wellsMenu, addWellsMenu, removeWellsMenu, renameWellsMenu;


        void CreateMenuItems()
        {
            //
            // File Menu Items
            //
            newMenu = new ToolStripMenuItem();
            newMenu.Name = "New_FileMenu";
            newMenu.Size = new Size(152, 22);
            newMenu.Text = "&New";
            newMenu.Click += new System.EventHandler(newMenu_Click);
            //
            openMenu = new ToolStripMenuItem();
            openMenu.Name = "Open_FileMenu";
            openMenu.Size = new Size(152, 22);
            openMenu.Text = "&Open";
            openMenu.Click += new System.EventHandler(openMenu_Click);
            //
            saveMenu = new ToolStripMenuItem();
            saveMenu.Name = "Save_FileMenu";
            saveMenu.Size = new Size(152, 22);
            saveMenu.Text = "&Save";
            saveMenu.Click += new System.EventHandler(saveMenu_Click);
            //
            saveAsMenu = new ToolStripMenuItem();
            saveAsMenu.Name = "SaveAs_FileMenu";
            saveAsMenu.Size = new Size(152, 22);
            saveAsMenu.Text = "Save &As";
            saveAsMenu.Click += new System.EventHandler(saveAsMenu_Click);
            //
            separatorUpFileMenu = new ToolStripSeparator();
            separatorUpFileMenu.Name = "separatorUp_FileMenu";
            separatorUpFileMenu.Size = new Size(149, 6);
            recentFilesFileMenu = new ToolStripMenuItem[maxRecentFiles];
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                recentFilesFileMenu[i] = new ToolStripMenuItem();
                recentFilesFileMenu[i].Size = new Size(152, 22);
                recentFilesFileMenu[i].Click += new System.EventHandler(recentFilesMenu_Click);
            }
            separatorDownFileMenu = new ToolStripSeparator();
            separatorDownFileMenu.Name = "separator2_FileMenu";
            separatorDownFileMenu.Size = new Size(149, 6);
            UpdateRecentFile();
            //
            exitMenu = new ToolStripMenuItem();
            exitMenu.Name = "Exit_FileMenuItem";
            exitMenu.Size = new Size(152, 22);
            exitMenu.Text = "E&xit";
            exitMenu.Click += new System.EventHandler(exit_Click);
            //
            // File Menu
            //
            menu_file.DropDownItems.Add(newMenu);
            menu_file.DropDownItems.Add(openMenu);
            menu_file.DropDownItems.Add(saveMenu);
            menu_file.DropDownItems.Add(saveAsMenu);
            menu_file.DropDownItems.Add(separatorUpFileMenu);
            for (int i = 0; i < maxRecentFiles; ++i)
                menu_file.DropDownItems.Add(recentFilesFileMenu[i]);
            menu_file.DropDownItems.Add(separatorDownFileMenu);
            menu_file.DropDownItems.Add(exitMenu);
            // modifier
            /*
            modifierForm.ApplyEvent += modifierForm_ApplyEvent;
            modifierForm.GetFromGrid += modifierForm_GetFromGrid;
            modifierForm.FormClosed += ModifierForm_FormClosed;
            modifierForm.FormClosing += ModifierForm_FormClosing;
            */
            // command
            commandForm.ExecuteEvent += commandForm_Execute;
            // column Value
            this.setValueFrom.ApplyEvent += SetValueFrom_ApplyEvent;
            this.setValueFrom.Text = "Set Value";
            // column Radius
            this.setRadiusFrom.ApplyEvent += SetRadiusFrom_ApplyEvent;
            this.setRadiusFrom.Text = "Set Radius";
            // column Use
            this.setUseForm.ApplyEvent += SetUseForm_ApplyEvent;
            this.setUseForm.Text = "Set Use";
            // Modifier Name Form
            setModifierNameForm.ApplyEvent += SetModifierNameForm_ApplyEvent;
            setModifierNameForm.Text = "Set Modifier Name";
            // scalePropForm
            scalePropForm.ApplyEvent += ScalePropForm_ApplyEvent;
            scalePropForm.GetFromPropEvent += ScalePropForm_GetFromPropEvent;
            scalePropForm.Text = "Scale";
            // modifier form
            modifierForm.FormClosing += ModifierForm_FormClosing;
            // setModelNameForm
            setModelNameForm.Text = "Rename Model";
            setModelNameForm.ApplyEvent += SetModelNameForm_ApplyEvent;
            // setGridNameForm
            setGridNameForm.Text = "Rename Grid";
            setGridNameForm.ApplyEvent += SetGridNameForm_ApplyEvent;
            // setPropNameForm
            setPropNameForm.Text = "Rename Prop";
            setPropNameForm.ApplyEvent += SetPropNameForm_ApplyEvent;
            // coordToolStripStatusLabel
            coordToolStripStatusLabel.Alignment = ToolStripItemAlignment.Right;
        }


        void CreateTreeView()
        {
            CreateTreeViewContextMenus();
        }



        ContextMenu modelsContextMenu, gridsContextMenu, gridContextMenu, wellsContextMenu, propsContextMenu,
                    propContextMenu, modifiersContextMenu;

        MenuItem addModelMenuItem, createGridMenuItem, importGridMenuItem, importWellsMenuItem, removeWellMenuItem,
                 addFolderMenuItem, importPropMenuItem, createPropMenuItem, exportPropMenuItem,
                 editModifierMenuItem, deleteModifierMenuItem;

        const string addModelMenuItemText = "Add New Model";
        const string renameMenuItemText = "Rename";
        const string createGridMenuItemText = "Create Grid";
        const string importGridMenuItemText = "Import Grid";
        const string importWellsMenuItemText = "Import Wells";
        const string removeWellsMenuItemText = "Remove Well";
        const string addFolderMenuItemText = "Add Folder";
        const string importPropMenuItemText = "Import Prop";
        const string createPropMenuItemText = "Create Prop";
        const string exportPropMenuItemText = "Export Prop";
        const string editModifierMenuItemText = "Edit Modifier...";
        const string deleteModifierMenuItemText = "Delete Modifier";

        void CreateTreeViewContextMenus()
        {
            //common
            addModelMenuItem        = new MenuItem(addModelMenuItemText,    new System.EventHandler(addModelMenuItem_Click)         );
            createGridMenuItem      = new MenuItem(createGridMenuItemText,  new System.EventHandler(createGridMenuItem_Click)       );
            importGridMenuItem      = new MenuItem(importGridMenuItemText,  new System.EventHandler(importGridMenuItem_Click)       );
            importWellsMenuItem     = new MenuItem(importWellsMenuItemText, new System.EventHandler(importWellsMenuItem_Click)      );
            removeWellMenuItem      = new MenuItem(removeWellsMenuItemText, new System.EventHandler(removeWellMenuItem_Click)       );
            addFolderMenuItem       = new MenuItem(addFolderMenuItemText,   new System.EventHandler(addFolderMenuItem_Click)        );
            importPropMenuItem      = new MenuItem(importPropMenuItemText,  new System.EventHandler(importPropMenuItem_Click)       );
            createPropMenuItem      = new MenuItem(createPropMenuItemText,  new System.EventHandler(createPropMenuItem_Click)       );
            exportPropMenuItem      = new MenuItem(exportPropMenuItemText,  new System.EventHandler(exportPropMenuItem_Click)       );
            editModifierMenuItem    = new MenuItem(exportPropMenuItemText,  new System.EventHandler(editModifierMenuItem_Click)     );
            deleteModifierMenuItem  = new MenuItem(exportPropMenuItemText,  new System.EventHandler(deleteModifierMenuItem_Click)   );

            //models ListBox
            modelsContextMenu = new ContextMenu();
            modelsContextMenu.MenuItems.Add(addModelMenuItem);
            listBox_models.ContextMenu = modelsContextMenu;
            //grids ListBox
            gridsContextMenu = new ContextMenu();
            gridsContextMenu.MenuItems.Add(importGridMenuItem);
            gridsContextMenu.MenuItems.Add(createGridMenuItem);
            listBox_grids.ContextMenu = gridsContextMenu;
            //grid
            gridContextMenu = new ContextMenu();
            // props ListBox
            propsContextMenu = new ContextMenu();
            propsContextMenu.MenuItems.Add(importPropMenuItem);
            propsContextMenu.MenuItems.Add(createPropMenuItem);
            propsContextMenu.MenuItems.Add(addFolderMenuItem);
            listBox_props.ContextMenu = propsContextMenu;
            //prop
            propContextMenu = new ContextMenu();
            propContextMenu.MenuItems.Add(exportPropMenuItem);
            //wells TreeView
            wellsContextMenu = new ContextMenu();
            wellsContextMenu.MenuItems.Add(importWellsMenuItem);
            wellsContextMenu.MenuItems.Add(removeWellMenuItem);
            wellsContextMenu.MenuItems.Add(addFolderMenuItem);
            this.treeView_wells.ContextMenu = wellsContextMenu;
            // modifiers
            /*
            modifiersContextMenu = new ContextMenu();
            modifiersContextMenu.MenuItems.Add(editModifierMenuItem);
            modifiersContextMenu.MenuItems.Add(deleteModifierMenuItem);
            this.checkedListBox_modifiers.ContextMenu = modifiersContextMenu;
            */
        }


        





        const string gridTreeNodeText  = "GRID";
        const string wellsTreeNodeText = "WELLS";
        const string propsTreeNodeText = "Properties";

        

        



        


        void addModelMenuItem_Click(object sender, EventArgs e)
        {
            AddNewModel();
        }


        


        void AddNewModel()
        {
            this.CurrProject.Models.Add(new Model());
            UpdateModels(this.CurrProject.Models);
        }



        


        TreeNode ProjectTreeViewSelectedNode { set; get; }





        void createGridMenuItem_Click(object sender, EventArgs e)
        {
            CreateGridForm createGridForm = new CreateGridForm();  // переделать
            createGridForm.ShowDialog();
            if (!createGridForm.Add)
                return;
            Cursor.Current = Cursors.WaitCursor;
            Grid grid = new GeoEdit.Grid(createGridForm.Title, createGridForm.NX, createGridForm.NY, createGridForm.NZ, 
                                         createGridForm.XSize, createGridForm.YSize, createGridForm.ZSize, createGridForm.Depth);
            CurrModel.Grids.Add(grid);
            UpdateGrids(CurrModel.Grids);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Grid Created Successfully!");
        }




        void importGridMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*" + "|" + "CMG File (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            FileType type = dialog.FilterIndex == 1 ? FileType.GRDECL_ASCII : FileType.CMG_ASCII;
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            Grid grid = new GeoEdit.Grid();
            if (grid.Read(filename, type))
            {
                CurrModel.Grids.Add(grid);
                UpdateGrids(CurrModel.Grids);
                msg = "Grid Imported Successfully!";
            }
            else
            {
                msg = "Grid Import Error!";
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
        }




        WellImportForm wellImportForm = new WellImportForm();
        void importWellsMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Well TXT Files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string file = dialog.FileName;
            if (file == null || file == string.Empty) return;

            Cursor.Current = Cursors.WaitCursor;
            CurrGrid.Wells.AddRange(Compdat.Read(file));
            UpdateWells(CurrGrid.Wells);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Wells Import Complited!");

            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IJ Indexes (*.*)|*.*" + "|" + "XY Coordinates (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            if (dialog.FileName.Count() == 0)
                return;
            switch (dialog.FilterIndex)
            {
                case 1: // IJ Indexes
                    {
                        string[] lines = System.IO.File.ReadAllLines(dialog.FileName, Encoding.GetEncoding(1251));
                        CurrModel.Wells.Clear();
                        foreach (string line in lines)
                        {
                            string clearline = GRDECLReader.ClearLine(line);
                            if (clearline == string.Empty)
                                continue;
                            string[] split = clearline.Split();
                            //Point3D point = CurrentGridPlane.Faces[well.Location.X - 1, well.Location.Y - 1].Center();
                            CurrModel.Wells.Add(new SimpleWell(split[0], new PointF(float.Parse(split[1]), float.Parse(split[2])), true));
                        }
                    }
                    break;
                case 2: // XY Coordinates
                    {
                        string[] lines = System.IO.File.ReadAllLines(dialog.FileName, Encoding.GetEncoding(1251));
                        CurrModel.Wells.Clear();
                        foreach (string line in lines)
                        {
                            string clearline = GRDECLReader.ClearLine(line);
                            if (clearline == string.Empty)
                                continue;
                            string[] split = clearline.Split();
                            CurrModel.Wells.Add(new SimpleWell(split[0], new PointF(float.Parse(split[1]), float.Parse(split[2])), true));
                        }
                    }
                    break;
            }
            */
        }




        void removeWellMenuItem_Click(object sender, EventArgs e)
        {
            string message = string.Format("Are you sure you would like to remove well {0}?", this.treeView_wells.SelectedNode.Text);
            string caption = "Modifier Deleting";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            CurrGrid.Wells.RemoveAt((int)this.treeView_wells.SelectedNode.Tag);
            this.treeView_wells.Nodes.Remove(this.treeView_wells.SelectedNode);
            UpdateWells(CurrGrid.Wells);
        }






        void addFolderMenuItem_Click(object sender, EventArgs e)
        {
        }




        
        void importPropMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*" + "|" + "CMG File (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            FileType type = dialog.FilterIndex == 1 ? FileType.GRDECL_ASCII : FileType.CMG_ASCII;
            List<string> fileKW = (type == FileType.GRDECL_ASCII) ? GRDECLReader.KWContains(filename) : CMGReader.KWContains(filename);
            ImportPropForm form = new ImportPropForm();
            if (fileKW.Count() > 1)
            {
                form.KW = fileKW;
                form.ShowDialog();
            }
            else
                form.ChoosenKW = fileKW;
            Cursor.Current = Cursors.WaitCursor;
            foreach (string kw in form.ChoosenKW)
            {
                Prop prop = new Prop();
                if (prop.Read(CurrGrid.SpecGrid.NX, CurrGrid.SpecGrid.NY, CurrGrid.SpecGrid.NZ, kw, filename, type))
                    CurrGrid.Props.Add(prop);
            }
            foreach (string kw in form.ChoosenKW)
            {
                this.listBox_props.Items.Add(kw);
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Property Imported Successfully!");
        }


        




        static int propNo = 1;
        const string defPropTitle = "Prop";
        const double defPropValue = 1.0f;
        void createPropMenuItem_Click(object sender, EventArgs e)
        {
            CreatePropForm dialog = new CreatePropForm();
            dialog.Value = defPropValue;
            dialog.Title = defPropTitle + " " + (propNo++).ToString();
            dialog.ShowDialog();
            if (!dialog.Add)
                return;
            Cursor.Current = Cursors.WaitCursor;
            Prop p = new Prop(CurrGrid.NX(), CurrGrid.NY(), CurrGrid.NZ(), dialog.Value);
            CurrGrid.Props.Add(p);
            this.listBox_props.Items.Add(p.Title);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Property Created Successfully!");
        }









        void exportPropMenuItem_Click(object sender, EventArgs e)
        {

        }









        void CreateMainOpenGLControl()
        {
            openGLControl_main.MouseWheel += new MouseEventHandler(mainOpenGLControl_MouseWheel);
        }


        //double x_shift = 0.0f;
        //double y_shift = 0.0f;
        //PointF translate = new PointF(0f, 0f);
        //double zoomFactor = 1.0f;

        const int bufferSize = 4;
        uint[] buffer = new uint[bufferSize];





        void UpdateMainOpenGLControl()
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (CurrModel == null) return;
            gl.LoadIdentity();
            gl.Translate(CurrModel.Position.X, CurrModel.Position.Y, 0f);
            gl.Scale(CurrModel.Position.Z, CurrModel.Position.Z, 1f);
            if(CurrGridPlane != null)
            {
                if (ShowGridLines)
                    Draw(CurrGridPlane);
                if (AddModifierMode || this.AddModifierModeOnSelectedMode)
                {
                    DrawCircle(CurrMouseLocationF.X, CurrMouseLocationF.Y, modifierForm.Radius, 360, Color.AntiqueWhite, -10);
                }
                /*
                else if(this.modifierForm.Visible && this.modifierForm.ShowRadius)
                {
                    //PointF winPoint = ProjToWinCoord(this.modifierForm.Location.X, this.modifierForm.Location.Y);
                    //DrawCircle(winPoint.X, winPoint.Y, this.modifierForm.ModifierRadius, 360, Color.AntiqueWhite, -10f);
                    DrawCircle(this.modifierForm.ModifierPoint.X, this.modifierForm.ModifierPoint.Y, 
                               this.modifierForm.ModifierRadius, 360, Color.AntiqueWhite, -10f);
                }
                */
                Draw(CurrProp, CurrGridPlane);
                Draw(CurrProp.Scale);
            }
            if (CurrWellsPlane != null)
                Draw(CurrWellsPlane);
        }


        


        GridPlane CurrGridPlane { set; get; }


        void UpdateGridPlane()
        {
            if (CurrGrid != null && CurrProp != null && CurrLeyers != null && CurrLeyers.Count() > 0)
            {
                // buttons
                this.toolStripButton_scale.Enabled = true;
                this.toolStripButton_focus.Enabled = true;
                this.toolStripButton_gridLines.Enabled = true;
                //
                CurrGridPlane = new GridPlane(CurrGrid, CurrProp, SelectedLeyers);
            }
            else
            {
                // buttons
                this.toolStripButton_scale.Enabled = false;
                this.toolStripButton_focus.Enabled = false;
                this.toolStripButton_gridLines.Enabled = false;
                //
                CurrGridPlane = null;
            }
        }










        const int scale_x_min = 10;
        const int scale_x_max = 40;
        const int scale_max_hight = 400;


        /*
        void Draw(Scale scale)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            int scale_hight = Math.Min(gl.RenderContextProvider.Height * 80 / 100, scale_max_hight);
            int scale_y_min = gl.RenderContextProvider.Height / 2 + scale_hight / 2;
            int scale_y_max = gl.RenderContextProvider.Height / 2 - scale_hight / 2;

            const int nd = 100;

            //PointF min = WinToProjCoord(scale_x_min, scale_y_min);
            //PointF max = WinToProjCoord(scale_x_max, scale_y_max);

            float y = scale_y_min;
            float y_step = (scale_y_max - scale_y_min) / nd;

            float v = scale.Min;
            float v_step = (scale.Max - scale.Min) / nd;

            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nd; ++i)
            {
                Color color = scale.ValueColor(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(scale_x_min, y, scalePlaneDepth);
                gl.Vertex(scale_x_max, y, scalePlaneDepth);
                y += y_step;
                v += v_step;
                color = scale.ValueColor(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(scale_x_max, y, scalePlaneDepth);
                gl.Vertex(scale_x_min, y, scalePlaneDepth);
            }
            gl.End();


            int nstep = 10;
            int sill = 7;
            y = scale_y_min;            

            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < nd + 1; i += nstep)
            {
                gl.Vertex(scale_x_min       , y, scalePlaneDepth);
                gl.Vertex(scale_x_max + sill, y, scalePlaneDepth);
                y += y_step * nstep;
            }
            gl.End();
            

            y = scale_y_min;
            v = scale.Min;
           
            for (int i = 0; i < nd + 1; i += nstep)
            {
                PointF winPoint = ProjToWinCoord(scale_x_max, y);
                gl.DrawText((int)winPoint.X + sill, (int)winPoint.Y, Color.Gray.R, Color.Gray.G, Color.Gray.B, "", 10, v.ToString());
                y += y_step * nstep;
                v += v_step * nstep;
            }           
        }
        */




        
        void Draw(PropScale scale)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            int scale_hight = Math.Min(gl.RenderContextProvider.Height * 80 / 100, scale_max_hight);
            int scale_y_min = gl.RenderContextProvider.Height / 2 + scale_hight / 2;
            int scale_y_max = gl.RenderContextProvider.Height / 2 - scale_hight / 2;

            int nd = 100;


            Point2D min = WinToProjCoord(scale_x_min, scale_y_min);
            Point2D max = WinToProjCoord(scale_x_max, scale_y_max);

            double y = min.Y;
            double y_step = (max.Y - min.Y) / nd;

            double v = scale.Min;
            double v_step = (scale.Max - scale.Min) / nd;

            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nd; ++i)
            {
                Color color = scale.Color(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(min.X, y, scalePlaneDepth);
                gl.Vertex(max.X, y, scalePlaneDepth);
                y += y_step;
                v += v_step;
                color = scale.Color(v);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(max.X, y, scalePlaneDepth);
                gl.Vertex(min.X, y, scalePlaneDepth);
            }
            gl.End();


            int nstep = 10;
            int sill = 0;
            y = min.Y;

            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < nd + 1; i += nstep)
            {
                gl.Vertex(min.X, y, scalePlaneDepth);
                gl.Vertex(max.X + sill, y, scalePlaneDepth);
                y += y_step * nstep;
            }
            gl.End();


            y = min.Y;
            v = scale.Min;

            for (int i = 0; i < nd + 1; i += nstep)
            {
                PointF winPoint = ProjToWinCoord((float)max.X, (float)y);
                gl.DrawText((int)winPoint.X + sill, (int)winPoint.Y, Color.Gray.R, Color.Gray.G, Color.Gray.B, "", 10, v.ToString());
                y += y_step * nstep;
                v += v_step * nstep;
            }
        }
        






        const double propPlaneDepth         =  0;
        const double gridLinesPlaneDepth    = -1;
        const double wellLinesPlaneDepth    = -2;
        const double scalePlaneDepth        = -3;





        void Draw(Prop prop, GridPlane plane)
        {
            int nx = plane.NX();
            int ny = plane.NY();
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            /*
            gl.LoadIdentity();
            gl.Translate(x_shift, y_shift, 0f);
            //gl.Translate(translate.X, translate.Y, 0f);
            gl.Scale(zoomFactor, zoomFactor, 1f);
             * */            
            gl.Begin(BeginMode.Quads);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    if (plane.Act[i, j] == false)
                        continue;
                    Color color = prop.Scale.Color(plane.Values[i, j]);
                    //Color color = (curGrid.Cells[i, j, k].Act == false) ? Color.Black : curProp.Scale.ValueColor(curProp.Values[i, j, k]);
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, propPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, propPlaneDepth);
                }
            gl.End();
        }




        



        void Draw(GridPlane plane)
        {
            int nx = plane.NX();
            int ny = plane.NY();
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.Begin(BeginMode.Lines);
            gl.Color(Color.Gray);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    /*
                    if (CurrentGridPlane.Act[i, j] == false)
                        continue;
                    */
                    // 1
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, gridLinesPlaneDepth);
                    // 2
                    gl.Vertex(plane.Faces[i, j].Corners[1].X, plane.Faces[i, j].Corners[1].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, gridLinesPlaneDepth);
                    // 3
                    gl.Vertex(plane.Faces[i, j].Corners[3].X, plane.Faces[i, j].Corners[3].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, gridLinesPlaneDepth);
                    // 4
                    gl.Vertex(plane.Faces[i, j].Corners[2].X, plane.Faces[i, j].Corners[2].Y, gridLinesPlaneDepth);
                    gl.Vertex(plane.Faces[i, j].Corners[0].X, plane.Faces[i, j].Corners[0].Y, gridLinesPlaneDepth);
                }
            gl.End();
        }




        








        /*
        void Draw(List<Well> wells)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            foreach (Well well in wells)
            {
                if (!well.Checked) continue;
                //Point3D point = CurrentGridPlane.Faces[ - 1, well.Location.Y - 1].Center();
                PointF winPoint = ProjToWinCoord(well.Location.X, well.Location.Y);
                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2, wellColor.R, wellColor.G, wellColor.B, "", 10, well.Name);
                gl.Begin(BeginMode.Points);
                gl.Color(wellColor.R, wellColor.G, wellColor.B);
                gl.Vertex(well.Location.X, well.Location.Y);
                gl.End();
            }
        }
        */


        Color wellColor = Color.Goldenrod;
        WellsPlane CurrWellsPlane;

        void Draw(WellsPlane plane)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            foreach (WellFace well in CurrWellsPlane.Faces)
            {
                if (!well.Checked) continue;
                // title
                PointF winPoint = ProjToWinCoord((float)well.Trajectory[0].X, (float)well.Trajectory[0].Y);
                gl.DrawText((int)winPoint.X + 2, (int)winPoint.Y + 2, wellColor.R, wellColor.G, wellColor.B, "", 10, well.Title);
                int i, count = well.Trajectory.Count();
                // points
                gl.Color(wellColor);
                gl.Begin(BeginMode.Points);
                for (i = 0; i < count; ++i)
                {
                    gl.Vertex(well.Trajectory[i].X, well.Trajectory[i].Y, wellLinesPlaneDepth);
                }
                gl.End();
                // lintes
                gl.Color(wellColor);
                gl.Begin(BeginMode.Lines);
                for (i = 1; i < count; ++i)
                {
                    gl.Vertex(well.Trajectory[i - 1].X, well.Trajectory[i - 1].Y, wellLinesPlaneDepth);
                    gl.Vertex(well.Trajectory[i - 0].X, well.Trajectory[i - 0].Y, wellLinesPlaneDepth);
                }
                gl.End();
            }
        }


        void UpdateWellsPlane()
        {
            if (CurrGrid != null && CurrLeyers != null && CurrLeyers.Count() > 0)// && wells != null && wells.Count() > 0)
            {
                CurrWellsPlane = new WellsPlane(CurrGrid, CurrLeyers);
            }
            else
            {
                CurrWellsPlane = null;
            }
        }









        void DrawCircle(double cx, double cy, double r, int num_segments, Color color, double z)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.Begin(BeginMode.LineLoop);
            gl.Color(color);
            for (int ii = 0; ii < num_segments; ii++)
            {
                double theta = 2.0f * 3.1415926f * (double)ii / (double)num_segments; //get the current angle 
                double x = r * Math.Cos(theta); //calculate the x component 
                double y = r * Math.Sin(theta); //calculate the y component 
                gl.Vertex(x + cx, y + cy, z); //output vertex 
            }
            gl.End();
        }





        







        void HomePosition()
        {
            if (CurrGridPlane == null)
                return;
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;

            double viewWidth = this.openGLControl_main.Width;
            double viewHeight = this.openGLControl_main.Height;

            ///double viewWidth = gl.RenderContextProvider.Width;
            ///double viewHeight = gl.RenderContextProvider.Height;

            //float viewHeight = (float)mainOpenGLControl.Size.Height;
            //float viewWidth = (float)mainOpenGLControl.Size.Width;

            double gridMinX = CurrGridPlane.MinX();
            double gridMaxX = CurrGridPlane.MaxX();
            double gridMinY = CurrGridPlane.MinY();
            double gridMaxY = CurrGridPlane.MaxY();
            double gridWidth = gridMaxX - gridMinX;
            double gridHeight = gridMaxY - gridMinY;
            CurrModel.Position.Z = Math.Min(viewHeight / gridHeight, viewWidth / gridWidth);
            CurrModel.Position.X = -gridMinX * CurrModel.Position.Z + viewWidth / 2 - gridWidth * CurrModel.Position.Z / 2;
            CurrModel.Position.Y = -gridMinY * CurrModel.Position.Z + viewHeight / 2 - gridHeight * CurrModel.Position.Z / 2;
        }

        


        



        int SearchRightPoint(List<Point2D> points)
        {
            Point2D right = points[0];
            int num = 0;
            for (int i = 1; i < points.Count; ++i)
                if (points[i].X > right.X)
                {
                    right = points[i];
                    num = i;
                }
            return num;
        }




        int CheckIntersection(Point2D a1, Point2D a2, Point2D b1, Point2D b2)
        {
            double eps = 0.000001f;
            double d = (a1.X - a2.X) * (b2.Y - b1.Y) - (a1.Y - a2.Y) * (b2.X - b1.X);
            double da = (a1.X - b1.X) * (b2.Y - b1.Y) - (a1.Y - b1.Y) * (b2.X - b1.X);
            double db = (a1.X - a2.X) * (a1.Y - b1.Y) - (a1.Y - a2.Y) * (a1.X - b1.X);
            if (Math.Abs(d) < eps)
                return 0;
            else
            {
                double ta = da / d;
                double tb = db / d;
                if ((Math.Abs(ta) < eps) && ((0 <= tb) && (tb <= 1)))
                    return 2;
                else
                    if ((0 <= ta) && (0 <= tb) && (tb <= 1))
                        return 1;
                    else return -1;
            }
        }



        


        
        bool IsPointInsidePolygon(Point2D[] p, double x, double y)
        {
            int i1, i2, N;
            double S, S1, S2, S3;
            bool flag = false;
            N = p.Length;
            for (int n = 0; n < N; n++)
            {
                flag = false;
                i1 = n < N - 1 ? n + 1 : 0;
                while (!flag)
                {
                    i2 = i1 + 1;
                    if (i2 >= N)
                        i2 = 0;
                    if (i2 == (n < N - 1 ? n + 1 : 0))
                        break;
                    S = Math.Abs(p[i1].X * (p[i2].Y - p[n].Y) +
                             p[i2].X * (p[n].Y - p[i1].Y) +
                             p[n].X * (p[i1].Y - p[i2].Y));
                    S1 = Math.Abs(p[i1].X * (p[i2].Y - y) +
                              p[i2].X * (y - p[i1].Y) +
                              x * (p[i1].Y - p[i2].Y));
                    S2 = Math.Abs(p[n].X * (p[i2].Y - y) +
                              p[i2].X * (y - p[n].Y) +
                              x * (p[n].Y - p[i2].Y));
                    S3 = Math.Abs(p[i1].X * (p[n].Y - y) +
                              p[n].X * (y - p[i1].Y) +
                              x * (p[i1].Y - p[n].Y));
                    if (S == S1 + S2 + S3)
                    {
                        flag = true;
                        break;
                    }
                    i1 = i1 + 1;
                    if (i1 >= N)
                        i1 = 0;
                }
                if (!flag)
                    break;
            }
            return flag;
        }








        private void mainOpenGLControl_MouseEnter(object sender, EventArgs e)
        {

        }



        private void mainOpenGLControl_MouseLeave(object sender, EventArgs e)
        {
            coordToolStripStatusLabel.Text = string.Empty;
        }





        private void mainOpenGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && AddModifierMode)
            {
                AddModifiersGroup(new ModifiersGroup(modifierForm.Title, CurrMouseLocationF, modifierForm.Radius, modifierForm.Value, CurrProp.NZ()), false);
                GenerateModTitle();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left && AddModifierModeOnSelectedMode)
            {
                AddModifiersGroup(new ModifiersGroup(modifierForm.Title, CurrMouseLocationF, modifierForm.Radius, modifierForm.Value, CurrProp.NZ()), true);
                GenerateModTitle();
            }
        }



        void AddModifiersGroup(ModifiersGroup mgroup, bool selected)
        {
            Cursor.Current = Cursors.WaitCursor;
            CurrProp.Groups.Add(mgroup);
            int i = (int)CurrProp.Groups.Count() - 1;
            TreeNode node = new TreeNode(mgroup.Title);
            node.Checked = true;
            node.Tag = i;
            if (!selected)
            {
                this.treeView_modifiersGroups.Nodes.Add(node);
            }
            else
            {
                TreeNode folder = this.treeView_modifiersGroups.SelectedNode;
                if (this.treeView_modifiersGroups.SelectedNode.Tag != null)
                {
                    TreeNode clone = new TreeNode(folder.Text);
                    clone.Tag = folder.Tag;
                    clone.Checked = folder.Checked;
                    folder.Text = "folder_" + (imodfolder++).ToString();
                    folder.Tag = null;
                    folder.Nodes.Add(clone);
                }
                folder.Nodes.Add(node);
                folder.Expand();
            }
            CurrProp.Apply(CurrGrid, i);
            //CurrProp.UpdateScale();
            UpdateGridPlane();
            Cursor.Current = Cursors.Default;
        }

        static int imodfolder = 1;
        static int imod = 1;
        void GenerateModTitle()
        {
            this.modifierForm.Title = "mod_" + (imod++).ToString();
        }



        Point PrevMouseLocation = new Point();
        private void mainOpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.NoMove2D;
                PrevMouseLocation.X = e.X;
                PrevMouseLocation.Y = e.Y;
                //prevLoc = WinToProjCoord(e.X, e.Y);
            }
        }






        Point CurrCellIndex = new System.Drawing.Point();





        Point CurrMouseLocation = new Point();
        private void mainOpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedLeyers.Count() == 0)
                return;
            CurrMouseLocation.X = e.X;
            CurrMouseLocation.Y = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                CurrModel.Position.X += +(CurrMouseLocation.X - PrevMouseLocation.X);
                CurrModel.Position.Y += -(CurrMouseLocation.Y - PrevMouseLocation.Y); // в View ось у направлена вниз
                PrevMouseLocation.X = CurrMouseLocation.X;
                PrevMouseLocation.Y = CurrMouseLocation.Y;
            }
            else
            {
                ShowCurrLocation(CurrMouseLocation);
            }
        }



        Point2D CurrMouseLocationF = new Point2D();

        void ShowCurrLocation(Point winLoc)
        {
            CurrMouseLocationF = WinToProjCoord(winLoc.X, winLoc.Y);
            coordToolStripStatusLabel.Text = "X=" + CurrMouseLocationF.X.ToString() + " Y=" + CurrMouseLocationF.Y.ToString();
            if (CurrGridPlane == null)
                return;
            CurrCellIndex = CurrGridPlane.Index(CurrMouseLocationF);
            if (CurrCellIndex == GridPlane.UndefIndex)
            {
                toolStripTextBox_cellValue.Text = string.Empty;
                toolStripTextBox_index.Text = string.Empty;
            }
            else
            {
                toolStripTextBox_index.Text = "I=" + (CurrCellIndex.X + 1).ToString() + " J=" + (CurrCellIndex.Y + 1).ToString();
                toolStripTextBox_cellValue.Text = CurrGridPlane.Values[CurrCellIndex.X, CurrCellIndex.Y].ToString();
            }
        }






        Point2D WinToProjCoord(int xWin, int yWin)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            double[] p = gl.UnProject(xWin, (gl.RenderContextProvider.Height - yWin), 0.0);
            return new Point2D(p[0], p[1]);
        }





        PointF ProjToWinCoord(float xProj, float yProj)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            Vertex v = gl.Project(new Vertex(xProj, yProj, 0));
            return new PointF(v.X, v.Y);
        }





        const int wheelDelta = 120;
        private void mainOpenGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomInOut(true, Math.Abs(e.Delta) / wheelDelta, e.Location);
            else
                ZoomInOut(false, Math.Abs(e.Delta) / wheelDelta, e.Location);
        }




        private void ZoomInOut(bool zoom, int times, Point refPoint)
        {
            const double zoomRatio = 1.05f;
            double mult = 1f;
            if (zoom)
                for (int i = 0; i < times; ++i)
                    mult *= zoomRatio;
            else
                for (int i = 0; i < times; ++i)
                    mult /= zoomRatio;
            CurrModel.Position.Z *= mult;
            //const float minZoomFactor = 0.000001f;
            //zoomFactor = Math.Max(zoomFactor, minZoomFactor);
            Point2D projPoint = WinToProjCoord(refPoint.X, refPoint.Y);
            double scale = (1 - mult) / mult * CurrModel.Position.Z;
            CurrModel.Position.X += projPoint.X * scale;
            CurrModel.Position.Y += projPoint.Y * scale;
            ////UpdateMainOpenGLControl();
            ////mainOpenGLControl_OpenGLDraw(null, null);
        }





        private void homeToolStripButton_Click(object sender, EventArgs e)
        {
            HomePosition();
        }





        private void mainOpenGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            UpdateMainOpenGLControl();
        }




        private void mainOpenGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
            gl.Viewport(0, 0, openGLControl_main.Size.Width, openGLControl_main.Size.Height);
        }





        const double orthoLeft = 0;
        const double orthoRight = 1000;
        const double orthoBottom = 0;
        const double orthoTop = 1000;
        const double orthoZNear = 1000;
        const double orthoZFar = -1000;
        double curOrthoLeft, curOrthoRight, curOrthoBottom, curOrthoTop, aspect;

        private void mainOpenGLControl_Resized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl_main.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            aspect = (double)gl.RenderContextProvider.Width / (double)gl.RenderContextProvider.Height;
            if (aspect > 1)
            {
                curOrthoLeft = -orthoLeft;
                curOrthoRight = orthoRight;
                curOrthoBottom = -orthoBottom / aspect;
                curOrthoTop = orthoTop / aspect;
            }
            else
            {
                curOrthoLeft = -orthoLeft * aspect;
                curOrthoRight = orthoRight * aspect;
                curOrthoBottom = -orthoBottom;
                curOrthoTop = orthoTop;
            }
            gl.Ortho(curOrthoLeft, curOrthoRight, curOrthoBottom, curOrthoTop, orthoZNear, orthoZFar);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }







        




        

        



        const double shiftFrac = 0.01f;
        private void mainOpenGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            double gridMinX = CurrGridPlane.MinX();
            double gridMaxX = CurrGridPlane.MaxX();
            double gridMinY = CurrGridPlane.MinY();
            double gridMaxY = CurrGridPlane.MaxY();
            double gridHeight = gridMaxY - gridMinY;
            double gridWidth = gridMaxX - gridMinX;
            double min = Math.Min(gridWidth, gridHeight);
            switch (e.KeyCode)
            {
                case Keys.W: // Up
                    CurrModel.Position.Y -= -(min * shiftFrac); // у View ось у направлена вниз
                    break;
                case Keys.S: // Down
                    CurrModel.Position.Y += -(min * shiftFrac); // у View ось у направлена вниз
                    //NextLayer();
                    break;
                case Keys.D: // Right
                    CurrModel.Position.X += +(min * shiftFrac);
                    break;
                case Keys.A: // Left
                    CurrModel.Position.X -= +(min * shiftFrac);
                    break;
                case Keys.Q:
                    PrevLayer();
                    ShowCurrLocation(CurrMouseLocation);
                    break;
                case Keys.E:
                    NextLayer();
                    ShowCurrLocation(CurrMouseLocation);
                    break;
            }
        }



        void NextLayer()
        {
            int[] selected = SelectedLeyers;
            int count = selected.Count();
            int last = count - 1;
            if (count == 0)
                return;
            listBox_layers.ClearSelected();
            listBox_layers.SelectedIndex = Math.Min(selected[last] + 1, listBox_layers.Items.Count - 1);
        }


        void PrevLayer()
        {
            int[] selected = SelectedLeyers;
            int count = selected.Count();
            if (count == 0)
                return;
            listBox_layers.ClearSelected();
            listBox_layers.SelectedIndex = Math.Max(selected[0] - 1, 0);
        }





        private void mainOpenGLControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            switch (e.KeyChar)
            {
                case 'w': // up
                case 'W': // up
                    break;
                case Keys.S: // down
                    break;
                case Keys.A: // left
                    break;
                case Keys.D: // right
                    break;
            }
            */
        }








        private static Assembly CompileSourceCodeDom(string sourceCode)
        {
            CodeDomProvider cpd = new CSharpCodeProvider();
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.GenerateExecutable = false;
            CompilerResults cr = cpd.CompileAssemblyFromSource(cp, sourceCode);
            return cr.CompiledAssembly;
        }




        private static void ExecuteFromAssembly(Assembly assembly)
        {
            Type fooType = assembly.GetType("Foo");
            MethodInfo printMethod = fooType.GetMethod("Print");
            object foo = assembly.CreateInstance("Foo");
            printMethod.Invoke(foo, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
        }




        




        public bool ShowGridLines
        {
            set
            {
                if (value)
                    toolStripButton_gridLines.CheckState = CheckState.Checked;
                else
                    toolStripButton_gridLines.CheckState = CheckState.Unchecked;
            }
            get
            {
                return toolStripButton_gridLines.CheckState == CheckState.Checked;
            }
        }




       





        private void mainOpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Cursor.Current = Cursors.Default;
            }
        }




        





        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurrProp == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*" + "|" + "CMG File (*.*)|*.*";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            FileType type = dialog.FilterIndex == 1 ? FileType.GRDECL_ASCII : FileType.CMG_ASCII;
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            if (CurrProp.Write(CurrProp.Title, filename, type))
                msg = "Property Exported Successfully!";
            else
                msg = "Export Error! Abort operation.";
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
        }








        CommandForm commandForm = new CommandForm();
        private void commandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandForm.Show(this);
        }



        void commandForm_Execute(object sender, EventArgs e)
        {
            commandForm.Execute(CurrProject);
            UpdateProject(CurrProject);
        }




        public void Execute(string code)
        {
            code =
            "using System; " +
            "using System.Windows.Forms; " +
            "using GeoEdit; " +
            "using System.Drawing;" +
            "namespace MyNamespace" +
            "{ " +
            "   public class MyClass " +
            "   { " +
            "        public MyClass(Project p) { project = p; }" +
            "        public Project project {set;get;}" +
            "        public void Func()" +
            "        { " +
                        code +
            "        } " +
            "   } " +
            "} ";
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

            object myClass =
                res.CompiledAssembly.CreateInstance("MyNamespace.MyClass", false, BindingFlags.CreateInstance, null,
                                                    new object[] { CurrProject }, CultureInfo.CurrentCulture, null);

            myClass.GetType().GetMethod("Func").Invoke(myClass, new object[] { });
            //UpdateProject(CurrProject);
        }





        //abscissae
        //ordinate








            
        


        



        

        





        private void treeView_modifiers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void treeView_modifiers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Modifier m = (Modifier)e.Node.Tag;
            //modifierForm.Show(m);
        }





        







        Project CurrProject { set; get; }
        Model CurrModel { set; get; }
        Grid CurrGrid { set; get; }
        Prop CurrProp { set; get; }
        ModifiersGroup CurrModifiersGroup { set; get; }
        int[] CurrLeyers { set; get; }







        void UpdateProject(Project newProject)
        {
            UpdateModels(newProject.Models);
            CurrProject = newProject;
        }





        void UpdateModels(List<Model> models)
        {
            listBox_models.Items.Clear();
            foreach (Model model in models)
                listBox_models.Items.Add(model.Title);
            UpdateModel(null);
        }



        void UpdateModel(Model model)
        {
            // model
            this.toolStripButton_removeModel.Enabled = (model == null) ? false : true;
            this.toolStripButton_renameModel.Enabled = (model == null) ? false : true;
            // grid
            this.toolStripButton_createGrid.Enabled = (model == null) ? false : true;
            this.toolStripButton_importGrid.Enabled = (model == null) ? false : true;
            // view
            this.openGLControl_main.Enabled = (model == null) ? false : true;
            //
            if (model != null)
            {
                //UpdateWells(model.Wells);
                UpdateGrids(model.Grids);
            }
            else
            {
                //UpdateWells(new List<Well>());
                UpdateGrids(new List<Grid>());
            }
            this.CurrModel = model;
        }




        void UpdateWells(List<Compdat> wells) 
        {
            this.treeView_wells.Nodes.Clear();
            for (int i = 0; i < wells.Count; ++i)
            {
                TreeNode node = new TreeNode(wells[i].Title);
                node.Checked = wells[i].Checked;
                node.Tag = i;
                this.treeView_wells.Nodes.Add(node);
            }
            UpdateWell(null);
        }


        void UpdateWell(Well well)
        {
            // well
            this.toolStripButton_removeWells.Enabled = (well == null) ? false : true;
            this.toolStripButton_renameWells.Enabled = (well == null) ? false : true;
        }




        void UpdateGrids(List<Grid> grids) 
        {
            listBox_grids.Items.Clear();
            foreach (Grid grid in grids)
                listBox_grids.Items.Add(grid.Title);
            UpdateGrid(null);
        }


        void UpdateGrid(Grid grid)
        {
            // grid
            this.toolStripButton_renameGrid.Enabled = (grid == null) ? false : true;
            this.toolStripButton_removeGrid.Enabled = (grid == null) ? false : true;
            this.toolStripButton_exportGrid.Enabled = (grid == null) ? false : true;
            // prop
            this.toolStripButton_createProp.Enabled = (grid == null) ? false : true;
            this.toolStripButton_importProp.Enabled = (grid == null) ? false : true;
            // wells
            this.toolStripButton_importWells.Enabled = (grid == null) ? false : true;
            //
            if (grid != null)
            {
                UpdateLayers(grid.NZ());
                UpdateProps(grid.Props);
                UpdateWells(grid.Wells);
            }
            else
            {
                UpdateLayers(0);
                UpdateProps(new List<GeoEdit.Prop>());
                UpdateWells(new List<Compdat>());
            }
            this.CurrGrid = grid;
        }



        void UpdateLayers(int n) 
        {
            CurrLeyers = null;
            listBox_layers.Items.Clear();
            for (int i = 0; i < n; ++i)
                listBox_layers.Items.Add("Layer " + (i + 1).ToString());
            UpdateGridPlane();
        }



        void UpdateProps(List<Prop> props) 
        {
            CurrProp = null;
            listBox_props.Items.Clear();
            foreach (Prop prop in props)
                listBox_props.Items.Add(prop.Title);
            UpdateProp(null);
        }



        void UpdateProp(Prop prop)
        {
            this.CurrProp = prop;
            // prop
            this.toolStripButton_renameProp.Enabled = (prop == null) ? false : true; 
            this.toolStripButton_removeProp.Enabled = (prop == null) ? false : true;
            this.toolStripButton_exportProp.Enabled = (prop == null) ? false : true;
            // modifiers
            this.toolStripButton_addModifier.Enabled = (prop == null) ? false : true;
            // layers
            this.listBox_layers.Enabled = (prop == null) ? false : true;
            //
            if (prop != null)
            {
                UpdateModifiersGroups(prop.Groups);
            }
            else
            {
                UpdateModifiersGroups(null);
            }
        }




        //bool updateModifiersList;
        void UpdateModifiersGroups(List<ModifiersGroup> mgroups)
        {
            //updateModifiersList = true;
            this.treeView_modifiersGroups.Nodes.Clear();
            if(mgroups != null)
                for (int i = 0; i < mgroups.Count; ++i)
                {
                    TreeNode node = new TreeNode(mgroups[i].Title);
                    node.Checked = mgroups[i].Applied;
                    node.Tag = i;
                    this.treeView_modifiersGroups.Nodes.Add(node);
                }
            //updateModifiersList = false;
            UpdateModifiersGroup(null);
        }





        void UpdateModifiersGroup(ModifiersGroup mgroup)
        {
            // modofier groups
            this.toolStripButton_addModOnSelected.Enabled = (mgroup == null) ? false : true;
            this.toolStripButton_removeModifier.Enabled   = (mgroup == null) ? false : true;
            this.toolStripButton_renameModifier.Enabled   = (mgroup == null) ? false : true;
            //
            this.dataGridView_modifiersGroup.Rows.Clear();
            if (mgroup != null)
            {
                int nz = CurrProp.NZ();
                for (int k = 0; k < nz; ++k)
                {
                    this.dataGridView_modifiersGroup.Rows.Add(new object[] 
                                                { mgroup.Modifiers[k].Value, mgroup.Modifiers[k].Radius, mgroup.Modifiers[k].Use });
                    this.dataGridView_modifiersGroup.Rows[k].HeaderCell.Value = "K " + (k + 1).ToString();
                }
                CurrModifiersGroup = mgroup;
            }
        }


        private void modelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.listBox_models.SelectedIndex;
            if (i >= 0)
                UpdateModel(this.CurrProject.Models[i]);
            else
                UpdateModel(null);
        }

        

        private void gridsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.listBox_grids.SelectedIndex;
            if (i >= 0)
                UpdateGrid(this.CurrModel.Grids[i]);
            else
                UpdateGrid(null);
        }






        private void propsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.listBox_props.SelectedIndex;
            if (i >= 0)
                UpdateProp(this.CurrGrid.Props[i]);
            else
                UpdateProp(null);
            this.UpdateGridPlane();
            this.UpdateWellsPlane();
        }






        private void layersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CurrLeyers = SelectedLeyers;
            UpdateGridPlane();
            this.UpdateWellsPlane();
            ShowCurrLocation(CurrMouseLocation);
            Cursor.Current = Cursors.Default;
        }




        

        int[] SelectedLeyers
        {
            get
            {
                int[] result = new int[listBox_layers.SelectedIndices.Count];
                int n = 0;
                foreach (int index in listBox_layers.SelectedIndices)
                    result[n++] = index;
                return result;
            }
        }

        






        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = String.Format("Modifiers File (*.{0})|*.{0}", "txt");
            dialog.DefaultExt = "txt";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == null || filename == string.Empty)
                return;
            try
            {
                List<string> lines = new List<string>();
                lines.Add(Modifier.TextHeader);
                foreach (Modifier mod in CurrProp.Modifiers) { lines.Add(mod.TextForm()); }
                System.IO.File.WriteAllLines(filename, lines.ToArray(), Encoding.GetEncoding(1251));
            }
            catch(Exception ex)
            {
                //
            }
            */
        }




        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = String.Format("Modifiers File (*.{0})|*.{0}", "txt");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == null && filename == string.Empty)
                return;
            foreach (Modifier mod in Modifier.Read(filename))
            {
                AddModifier(mod, false);
            }
            */
        }

        private void mainForm_DragDrop(object sender, DragEventArgs e)
        {
            
        }



        private void toolStripButton_addModOnSelected_Click(object sender, EventArgs e)
        {
            AddModifierModeOnSelectedMode = this.toolStripButton_addModOnSelected.Checked;
        }



        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = String.Format("Modifiers File (*.{0})|*.{0}", "txt");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == null && filename == string.Empty)
                return;
            while(CurrProp.Modifiers.Count() != 0)
            {
                Modifier mod = CurrProp.Modifiers.Last();
                RemoveModifier(ref mod);
            }
            foreach (Modifier mod in Modifier.Read(filename))
            {
                AddModifier(mod, false);
            }
            */
        }




        private void treeView_modifiers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (CurrProp != null)
            {
                if (e.Node.Tag == null)
                    return;
                Cursor.Current = Cursors.WaitCursor;
                int i = (int)e.Node.Tag;
                CurrProp.Apply(CurrGrid, i);
                //CurrProp.UpdateScale();
                /*
                foreach (int layer in CurrLeyers)
                {
                    if (CurrProp.Modifiers[i].Layers.Contains(layer))
                    {
                        UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
                        break;
                    }
                }
                */
                UpdateGridPlane();
                Cursor.Current = Cursors.Default;
            }
        }




        void Fill(ref TreeNode parent, ref List<int> result)
        {
            if (parent.Nodes.Count == 0)
                result.Add((int)parent.Tag);
            else
                for (int i = 0; i < parent.Nodes.Count; ++i)
                {
                    TreeNode node = parent.Nodes[i];
                    node.Checked = parent.Checked;
                    Fill(ref node, ref result);
                }
        }




        const int col_value  = 0;
        const int col_radius = 1;
        const int col_use    = 2;

        private void dataGridView_modifiersGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrModifiersGroup == null ||
                CurrModifiersGroup.Modifiers.Count() != this.dataGridView_modifiersGroup.Rows.Count ||
                e.ColumnIndex < 0 ||
                this.dataGridView_modifiersGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            Cursor.Current = Cursors.WaitCursor;


            string value = this.dataGridView_modifiersGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            Modifier modifier = CurrModifiersGroup.Modifiers[e.RowIndex];
            if (CurrModifiersGroup.Applied && modifier.Use)
                CurrProp.Apply(CurrGrid, modifier, false);
            switch (e.ColumnIndex)
            {
                case col_value:
                    modifier.Value = double.Parse(value);
                    break;
                case col_radius:
                    modifier.Radius = double.Parse(value);
                    break;
                case col_use:
                    modifier.Use = bool.Parse(value);
                    break;
            }
            if (CurrModifiersGroup.Applied && modifier.Use)
                CurrProp.Apply(CurrGrid, modifier, true);
            if (CurrModifiersGroup.Applied)
            {
                //CurrProp.UpdateScale();
                UpdateGridPlane();
            }
            
            Cursor.Current = Cursors.Default;
        }



        /*
        private void dataGridView_modifiersGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrModifiersGroup == null ||
                CurrModifiersGroup.Modifiers.Count() != this.dataGridView_modifiersGroup.Rows.Count ||
                e.ColumnIndex < 0 ||
                this.dataGridView_modifiersGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            Cursor.Current = Cursors.WaitCursor;


            string value = this.dataGridView_modifiersGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            foreach (DataGridCell cell in this.dataGridView_modifiersGroup.SelectedCells)
            {
                if (cell.ColumnNumber == e.ColumnIndex)
                {
                    Modifier modifier = CurrModifiersGroup.Modifiers[cell.RowNumber];
                    if (CurrModifiersGroup.Applied && modifier.Use)
                        CurrProp.Apply(CurrGrid, modifier, false);
                    switch (e.ColumnIndex) // оптимизировать
                    {
                        case col_value:
                            modifier.Value = float.Parse(value);
                            break;
                        case col_radius:
                            modifier.Radius = float.Parse(value);
                            break;
                        case col_use:
                            modifier.Use = bool.Parse(value);
                            break;
                    }
                    if (CurrModifiersGroup.Applied && modifier.Use)
                        CurrProp.Apply(CurrGrid, modifier, true);
                }
            }
            if (CurrModifiersGroup.Applied)
            {
                CurrProp.UpdateScale();
                UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
            }

            Cursor.Current = Cursors.Default;
        }
        */





        private void dataGridView_modifiersGroup_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView_modifiersGroup.IsCurrentCellDirty)
            {
                dataGridView_modifiersGroup.CommitEdit(DataGridViewDataErrorContexts.Commit);
                
            }
            /*
            else
                this.contextMenuStrip_columnUse.Show();
                */
        }

        private void dataGridView_modifiersGroup_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case col_value:
                    e.Cancel = !IsValidValue(e.FormattedValue.ToString());
                    break;
                case col_radius:
                    e.Cancel = !IsValidValue(e.FormattedValue.ToString());
                    break;
            }
        }



        bool IsValidValue(string txtValue)
        {
            double value;
            return double.TryParse(txtValue, out value);
            //return (float.TryParse(txtValue, out value) && value >= 0f);
        }



        SetValueForm setValueFrom = new SetValueForm();
        private void setValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setValueFrom.ShowDialog(this);
        }
        private void SetValueFrom_ApplyEvent(object sender, EventArgs e)
        {
            double value = setValueFrom.Value;
            bool isMult = this.setValueFrom.IsMult;
            foreach (DataGridViewTextBoxCell cell in this.dataGridView_modifiersGroup.SelectedCells)
                if (cell.ColumnIndex == col_value)
                    if (isMult) cell.Value = value * double.Parse(cell.Value.ToString());
                    else        cell.Value = value;
        }


        SetValueForm setRadiusFrom = new SetValueForm();
        private void setRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setRadiusFrom.ShowDialog(this);
        }
        private void SetRadiusFrom_ApplyEvent(object sender, EventArgs e)
        {
            double value = setRadiusFrom.Value;
            bool isMult = this.setRadiusFrom.IsMult;
            foreach (DataGridViewTextBoxCell cell in this.dataGridView_modifiersGroup.SelectedCells)
                if (cell.ColumnIndex == col_radius)
                    if (isMult) cell.Value = value * double.Parse(cell.Value.ToString());
                    else        cell.Value = value;
        }


        SetCheckForm setUseForm = new SetCheckForm();
        private void setUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setUseForm.ShowDialog(this);
        }


        SetNameForm setModelNameForm = new SetNameForm();
        private void toolStripButton_renameModel_Click(object sender, EventArgs e)
        {
            setModelNameForm.Value = CurrModel.Title;
            setModelNameForm.ShowDialog(this);
        }
        private void SetModelNameForm_ApplyEvent(object sender, EventArgs e)
        {
            CurrModel.Title = setModelNameForm.Value;
            this.UpdateModels(CurrProject.Models);
        }



        private void toolStripButton_removeModel_Click(object sender, EventArgs e)
        {
            string message = string.Format("Are you sure that you would like to delete the {0} model?", CurrModel.Title);
            string caption = "Model Deleting";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            Cursor.Current = Cursors.WaitCursor;
            CurrProject.Models.Remove(CurrModel);
            UpdateModels(CurrProject.Models);
            Cursor.Current = Cursors.Default;
        }


        SetNameForm setGridNameForm = new SetNameForm();
        private void toolStripButton_renameGrid_Click(object sender, EventArgs e)
        {
            setGridNameForm.Value = CurrGrid.Title;
            setGridNameForm.ShowDialog(this);
        }

        private void SetGridNameForm_ApplyEvent(object sender, EventArgs e)
        {
            CurrGrid.Title = setGridNameForm.Value;
            this.UpdateGrids(CurrModel.Grids);
        }



        private void toolStripButton_removeGrid_Click(object sender, EventArgs e)
        {
            string message = string.Format("Are you sure that you would like to delete the {0} grid?", CurrGrid.Title);
            string caption = "Grid Deleting";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            Cursor.Current = Cursors.WaitCursor;
            CurrGridPlane = null;
            CurrModel.Grids.Remove(CurrGrid);
            this.UpdateGrids(CurrModel.Grids);
            Cursor.Current = Cursors.Default;
        }


        SetNameForm setPropNameForm = new SetNameForm();
        private void toolStripButton_renameProp_Click(object sender, EventArgs e)
        {
            setPropNameForm.Value = CurrProp.Title;
            setPropNameForm.ShowDialog(this);
        }

        private void SetPropNameForm_ApplyEvent(object sender, EventArgs e)
        {
            CurrProp.Title = setPropNameForm.Value;
            this.UpdateProps(CurrGrid.Props);
        }

        private void toolStripButton_removeProp_Click(object sender, EventArgs e)
        {
            string message = string.Format("Are you sure that you would like to delete the {0} prop?", CurrProp.Title);
            string caption = "Prop Deleting";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            Cursor.Current = Cursors.WaitCursor;
            CurrGridPlane = null;
            CurrGrid.Props.Remove(CurrProp);
            this.UpdateProps(CurrGrid.Props);
            Cursor.Current = Cursors.Default;
        }

        private void toolStripButton_exportGrid_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*" + "|" + "CMG File (*.*)|*.*";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            FileType type = dialog.FilterIndex == 1 ? FileType.GRDECL_ASCII : FileType.CMG_ASCII;
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            if (CurrGrid.Write(filename, type))
                msg = "Property Exported Successfully!";
            else
                msg = "Export Error! Abort operation.";
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
        }



        private void SetUseForm_ApplyEvent(object sender, EventArgs e)
        {
            bool value = this.setUseForm.Value;
            foreach (DataGridViewCheckBoxCell cell in this.dataGridView_modifiersGroup.SelectedCells)
                if (cell.ColumnIndex == col_use)
                    cell.Value = value;
        }


        ScalePropForm scalePropForm = new ScalePropForm();
        private void toolStripButton_propScale_Click(object sender, EventArgs e)
        {
            if (CurrProp == null) return;
            scalePropForm.PropScale = CurrProp.Scale;
            scalePropForm.ShowDialog(this);
        }
        private void ScalePropForm_ApplyEvent(object sender, EventArgs e)
        {
            CurrProp.Scale = scalePropForm.PropScale;
            this.UpdateGridPlane();
        }

        private void toolStripButton_propExport_Click(object sender, EventArgs e)
        {
            if (CurrProp == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*" + "|" + "CMG File (*.*)|*.*";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == string.Empty)
                return;
            FileType type = dialog.FilterIndex == 1 ? FileType.GRDECL_ASCII : FileType.CMG_ASCII;
            Cursor.Current = Cursors.WaitCursor;
            string msg;
            if (CurrProp.Write(CurrProp.Title, filename, type))
                msg = "Property Exported Successfully!";
            else
                msg = "Export Error! Abort operation.";
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg);
        }

        private void ScalePropForm_GetFromPropEvent(object sender, EventArgs e)
        {
            scalePropForm.PropScale = new GeoEdit.PropScale(CurrProp.Min(), CurrProp.Max());
        }



        /*
        private void treeView_modifiers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (CurrProp != null && !updateModifiersList)
            {
                if (e.Node.Tag == null)
                    return;
                Cursor.Current = Cursors.WaitCursor;
                int i = (int)e.Node.Tag;
                CurrProp.Apply(CurrGrid, i);
                CurrProp.UpdateScale();
                foreach (int layer in CurrLeyers)
                {
                    if (CurrProp.Modifiers[i].Layers.Contains(layer))
                    {
                        UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
                        break;
                    }
                }
                UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
                Cursor.Current = Cursors.Default;
            }
        }
        */


        private void treeView_wells_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (CurrModel == null) return;
            CurrGrid.Wells[(int)e.Node.Tag].Checked = e.Node.Checked;
            UpdateWellsPlane();
        }



        private void treeView_modifiers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ModifiersGroupSelected = (e.Node != null && e.Node.Tag != null);
            UpdateModifiersGroup(ModifiersGroupSelected ? CurrProp.Groups[(int)e.Node.Tag] : null);
        }


        bool ModifiersGroupSelected
        {
            set
            {
                this.toolStripButton_addModOnSelected.Enabled   = value;
            }
            get
            {
                return this.toolStripButton_addModOnSelected.Enabled;
            }
        }




        ModifierForm modifierForm = new ModifierForm();
        private void toolStripButton_modifiers_Click(object sender, EventArgs e)
        {
            AddModifierMode = true;
        }

        private void ModifierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddModifierMode = false;
        }

        bool _addModifierMode = false;
        bool AddModifierMode
        {
            get
            {
                return _addModifierMode;
            }
            set
            {
                _addModifierMode = value;
                // modifier
                //this.toolStripButton_addModifier.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
                //this.toolStripButton_addModOnSelected.CheckState = CheckState.Unchecked;
                this.toolStripButton_addModifier.Enabled = !value;
                this.toolStripButton_addModOnSelected.Enabled = !value;
                //this.treeView_modifiersGroups.Enabled = !value;
                //this.toolStripButton_renameModifier.Enabled = !value;
                //this.toolStripButton_removeModifier.Enabled = !value;
                // models
                this.toolStrip_models.Enabled = !value;
                this.listBox_models.Enabled = !value;
                // grids
                this.toolStrip_grids.Enabled = !value;
                this.listBox_grids.Enabled = !value;
                // props
                this.toolStrip_props.Enabled = !value;
                this.listBox_props.Enabled = !value;
                //
                if (value) modifierForm.Show(this);
            }
        }




        bool AddModifierModeOnSelectedMode { set; get; }
        /*
        {
            get
            {
                return this.toolStripButton_addModOnSelected.CheckState == CheckState.Checked;
                //return toolStripButton_modifiers.Checked;
            }
            set
            {
                this.toolStripButton_addModOnSelected.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
                this.toolStripButton_modAddNew.CheckState = CheckState.Unchecked;
                this.toolStripButton_modAddNew.Enabled = !value;
                this.treeView_modifiersGroups.Enabled = !value;
                this.toolStripLabel_modValue.Visible = value;
                this.toolStripTextBox_modValue.Visible = value;
                this.toolStripLabel_modRadius.Visible = value;
                this.toolStripTextBox_modRadius.Visible = value;
                this.toolStripLabel_modTitle.Visible = value;
                this.toolStripTextBox_modTitle.Visible = value;
                this.toolStripButton_modSettings.Enabled = !value;
                this.toolStripButton_modDelete.Enabled = !value;
            }
        }
        */


            





        private void toolStripTextBox_modTitle_TextChanged(object sender, EventArgs e)
        {
            /*
            if(CurrModifier != null)
                CurrModifier.Title = ModifierTitle;
                */
        }




        private void toolStripTextBox_modValue_TextChanged(object sender, EventArgs e)
        {
            /*
            if (CurrModifiersGroup != null)
                CurrModifiersGroup.Value = ModifierValue;
                */
        }




        private void toolStripTextBox_modRadius_TextChanged(object sender, EventArgs e)
        {
            /*
            if (CurrModifiersGroup != null)
                CurrModifiersGroup.Radius = ModifierRadius;
                */
        }



        
        





        void editModifierMenuItem_Click(object sender, EventArgs e)
        {

        }




        void deleteModifierMenuItem_Click(object sender, EventArgs e)
        {

        }







        private void toolStripButton_modDelete_Click(object sender, EventArgs e)
        {
            int i = (int)this.treeView_modifiersGroups.SelectedNode.Tag;
            ModifiersGroup mg = CurrProp.Groups[i];
            string message = string.Format("Are you sure that you would like to delete the {0} modifier?", mg.Title);
            string caption = "Modifier Deleting";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            Cursor.Current = Cursors.WaitCursor;
            if (mg.Applied)
            {
                CurrProp.Apply(CurrGrid, i);
                UpdateGridPlane();
            }
            CurrProp.Groups.RemoveAt(i);
            this.UpdateModifiersGroups(CurrProp.Groups);            
            Cursor.Current = Cursors.Default;
        }




        void RemoveModifier(ref Modifier mod)
        {
            /*
            if (mod.Applied)
                CurrProp.Apply(CurrGrid, (int)this.treeView_modifiers.SelectedNode.Tag);
            CurrProp.Modifiers.Remove(mod);
            UpdateModifiers(CurrProp.Modifiers);
            CurrProp.UpdateScale();
            UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
            */
        }








        
        SetNameForm setModifierNameForm = new SetNameForm();
        private void toolStripButton_modSettings_Click(object sender, EventArgs e)
        {
            setModifierNameForm.Value = CurrModifiersGroup.Title;
            setModifierNameForm.ShowDialog(this);
        }
        private void SetModifierNameForm_ApplyEvent(object sender, EventArgs e)
        {
            CurrModifiersGroup.Title = setModifierNameForm.Value;
            this.UpdateModifiersGroups(CurrProp.Groups);
        }




        /*
        void modifierForm_ApplyEvent(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = (int)this.treeView_modifiersGroups.SelectedNode.Tag;
            if (CurrProp.ModifiersGroups[i].Applied)
            {
                CurrProp.Apply(CurrGrid, i);
                CurrProp.ModifiersGroups[i] = modifierForm.Result();
                CurrProp.Apply(CurrGrid, i);
            }
            else
            {
                CurrProp.ModifiersGroups[i] = modifierForm.Result();
            }
            this.treeView_modifiersGroups.SelectedNode.Text = CurrProp.ModifiersGroups[i].Title;
            CurrProp.UpdateScale();
            UpdateGridPlane(CurrGrid, CurrProp, CurrLeyers);
            Cursor.Current = Cursors.Default;
        }
        void modifierForm_GetFromGrid(object sender, EventArgs e)
        {
            modifierForm.ModifierLayers = CurrLeyers;
        }
        */

            



    }
}

