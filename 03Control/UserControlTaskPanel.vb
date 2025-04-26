Imports System.Diagnostics
Imports System.IO
Imports System.Threading.Tasks
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Imports System.Dynamic
Imports System.Windows.Forms
Imports System.Xml
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports Microsoft.Web
Imports System.Web.UI.WebControls.WebParts
Public Class WebviewTaskPaneControl
    Public WithEvents wvCoreWevview2 As CoreWebView2
    Public dicBottomButtonScript As Dictionary(Of String, Dictionary(Of String, String))
    Public isHaveIframe As Boolean = False
    Private Sub MyTaskPaneControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ReadBottomButtonScriptToDictionary() '读取底部按钮脚本列表到字典
        MdlLoad.initializeThisAddinWebview2Async() '初始化WebView2控件，绑定WebView2的事件到webviewEvent模块的相关函数中，加载响应过滤器脚本
    End Sub '自定义控件加载事件处理程序

    Private Sub wvCoreWevview2_SourceChanged(sender As Object, e As CoreWebView2SourceChangedEventArgs) Handles wvCoreWevview2.SourceChanged
        If e.IsNewDocument = False Then
            'Dim wvWebview As Microsoft.Web.WebView2.Core.CoreWebView2 = sender
            'wvWebview.AddHostObjectToScript()
            'wvCoreWevview2.AddScriptToExecuteOnDocumentCreatedAsync()
            Debug.WriteLine("SourceChanged:IsNewDocument:" & e.IsNewDocument)
        End If
    End Sub
    Private Sub wvCoreWevview2_NotificationReceived(sender As Object, e As CoreWebView2NotificationReceivedEventArgs) Handles wvCoreWevview2.NotificationReceived
        Debug.WriteLine(e.Notification)
    End Sub
    Private Sub wvCoreWevview2_ContentLoading(sender As Object, e As CoreWebView2ContentLoadingEventArgs) Handles wvCoreWevview2.ContentLoading
        Debug.WriteLine("ContentLoading:" & e.IsErrorPage)
    End Sub


    Private Sub txbURL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txbURL.KeyPress
        If e.KeyChar = Chr(13) Then
            Try
                Me.wvCoreWevview2.Navigate(sender.Text)
            Catch ex As Exception
                Debug.WriteLine(ex.InnerException.Message)
            End Try
        End If

    End Sub '地址文本框按下回车键事件处理程序
    Private Sub btnNavigate_Click(sender As Object, e As EventArgs) Handles btnNavigate.Click
        If Me.txbURL.Text <> "" Then
            Try
                Me.wvCoreWevview2.Navigate(Me.txbURL.Text)
            Catch ex As Exception
                Debug.WriteLine(ex.InnerException.Message)
            End Try
        End If
    End Sub '导航按钮点击事件处理程序


    Private Async Sub btnForward_Click(sender As Object, e As EventArgs) Handles btnForward.Click
        If Me.tooltxtForward.Text <> "" Then
            Try
                Dim strResult As CoreWebView2ExecuteScriptResult = Await Me.wvCoreWevview2.ExecuteScriptWithResultAsync(Me.tooltxtForward.Text)
                If strResult.ResultAsJson = "true" Then Debug.WriteLine("向前执行成功！")
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End If

    End Sub '向前按钮点击事件处理程序
    Private Sub lbCloseLoading_Click(sender As Object, e As EventArgs) Handles lbCloseLoading.Click
        Try
            xlApp.Run("frmHidden")
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
        Me.conMenuAllSetup.Enabled = False
        Me.conMenuAllSetup.Enabled = True
    End Sub '关闭加载窗体按钮点击事件处理程序
    Private Async Sub btnBackward_Click(sender As Object, e As EventArgs) Handles btnBackward.Click
        If Me.tooltxtBackward.Text <> "" Then
            Try
                Dim strResult As CoreWebView2ExecuteScriptResult = Await Me.wvCoreWevview2.ExecuteScriptWithResultAsync(Me.tooltxtBackward.Text)
                If strResult.ResultAsJson = "true" Then Debug.WriteLine("向后执行成功！")
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End If
    End Sub '向后按钮点击事件处理程序


    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        Dim objButtonUp As Button = CType(sender, Button)
        With Globals.ThisAddIn.tpAddinWebviewTaskPanel
            If objButtonUp.Text = "⬇" Then
                objButtonUp.Text = "⬆"
                .DockPosition = System.Windows.Forms.DockStyle.Top : .Height = 500
            Else
                objButtonUp.Text = "⬇"
                .DockPosition = System.Windows.Forms.DockStyle.Top : .Height = 105
            End If
        End With

    End Sub '向上按钮点击事件处理程序
    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        Dim objButtonUp As Button = CType(sender, Button)
        With Globals.ThisAddIn.tpAddinWebviewTaskPanel
            If objButtonUp.Text = "⬅" Then
                objButtonUp.Text = "⮕"
                Try
                    .Width = xlApp.ActiveWindow.Width
                Catch ex As Exception
                    .DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
                    .Width = xlApp.ActiveWindow.Width
                End Try
            Else
                objButtonUp.Text = "⬅"
                Try
                    .Width = 450
                Catch ex As Exception
                    .DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
                    .Width = 450
                End Try
            End If
        End With
    End Sub '向右按钮点击事件处理程序


    Private Sub MenuItemRequestDirection_Click(sender As Object, e As EventArgs) Handles MenuItemRequestFilter.Click

    End Sub '请求方向菜单点击执行程序
    Private Sub MenuItemResponseParser_Click(sender As Object, e As EventArgs) Handles MenuItemResponseFilter.Click
        Dim frmAnalysis As New frmAnalysiserSetup
        frmAnalysis.Show()
    End Sub '响应过滤器设置菜单点击执行程序
    Private Sub MenuItemLoginNas_Click(sender As Object, e As EventArgs) Handles MenuItemLoginNas.Click
        Dim frmLoginForm As New LoginForm
        frmLoginForm.UsernameTextBox.Text = My.ChungJee.Default.strNasUserName
        If My.ChungJee.Default.strNasPassWord <> "" Then frmLoginForm.PasswordTextBox.Text = My.ChungJee.Default.strNasPassWord
        frmLoginForm.ShowDialog()
    End Sub '登录NAS菜单点击执行程序
    Private Sub menuItemSystemParameterSetup_Click(sender As Object, e As EventArgs) Handles menuItemSystemParameterSetup.Click
        Dim frmSystemSetup1 As New frmSystemSetup
        frmSystemSetup1.Show()
    End Sub '系统参数设置菜单点击执行程序


    Private Sub conMenuItemLoginNas_Click(sender As Object, e As EventArgs) Handles conMenuItemLoginNas.Click
        MenuItemLoginNas.PerformClick()
    End Sub '登录NAS菜单点击执行程序
    Private Sub conMenuItemSystemSetup_Click(sender As Object, e As EventArgs) Handles conMenuItemSystemSetup.Click
        menuItemSystemParameterSetup.PerformClick()
    End Sub ' '系统参数设置菜单点击执行程序
    Private Sub conMenuItemResponseFilterSetup_Click(sender As Object, e As EventArgs) Handles conMenuItemResponseFilterSetup.Click
        MenuItemResponseFilter.PerformClick()
    End Sub '响应过滤器设置菜单点击执行程序
    Private Sub conMenuItemRequestFilterSetup_Click(sender As Object, e As EventArgs) Handles conMenuItemRequestFilterSetup.Click
        'Program.Main()
    End Sub

    Sub ReadBottomButtonScriptToDictionary()
        If dicBottomButtonScript Is Nothing Then
            dicBottomButtonScript = New Dictionary(Of String, Dictionary(Of String, String))
            Dim jsonBottomButtonScriptList As Newtonsoft.Json.Linq.JObject
            'Debug.WriteLine(My.ChungJee.Default.jsonBottomButtonScript)
            'My.ChungJee.Default.jsonBottomButtonScript = ""
            Try
                jsonBottomButtonScriptList = Newtonsoft.Json.Linq.JObject.Parse(My.ChungJee.Default.jsonBottomButtonScript)
            Catch ex As Exception
                MsgBox("底部按钮脚本列表加载失败！")
                Return
            End Try
            For Each key As JToken In jsonBottomButtonScriptList("data")
                Dim strBottomButtonScriptKeyUrl As New Uri(key("url"))
                Dim strCurrentBottomButtonScript As String = strBottomButtonScriptKeyUrl.AbsolutePath
                Dim strURLKeyName As String = Regex.Replace(strBottomButtonScriptKeyUrl.AbsolutePath, "[^a-zA-Z0-9]", "")
                If dicBottomButtonScript.ContainsKey(strURLKeyName) Then
                    dicBottomButtonScript.Item(strURLKeyName).Add("lbPage", key("lbPage"))
                    dicBottomButtonScript.Item(strURLKeyName).Add("tooltxtForward", key("tooltxtForward"))
                    dicBottomButtonScript.Item(strURLKeyName).Add("tooltxtBackward", key("tooltxtBackward"))
                Else

                    dicBottomButtonScript.Add(strURLKeyName, New Dictionary(Of String, String))
                    dicBottomButtonScript.Item(strURLKeyName).Add("lbPage", key("lbPage"))
                    dicBottomButtonScript.Item(strURLKeyName).Add("tooltxtForward", key("tooltxtForward"))
                    dicBottomButtonScript.Item(strURLKeyName).Add("tooltxtBackward", key("tooltxtBackward"))
                End If
            Next key

        End If
    End Sub '设定底部按钮脚本

    Public Async Sub ExecuteJavaScriptAsync()
        Dim strScript As String = Me.tooltxtLable.Text
        If Me.wbvTaskPan.InvokeRequired Then
            Await Task.Run(Sub() Me.wbvTaskPan.Invoke(Sub() ExecuteJavaScriptAsync()))
        Else
            Dim result As CoreWebView2ExecuteScriptResult = Await Me.wbvTaskPan.CoreWebView2.ExecuteScriptWithResultAsync(strScript)
            If result.ResultAsJson <> "" And result.ResultAsJson <> "null" Then
                Me.lbPage.Text = result.ResultAsJson.Replace("""", "").Replace(" ", "")
            Else
                Me.lbPage.Text = "......"
            End If
            Me.conMenuAllSetup.Enabled = False : Me.conMenuAllSetup.Enabled = True
        End If
    End Sub '执行JavaScript脚本

    Private Async Sub ExecuteScriptUsingDevTools(script As String)
        Try
            ' 构造参数
            Dim parameters As String = "{""expression"":""" & script.Replace("""", "\""") & """}"

            ' 调用 Runtime.evaluate 方法
            Dim result As String = Await Me.wvCoreWevview2.CallDevToolsProtocolMethodAsync("Runtime.evaluate", parameters)

            ' 输出结果
            Debug.WriteLine("Script Execution Result: " & result)
        Catch ex As Exception
            ' 捕获并处理异常
            Debug.WriteLine("Error executing script: " & ex.Message)
        End Try
    End Sub ' '使用DevTools执行脚本

End Class
