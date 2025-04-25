<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnalysiserSetup
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("网络响应过滤器")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnalysiserSetup))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnAddFilter = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnBrowseFile = New System.Windows.Forms.Button()
        Me.btnUploadFile = New System.Windows.Forms.Button()
        Me.txtFilterUrl = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.trvResponseFilter = New System.Windows.Forms.TreeView()
        Me.ContextMenuReaponseFilter = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.删除节点ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.更新节点ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.插入节点ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新加载ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtContentEdit = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuReaponseFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnAddFilter)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.btnBrowseFile)
        Me.Panel1.Controls.Add(Me.btnUploadFile)
        Me.Panel1.Controls.Add(Me.txtFilterUrl)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(5, 522)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(984, 30)
        Me.Panel1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.Location = New System.Drawing.Point(335, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 30)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "字体设置"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddFilter.Location = New System.Drawing.Point(235, 0)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(100, 30)
        Me.btnAddFilter.TabIndex = 8
        Me.btnAddFilter.Text = "添加过滤器"
        Me.btnAddFilter.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtFileName.Location = New System.Drawing.Point(595, 0)
        Me.txtFileName.Multiline = True
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(209, 30)
        Me.txtFileName.TabIndex = 7
        '
        'btnBrowseFile
        '
        Me.btnBrowseFile.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnBrowseFile.Location = New System.Drawing.Point(804, 0)
        Me.btnBrowseFile.Name = "btnBrowseFile"
        Me.btnBrowseFile.Size = New System.Drawing.Size(90, 30)
        Me.btnBrowseFile.TabIndex = 6
        Me.btnBrowseFile.Text = "浏览文件"
        Me.btnBrowseFile.UseVisualStyleBackColor = True
        '
        'btnUploadFile
        '
        Me.btnUploadFile.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUploadFile.Location = New System.Drawing.Point(894, 0)
        Me.btnUploadFile.Name = "btnUploadFile"
        Me.btnUploadFile.Size = New System.Drawing.Size(90, 30)
        Me.btnUploadFile.TabIndex = 5
        Me.btnUploadFile.Text = "上传文件"
        Me.btnUploadFile.UseVisualStyleBackColor = True
        '
        'txtFilterUrl
        '
        Me.txtFilterUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterUrl.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtFilterUrl.Font = New System.Drawing.Font("宋体", 10.0!)
        Me.txtFilterUrl.Location = New System.Drawing.Point(0, 0)
        Me.txtFilterUrl.Multiline = True
        Me.txtFilterUrl.Name = "txtFilterUrl"
        Me.txtFilterUrl.Size = New System.Drawing.Size(235, 30)
        Me.txtFilterUrl.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(5, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(984, 26)
        Me.Panel2.TabIndex = 8
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(5, 26)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.trvResponseFilter)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtContentEdit)
        Me.SplitContainer1.Size = New System.Drawing.Size(984, 496)
        Me.SplitContainer1.SplitterDistance = 332
        Me.SplitContainer1.TabIndex = 9
        '
        'trvResponseFilter
        '
        Me.trvResponseFilter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvResponseFilter.CheckBoxes = True
        Me.trvResponseFilter.ContextMenuStrip = Me.ContextMenuReaponseFilter
        Me.trvResponseFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvResponseFilter.Font = New System.Drawing.Font("宋体", 12.0!)
        Me.trvResponseFilter.FullRowSelect = True
        Me.trvResponseFilter.LabelEdit = True
        Me.trvResponseFilter.Location = New System.Drawing.Point(0, 0)
        Me.trvResponseFilter.Name = "trvResponseFilter"
        TreeNode1.Name = "节点0"
        TreeNode1.Text = "网络响应过滤器"
        Me.trvResponseFilter.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.trvResponseFilter.Size = New System.Drawing.Size(332, 496)
        Me.trvResponseFilter.TabIndex = 7
        '
        'ContextMenuReaponseFilter
        '
        Me.ContextMenuReaponseFilter.Font = New System.Drawing.Font("Microsoft YaHei UI", 10.0!)
        Me.ContextMenuReaponseFilter.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuReaponseFilter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.删除节点ToolStripMenuItem, Me.更新节点ToolStripMenuItem, Me.插入节点ToolStripMenuItem, Me.重新加载ToolStripMenuItem})
        Me.ContextMenuReaponseFilter.Name = "ContextMenuReaponseFilter"
        Me.ContextMenuReaponseFilter.Size = New System.Drawing.Size(153, 116)
        '
        '删除节点ToolStripMenuItem
        '
        Me.删除节点ToolStripMenuItem.Image = Global.Webview2ToExcel.My.Resources.Resources._112
        Me.删除节点ToolStripMenuItem.Name = "删除节点ToolStripMenuItem"
        Me.删除节点ToolStripMenuItem.Size = New System.Drawing.Size(152, 28)
        Me.删除节点ToolStripMenuItem.Text = "删除节点"
        '
        '更新节点ToolStripMenuItem
        '
        Me.更新节点ToolStripMenuItem.Image = Global.Webview2ToExcel.My.Resources.Resources.l8g7ce39
        Me.更新节点ToolStripMenuItem.Name = "更新节点ToolStripMenuItem"
        Me.更新节点ToolStripMenuItem.Size = New System.Drawing.Size(152, 28)
        Me.更新节点ToolStripMenuItem.Text = "更新节点"
        '
        '插入节点ToolStripMenuItem
        '
        Me.插入节点ToolStripMenuItem.Image = Global.Webview2ToExcel.My.Resources.Resources.屏幕截图_25_4_2025_16414_image_baidu_com
        Me.插入节点ToolStripMenuItem.Name = "插入节点ToolStripMenuItem"
        Me.插入节点ToolStripMenuItem.Size = New System.Drawing.Size(152, 28)
        Me.插入节点ToolStripMenuItem.Text = "插入节点"
        '
        '重新加载ToolStripMenuItem
        '
        Me.重新加载ToolStripMenuItem.Image = Global.Webview2ToExcel.My.Resources.Resources._2tshc3uf
        Me.重新加载ToolStripMenuItem.Name = "重新加载ToolStripMenuItem"
        Me.重新加载ToolStripMenuItem.Size = New System.Drawing.Size(152, 28)
        Me.重新加载ToolStripMenuItem.Text = "重新加载"
        '
        'txtContentEdit
        '
        Me.txtContentEdit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtContentEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtContentEdit.Location = New System.Drawing.Point(0, 0)
        Me.txtContentEdit.Multiline = True
        Me.txtContentEdit.Name = "txtContentEdit"
        Me.txtContentEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtContentEdit.Size = New System.Drawing.Size(648, 496)
        Me.txtContentEdit.TabIndex = 8
        '
        'frmAnalysiserSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 557)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAnalysiserSetup"
        Me.Opacity = 0.95R
        Me.Padding = New System.Windows.Forms.Padding(5, 0, 5, 5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "设置解析器"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuReaponseFilter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents txtFileName As Windows.Forms.TextBox
    Friend WithEvents btnBrowseFile As Windows.Forms.Button
    Friend WithEvents btnUploadFile As Windows.Forms.Button
    Friend WithEvents txtFilterUrl As Windows.Forms.TextBox
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents trvResponseFilter As Windows.Forms.TreeView
    Friend WithEvents txtContentEdit As Windows.Forms.TextBox
    Friend WithEvents btnAddFilter As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents ContextMenuReaponseFilter As Windows.Forms.ContextMenuStrip
    Friend WithEvents 删除节点ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents 更新节点ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents 插入节点ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents 重新加载ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
