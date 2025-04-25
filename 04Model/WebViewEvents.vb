Imports System.Diagnostics
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json.Linq

Module WebViewEvents
    'WebView2控件事件处理程序
    Private strCurrentJsonUrlListItem As String
    Dim strOldValue As String
    Dim strNewValue As String

    Public Sub EventWebResourceRequested(sender As Object, e As CoreWebView2WebResourceRequestedEventArgs)
        'Debug.WriteLine("WebResourceRequested:" & e.Request.Uri)
        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestSwitch.Checked = False Then Return
        Dim strRequestUrl As String = e.Request.Uri
        strCurrentJsonUrlListItem = "" : strOldValue = "" : strNewValue = ""
        For Each key As JToken In Globals.ThisAddIn.jsonResponseFilter.Item("data")
            'Debug.WriteLine(System.Web.HttpUtility.UrlDecode(strRequestUrl))
            'Debug.WriteLine(System.Web.HttpUtility.UrlDecode(key.Item("url").Value(Of String)))
            If InStr(System.Web.HttpUtility.UrlDecode(strRequestUrl), System.Web.HttpUtility.UrlDecode(key.Item("url").Value(Of String))) Then
                strCurrentJsonUrlListItem = key.Item("url").Value(Of String)
                Try
                    strOldValue = key.Item("oldValue").Value(Of String)
                    strNewValue = key.Item("newValue").Value(Of String)
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                End Try
                Exit For
            End If
        Next
        If strCurrentJsonUrlListItem = "" Then xlApp.Run("frmHidden") ： Return
        Debug.WriteLine(Now() & strCurrentJsonUrlListItem & Chr(13))
        xlApp.Run("frmShow")
        If UCase(e.Request.Method) = "POST" Then
            If strOldValue = "" Or strNewValue = "" Then xlApp.Run("frmHidden") ： Return
            Dim requestBody As String
            Dim contentStream As Stream = e.Request.Content
            If contentStream IsNot Nothing Then
                Using reader As New StreamReader(contentStream)
                    requestBody = reader.ReadToEnd()
                    requestBody = requestBody.Replace(strOldValue, strNewValue)
                    If Not String.IsNullOrEmpty(requestBody) Then
                        Dim modifiedStream As New MemoryStream(System.Text.Encoding.UTF8.GetBytes(requestBody))
                        e.Request.Content = modifiedStream
                    End If
                End Using
            End If
        ElseIf UCase(e.Request.Method) = "GET" Then
            If InStr(e.Request.Uri, "xls") <= 0 Then Return
        End If
    End Sub 'webview2控件网络资源请求事件执行程序
    Public Async Sub EventWebResourceResponseReceived(sender As Object, e As CoreWebView2WebResourceResponseReceivedEventArgs)
        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemResponseSwitch.Checked = False Then Return
        Dim request As CoreWebView2WebResourceRequest = e.Request : Dim strRequestUrl As String = request.Uri
        Dim response As CoreWebView2WebResourceResponseView = e.Response
        If response.StatusCode <> 200 Then Debug.WriteLine(response.StatusCode) : Return
        ' Debug.WriteLine(strRequestUrl)
        'Debug.WriteLine(Now() & "：" & strCurrentJsonUrlListItem & Chr(13))
        For Each key As JToken In Globals.ThisAddIn.jsonResponseFilter.Item("data")
            If InStr(System.Web.HttpUtility.UrlDecode(strRequestUrl), System.Web.HttpUtility.UrlDecode(key.Item("url").Value(Of String))) Then
                xlApp.Run("frmShow")
                Dim uriRequest As New Uri(key.Item("url"))
                Dim content As String = Await getResponseContext(response)
                Debug.WriteLine(Now() & "：" & key.Item("url").Value(Of String) & Chr(13))
                If content <> "" And content.Length > 10 Then
                    If runVBAFunction(uriRequest, content) = True Then
                        With Globals.ThisAddIn.tpConstomWebVieTaskPanel
                            .ExecuteJavaScriptAsync()
                            .conMenuAllSetup.Enabled = False : .conMenuAllSetup.Enabled = True
                        End With
                    Else
                        If MsgBox("数据解析失败！是否让大模型进行解析输出？", MsgBoxStyle.YesNo, "请选择是否大模型解析输出") = MsgBoxResult.Yes Then
                            DeepSeek.jsonToVBA(content)
                        End If
                    End If
                End If
                xlApp.Run("frmHidden")
                Return
            End If
        Next key
    End Sub 'webview2控件网络资源响应事件执行程序
    Public Sub EventDownloadStarting(sender As Object, e As CoreWebView2DownloadStartingEventArgs)
        MdlLoad.strFileFullName = e.ResultFilePath
        'MsgBox(e.ResultFilePath)
        Debug.WriteLine("DownloadStarting:" & MdlLoad.strFileFullName)
        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestSwitch.Checked = False Then Return
        If InStr(MdlLoad.strFileFullName, ".xls") Then
            Dim blobUrl As String = e.DownloadOperation.Uri
            If blobUrl.StartsWith("blob:") Then
                Debug.WriteLine("检测到 blob URL: " & blobUrl)
                HandleBlobUrl(blobUrl)
                e.Cancel = True ' 取消默认下载操作
                Return
            End If
            xlApp.Run("frmShow")
            Dim strFileName As String = WebViewEvents.downloadFileToExcel(e.DownloadOperation.Uri).Result
            If strFileName <> "" Then
                Dim uriRequestFile As New Uri((e.DownloadOperation.Uri))
                uriRequestFile = New Uri(Replace(uriRequestFile.AbsoluteUri, uriRequestFile.Query, ""))
                uriRequestFile = New Uri(uriRequestFile.AbsoluteUri.Substring(0, uriRequestFile.AbsoluteUri.LastIndexOf("/") + 1) & Path.GetFileNameWithoutExtension(strFileName))
                MdlLoad.runVBAFunction(uriRequestFile, strFileName)
                With Globals.ThisAddIn.tpConstomWebVieTaskPanel
                    .conMenuAllSetup.Enabled = False : .conMenuAllSetup.Enabled = True
                End With
                e.Cancel = True
            End If
            xlApp.Run("frmHidden")
        End If
    End Sub 'webview2控件下载事件执行程序

    Public Sub EventWebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs)
        Dim base64Data As String = e.WebMessageAsJson
        If base64Data.StartsWith("""data:text/plain;base64,") Then
            SaveBase64ToFile(base64Data, MdlLoad.strFileFullName)
        End If
    End Sub 'WebView2控件消息接收事件执行程序
    Public Sub EventNavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs)
        'sender.AddScriptToExecuteOnDocumentCreatedAsync("<script>const iframe = document.getElementsByTagNace('iframe')[0];iframe.addEventListener('load', () => {window.chrome.webview.postMessage('注入脚本成功')});</script>")
    End Sub 'webview2控件导航开始事件执行程序
    Public Sub EventNavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)
        Debug.WriteLine("NavigationCompleted:" & sender.source)
    End Sub 'webview2控件导航完成事件执行程序

    Public Sub EventDOMContentLoaded(sender As Object, e As CoreWebView2DOMContentLoadedEventArgs)
        Debug.WriteLine("DOMContentLoaded:" & e.NavigationId)
    End Sub 'webview2控件DOM加载完成事件执行程序
    Public Sub EventFrameNavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs)
        Debug.WriteLine("FrameNavigationStarting:" & e.NavigationId.ToString())
    End Sub 'webview2控件框架导航开始事件执行程序
    Public Async Sub EventFrameNavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)
        If e.HttpStatusCode <> 200 Then Return
        If e.IsSuccess = False Then Return
        Debug.WriteLine("FrameNavigationCompleted" & e.NavigationId.ToString())
        'Dim strResult1 As CoreWebView2ExecuteScriptResult = Await Me.wvCoreWevview2.ExecuteScriptWithResultAsync("window.chrome.webview.postMessage(document.querySelector('iframe').contentDocument.location.href)")
        With Globals.ThisAddIn.tpConstomWebVieTaskPanel
            .tooltxtLable.Text = ""
            .tooltxtBackward.Text = ""
            .tooltxtForward.Text = ""
            .lbPage.Text = "......"

            Dim strResult As CoreWebView2ExecuteScriptResult = Await .wvCoreWevview2.ExecuteScriptWithResultAsync("document.querySelector('iframe').contentDocument.location.href")
            If strResult.ResultAsJson = "" Or strResult.ResultAsJson = "null" Then
                Return
            End If
            Dim objFrameDocumentUrl As New System.Uri(strResult.ResultAsJson.Replace("""", ""))
            Dim strCurrentBottomButtonScript As String = objFrameDocumentUrl.AbsolutePath
            strCurrentBottomButtonScript = Regex.Replace(strCurrentBottomButtonScript, "[^a-zA-Z0-9]", "")
            If .dicBottomButtonScript.ContainsKey(strCurrentBottomButtonScript) Then
                .tooltxtLable.Text = .dicBottomButtonScript(strCurrentBottomButtonScript)("lbPage")
                .tooltxtBackward.Text = .dicBottomButtonScript(strCurrentBottomButtonScript)("tooltxtBackward")
                .tooltxtForward.Text = .dicBottomButtonScript(strCurrentBottomButtonScript)("tooltxtForward")
            End If
        End With
    End Sub 'webview2控件框架导航完成事件执行程序

    Public Sub EventNewWindowRequested(sender As Object, e As CoreWebView2NewWindowRequestedEventArgs)
        Debug.WriteLine("NewWindowRequested:" & e.Uri)
        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestSwitch.Checked = False Then Return
        If InStr(e.Uri, ".xls") Then
            xlApp.Run("frmShow")
            e.Handled = True
            Dim strFileName As String = WebViewEvents.downloadFileToExcel(e.Uri).Result
            If strFileName <> "" Then
                Dim uriRequestFile As New Uri((e.Uri))
                uriRequestFile = New Uri(Replace(uriRequestFile.AbsoluteUri, uriRequestFile.Query, ""))
                uriRequestFile = New Uri(uriRequestFile.AbsoluteUri.Substring(0, uriRequestFile.AbsoluteUri.LastIndexOf("/") + 1) & Path.GetFileNameWithoutExtension(strFileName))
                MdlLoad.runVBAFunction(uriRequestFile, strFileName)
                With Globals.ThisAddIn.tpConstomWebVieTaskPanel
                    .conMenuAllSetup.Enabled = False : .conMenuAllSetup.Enabled = True
                End With
            End If
            xlApp.Run("frmHidden")
        Else
            CType(sender, Microsoft.Web.WebView2.Core.CoreWebView2).Navigate(e.Uri)
        End If
    End Sub 'webview2控件新窗口请求事件执行程序

    Async Function downloadFileToExcel(strUrl As String) As Threading.Tasks.Task(Of String)
        Dim uriFileName As New Uri(strUrl)
        Dim fileName As String = Regex.Replace(System.Web.HttpUtility.UrlDecode(uriFileName.AbsolutePath.Substring(uriFileName.AbsolutePath.LastIndexOf("/") + 1)), "([^\u4e00-\u9fa5]{0,}$)", "")
        Using httpClient As New System.Net.Http.HttpClient()
            Dim response As System.Net.Http.HttpResponseMessage = Await httpClient.GetAsync(strUrl)
            response.EnsureSuccessStatusCode()
            Dim contentStream As Byte() = Await response.Content.ReadAsByteArrayAsync()
            Dim unixTimestamp As String = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds
            Dim filePath As String
            If fileName <> "" Then
                filePath = Path.Combine(System.IO.Path.GetTempPath(), fileName & ".xls")
            Else
                filePath = Path.Combine(System.IO.Path.GetTempPath(), unixTimestamp & ".xls")
            End If
            Try
                File.WriteAllBytes(filePath, contentStream)
                Return filePath
            Catch ex As Exception
                MsgBox("Error writing content to file: " & ex.Message)
            End Try
        End Using
        Return ""
    End Function '根据URL地址下载文件到临时目录，并将文件名返回

    Public Async Sub HandleBlobUrl(blobUrl As String)
        Dim script As String = $"
        fetch('{blobUrl}')
            .then(response => response.blob())
            .then(blob => {{
                const reader = new FileReader();
                reader.onload = () => {{
                    window.chrome.webview.postMessage(reader.result);
                }};
                reader.readAsDataURL(blob);
            }})
            .catch(error => console.error('Error fetching blob:', error));
    "
        Await Globals.ThisAddIn.tpConstomWebVieTaskPanel.wvCoreWevview2.ExecuteScriptAsync(script)
    End Sub
    Public Sub SaveBase64ToFile(base64Data As String, filePath As String)
        Dim bytes As Byte() = Convert.FromBase64String(base64Data.Replace("data:text/plain;base64,", "").Replace("""", ""))
        File.WriteAllBytes(filePath, bytes)
        'Debug.WriteLine("文件已保存到: " & filePath)
        Dim uriRequestFile As New Uri("http://strVBAFunctionName")
        MdlLoad.runVBAFunction(uriRequestFile, filePath)

    End Sub
End Module
