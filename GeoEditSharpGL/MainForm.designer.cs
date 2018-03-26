namespace GeoEdit
{
    partial class mainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_command = new System.Windows.Forms.ToolStripMenuItem();
            this.modifiersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGLControl_main = new SharpGL.OpenGLControl();
            this.listBox_layers = new System.Windows.Forms.ListBox();
            this.splitContainer_modifiers = new System.Windows.Forms.SplitContainer();
            this.treeView_modifiersGroups = new System.Windows.Forms.TreeView();
            this.dataGridView_modifiersGroup = new System.Windows.Forms.DataGridView();
            this.Column_mult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_columnValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Column_radius = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_columnRadius = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Column_apply = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip_columnUse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_modifiers = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_addModifier = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_addModOnSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_renameModifier = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_removeModifier = new System.Windows.Forms.ToolStripButton();
            this.treeView_wells = new System.Windows.Forms.TreeView();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.splitContainer_project = new System.Windows.Forms.SplitContainer();
            this.splitContainer_model = new System.Windows.Forms.SplitContainer();
            this.splitContainer_models = new System.Windows.Forms.SplitContainer();
            this.listBox_models = new System.Windows.Forms.ListBox();
            this.toolStrip_models = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_addModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_renameModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_removeModel = new System.Windows.Forms.ToolStripButton();
            this.splitContainer_grids = new System.Windows.Forms.SplitContainer();
            this.listBox_grids = new System.Windows.Forms.ListBox();
            this.toolStrip_grids = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_importGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_exportGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_renameGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_removeGrid = new System.Windows.Forms.ToolStripButton();
            this.listBox_props = new System.Windows.Forms.ListBox();
            this.toolStrip_props = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_exportProp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_importProp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createProp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_renameProp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_removeProp = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_wells = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_importWells = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_renameWells = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_removeWells = new System.Windows.Forms.ToolStripButton();
            this.splitContainer_view = new System.Windows.Forms.SplitContainer();
            this.toolStrip_view = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox_cellValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_focus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_gridLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_scale = new System.Windows.Forms.ToolStripButton();
            this.coordToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip_main = new System.Windows.Forms.StatusStrip();
            this.imageList_modifiers = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_modifiers)).BeginInit();
            this.splitContainer_modifiers.Panel1.SuspendLayout();
            this.splitContainer_modifiers.Panel2.SuspendLayout();
            this.splitContainer_modifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_modifiersGroup)).BeginInit();
            this.contextMenuStrip_columnValue.SuspendLayout();
            this.contextMenuStrip_columnRadius.SuspendLayout();
            this.contextMenuStrip_columnUse.SuspendLayout();
            this.toolStrip_modifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_project)).BeginInit();
            this.splitContainer_project.Panel1.SuspendLayout();
            this.splitContainer_project.Panel2.SuspendLayout();
            this.splitContainer_project.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_model)).BeginInit();
            this.splitContainer_model.Panel1.SuspendLayout();
            this.splitContainer_model.Panel2.SuspendLayout();
            this.splitContainer_model.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_models)).BeginInit();
            this.splitContainer_models.Panel1.SuspendLayout();
            this.splitContainer_models.Panel2.SuspendLayout();
            this.splitContainer_models.SuspendLayout();
            this.toolStrip_models.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_grids)).BeginInit();
            this.splitContainer_grids.Panel1.SuspendLayout();
            this.splitContainer_grids.Panel2.SuspendLayout();
            this.splitContainer_grids.SuspendLayout();
            this.toolStrip_grids.SuspendLayout();
            this.toolStrip_props.SuspendLayout();
            this.toolStrip_wells.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_view)).BeginInit();
            this.splitContainer_view.Panel1.SuspendLayout();
            this.splitContainer_view.Panel2.SuspendLayout();
            this.splitContainer_view.SuspendLayout();
            this.toolStrip_view.SuspendLayout();
            this.statusStrip_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_main
            // 
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.toolStripMenuItem_edit,
            this.helpToolStripMenuItem});
            this.menuStrip_main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_main.Name = "menuStrip_main";
            this.menuStrip_main.Size = new System.Drawing.Size(1073, 24);
            this.menuStrip_main.TabIndex = 0;
            this.menuStrip_main.Text = "menuStrip1";
            // 
            // menu_file
            // 
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(37, 20);
            this.menu_file.Text = "&File";
            // 
            // toolStripMenuItem_edit
            // 
            this.toolStripMenuItem_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_command,
            this.modifiersToolStripMenuItem});
            this.toolStripMenuItem_edit.Name = "toolStripMenuItem_edit";
            this.toolStripMenuItem_edit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem_edit.Text = "&Edit";
            // 
            // toolStripMenuItem_command
            // 
            this.toolStripMenuItem_command.Name = "toolStripMenuItem_command";
            this.toolStripMenuItem_command.Size = new System.Drawing.Size(131, 22);
            this.toolStripMenuItem_command.Text = "&Command";
            this.toolStripMenuItem_command.Click += new System.EventHandler(this.commandToolStripMenuItem_Click);
            // 
            // modifiersToolStripMenuItem
            // 
            this.modifiersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.toolStripMenuItem_add,
            this.replaceToolStripMenuItem});
            this.modifiersToolStripMenuItem.Name = "modifiersToolStripMenuItem";
            this.modifiersToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.modifiersToolStripMenuItem.Text = "&Modifiers";
            this.modifiersToolStripMenuItem.Visible = false;
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.exportToolStripMenuItem.Text = "E&xport";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_add
            // 
            this.toolStripMenuItem_add.Name = "toolStripMenuItem_add";
            this.toolStripMenuItem_add.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem_add.Text = "&Add";
            this.toolStripMenuItem_add.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.replaceToolStripMenuItem.Text = "&Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openGLControl_main
            // 
            this.openGLControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl_main.DrawFPS = true;
            this.openGLControl_main.Location = new System.Drawing.Point(0, 0);
            this.openGLControl_main.Name = "openGLControl_main";
            this.openGLControl_main.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl_main.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl_main.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl_main.Size = new System.Drawing.Size(468, 679);
            this.openGLControl_main.TabIndex = 5;
            this.openGLControl_main.OpenGLInitialized += new System.EventHandler(this.mainOpenGLControl_OpenGLInitialized);
            this.openGLControl_main.OpenGLDraw += new SharpGL.RenderEventHandler(this.mainOpenGLControl_OpenGLDraw);
            this.openGLControl_main.Resized += new System.EventHandler(this.mainOpenGLControl_Resized);
            this.openGLControl_main.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainOpenGLControl_KeyDown);
            this.openGLControl_main.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mainOpenGLControl_KeyPress);
            this.openGLControl_main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseClick);
            this.openGLControl_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseDown);
            this.openGLControl_main.MouseEnter += new System.EventHandler(this.mainOpenGLControl_MouseEnter);
            this.openGLControl_main.MouseLeave += new System.EventHandler(this.mainOpenGLControl_MouseLeave);
            this.openGLControl_main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseMove);
            this.openGLControl_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainOpenGLControl_MouseUp);
            // 
            // listBox_layers
            // 
            this.listBox_layers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_layers.FormattingEnabled = true;
            this.listBox_layers.Location = new System.Drawing.Point(0, 0);
            this.listBox_layers.Name = "listBox_layers";
            this.listBox_layers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_layers.Size = new System.Drawing.Size(97, 679);
            this.listBox_layers.TabIndex = 0;
            this.listBox_layers.SelectedIndexChanged += new System.EventHandler(this.layersListBox_SelectedIndexChanged);
            this.listBox_layers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainOpenGLControl_KeyDown);
            // 
            // splitContainer_modifiers
            // 
            this.splitContainer_modifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_modifiers.Location = new System.Drawing.Point(0, 25);
            this.splitContainer_modifiers.Name = "splitContainer_modifiers";
            // 
            // splitContainer_modifiers.Panel1
            // 
            this.splitContainer_modifiers.Panel1.Controls.Add(this.treeView_modifiersGroups);
            // 
            // splitContainer_modifiers.Panel2
            // 
            this.splitContainer_modifiers.Panel2.Controls.Add(this.dataGridView_modifiersGroup);
            this.splitContainer_modifiers.Size = new System.Drawing.Size(354, 679);
            this.splitContainer_modifiers.SplitterDistance = 129;
            this.splitContainer_modifiers.TabIndex = 0;
            // 
            // treeView_modifiersGroups
            // 
            this.treeView_modifiersGroups.CheckBoxes = true;
            this.treeView_modifiersGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_modifiersGroups.Location = new System.Drawing.Point(0, 0);
            this.treeView_modifiersGroups.Name = "treeView_modifiersGroups";
            this.treeView_modifiersGroups.Size = new System.Drawing.Size(129, 679);
            this.treeView_modifiersGroups.TabIndex = 0;
            this.treeView_modifiersGroups.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_modifiers_AfterCheck);
            this.treeView_modifiersGroups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_modifiers_AfterSelect);
            // 
            // dataGridView_modifiersGroup
            // 
            this.dataGridView_modifiersGroup.AllowUserToAddRows = false;
            this.dataGridView_modifiersGroup.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_modifiersGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_modifiersGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_modifiersGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_mult,
            this.Column_radius,
            this.Column_apply});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_modifiersGroup.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_modifiersGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_modifiersGroup.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_modifiersGroup.Name = "dataGridView_modifiersGroup";
            this.dataGridView_modifiersGroup.Size = new System.Drawing.Size(221, 679);
            this.dataGridView_modifiersGroup.TabIndex = 0;
            this.dataGridView_modifiersGroup.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_modifiersGroup_CellValidating);
            this.dataGridView_modifiersGroup.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_modifiersGroup_CellValueChanged);
            this.dataGridView_modifiersGroup.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_modifiersGroup_CurrentCellDirtyStateChanged);
            // 
            // Column_mult
            // 
            this.Column_mult.ContextMenuStrip = this.contextMenuStrip_columnValue;
            this.Column_mult.HeaderText = "Mult";
            this.Column_mult.Name = "Column_mult";
            this.Column_mult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_mult.Width = 50;
            // 
            // contextMenuStrip_columnValue
            // 
            this.contextMenuStrip_columnValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setValueToolStripMenuItem});
            this.contextMenuStrip_columnValue.Name = "contextMenuStrip_columnValue";
            this.contextMenuStrip_columnValue.Size = new System.Drawing.Size(132, 26);
            // 
            // setValueToolStripMenuItem
            // 
            this.setValueToolStripMenuItem.Name = "setValueToolStripMenuItem";
            this.setValueToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.setValueToolStripMenuItem.Text = "Set Value...";
            this.setValueToolStripMenuItem.Click += new System.EventHandler(this.setValueToolStripMenuItem_Click);
            // 
            // Column_radius
            // 
            this.Column_radius.ContextMenuStrip = this.contextMenuStrip_columnRadius;
            this.Column_radius.HeaderText = "Radius";
            this.Column_radius.Name = "Column_radius";
            this.Column_radius.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_radius.Width = 50;
            // 
            // contextMenuStrip_columnRadius
            // 
            this.contextMenuStrip_columnRadius.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setRadiusToolStripMenuItem});
            this.contextMenuStrip_columnRadius.Name = "contextMenuStrip_columnRadius";
            this.contextMenuStrip_columnRadius.Size = new System.Drawing.Size(138, 26);
            // 
            // setRadiusToolStripMenuItem
            // 
            this.setRadiusToolStripMenuItem.Name = "setRadiusToolStripMenuItem";
            this.setRadiusToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.setRadiusToolStripMenuItem.Text = "Set Radius...";
            this.setRadiusToolStripMenuItem.Click += new System.EventHandler(this.setRadiusToolStripMenuItem_Click);
            // 
            // Column_apply
            // 
            this.Column_apply.ContextMenuStrip = this.contextMenuStrip_columnUse;
            this.Column_apply.HeaderText = "Apply";
            this.Column_apply.Name = "Column_apply";
            this.Column_apply.Width = 50;
            // 
            // contextMenuStrip_columnUse
            // 
            this.contextMenuStrip_columnUse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setUseToolStripMenuItem});
            this.contextMenuStrip_columnUse.Name = "contextMenuStrip_columnUse";
            this.contextMenuStrip_columnUse.Size = new System.Drawing.Size(122, 26);
            // 
            // setUseToolStripMenuItem
            // 
            this.setUseToolStripMenuItem.Name = "setUseToolStripMenuItem";
            this.setUseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.setUseToolStripMenuItem.Text = "Set Use...";
            this.setUseToolStripMenuItem.Click += new System.EventHandler(this.setUseToolStripMenuItem_Click);
            // 
            // toolStrip_modifiers
            // 
            this.toolStrip_modifiers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_addModifier,
            this.toolStripButton_addModOnSelected,
            this.toolStripButton_renameModifier,
            this.toolStripButton_removeModifier});
            this.toolStrip_modifiers.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_modifiers.Name = "toolStrip_modifiers";
            this.toolStrip_modifiers.Size = new System.Drawing.Size(354, 25);
            this.toolStrip_modifiers.TabIndex = 0;
            this.toolStrip_modifiers.Text = "toolStrip5";
            // 
            // toolStripButton_addModifier
            // 
            this.toolStripButton_addModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_addModifier.Image = global::GeoEdit.Properties.Resources.Add;
            this.toolStripButton_addModifier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_addModifier.Name = "toolStripButton_addModifier";
            this.toolStripButton_addModifier.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_addModifier.Text = "Add Modifier";
            this.toolStripButton_addModifier.Click += new System.EventHandler(this.toolStripButton_modifiers_Click);
            // 
            // toolStripButton_addModOnSelected
            // 
            this.toolStripButton_addModOnSelected.CheckOnClick = true;
            this.toolStripButton_addModOnSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_addModOnSelected.Image = global::GeoEdit.Properties.Resources.Folder_Add_01;
            this.toolStripButton_addModOnSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_addModOnSelected.Name = "toolStripButton_addModOnSelected";
            this.toolStripButton_addModOnSelected.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_addModOnSelected.Text = "Add Modifier on Selected";
            this.toolStripButton_addModOnSelected.Visible = false;
            this.toolStripButton_addModOnSelected.Click += new System.EventHandler(this.toolStripButton_addModOnSelected_Click);
            // 
            // toolStripButton_renameModifier
            // 
            this.toolStripButton_renameModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_renameModifier.Image = global::GeoEdit.Properties.Resources.Rename_icon;
            this.toolStripButton_renameModifier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_renameModifier.Name = "toolStripButton_renameModifier";
            this.toolStripButton_renameModifier.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_renameModifier.Text = "Rename Modifier";
            this.toolStripButton_renameModifier.Click += new System.EventHandler(this.toolStripButton_modSettings_Click);
            // 
            // toolStripButton_removeModifier
            // 
            this.toolStripButton_removeModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_removeModifier.Image = global::GeoEdit.Properties.Resources.deletered;
            this.toolStripButton_removeModifier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_removeModifier.Name = "toolStripButton_removeModifier";
            this.toolStripButton_removeModifier.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_removeModifier.Text = "Remove Modifier";
            this.toolStripButton_removeModifier.Click += new System.EventHandler(this.toolStripButton_modDelete_Click);
            // 
            // treeView_wells
            // 
            this.treeView_wells.CheckBoxes = true;
            this.treeView_wells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_wells.Location = new System.Drawing.Point(0, 25);
            this.treeView_wells.Name = "treeView_wells";
            this.treeView_wells.Size = new System.Drawing.Size(142, 333);
            this.treeView_wells.TabIndex = 0;
            this.treeView_wells.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_wells_AfterCheck);
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 24);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.splitContainer_project);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.splitContainer_view);
            this.splitContainer_main.Panel2.Controls.Add(this.toolStrip_view);
            this.splitContainer_main.Size = new System.Drawing.Size(1073, 704);
            this.splitContainer_main.SplitterDistance = 500;
            this.splitContainer_main.TabIndex = 2;
            // 
            // splitContainer_project
            // 
            this.splitContainer_project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_project.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_project.Name = "splitContainer_project";
            // 
            // splitContainer_project.Panel1
            // 
            this.splitContainer_project.Panel1.Controls.Add(this.splitContainer_model);
            // 
            // splitContainer_project.Panel2
            // 
            this.splitContainer_project.Panel2.Controls.Add(this.splitContainer_modifiers);
            this.splitContainer_project.Panel2.Controls.Add(this.toolStrip_modifiers);
            this.splitContainer_project.Size = new System.Drawing.Size(500, 704);
            this.splitContainer_project.SplitterDistance = 142;
            this.splitContainer_project.TabIndex = 1;
            // 
            // splitContainer_model
            // 
            this.splitContainer_model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_model.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_model.Name = "splitContainer_model";
            this.splitContainer_model.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_model.Panel1
            // 
            this.splitContainer_model.Panel1.Controls.Add(this.splitContainer_models);
            // 
            // splitContainer_model.Panel2
            // 
            this.splitContainer_model.Panel2.Controls.Add(this.treeView_wells);
            this.splitContainer_model.Panel2.Controls.Add(this.toolStrip_wells);
            this.splitContainer_model.Size = new System.Drawing.Size(142, 704);
            this.splitContainer_model.SplitterDistance = 342;
            this.splitContainer_model.TabIndex = 0;
            // 
            // splitContainer_models
            // 
            this.splitContainer_models.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_models.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_models.Name = "splitContainer_models";
            this.splitContainer_models.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_models.Panel1
            // 
            this.splitContainer_models.Panel1.Controls.Add(this.listBox_models);
            this.splitContainer_models.Panel1.Controls.Add(this.toolStrip_models);
            // 
            // splitContainer_models.Panel2
            // 
            this.splitContainer_models.Panel2.Controls.Add(this.splitContainer_grids);
            this.splitContainer_models.Size = new System.Drawing.Size(142, 342);
            this.splitContainer_models.SplitterDistance = 107;
            this.splitContainer_models.TabIndex = 0;
            // 
            // listBox_models
            // 
            this.listBox_models.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_models.FormattingEnabled = true;
            this.listBox_models.Location = new System.Drawing.Point(0, 25);
            this.listBox_models.Name = "listBox_models";
            this.listBox_models.Size = new System.Drawing.Size(142, 82);
            this.listBox_models.TabIndex = 0;
            this.listBox_models.SelectedIndexChanged += new System.EventHandler(this.modelsListBox_SelectedIndexChanged);
            // 
            // toolStrip_models
            // 
            this.toolStrip_models.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_addModel,
            this.toolStripButton_renameModel,
            this.toolStripButton_removeModel});
            this.toolStrip_models.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_models.Name = "toolStrip_models";
            this.toolStrip_models.Size = new System.Drawing.Size(142, 25);
            this.toolStrip_models.TabIndex = 0;
            this.toolStrip_models.Text = "toolStrip1";
            // 
            // toolStripButton_addModel
            // 
            this.toolStripButton_addModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_addModel.Image = global::GeoEdit.Properties.Resources.Add;
            this.toolStripButton_addModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_addModel.Name = "toolStripButton_addModel";
            this.toolStripButton_addModel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_addModel.Text = "Add Model";
            this.toolStripButton_addModel.Click += new System.EventHandler(this.addModelMenuItem_Click);
            // 
            // toolStripButton_renameModel
            // 
            this.toolStripButton_renameModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_renameModel.Image = global::GeoEdit.Properties.Resources.Rename_icon;
            this.toolStripButton_renameModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_renameModel.Name = "toolStripButton_renameModel";
            this.toolStripButton_renameModel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_renameModel.Text = "Rename Model";
            this.toolStripButton_renameModel.Click += new System.EventHandler(this.toolStripButton_renameModel_Click);
            // 
            // toolStripButton_removeModel
            // 
            this.toolStripButton_removeModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_removeModel.Image = global::GeoEdit.Properties.Resources.deletered;
            this.toolStripButton_removeModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_removeModel.Name = "toolStripButton_removeModel";
            this.toolStripButton_removeModel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_removeModel.Text = "Remove Model";
            this.toolStripButton_removeModel.Click += new System.EventHandler(this.toolStripButton_removeModel_Click);
            // 
            // splitContainer_grids
            // 
            this.splitContainer_grids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_grids.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_grids.Name = "splitContainer_grids";
            this.splitContainer_grids.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_grids.Panel1
            // 
            this.splitContainer_grids.Panel1.Controls.Add(this.listBox_grids);
            this.splitContainer_grids.Panel1.Controls.Add(this.toolStrip_grids);
            // 
            // splitContainer_grids.Panel2
            // 
            this.splitContainer_grids.Panel2.Controls.Add(this.listBox_props);
            this.splitContainer_grids.Panel2.Controls.Add(this.toolStrip_props);
            this.splitContainer_grids.Size = new System.Drawing.Size(142, 231);
            this.splitContainer_grids.SplitterDistance = 106;
            this.splitContainer_grids.TabIndex = 0;
            // 
            // listBox_grids
            // 
            this.listBox_grids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_grids.FormattingEnabled = true;
            this.listBox_grids.Location = new System.Drawing.Point(0, 25);
            this.listBox_grids.Name = "listBox_grids";
            this.listBox_grids.Size = new System.Drawing.Size(142, 81);
            this.listBox_grids.TabIndex = 0;
            this.listBox_grids.SelectedIndexChanged += new System.EventHandler(this.gridsListBox_SelectedIndexChanged);
            // 
            // toolStrip_grids
            // 
            this.toolStrip_grids.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_importGrid,
            this.toolStripButton_createGrid,
            this.toolStripButton_exportGrid,
            this.toolStripButton_renameGrid,
            this.toolStripButton_removeGrid});
            this.toolStrip_grids.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_grids.Name = "toolStrip_grids";
            this.toolStrip_grids.Size = new System.Drawing.Size(142, 25);
            this.toolStrip_grids.TabIndex = 0;
            this.toolStrip_grids.Text = "toolStrip2";
            // 
            // toolStripButton_importGrid
            // 
            this.toolStripButton_importGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_importGrid.Image = global::GeoEdit.Properties.Resources.Import_A_512;
            this.toolStripButton_importGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_importGrid.Name = "toolStripButton_importGrid";
            this.toolStripButton_importGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_importGrid.Text = "Import Grid";
            this.toolStripButton_importGrid.Click += new System.EventHandler(this.importGridMenuItem_Click);
            // 
            // toolStripButton_createGrid
            // 
            this.toolStripButton_createGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_createGrid.Image = global::GeoEdit.Properties.Resources.visart;
            this.toolStripButton_createGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createGrid.Name = "toolStripButton_createGrid";
            this.toolStripButton_createGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_createGrid.Text = "Create Grid";
            this.toolStripButton_createGrid.Click += new System.EventHandler(this.createGridMenuItem_Click);
            // 
            // toolStripButton_exportGrid
            // 
            this.toolStripButton_exportGrid.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_exportGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_exportGrid.Image = global::GeoEdit.Properties.Resources._237009_file_document__arrow_move_export_128;
            this.toolStripButton_exportGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_exportGrid.Name = "toolStripButton_exportGrid";
            this.toolStripButton_exportGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_exportGrid.Text = "Export Gird";
            this.toolStripButton_exportGrid.Click += new System.EventHandler(this.toolStripButton_exportGrid_Click);
            // 
            // toolStripButton_renameGrid
            // 
            this.toolStripButton_renameGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_renameGrid.Image = global::GeoEdit.Properties.Resources.Rename_icon;
            this.toolStripButton_renameGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_renameGrid.Name = "toolStripButton_renameGrid";
            this.toolStripButton_renameGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_renameGrid.Text = "Rename Grid";
            this.toolStripButton_renameGrid.Click += new System.EventHandler(this.toolStripButton_renameGrid_Click);
            // 
            // toolStripButton_removeGrid
            // 
            this.toolStripButton_removeGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_removeGrid.Image = global::GeoEdit.Properties.Resources.deletered;
            this.toolStripButton_removeGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_removeGrid.Name = "toolStripButton_removeGrid";
            this.toolStripButton_removeGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_removeGrid.Text = "Remove Grid";
            this.toolStripButton_removeGrid.Click += new System.EventHandler(this.toolStripButton_removeGrid_Click);
            // 
            // listBox_props
            // 
            this.listBox_props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_props.FormattingEnabled = true;
            this.listBox_props.Location = new System.Drawing.Point(0, 25);
            this.listBox_props.Name = "listBox_props";
            this.listBox_props.Size = new System.Drawing.Size(142, 96);
            this.listBox_props.TabIndex = 0;
            this.listBox_props.SelectedIndexChanged += new System.EventHandler(this.propsListBox_SelectedIndexChanged);
            // 
            // toolStrip_props
            // 
            this.toolStrip_props.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_exportProp,
            this.toolStripButton_importProp,
            this.toolStripButton_createProp,
            this.toolStripButton_renameProp,
            this.toolStripButton_removeProp});
            this.toolStrip_props.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_props.Name = "toolStrip_props";
            this.toolStrip_props.Size = new System.Drawing.Size(142, 25);
            this.toolStrip_props.TabIndex = 0;
            this.toolStrip_props.Text = "toolStrip3";
            // 
            // toolStripButton_exportProp
            // 
            this.toolStripButton_exportProp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_exportProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_exportProp.Image = global::GeoEdit.Properties.Resources._237009_file_document__arrow_move_export_128;
            this.toolStripButton_exportProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_exportProp.Name = "toolStripButton_exportProp";
            this.toolStripButton_exportProp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_exportProp.Text = "Export Prop";
            this.toolStripButton_exportProp.Click += new System.EventHandler(this.toolStripButton_propExport_Click);
            // 
            // toolStripButton_importProp
            // 
            this.toolStripButton_importProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_importProp.Image = global::GeoEdit.Properties.Resources.Import_A_512;
            this.toolStripButton_importProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_importProp.Name = "toolStripButton_importProp";
            this.toolStripButton_importProp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_importProp.Text = "Import Prop";
            this.toolStripButton_importProp.Click += new System.EventHandler(this.importPropMenuItem_Click);
            // 
            // toolStripButton_createProp
            // 
            this.toolStripButton_createProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_createProp.Image = global::GeoEdit.Properties.Resources.visart;
            this.toolStripButton_createProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createProp.Name = "toolStripButton_createProp";
            this.toolStripButton_createProp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_createProp.Text = "Create Prop";
            this.toolStripButton_createProp.Click += new System.EventHandler(this.createPropMenuItem_Click);
            // 
            // toolStripButton_renameProp
            // 
            this.toolStripButton_renameProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_renameProp.Image = global::GeoEdit.Properties.Resources.Rename_icon;
            this.toolStripButton_renameProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_renameProp.Name = "toolStripButton_renameProp";
            this.toolStripButton_renameProp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_renameProp.Text = "Rename Prop";
            this.toolStripButton_renameProp.Click += new System.EventHandler(this.toolStripButton_renameProp_Click);
            // 
            // toolStripButton_removeProp
            // 
            this.toolStripButton_removeProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_removeProp.Image = global::GeoEdit.Properties.Resources.deletered;
            this.toolStripButton_removeProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_removeProp.Name = "toolStripButton_removeProp";
            this.toolStripButton_removeProp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_removeProp.Text = "Remove Prop";
            this.toolStripButton_removeProp.Click += new System.EventHandler(this.toolStripButton_removeProp_Click);
            // 
            // toolStrip_wells
            // 
            this.toolStrip_wells.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_importWells,
            this.toolStripButton_renameWells,
            this.toolStripButton_removeWells});
            this.toolStrip_wells.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_wells.Name = "toolStrip_wells";
            this.toolStrip_wells.Size = new System.Drawing.Size(142, 25);
            this.toolStrip_wells.TabIndex = 0;
            this.toolStrip_wells.Text = "toolStrip4";
            // 
            // toolStripButton_importWells
            // 
            this.toolStripButton_importWells.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_importWells.Image = global::GeoEdit.Properties.Resources.Import_A_512;
            this.toolStripButton_importWells.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_importWells.Name = "toolStripButton_importWells";
            this.toolStripButton_importWells.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_importWells.Text = "Import Wells";
            this.toolStripButton_importWells.Click += new System.EventHandler(this.importWellsMenuItem_Click);
            // 
            // toolStripButton_renameWells
            // 
            this.toolStripButton_renameWells.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_renameWells.Image = global::GeoEdit.Properties.Resources.Rename_icon;
            this.toolStripButton_renameWells.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_renameWells.Name = "toolStripButton_renameWells";
            this.toolStripButton_renameWells.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_renameWells.Text = "Rename Well";
            // 
            // toolStripButton_removeWells
            // 
            this.toolStripButton_removeWells.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_removeWells.Image = global::GeoEdit.Properties.Resources.deletered;
            this.toolStripButton_removeWells.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_removeWells.Name = "toolStripButton_removeWells";
            this.toolStripButton_removeWells.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_removeWells.Text = "Remove Well";
            // 
            // splitContainer_view
            // 
            this.splitContainer_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_view.Location = new System.Drawing.Point(0, 25);
            this.splitContainer_view.Name = "splitContainer_view";
            // 
            // splitContainer_view.Panel1
            // 
            this.splitContainer_view.Panel1.Controls.Add(this.openGLControl_main);
            // 
            // splitContainer_view.Panel2
            // 
            this.splitContainer_view.Panel2.Controls.Add(this.listBox_layers);
            this.splitContainer_view.Size = new System.Drawing.Size(569, 679);
            this.splitContainer_view.SplitterDistance = 468;
            this.splitContainer_view.TabIndex = 1;
            // 
            // toolStrip_view
            // 
            this.toolStrip_view.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_cellValue,
            this.toolStripTextBox_index,
            this.toolStripButton_focus,
            this.toolStripButton_gridLines,
            this.toolStripButton_scale});
            this.toolStrip_view.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_view.Name = "toolStrip_view";
            this.toolStrip_view.Size = new System.Drawing.Size(569, 25);
            this.toolStrip_view.TabIndex = 0;
            this.toolStrip_view.Text = "toolStrip1";
            // 
            // toolStripTextBox_cellValue
            // 
            this.toolStripTextBox_cellValue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_cellValue.Name = "toolStripTextBox_cellValue";
            this.toolStripTextBox_cellValue.ReadOnly = true;
            this.toolStripTextBox_cellValue.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripTextBox_index
            // 
            this.toolStripTextBox_index.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_index.Name = "toolStripTextBox_index";
            this.toolStripTextBox_index.ReadOnly = true;
            this.toolStripTextBox_index.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripButton_focus
            // 
            this.toolStripButton_focus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_focus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_focus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_focus.Image")));
            this.toolStripButton_focus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_focus.Name = "toolStripButton_focus";
            this.toolStripButton_focus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_focus.Text = "Home";
            this.toolStripButton_focus.Click += new System.EventHandler(this.homeToolStripButton_Click);
            // 
            // toolStripButton_gridLines
            // 
            this.toolStripButton_gridLines.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_gridLines.CheckOnClick = true;
            this.toolStripButton_gridLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_gridLines.Image = global::GeoEdit.Properties.Resources._3x3_grid;
            this.toolStripButton_gridLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_gridLines.Name = "toolStripButton_gridLines";
            this.toolStripButton_gridLines.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_gridLines.Text = "Show/Hide Grid Lines";
            // 
            // toolStripButton_scale
            // 
            this.toolStripButton_scale.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_scale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_scale.Image = global::GeoEdit.Properties.Resources.scale;
            this.toolStripButton_scale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_scale.Name = "toolStripButton_scale";
            this.toolStripButton_scale.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_scale.Text = "Scale";
            this.toolStripButton_scale.Click += new System.EventHandler(this.toolStripButton_propScale_Click);
            // 
            // coordToolStripStatusLabel
            // 
            this.coordToolStripStatusLabel.Alignment = global::GeoEdit.Properties.Settings.Default.Alignment;
            this.coordToolStripStatusLabel.Name = "coordToolStripStatusLabel";
            this.coordToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip_main
            // 
            this.statusStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coordToolStripStatusLabel});
            this.statusStrip_main.Location = new System.Drawing.Point(0, 728);
            this.statusStrip_main.Name = "statusStrip_main";
            this.statusStrip_main.Size = new System.Drawing.Size(1073, 22);
            this.statusStrip_main.TabIndex = 1;
            // 
            // imageList_modifiers
            // 
            this.imageList_modifiers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_modifiers.ImageStream")));
            this.imageList_modifiers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_modifiers.Images.SetKeyName(0, "docs-icon.png");
            this.imageList_modifiers.Images.SetKeyName(1, "List_Checklist_Check_Checkmark_Checked_Unchecked_Checkbox_Properties_Property_Opt" +
        "ions_Preferences_Choose_Choice_Select_Selected_Mark_Marked_Todo_To_Do_To-do-128." +
        "png");
            this.imageList_modifiers.Images.SetKeyName(2, "Focus-128.png");
            // 
            // mainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 750);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.statusStrip_main);
            this.Controls.Add(this.menuStrip_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_main;
            this.Name = "mainForm";
            this.Text = "PEXEL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.close_Event);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainForm_DragDrop);
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl_main)).EndInit();
            this.splitContainer_modifiers.Panel1.ResumeLayout(false);
            this.splitContainer_modifiers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_modifiers)).EndInit();
            this.splitContainer_modifiers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_modifiersGroup)).EndInit();
            this.contextMenuStrip_columnValue.ResumeLayout(false);
            this.contextMenuStrip_columnRadius.ResumeLayout(false);
            this.contextMenuStrip_columnUse.ResumeLayout(false);
            this.toolStrip_modifiers.ResumeLayout(false);
            this.toolStrip_modifiers.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            this.splitContainer_main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.splitContainer_project.Panel1.ResumeLayout(false);
            this.splitContainer_project.Panel2.ResumeLayout(false);
            this.splitContainer_project.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_project)).EndInit();
            this.splitContainer_project.ResumeLayout(false);
            this.splitContainer_model.Panel1.ResumeLayout(false);
            this.splitContainer_model.Panel2.ResumeLayout(false);
            this.splitContainer_model.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_model)).EndInit();
            this.splitContainer_model.ResumeLayout(false);
            this.splitContainer_models.Panel1.ResumeLayout(false);
            this.splitContainer_models.Panel1.PerformLayout();
            this.splitContainer_models.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_models)).EndInit();
            this.splitContainer_models.ResumeLayout(false);
            this.toolStrip_models.ResumeLayout(false);
            this.toolStrip_models.PerformLayout();
            this.splitContainer_grids.Panel1.ResumeLayout(false);
            this.splitContainer_grids.Panel1.PerformLayout();
            this.splitContainer_grids.Panel2.ResumeLayout(false);
            this.splitContainer_grids.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_grids)).EndInit();
            this.splitContainer_grids.ResumeLayout(false);
            this.toolStrip_grids.ResumeLayout(false);
            this.toolStrip_grids.PerformLayout();
            this.toolStrip_props.ResumeLayout(false);
            this.toolStrip_props.PerformLayout();
            this.toolStrip_wells.ResumeLayout(false);
            this.toolStrip_wells.PerformLayout();
            this.splitContainer_view.Panel1.ResumeLayout(false);
            this.splitContainer_view.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_view)).EndInit();
            this.splitContainer_view.ResumeLayout(false);
            this.toolStrip_view.ResumeLayout(false);
            this.toolStrip_view.PerformLayout();
            this.statusStrip_main.ResumeLayout(false);
            this.statusStrip_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_edit;
        private SharpGL.OpenGLControl openGLControl_main;
        private System.Windows.Forms.ListBox listBox_layers;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.ToolStripStatusLabel coordToolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip_main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_command;
        private System.Windows.Forms.ToolStripMenuItem modifiersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_add;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView_wells;
        private System.Windows.Forms.TreeView treeView_modifiersGroups;
        private System.Windows.Forms.ImageList imageList_modifiers;
        private System.Windows.Forms.SplitContainer splitContainer_modifiers;
        private System.Windows.Forms.DataGridView dataGridView_modifiersGroup;
        private System.Windows.Forms.SplitContainer splitContainer_project;
        private System.Windows.Forms.SplitContainer splitContainer_model;
        private System.Windows.Forms.SplitContainer splitContainer_models;
        private System.Windows.Forms.ListBox listBox_models;
        private System.Windows.Forms.SplitContainer splitContainer_grids;
        private System.Windows.Forms.ListBox listBox_grids;
        private System.Windows.Forms.ListBox listBox_props;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_columnValue;
        private System.Windows.Forms.ToolStripMenuItem setValueToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_columnRadius;
        private System.Windows.Forms.ToolStripMenuItem setRadiusToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_columnUse;
        private System.Windows.Forms.ToolStripMenuItem setUseToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_mult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_radius;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_apply;
        private System.Windows.Forms.ToolStrip toolStrip_modifiers;
        private System.Windows.Forms.ToolStrip toolStrip_models;
        private System.Windows.Forms.ToolStrip toolStrip_grids;
        private System.Windows.Forms.ToolStrip toolStrip_props;
        private System.Windows.Forms.ToolStrip toolStrip_wells;
        private System.Windows.Forms.ToolStripButton toolStripButton_exportProp;
        private System.Windows.Forms.ToolStripButton toolStripButton_addModel;
        private System.Windows.Forms.ToolStripButton toolStripButton_addModifier;
        private System.Windows.Forms.ToolStripButton toolStripButton_removeModifier;
        private System.Windows.Forms.ToolStripButton toolStripButton_removeModel;
        private System.Windows.Forms.ToolStripButton toolStripButton_importGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton_createGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton_exportGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton_removeGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton_importProp;
        private System.Windows.Forms.ToolStripButton toolStripButton_createProp;
        private System.Windows.Forms.ToolStripButton toolStripButton_removeProp;
        private System.Windows.Forms.ToolStripButton toolStripButton_importWells;
        private System.Windows.Forms.ToolStripButton toolStripButton_removeWells;
        private System.Windows.Forms.ToolStripButton toolStripButton_renameModel;
        private System.Windows.Forms.ToolStripButton toolStripButton_renameGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton_renameProp;
        private System.Windows.Forms.ToolStripButton toolStripButton_renameWells;
        private System.Windows.Forms.ToolStripButton toolStripButton_renameModifier;
        private System.Windows.Forms.SplitContainer splitContainer_view;
        private System.Windows.Forms.ToolStrip toolStrip_view;
        private System.Windows.Forms.ToolStripButton toolStripButton_scale;
        private System.Windows.Forms.ToolStripButton toolStripButton_gridLines;
        private System.Windows.Forms.ToolStripButton toolStripButton_focus;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_cellValue;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_index;
        private System.Windows.Forms.ToolStripButton toolStripButton_addModOnSelected;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

