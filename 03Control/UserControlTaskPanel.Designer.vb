<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WebviewTaskPaneControl
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebviewTaskPaneControl))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.spcAddress = New System.Windows.Forms.SplitContainer()
        Me.txbURL = New System.Windows.Forms.TextBox()
        Me.btnNavigate = New System.Windows.Forms.Button()
        Me.conMenuAllSetup = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuItemRequestSwitch = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemResponseSwitch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripFirst = New System.Windows.Forms.ToolStripSeparator()
        Me.conMenuItemLoginNas = New System.Windows.Forms.ToolStripMenuItem()
        Me.conMenuItemSystemSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSecond = New System.Windows.Forms.ToolStripSeparator()
        Me.conMenuItemResponseFilterSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.conMenuItemRequestFilterSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.wbvTaskPan = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.palBottom = New System.Windows.Forms.Panel()
        Me.btnBackward = New System.Windows.Forms.Button()
        Me.lbCloseLoading = New System.Windows.Forms.Label()
        Me.btnForward = New System.Windows.Forms.Button()
        Me.lbPage = New System.Windows.Forms.Label()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.mnuSystem = New System.Windows.Forms.MenuStrip()
        Me.MenuItemSystemSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemRequestFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemResponseFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuItemSystemParameterSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemLoginNas = New System.Windows.Forms.ToolStripMenuItem()
        Me.tooltxtLable = New System.Windows.Forms.ToolStripTextBox()
        Me.tooltxtForward = New System.Windows.Forms.ToolStripTextBox()
        Me.tooltxtBackward = New System.Windows.Forms.ToolStripTextBox()
        CType(Me.spcAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcAddress.Panel1.SuspendLayout()
        Me.spcAddress.Panel2.SuspendLayout()
        Me.spcAddress.SuspendLayout()
        Me.conMenuAllSetup.SuspendLayout()
        CType(Me.wbvTaskPan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.palBottom.SuspendLayout()
        Me.mnuSystem.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "13")
        Me.ImageList1.Images.SetKeyName(1, "9")
        '
        'spcAddress
        '
        Me.spcAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spcAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.spcAddress.Location = New System.Drawing.Point(0, 0)
        Me.spcAddress.Margin = New System.Windows.Forms.Padding(0)
        Me.spcAddress.Name = "spcAddress"
        '
        'spcAddress.Panel1
        '
        Me.spcAddress.Panel1.Controls.Add(Me.txbURL)
        Me.spcAddress.Panel1MinSize = 22
        '
        'spcAddress.Panel2
        '
        Me.spcAddress.Panel2.Controls.Add(Me.btnNavigate)
        Me.spcAddress.Panel2MinSize = 22
        Me.spcAddress.Size = New System.Drawing.Size(704, 27)
        Me.spcAddress.SplitterDistance = 576
        Me.spcAddress.SplitterIncrement = 2
        Me.spcAddress.SplitterWidth = 2
        Me.spcAddress.TabIndex = 2
        '
        'txbURL
        '
        Me.txbURL.AutoCompleteCustomSource.AddRange(New String() {"https://www.baidu.com", "https://e.dianping.com"})
        Me.txbURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txbURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txbURL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txbURL.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txbURL.Font = New System.Drawing.Font("宋体", 12.0!)
        Me.txbURL.Location = New System.Drawing.Point(0, 2)
        Me.txbURL.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.txbURL.Name = "txbURL"
        Me.txbURL.Size = New System.Drawing.Size(574, 23)
        Me.txbURL.TabIndex = 0
        '
        'btnNavigate
        '
        Me.btnNavigate.AutoSize = True
        Me.btnNavigate.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnNavigate.ContextMenuStrip = Me.conMenuAllSetup
        Me.btnNavigate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNavigate.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.btnNavigate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNavigate.ImageIndex = 1
        Me.btnNavigate.ImageList = Me.ImageList1
        Me.btnNavigate.Location = New System.Drawing.Point(0, 0)
        Me.btnNavigate.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.btnNavigate.Name = "btnNavigate"
        Me.btnNavigate.Size = New System.Drawing.Size(124, 25)
        Me.btnNavigate.TabIndex = 0
        Me.btnNavigate.TabStop = False
        Me.btnNavigate.Text = "转到"
        Me.btnNavigate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnNavigate.UseVisualStyleBackColor = False
        '
        'conMenuAllSetup
        '
        Me.conMenuAllSetup.AllowDrop = True
        Me.conMenuAllSetup.AllowMerge = False
        Me.conMenuAllSetup.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.conMenuAllSetup.ImageScalingSize = New System.Drawing.Size(25, 25)
        Me.conMenuAllSetup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemRequestSwitch, Me.MenuItemResponseSwitch, Me.ToolStripFirst, Me.conMenuItemLoginNas, Me.conMenuItemSystemSetup, Me.ToolStripSecond, Me.conMenuItemResponseFilterSetup, Me.conMenuItemRequestFilterSetup})
        Me.conMenuAllSetup.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.conMenuAllSetup.Name = "ContextMenuStrip1"
        Me.conMenuAllSetup.Size = New System.Drawing.Size(220, 236)
        '
        'MenuItemRequestSwitch
        '
        Me.MenuItemRequestSwitch.AccessibleDescription = ""
        Me.MenuItemRequestSwitch.CheckOnClick = True
        Me.MenuItemRequestSwitch.Name = "MenuItemRequestSwitch"
        Me.MenuItemRequestSwitch.Size = New System.Drawing.Size(219, 32)
        Me.MenuItemRequestSwitch.Text = "请求过滤器拦截"
        '
        'MenuItemResponseSwitch
        '
        Me.MenuItemResponseSwitch.CheckOnClick = True
        Me.MenuItemResponseSwitch.Name = "MenuItemResponseSwitch"
        Me.MenuItemResponseSwitch.Size = New System.Drawing.Size(219, 32)
        Me.MenuItemResponseSwitch.Text = "响应过滤器捕捉"
        '
        'ToolStripFirst
        '
        Me.ToolStripFirst.Name = "ToolStripFirst"
        Me.ToolStripFirst.Size = New System.Drawing.Size(216, 6)
        '
        'conMenuItemLoginNas
        '
        Me.conMenuItemLoginNas.Image = Global.Webview2ToExcel.My.Resources.Resources.qkwck9cj
        Me.conMenuItemLoginNas.Name = "conMenuItemLoginNas"
        Me.conMenuItemLoginNas.Size = New System.Drawing.Size(219, 32)
        Me.conMenuItemLoginNas.Text = "登录NAS系统"
        '
        'conMenuItemSystemSetup
        '
        Me.conMenuItemSystemSetup.Image = Global.Webview2ToExcel.My.Resources.Resources.l8ao716z
        Me.conMenuItemSystemSetup.Name = "conMenuItemSystemSetup"
        Me.conMenuItemSystemSetup.Size = New System.Drawing.Size(219, 32)
        Me.conMenuItemSystemSetup.Text = "系统参数设置"
        '
        'ToolStripSecond
        '
        Me.ToolStripSecond.Name = "ToolStripSecond"
        Me.ToolStripSecond.Size = New System.Drawing.Size(216, 6)
        '
        'conMenuItemResponseFilterSetup
        '
        Me.conMenuItemResponseFilterSetup.Image = Global.Webview2ToExcel.My.Resources.Resources.response11
        Me.conMenuItemResponseFilterSetup.Name = "conMenuItemResponseFilterSetup"
        Me.conMenuItemResponseFilterSetup.Size = New System.Drawing.Size(219, 32)
        Me.conMenuItemResponseFilterSetup.Text = "响应过滤器设置"
        '
        'conMenuItemRequestFilterSetup
        '
        Me.conMenuItemRequestFilterSetup.Image = Global.Webview2ToExcel.My.Resources.Resources.rtzh4ero1
        Me.conMenuItemRequestFilterSetup.Name = "conMenuItemRequestFilterSetup"
        Me.conMenuItemRequestFilterSetup.Size = New System.Drawing.Size(219, 32)
        Me.conMenuItemRequestFilterSetup.Text = "请求过滤器设置"
        '
        'wbvTaskPan
        '
        Me.wbvTaskPan.AllowExternalDrop = True
        Me.wbvTaskPan.CreationProperties = Nothing
        Me.wbvTaskPan.DefaultBackgroundColor = System.Drawing.Color.White
        Me.wbvTaskPan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbvTaskPan.Location = New System.Drawing.Point(0, 27)
        Me.wbvTaskPan.Name = "wbvTaskPan"
        Me.wbvTaskPan.Size = New System.Drawing.Size(704, 477)
        Me.wbvTaskPan.TabIndex = 1
        Me.wbvTaskPan.ZoomFactor = 1.0R
        '
        'palBottom
        '
        Me.palBottom.ContextMenuStrip = Me.conMenuAllSetup
        Me.palBottom.Controls.Add(Me.btnBackward)
        Me.palBottom.Controls.Add(Me.lbCloseLoading)
        Me.palBottom.Controls.Add(Me.btnForward)
        Me.palBottom.Controls.Add(Me.lbPage)
        Me.palBottom.Controls.Add(Me.btnRight)
        Me.palBottom.Controls.Add(Me.btnUp)
        Me.palBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.palBottom.Font = New System.Drawing.Font("宋体", 12.0!)
        Me.palBottom.Location = New System.Drawing.Point(0, 474)
        Me.palBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.palBottom.Name = "palBottom"
        Me.palBottom.Size = New System.Drawing.Size(704, 30)
        Me.palBottom.TabIndex = 3
        '
        'btnBackward
        '
        Me.btnBackward.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBackward.Font = New System.Drawing.Font("宋体", 11.0!)
        Me.btnBackward.Location = New System.Drawing.Point(102, 0)
        Me.btnBackward.Margin = New System.Windows.Forms.Padding(0)
        Me.btnBackward.Name = "btnBackward"
        Me.btnBackward.Size = New System.Drawing.Size(25, 30)
        Me.btnBackward.TabIndex = 20
        Me.btnBackward.Text = ">"
        Me.btnBackward.UseVisualStyleBackColor = True
        '
        'lbCloseLoading
        '
        Me.lbCloseLoading.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbCloseLoading.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbCloseLoading.Font = New System.Drawing.Font("微软雅黑", 14.0!)
        Me.lbCloseLoading.Location = New System.Drawing.Point(74, 0)
        Me.lbCloseLoading.Name = "lbCloseLoading"
        Me.lbCloseLoading.Size = New System.Drawing.Size(28, 30)
        Me.lbCloseLoading.TabIndex = 19
        Me.lbCloseLoading.Text = "◉"
        Me.lbCloseLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnForward
        '
        Me.btnForward.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnForward.Font = New System.Drawing.Font("宋体", 11.0!)
        Me.btnForward.Location = New System.Drawing.Point(49, 0)
        Me.btnForward.Margin = New System.Windows.Forms.Padding(0)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(25, 30)
        Me.btnForward.TabIndex = 18
        Me.btnForward.Text = "<"
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'lbPage
        '
        Me.lbPage.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbPage.Font = New System.Drawing.Font("宋体", 8.0!)
        Me.lbPage.Location = New System.Drawing.Point(0, 0)
        Me.lbPage.Margin = New System.Windows.Forms.Padding(0)
        Me.lbPage.Name = "lbPage"
        Me.lbPage.Size = New System.Drawing.Size(49, 30)
        Me.lbPage.TabIndex = 17
        Me.lbPage.Text = "......"
        Me.lbPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRight
        '
        Me.btnRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRight.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnRight.Location = New System.Drawing.Point(648, 0)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(28, 30)
        Me.btnRight.TabIndex = 9
        Me.btnRight.Text = "⬅"
        Me.btnRight.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnUp.Location = New System.Drawing.Point(676, 0)
        Me.btnUp.Margin = New System.Windows.Forms.Padding(0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(28, 30)
        Me.btnUp.TabIndex = 8
        Me.btnUp.Text = "⬆"
        Me.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(312, 28)
        '
        'mnuSystem
        '
        Me.mnuSystem.AllowDrop = True
        Me.mnuSystem.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.mnuSystem.ContextMenuStrip = Me.conMenuAllSetup
        Me.mnuSystem.Dock = System.Windows.Forms.DockStyle.None
        Me.mnuSystem.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.mnuSystem.GripMargin = New System.Windows.Forms.Padding(0)
        Me.mnuSystem.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.mnuSystem.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuSystem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemSystemSetup})
        Me.mnuSystem.Location = New System.Drawing.Point(127, 209)
        Me.mnuSystem.Name = "mnuSystem"
        Me.mnuSystem.Padding = New System.Windows.Forms.Padding(0)
        Me.mnuSystem.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.mnuSystem.Size = New System.Drawing.Size(100, 27)
        Me.mnuSystem.TabIndex = 22
        Me.mnuSystem.Text = "mnuSystem"
        Me.mnuSystem.Visible = False
        '
        'MenuItemSystemSetup
        '
        Me.MenuItemSystemSetup.DoubleClickEnabled = True
        Me.MenuItemSystemSetup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemRequestFilter, Me.MenuItemResponseFilter, Me.menuItemSystemParameterSetup, Me.MenuItemLoginNas, Me.tooltxtLable, Me.tooltxtForward, Me.tooltxtBackward})
        Me.MenuItemSystemSetup.Name = "MenuItemSystemSetup"
        Me.MenuItemSystemSetup.Size = New System.Drawing.Size(92, 27)
        Me.MenuItemSystemSetup.Text = "系统设置"
        '
        'MenuItemRequestFilter
        '
        Me.MenuItemRequestFilter.ImageTransparentColor = System.Drawing.Color.White
        Me.MenuItemRequestFilter.Name = "MenuItemRequestFilter"
        Me.MenuItemRequestFilter.Size = New System.Drawing.Size(230, 28)
        Me.MenuItemRequestFilter.Text = "请求过滤转向设置"
        '
        'MenuItemResponseFilter
        '
        Me.MenuItemResponseFilter.Name = "MenuItemResponseFilter"
        Me.MenuItemResponseFilter.Size = New System.Drawing.Size(230, 28)
        Me.MenuItemResponseFilter.Text = "响应过滤解析设置"
        '
        'menuItemSystemParameterSetup
        '
        Me.menuItemSystemParameterSetup.ImageTransparentColor = System.Drawing.Color.White
        Me.menuItemSystemParameterSetup.Name = "menuItemSystemParameterSetup"
        Me.menuItemSystemParameterSetup.Size = New System.Drawing.Size(230, 28)
        Me.menuItemSystemParameterSetup.Text = "系统参数设置"
        '
        'MenuItemLoginNas
        '
        Me.MenuItemLoginNas.ImageTransparentColor = System.Drawing.Color.White
        Me.MenuItemLoginNas.Name = "MenuItemLoginNas"
        Me.MenuItemLoginNas.Size = New System.Drawing.Size(230, 28)
        Me.MenuItemLoginNas.Text = "登录到网络存储"
        '
        'tooltxtLable
        '
        Me.tooltxtLable.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.tooltxtLable.Name = "tooltxtLable"
        Me.tooltxtLable.Size = New System.Drawing.Size(100, 29)
        '
        'tooltxtForward
        '
        Me.tooltxtForward.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.tooltxtForward.Name = "tooltxtForward"
        Me.tooltxtForward.Size = New System.Drawing.Size(100, 29)
        '
        'tooltxtBackward
        '
        Me.tooltxtBackward.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.tooltxtBackward.Name = "tooltxtBackward"
        Me.tooltxtBackward.Size = New System.Drawing.Size(100, 29)
        '
        'WebviewTaskPaneControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.mnuSystem)
        Me.Controls.Add(Me.palBottom)
        Me.Controls.Add(Me.wbvTaskPan)
        Me.Controls.Add(Me.spcAddress)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "WebviewTaskPaneControl"
        Me.Size = New System.Drawing.Size(704, 504)
        Me.spcAddress.Panel1.ResumeLayout(False)
        Me.spcAddress.Panel1.PerformLayout()
        Me.spcAddress.Panel2.ResumeLayout(False)
        Me.spcAddress.Panel2.PerformLayout()
        CType(Me.spcAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcAddress.ResumeLayout(False)
        Me.conMenuAllSetup.ResumeLayout(False)
        CType(Me.wbvTaskPan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.palBottom.ResumeLayout(False)
        Me.mnuSystem.ResumeLayout(False)
        Me.mnuSystem.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList1 As Windows.Forms.ImageList
    Friend WithEvents spcAddress As Windows.Forms.SplitContainer
    Friend WithEvents txbURL As Windows.Forms.TextBox
    Friend WithEvents btnNavigate As Windows.Forms.Button
    Friend WithEvents wbvTaskPan As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents palBottom As Windows.Forms.Panel
    Friend WithEvents btnUp As Windows.Forms.Button
    Friend WithEvents btnRight As Windows.Forms.Button
    Friend WithEvents btnBackward As Windows.Forms.Button
    Friend WithEvents lbCloseLoading As Windows.Forms.Label
    Friend WithEvents btnForward As Windows.Forms.Button
    Friend WithEvents lbPage As Windows.Forms.Label
    Friend WithEvents conMenuAllSetup As Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuItemRequestSwitch As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemResponseSwitch As Windows.Forms.ToolStripMenuItem
    Friend WithEvents conMenuItemLoginNas As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripFirst As Windows.Forms.ToolStripSeparator
    Friend WithEvents conMenuItemSystemSetup As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSecond As Windows.Forms.ToolStripSeparator
    Friend WithEvents conMenuItemResponseFilterSetup As Windows.Forms.ToolStripMenuItem
    Friend WithEvents conMenuItemRequestFilterSetup As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSystem As Windows.Forms.MenuStrip
    Friend WithEvents MenuItemSystemSetup As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemRequestFilter As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemResponseFilter As Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuItemSystemParameterSetup As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemLoginNas As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tooltxtLable As Windows.Forms.ToolStripTextBox
    Friend WithEvents tooltxtForward As Windows.Forms.ToolStripTextBox
    Friend WithEvents tooltxtBackward As Windows.Forms.ToolStripTextBox
    Friend WithEvents BottomToolStripPanel As Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As Windows.Forms.ToolStripContentPanel
End Class
