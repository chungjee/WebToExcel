Partial Class Ribbon
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Windows.Forms 类撰写设计器支持所必需的
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        '组件设计器需要此调用。
        InitializeComponent()

    End Sub

    '组件重写释放以清理组件列表。
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

    '组件设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是组件设计器所必需的
    '可使用组件设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ribbon))
        Me.TabInter = Me.Factory.CreateRibbonTab
        Me.tab_WebView = Me.Factory.CreateRibbonTab
        Me.Group2 = Me.Factory.CreateRibbonGroup
        Me.btnMeiTuan = Me.Factory.CreateRibbonButton
        Me.btnTaxation = Me.Factory.CreateRibbonButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnTaskPanel = Me.Factory.CreateRibbonButton
        Me.Menu1 = Me.Factory.CreateRibbonMenu
        Me.chkRequestFilterSwitch = Me.Factory.CreateRibbonCheckBox
        Me.chkResponseFilterSwitch = Me.Factory.CreateRibbonCheckBox
        Me.Menu2 = Me.Factory.CreateRibbonMenu
        Me.btnLoginNas = Me.Factory.CreateRibbonButton
        Me.btnSystemSetup = Me.Factory.CreateRibbonButton
        Me.btnRequestFilter = Me.Factory.CreateRibbonButton
        Me.btnResponseFilter = Me.Factory.CreateRibbonButton
        Me.TabInter.SuspendLayout()
        Me.tab_WebView.SuspendLayout()
        Me.Group2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabInter
        '
        Me.TabInter.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office
        Me.TabInter.Label = "TabAddIns"
        Me.TabInter.Name = "TabInter"
        '
        'tab_WebView
        '
        Me.tab_WebView.Groups.Add(Me.Group2)
        Me.tab_WebView.Label = "数据聚合"
        Me.tab_WebView.Name = "tab_WebView"
        '
        'Group2
        '
        Me.Group2.Items.Add(Me.btnMeiTuan)
        Me.Group2.Items.Add(Me.btnTaxation)
        Me.Group2.Label = "WebView2"
        Me.Group2.Name = "Group2"
        '
        'btnMeiTuan
        '
        Me.btnMeiTuan.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnMeiTuan.Image = Global.Webview2ToExcel.My.Resources.Resources._5
        Me.btnMeiTuan.Label = "美团登录"
        Me.btnMeiTuan.Name = "btnMeiTuan"
        Me.btnMeiTuan.ScreenTip = "https://e.dianping.com/"
        Me.btnMeiTuan.ShowImage = True
        '
        'btnTaxation
        '
        Me.btnTaxation.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnTaxation.Image = Global.Webview2ToExcel.My.Resources.Resources.hura1zfr
        Me.btnTaxation.Label = "电子税务"
        Me.btnTaxation.Name = "btnTaxation"
        Me.btnTaxation.ScreenTip = "https://tpass.hebei.chinatax.gov.cn:8443/#/login"
        Me.btnTaxation.ShowImage = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "9")
        '
        'btnTaskPanel
        '
        Me.btnTaskPanel.Image = CType(resources.GetObject("btnTaskPanel.Image"), System.Drawing.Image)
        Me.btnTaskPanel.Label = "浏览器面板"
        Me.btnTaskPanel.Name = "btnTaskPanel"
        Me.btnTaskPanel.ShowImage = True
        '
        'Menu1
        '
        Me.Menu1.Image = CType(resources.GetObject("Menu1.Image"), System.Drawing.Image)
        Me.Menu1.Items.Add(Me.chkRequestFilterSwitch)
        Me.Menu1.Items.Add(Me.chkResponseFilterSwitch)
        Me.Menu1.Label = "功能开关"
        Me.Menu1.Name = "Menu1"
        Me.Menu1.ShowImage = True
        '
        'chkRequestFilterSwitch
        '
        Me.chkRequestFilterSwitch.Label = "请求过滤拦截"
        Me.chkRequestFilterSwitch.Name = "chkRequestFilterSwitch"
        '
        'chkResponseFilterSwitch
        '
        Me.chkResponseFilterSwitch.Label = "响应过滤捕捉"
        Me.chkResponseFilterSwitch.Name = "chkResponseFilterSwitch"
        '
        'Menu2
        '
        Me.Menu2.Image = CType(resources.GetObject("Menu2.Image"), System.Drawing.Image)
        Me.Menu2.Items.Add(Me.btnLoginNas)
        Me.Menu2.Items.Add(Me.btnSystemSetup)
        Me.Menu2.Items.Add(Me.btnRequestFilter)
        Me.Menu2.Items.Add(Me.btnResponseFilter)
        Me.Menu2.Label = "系统设置"
        Me.Menu2.Name = "Menu2"
        Me.Menu2.ShowImage = True
        '
        'btnLoginNas
        '
        Me.btnLoginNas.Image = CType(resources.GetObject("btnLoginNas.Image"), System.Drawing.Image)
        Me.btnLoginNas.Label = "登录NAS系统"
        Me.btnLoginNas.Name = "btnLoginNas"
        Me.btnLoginNas.ShowImage = True
        '
        'btnSystemSetup
        '
        Me.btnSystemSetup.Image = CType(resources.GetObject("btnSystemSetup.Image"), System.Drawing.Image)
        Me.btnSystemSetup.Label = "系统参数设置"
        Me.btnSystemSetup.Name = "btnSystemSetup"
        Me.btnSystemSetup.ShowImage = True
        '
        'btnRequestFilter
        '
        Me.btnRequestFilter.Image = CType(resources.GetObject("btnRequestFilter.Image"), System.Drawing.Image)
        Me.btnRequestFilter.Label = "请求过滤器设置"
        Me.btnRequestFilter.Name = "btnRequestFilter"
        Me.btnRequestFilter.ShowImage = True
        '
        'btnResponseFilter
        '
        Me.btnResponseFilter.Image = Global.Webview2ToExcel.My.Resources.Resources._13
        Me.btnResponseFilter.Label = "响应过滤器设置"
        Me.btnResponseFilter.Name = "btnResponseFilter"
        Me.btnResponseFilter.ShowImage = True
        '
        'Ribbon
        '
        Me.Name = "Ribbon"
        '
        'Ribbon.OfficeMenu
        '
        Me.OfficeMenu.Items.Add(Me.btnTaskPanel)
        Me.OfficeMenu.Items.Add(Me.Menu1)
        Me.OfficeMenu.Items.Add(Me.Menu2)
        Me.RibbonType = "Microsoft.Excel.Workbook"
        Me.Tabs.Add(Me.TabInter)
        Me.Tabs.Add(Me.tab_WebView)
        Me.TabInter.ResumeLayout(False)
        Me.TabInter.PerformLayout()
        Me.tab_WebView.ResumeLayout(False)
        Me.tab_WebView.PerformLayout()
        Me.Group2.ResumeLayout(False)
        Me.Group2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabInter As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents tab_WebView As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group2 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents btnMeiTuan As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents ImageList1 As Windows.Forms.ImageList
    Friend WithEvents btnTaskPanel As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu1 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents chkRequestFilterSwitch As Microsoft.Office.Tools.Ribbon.RibbonCheckBox
    Friend WithEvents chkResponseFilterSwitch As Microsoft.Office.Tools.Ribbon.RibbonCheckBox
    Friend WithEvents Menu2 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnSystemSetup As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnResponseFilter As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnRequestFilter As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnLoginNas As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnTaxation As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()>
    Friend ReadOnly Property Ribbon1() As Ribbon
        Get
            Return Me.GetRibbon(Of Ribbon)()
        End Get
    End Property
End Class