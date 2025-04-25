<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSystemSetup
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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
        Dim StrNasSidLabel As System.Windows.Forms.Label
        Dim StrNasUserNameLabel As System.Windows.Forms.Label
        Dim StrNasHostNameLabel As System.Windows.Forms.Label
        Dim Con_START_DATELabel As System.Windows.Forms.Label
        Dim Con_NAS_HOME_FOLDERNAMELabel As System.Windows.Forms.Label
        Dim Con_END_DATELabel As System.Windows.Forms.Label
        Dim Con_COMPANY_INFOLabel As System.Windows.Forms.Label
        Dim Con_BASE_DATALabel As System.Windows.Forms.Label
        Dim JsonUrlListLabel As System.Windows.Forms.Label
        Dim JsonBottomButtonScriptLabel As System.Windows.Forms.Label
        Dim StrNoVBAFunctionLabel As System.Windows.Forms.Label
        Dim StrLoadingFormNameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemSetup))
        Me.splBottom = New System.Windows.Forms.SplitContainer()
        Me.btnFontSize = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.palLeft = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.StrNasSidTextBox = New System.Windows.Forms.TextBox()
        Me.StrNasUserNameTextBox = New System.Windows.Forms.TextBox()
        Me.StrNasHostNameTextBox = New System.Windows.Forms.TextBox()
        Me.Con_START_DATETextBox = New System.Windows.Forms.TextBox()
        Me.Con_NAS_HOME_FOLDERNAMETextBox = New System.Windows.Forms.TextBox()
        Me.Con_END_DATETextBox = New System.Windows.Forms.TextBox()
        Me.Con_COMPANY_INFOTextBox = New System.Windows.Forms.TextBox()
        Me.Con_BASE_DATATextBox = New System.Windows.Forms.TextBox()
        Me.splFill = New System.Windows.Forms.SplitContainer()
        Me.JsonUrlListTextBox = New System.Windows.Forms.TextBox()
        Me.JsonBottomButtonScriptTextBox = New System.Windows.Forms.TextBox()
        Me.fntDialogSystemSetup = New System.Windows.Forms.FontDialog()
        Me.StrNoVBAFunctionTextBox = New System.Windows.Forms.TextBox()
        Me.ChungJeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StrLoadingFormNameTextBox = New System.Windows.Forms.TextBox()
        StrNasSidLabel = New System.Windows.Forms.Label()
        StrNasUserNameLabel = New System.Windows.Forms.Label()
        StrNasHostNameLabel = New System.Windows.Forms.Label()
        Con_START_DATELabel = New System.Windows.Forms.Label()
        Con_NAS_HOME_FOLDERNAMELabel = New System.Windows.Forms.Label()
        Con_END_DATELabel = New System.Windows.Forms.Label()
        Con_COMPANY_INFOLabel = New System.Windows.Forms.Label()
        Con_BASE_DATALabel = New System.Windows.Forms.Label()
        JsonUrlListLabel = New System.Windows.Forms.Label()
        JsonBottomButtonScriptLabel = New System.Windows.Forms.Label()
        StrNoVBAFunctionLabel = New System.Windows.Forms.Label()
        StrLoadingFormNameLabel = New System.Windows.Forms.Label()
        CType(Me.splBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splBottom.Panel2.SuspendLayout()
        Me.splBottom.SuspendLayout()
        Me.palLeft.SuspendLayout()
        CType(Me.splFill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splFill.Panel1.SuspendLayout()
        Me.splFill.Panel2.SuspendLayout()
        Me.splFill.SuspendLayout()
        CType(Me.ChungJeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StrNasSidLabel
        '
        StrNasSidLabel.AutoSize = True
        StrNasSidLabel.Location = New System.Drawing.Point(7, 283)
        StrNasSidLabel.Name = "StrNasSidLabel"
        StrNasSidLabel.Size = New System.Drawing.Size(135, 15)
        StrNasSidLabel.TabIndex = 50
        StrNasSidLabel.Text = "网络存储用户令牌:"
        '
        'StrNasUserNameLabel
        '
        StrNasUserNameLabel.AutoSize = True
        StrNasUserNameLabel.Location = New System.Drawing.Point(7, 207)
        StrNasUserNameLabel.Name = "StrNasUserNameLabel"
        StrNasUserNameLabel.Size = New System.Drawing.Size(135, 15)
        StrNasUserNameLabel.TabIndex = 48
        StrNasUserNameLabel.Text = "网络存储—用户名:"
        '
        'StrNasHostNameLabel
        '
        StrNasHostNameLabel.AutoSize = True
        StrNasHostNameLabel.Location = New System.Drawing.Point(7, 169)
        StrNasHostNameLabel.Name = "StrNasHostNameLabel"
        StrNasHostNameLabel.Size = New System.Drawing.Size(135, 15)
        StrNasHostNameLabel.TabIndex = 46
        StrNasHostNameLabel.Text = "网络存储主机地址:"
        '
        'Con_START_DATELabel
        '
        Con_START_DATELabel.AutoSize = True
        Con_START_DATELabel.Location = New System.Drawing.Point(37, 131)
        Con_START_DATELabel.Name = "Con_START_DATELabel"
        Con_START_DATELabel.Size = New System.Drawing.Size(105, 15)
        Con_START_DATELabel.TabIndex = 44
        Con_START_DATELabel.Text = "开始日期标识:"
        '
        'Con_NAS_HOME_FOLDERNAMELabel
        '
        Con_NAS_HOME_FOLDERNAMELabel.AutoSize = True
        Con_NAS_HOME_FOLDERNAMELabel.Location = New System.Drawing.Point(7, 245)
        Con_NAS_HOME_FOLDERNAMELabel.Name = "Con_NAS_HOME_FOLDERNAMELabel"
        Con_NAS_HOME_FOLDERNAMELabel.Size = New System.Drawing.Size(135, 15)
        Con_NAS_HOME_FOLDERNAMELabel.TabIndex = 42
        Con_NAS_HOME_FOLDERNAMELabel.Text = "网络存储主文件夹:"
        '
        'Con_END_DATELabel
        '
        Con_END_DATELabel.AutoSize = True
        Con_END_DATELabel.Location = New System.Drawing.Point(37, 93)
        Con_END_DATELabel.Name = "Con_END_DATELabel"
        Con_END_DATELabel.Size = New System.Drawing.Size(105, 15)
        Con_END_DATELabel.TabIndex = 40
        Con_END_DATELabel.Text = "结束日期标识:"
        '
        'Con_COMPANY_INFOLabel
        '
        Con_COMPANY_INFOLabel.AutoSize = True
        Con_COMPANY_INFOLabel.Location = New System.Drawing.Point(22, 55)
        Con_COMPANY_INFOLabel.Name = "Con_COMPANY_INFOLabel"
        Con_COMPANY_INFOLabel.Size = New System.Drawing.Size(120, 15)
        Con_COMPANY_INFOLabel.TabIndex = 38
        Con_COMPANY_INFOLabel.Text = "单位信息表名称:"
        '
        'Con_BASE_DATALabel
        '
        Con_BASE_DATALabel.AutoSize = True
        Con_BASE_DATALabel.Location = New System.Drawing.Point(22, 17)
        Con_BASE_DATALabel.Name = "Con_BASE_DATALabel"
        Con_BASE_DATALabel.Size = New System.Drawing.Size(120, 15)
        Con_BASE_DATALabel.TabIndex = 36
        Con_BASE_DATALabel.Text = "基础数据表名称:"
        '
        'JsonUrlListLabel
        '
        JsonUrlListLabel.BackColor = System.Drawing.SystemColors.ScrollBar
        JsonUrlListLabel.Dock = System.Windows.Forms.DockStyle.Top
        JsonUrlListLabel.Font = New System.Drawing.Font("微软雅黑", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        JsonUrlListLabel.Location = New System.Drawing.Point(0, 0)
        JsonUrlListLabel.Name = "JsonUrlListLabel"
        JsonUrlListLabel.Size = New System.Drawing.Size(469, 25)
        JsonUrlListLabel.TabIndex = 24
        JsonUrlListLabel.Text = "网络响应接受过滤器:"
        JsonUrlListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'JsonBottomButtonScriptLabel
        '
        JsonBottomButtonScriptLabel.BackColor = System.Drawing.SystemColors.ScrollBar
        JsonBottomButtonScriptLabel.Dock = System.Windows.Forms.DockStyle.Top
        JsonBottomButtonScriptLabel.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        JsonBottomButtonScriptLabel.Location = New System.Drawing.Point(0, 0)
        JsonBottomButtonScriptLabel.Name = "JsonBottomButtonScriptLabel"
        JsonBottomButtonScriptLabel.Size = New System.Drawing.Size(469, 25)
        JsonBottomButtonScriptLabel.TabIndex = 55
        JsonBottomButtonScriptLabel.Text = "底部按钮脚本过滤器:"
        JsonBottomButtonScriptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'splBottom
        '
        Me.splBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splBottom.Location = New System.Drawing.Point(0, 427)
        Me.splBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.splBottom.Name = "splBottom"
        '
        'splBottom.Panel2
        '
        Me.splBottom.Panel2.Controls.Add(Me.btnFontSize)
        Me.splBottom.Panel2.Controls.Add(Me.btnCancel)
        Me.splBottom.Panel2.Controls.Add(Me.btnOK)
        Me.splBottom.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 20, 3)
        Me.splBottom.Size = New System.Drawing.Size(863, 32)
        Me.splBottom.SplitterDistance = 383
        Me.splBottom.TabIndex = 24
        '
        'btnFontSize
        '
        Me.btnFontSize.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnFontSize.Location = New System.Drawing.Point(0, 0)
        Me.btnFontSize.Name = "btnFontSize"
        Me.btnFontSize.Size = New System.Drawing.Size(55, 29)
        Me.btnFontSize.TabIndex = 25
        Me.btnFontSize.Text = "字体"
        Me.btnFontSize.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(316, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 29)
        Me.btnCancel.TabIndex = 24
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(386, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 29)
        Me.btnOK.TabIndex = 23
        Me.btnOK.Text = "确定(&O)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'palLeft
        '
        Me.palLeft.Controls.Add(StrLoadingFormNameLabel)
        Me.palLeft.Controls.Add(Me.StrLoadingFormNameTextBox)
        Me.palLeft.Controls.Add(Me.btnClear)
        Me.palLeft.Controls.Add(StrNasSidLabel)
        Me.palLeft.Controls.Add(Me.StrNasSidTextBox)
        Me.palLeft.Controls.Add(StrNasUserNameLabel)
        Me.palLeft.Controls.Add(Me.StrNasUserNameTextBox)
        Me.palLeft.Controls.Add(StrNasHostNameLabel)
        Me.palLeft.Controls.Add(Me.StrNasHostNameTextBox)
        Me.palLeft.Controls.Add(Con_START_DATELabel)
        Me.palLeft.Controls.Add(Me.Con_START_DATETextBox)
        Me.palLeft.Controls.Add(Con_NAS_HOME_FOLDERNAMELabel)
        Me.palLeft.Controls.Add(Me.Con_NAS_HOME_FOLDERNAMETextBox)
        Me.palLeft.Controls.Add(Con_END_DATELabel)
        Me.palLeft.Controls.Add(Me.Con_END_DATETextBox)
        Me.palLeft.Controls.Add(Con_COMPANY_INFOLabel)
        Me.palLeft.Controls.Add(Me.Con_COMPANY_INFOTextBox)
        Me.palLeft.Controls.Add(Con_BASE_DATALabel)
        Me.palLeft.Controls.Add(Me.Con_BASE_DATATextBox)
        Me.palLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.palLeft.Location = New System.Drawing.Point(0, 0)
        Me.palLeft.Name = "palLeft"
        Me.palLeft.Size = New System.Drawing.Size(394, 427)
        Me.palLeft.TabIndex = 25
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.btnClear.Location = New System.Drawing.Point(280, 278)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(68, 26)
        Me.btnClear.TabIndex = 52
        Me.btnClear.Text = "清空"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'StrNasSidTextBox
        '
        Me.StrNasSidTextBox.Enabled = False
        Me.StrNasSidTextBox.Location = New System.Drawing.Point(148, 278)
        Me.StrNasSidTextBox.Name = "StrNasSidTextBox"
        Me.StrNasSidTextBox.Size = New System.Drawing.Size(126, 25)
        Me.StrNasSidTextBox.TabIndex = 51
        '
        'StrNasUserNameTextBox
        '
        Me.StrNasUserNameTextBox.Location = New System.Drawing.Point(148, 202)
        Me.StrNasUserNameTextBox.Name = "StrNasUserNameTextBox"
        Me.StrNasUserNameTextBox.Size = New System.Drawing.Size(200, 25)
        Me.StrNasUserNameTextBox.TabIndex = 49
        '
        'StrNasHostNameTextBox
        '
        Me.StrNasHostNameTextBox.Location = New System.Drawing.Point(148, 164)
        Me.StrNasHostNameTextBox.Name = "StrNasHostNameTextBox"
        Me.StrNasHostNameTextBox.Size = New System.Drawing.Size(200, 25)
        Me.StrNasHostNameTextBox.TabIndex = 47
        '
        'Con_START_DATETextBox
        '
        Me.Con_START_DATETextBox.Location = New System.Drawing.Point(148, 88)
        Me.Con_START_DATETextBox.Name = "Con_START_DATETextBox"
        Me.Con_START_DATETextBox.Size = New System.Drawing.Size(200, 25)
        Me.Con_START_DATETextBox.TabIndex = 45
        '
        'Con_NAS_HOME_FOLDERNAMETextBox
        '
        Me.Con_NAS_HOME_FOLDERNAMETextBox.Location = New System.Drawing.Point(148, 240)
        Me.Con_NAS_HOME_FOLDERNAMETextBox.Name = "Con_NAS_HOME_FOLDERNAMETextBox"
        Me.Con_NAS_HOME_FOLDERNAMETextBox.Size = New System.Drawing.Size(200, 25)
        Me.Con_NAS_HOME_FOLDERNAMETextBox.TabIndex = 43
        '
        'Con_END_DATETextBox
        '
        Me.Con_END_DATETextBox.Location = New System.Drawing.Point(148, 126)
        Me.Con_END_DATETextBox.Name = "Con_END_DATETextBox"
        Me.Con_END_DATETextBox.Size = New System.Drawing.Size(200, 25)
        Me.Con_END_DATETextBox.TabIndex = 41
        '
        'Con_COMPANY_INFOTextBox
        '
        Me.Con_COMPANY_INFOTextBox.Location = New System.Drawing.Point(148, 50)
        Me.Con_COMPANY_INFOTextBox.Name = "Con_COMPANY_INFOTextBox"
        Me.Con_COMPANY_INFOTextBox.Size = New System.Drawing.Size(200, 25)
        Me.Con_COMPANY_INFOTextBox.TabIndex = 39
        '
        'Con_BASE_DATATextBox
        '
        Me.Con_BASE_DATATextBox.Location = New System.Drawing.Point(148, 12)
        Me.Con_BASE_DATATextBox.Name = "Con_BASE_DATATextBox"
        Me.Con_BASE_DATATextBox.Size = New System.Drawing.Size(200, 25)
        Me.Con_BASE_DATATextBox.TabIndex = 37
        '
        'splFill
        '
        Me.splFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splFill.Location = New System.Drawing.Point(394, 0)
        Me.splFill.Name = "splFill"
        Me.splFill.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splFill.Panel1
        '
        Me.splFill.Panel1.Controls.Add(Me.JsonUrlListTextBox)
        Me.splFill.Panel1.Controls.Add(JsonUrlListLabel)
        '
        'splFill.Panel2
        '
        Me.splFill.Panel2.Controls.Add(Me.StrNoVBAFunctionTextBox)
        Me.splFill.Panel2.Controls.Add(StrNoVBAFunctionLabel)
        Me.splFill.Panel2.Controls.Add(Me.JsonBottomButtonScriptTextBox)
        Me.splFill.Panel2.Controls.Add(JsonBottomButtonScriptLabel)
        Me.splFill.Size = New System.Drawing.Size(469, 427)
        Me.splFill.SplitterDistance = 114
        Me.splFill.TabIndex = 28
        '
        'JsonUrlListTextBox
        '
        Me.JsonUrlListTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JsonUrlListTextBox.Font = New System.Drawing.Font("宋体", 10.0!)
        Me.JsonUrlListTextBox.Location = New System.Drawing.Point(0, 25)
        Me.JsonUrlListTextBox.Multiline = True
        Me.JsonUrlListTextBox.Name = "JsonUrlListTextBox"
        Me.JsonUrlListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.JsonUrlListTextBox.Size = New System.Drawing.Size(469, 89)
        Me.JsonUrlListTextBox.TabIndex = 26
        '
        'JsonBottomButtonScriptTextBox
        '
        Me.JsonBottomButtonScriptTextBox.AcceptsTab = True
        Me.JsonBottomButtonScriptTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.JsonBottomButtonScriptTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.JsonBottomButtonScriptTextBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.JsonBottomButtonScriptTextBox.Font = New System.Drawing.Font("宋体", 10.0!)
        Me.JsonBottomButtonScriptTextBox.Location = New System.Drawing.Point(0, 25)
        Me.JsonBottomButtonScriptTextBox.Multiline = True
        Me.JsonBottomButtonScriptTextBox.Name = "JsonBottomButtonScriptTextBox"
        Me.JsonBottomButtonScriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.JsonBottomButtonScriptTextBox.Size = New System.Drawing.Size(469, 152)
        Me.JsonBottomButtonScriptTextBox.TabIndex = 57
        '
        'fntDialogSystemSetup
        '
        Me.fntDialogSystemSetup.ShowApply = True
        Me.fntDialogSystemSetup.ShowColor = True
        '
        'StrNoVBAFunctionLabel
        '
        StrNoVBAFunctionLabel.Dock = System.Windows.Forms.DockStyle.Top
        StrNoVBAFunctionLabel.Font = New System.Drawing.Font("微软雅黑", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        StrNoVBAFunctionLabel.Location = New System.Drawing.Point(0, 177)
        StrNoVBAFunctionLabel.Name = "StrNoVBAFunctionLabel"
        StrNoVBAFunctionLabel.Size = New System.Drawing.Size(469, 25)
        StrNoVBAFunctionLabel.TabIndex = 59
        StrNoVBAFunctionLabel.Text = "通用数据格式函数:"
        StrNoVBAFunctionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StrNoVBAFunctionTextBox
        '
        Me.StrNoVBAFunctionTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StrNoVBAFunctionTextBox.Location = New System.Drawing.Point(0, 202)
        Me.StrNoVBAFunctionTextBox.Multiline = True
        Me.StrNoVBAFunctionTextBox.Name = "StrNoVBAFunctionTextBox"
        Me.StrNoVBAFunctionTextBox.Size = New System.Drawing.Size(469, 107)
        Me.StrNoVBAFunctionTextBox.TabIndex = 60
        '
        'ChungJeeBindingSource
        '
        Me.ChungJeeBindingSource.DataSource = GetType(Webview2ToExcel.My.ChungJee)
        '
        'StrLoadingFormNameLabel
        '
        StrLoadingFormNameLabel.AutoSize = True
        StrLoadingFormNameLabel.Location = New System.Drawing.Point(7, 320)
        StrLoadingFormNameLabel.Name = "StrLoadingFormNameLabel"
        StrLoadingFormNameLabel.Size = New System.Drawing.Size(120, 15)
        StrLoadingFormNameLabel.TabIndex = 52
        StrLoadingFormNameLabel.Text = "加载窗体的名称:"
        '
        'StrLoadingFormNameTextBox
        '
        Me.StrLoadingFormNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ChungJeeBindingSource, "strLoadingFormName", True))
        Me.StrLoadingFormNameTextBox.Location = New System.Drawing.Point(148, 317)
        Me.StrLoadingFormNameTextBox.Name = "StrLoadingFormNameTextBox"
        Me.StrLoadingFormNameTextBox.Size = New System.Drawing.Size(200, 25)
        Me.StrLoadingFormNameTextBox.TabIndex = 53
        '
        'frmSystemSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 459)
        Me.Controls.Add(Me.splFill)
        Me.Controls.Add(Me.palLeft)
        Me.Controls.Add(Me.splBottom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSystemSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "系统设置"
        Me.splBottom.Panel2.ResumeLayout(False)
        CType(Me.splBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splBottom.ResumeLayout(False)
        Me.palLeft.ResumeLayout(False)
        Me.palLeft.PerformLayout()
        Me.splFill.Panel1.ResumeLayout(False)
        Me.splFill.Panel1.PerformLayout()
        Me.splFill.Panel2.ResumeLayout(False)
        Me.splFill.Panel2.PerformLayout()
        CType(Me.splFill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splFill.ResumeLayout(False)
        CType(Me.ChungJeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splBottom As Windows.Forms.SplitContainer
    Friend WithEvents btnCancel As Windows.Forms.Button
    Friend WithEvents btnOK As Windows.Forms.Button
    Friend WithEvents btnClear As Windows.Forms.Button
    Friend WithEvents StrNasSidTextBox As Windows.Forms.TextBox
    Friend WithEvents StrNasUserNameTextBox As Windows.Forms.TextBox
    Friend WithEvents StrNasHostNameTextBox As Windows.Forms.TextBox
    Friend WithEvents Con_START_DATETextBox As Windows.Forms.TextBox
    Friend WithEvents Con_NAS_HOME_FOLDERNAMETextBox As Windows.Forms.TextBox
    Friend WithEvents Con_END_DATETextBox As Windows.Forms.TextBox
    Friend WithEvents Con_COMPANY_INFOTextBox As Windows.Forms.TextBox
    Friend WithEvents Con_BASE_DATATextBox As Windows.Forms.TextBox
    Friend WithEvents palLeft As Windows.Forms.Panel
    Friend WithEvents splFill As Windows.Forms.SplitContainer
    Friend WithEvents JsonUrlListTextBox As Windows.Forms.TextBox
    Friend WithEvents JsonBottomButtonScriptTextBox As Windows.Forms.TextBox
    Friend WithEvents btnFontSize As Windows.Forms.Button
    Friend WithEvents fntDialogSystemSetup As Windows.Forms.FontDialog
    Friend WithEvents StrNoVBAFunctionTextBox As Windows.Forms.TextBox
    Friend WithEvents StrLoadingFormNameTextBox As Windows.Forms.TextBox
    Friend WithEvents ChungJeeBindingSource As Windows.Forms.BindingSource
End Class
