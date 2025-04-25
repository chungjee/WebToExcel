Imports System.Diagnostics
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Tools
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json.Linq


Public Class ThisAddIn

    Public tpAddinWebviewTaskPanel As CustomTaskPane
    Public tpConstomWebVieTaskPanel As WebviewTaskPaneControl
    Public jsonResponseFilter As Newtonsoft.Json.Linq.JObject

    Public Sub ThisAddIn_Startup() Handles Me.Startup
        tpConstomWebVieTaskPanel = New WebviewTaskPaneControl()
        tpAddinWebviewTaskPanel = Me.CustomTaskPanes.Add(tpConstomWebVieTaskPanel, "数据聚合")
        'initializeAsync()
    End Sub

    Private Sub ThisAddIn_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
        tpConstomWebVieTaskPanel.Dispose()
        tpAddinWebviewTaskPanel.Dispose()
    End Sub

    Private Sub Application_SheetBeforeDoubleClick(Sh As Object, Target As Range, ByRef Cancel As Boolean) Handles Application.SheetBeforeDoubleClick
        'xlApp.DisplayAlerts = False
        'If Sh.name = My.ChungJee.Default.con_BASE_DATA Then
        '    tpAddinWebviewTaskPanel.Visible = True
        'End If
        'xlApp.DisplayAlerts = True
    End Sub

    Private Async Sub initializeAsync()
        Dim options As New Microsoft.Web.WebView2.Core.CoreWebView2EnvironmentOptions()
        Dim userDataFolder As String = System.IO.Path.GetTempPath()
        Dim environment As Microsoft.Web.WebView2.Core.CoreWebView2Environment = Await Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync(Nothing, userDataFolder, options)
        With Globals.ThisAddIn
            Await .tpConstomWebVieTaskPanel.wbvTaskPan.EnsureCoreWebView2Async(environment)
            .tpConstomWebVieTaskPanel.wbvTaskPan.ZoomFactor = 0.8
            .tpConstomWebVieTaskPanel.wvCoreWevview2 = .tpConstomWebVieTaskPanel.wbvTaskPan.CoreWebView2

            .tpConstomWebVieTaskPanel.wvCoreWevview2.AddWebResourceRequestedFilter("*://e.dianping.com/*", CoreWebView2WebResourceContext.XmlHttpRequest)
            .tpConstomWebVieTaskPanel.wvCoreWevview2.AddWebResourceRequestedFilter("*://mss-shon.sankuai.com/*", CoreWebView2WebResourceContext.All)
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebResourceRequested, AddressOf WebViewEvents.EventWebResourceRequested
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebResourceResponseReceived, AddressOf WebViewEvents.EventWebResourceResponseReceived
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.DownloadStarting, AddressOf WebViewEvents.EventDownloadStarting

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebMessageReceived, AddressOf WebViewEvents.EventWebMessageReceived
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NavigationStarting, AddressOf WebViewEvents.EventNavigationStarting
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NavigationCompleted, AddressOf WebViewEvents.EventNavigationCompleted

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.DOMContentLoaded, AddressOf WebViewEvents.EventDOMContentLoaded
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NewWindowRequested, AddressOf WebViewEvents.EventNewWindowRequested

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.FrameNavigationStarting, AddressOf WebViewEvents.EventFrameNavigationStarting
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.FrameNavigationCompleted, AddressOf WebViewEvents.EventFrameNavigationCompleted
            Try
                Dim objJsonUrlListTemp As JObject = Newtonsoft.Json.Linq.JObject.Parse(My.ChungJee.Default.jsonUrlList)
                .jsonResponseFilter = New Newtonsoft.Json.Linq.JObject()
                Dim jsonArray As New JArray
                For Each key As JToken In objJsonUrlListTemp.Item("data")
                    If key("enable") <> "false" Then
                        jsonArray.Add(key)
                    End If
                Next
                .jsonResponseFilter.Add("data", jsonArray)
            Catch ex As Exception
                MsgBox("请求过滤器列表加载失败！")
            End Try
        End With
    End Sub '初始化CoreWebview，进行事件绑定

End Class
