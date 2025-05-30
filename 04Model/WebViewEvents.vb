﻿Imports System.Diagnostics
Imports System.IO
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json.Linq

Module WebViewEvents  'WebView2控件事件处理模块
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
        If strCurrentJsonUrlListItem = "" Then xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden) ： Return
        'Debug.WriteLine(Now() & strCurrentJsonUrlListItem & Chr(13))
        xlApp.Run(Globals.Ribbons.Ribbon1.strFrmShow)
        If UCase(e.Request.Method) = "POST" Then
            If strOldValue = "" Or strNewValue = "" Then xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden) ： Return
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
        'Debug.WriteLine("WebResourceResponseReceived" & Now() & "：" & request.Uri & Chr(13))
        For Each key As JToken In Globals.ThisAddIn.jsonResponseFilter.Item("data")
            If InStr(System.Web.HttpUtility.UrlDecode(strRequestUrl), System.Web.HttpUtility.UrlDecode(key.Item("url").Value(Of String))) Then
                xlApp.Run(Globals.Ribbons.Ribbon1.strFrmShow)
                Dim uriRequest As New Uri(key.Item("url"))
                Dim content As String = Await getResponseContext(response)
                Debug.WriteLine(Now() & "：" & key.Item("url").Value(Of String) & Chr(13))
                If content <> "" And content.Length > 10 Then
                    Dim strVBAFunctionName As String = uriRequestToVBAFunctionName(uriRequest)
                    If runVBAFunction(strVBAFunctionName, content) = True Then
                        Globals.ThisAddIn.tpConstomWebVieTaskPanel.ExecuteJavaScriptAsync()
                    Else
                        Try
                            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden)
                        Catch ex As Exception

                        End Try
                        If MsgBox("数据解析失败！是否让大模型进行解析输出？", MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, "提示") = MsgBoxResult.Yes Then
                            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmShow)
                            Program.jsonToVBA(content, strVBAFunctionName)
                        End If
                    End If
                End If
                Try
                    xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden)
                Catch ex As Exception
                End Try
                Return
            End If
        Next key
    End Sub 'webview2控件网络资源响应事件执行程序
    Public Async Sub EventDownloadStarting(sender As Object, e As CoreWebView2DownloadStartingEventArgs)
        MdlLoad.strFileFullName = e.ResultFilePath
        'MsgBox(e.ResultFilePath)
        Debug.WriteLine("DownloadStarting:" & MdlLoad.strFileFullName)

        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestSwitch.Checked = False Then Return
        If InStr(MdlLoad.strFileFullName, ".xls") Then
            Dim blobUrl As String = e.DownloadOperation.Uri
            If blobUrl.StartsWith("blob:") Then
                HandleBlobUrl(blobUrl)
                e.Cancel = True ' 取消默认下载操作
                Return
            End If
            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmShow)
            Dim strFileName As String = Regex.Replace(Path.GetFileName(MdlLoad.strFileFullName), "([^\u4e00-\u9fa5]{0,}$)", "")
            If strFileName = "" Then strFileName = Path.GetFileNameWithoutExtension(MdlLoad.strFileFullName)
            Dim objUrl As New Uri(e.DownloadOperation.Uri) : objUrl = New Uri(Replace(objUrl.AbsoluteUri, objUrl.Query, ""))
            Dim objNetCookie As IList(Of System.Net.Cookie) = New List(Of System.Net.Cookie)

            Try
                Dim cookieManager As CoreWebView2CookieManager = Globals.ThisAddIn.tpConstomWebVieTaskPanel.wvCoreWevview2.CookieManager
                Dim cookies As IList(Of CoreWebView2Cookie) = Await cookieManager.GetCookiesAsync(e.DownloadOperation.Uri)
                ' 遍历并输出 Cookie 信息
                For Each cookie As CoreWebView2Cookie In cookies
                    If objUrl.Host.Contains(cookie.Domain) Then
                        objNetCookie.Insert(0, cookie.ToSystemNetCookie)
                        'objNetCookie.Add(New System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain))
                        'If cookie.Name = "lzkqow38189" Then
                        'objNetCookie.Add(New System.Net.Cookie("JSESSIONID", cookie.Value, cookie.Path, cookie.Domain))
                        'End If
                    End If
                Next
                strFileName = WebViewEvents.downloadFileToExcel(e.DownloadOperation.Uri, strFileName, objUrl, objNetCookie).Result
            Catch ex As Exception
                Debug.WriteLine("获取 Cookie 时出错: " & ex.Message)
                Return
            End Try

            If strFileName <> "" Then
                Dim uriRequestFile As New Uri((e.DownloadOperation.Uri))
                uriRequestFile = New Uri(Replace(uriRequestFile.AbsoluteUri, uriRequestFile.Query, ""))
                uriRequestFile = New Uri(uriRequestFile.AbsoluteUri.Substring(0, uriRequestFile.AbsoluteUri.LastIndexOf("/") + 1) & Path.GetFileNameWithoutExtension(strFileName))
                MdlLoad.runVBAFunction(uriRequestToVBAFunctionName(uriRequestFile), strFileName)
                With Globals.ThisAddIn.tpConstomWebVieTaskPanel
                    .conMenuAllSetup.Enabled = False : .conMenuAllSetup.Enabled = True
                End With
                e.Cancel = True
            End If
            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden)
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
        Debug.WriteLine("NavigationStarting:" & e.Uri)
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
            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmShow)
            e.Handled = True
            Dim strFileName As String = WebViewEvents.downloadFileToExcel(e.Uri).Result
            If strFileName <> "" Then
                Dim uriRequestFile As New Uri((e.Uri))
                uriRequestFile = New Uri(Replace(uriRequestFile.AbsoluteUri, uriRequestFile.Query, ""))
                uriRequestFile = New Uri(uriRequestFile.AbsoluteUri.Substring(0, uriRequestFile.AbsoluteUri.LastIndexOf("/") + 1) & Path.GetFileNameWithoutExtension(strFileName))
                MdlLoad.runVBAFunction(uriRequestToVBAFunctionName(uriRequestFile), strFileName)
                With Globals.ThisAddIn.tpConstomWebVieTaskPanel
                    .conMenuAllSetup.Enabled = False : .conMenuAllSetup.Enabled = True
                End With
                e.Handled = True
            End If
            xlApp.Run(Globals.Ribbons.Ribbon1.strFrmHidden)
        Else
            CType(sender, Microsoft.Web.WebView2.Core.CoreWebView2).Navigate(e.Uri)
            e.Handled = True
        End If
    End Sub 'webview2控件新窗口请求事件执行程序

    Async Function downloadFileToExcel(strUrl As String, Optional strFileName As String = "", Optional objUri As Uri = Nothing, Optional objCookies As List(Of System.Net.Cookie) = Nothing) As Threading.Tasks.Task(Of String)
        Dim uriFileName As New Uri(strUrl)
        If strFileName = "" Then strFileName = Regex.Replace(System.Web.HttpUtility.UrlDecode(uriFileName.AbsolutePath.Substring(uriFileName.AbsolutePath.LastIndexOf("/") + 1)), "([^\u4e00-\u9fa5]{0,}$)", "")
        Dim handler As New HttpClientHandler()
        handler.CookieContainer = New Net.CookieContainer()

        If objCookies IsNot Nothing Then
            For Each objCookie As System.Net.Cookie In objCookies
                handler.CookieContainer.Add(objUri, objCookie)
            Next
        End If
        Using httpClient As New System.Net.Http.HttpClient(handler)
            Dim response As System.Net.Http.HttpResponseMessage = Await httpClient.GetAsync(strUrl)
            response.EnsureSuccessStatusCode()
            Dim contentStream As Byte() = Await response.Content.ReadAsByteArrayAsync()
            Dim requestBody As String = ""
            Dim filePath As String
            If strFileName <> "" Then
                filePath = Path.Combine(System.IO.Path.GetTempPath(), strFileName & ".xls")
            Else
                filePath = Path.Combine(System.IO.Path.GetTempPath(), (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds & ".xls")
            End If

            If contentStream IsNot Nothing Then
                ' 如果 contentStream 是 Byte() 类型，将其包装为 MemoryStream
                Using memoryStream As New MemoryStream(contentStream)
                    Using reader As New StreamReader(memoryStream, System.Text.Encoding.UTF8)
                        requestBody = reader.ReadToEnd()
                    End Using
                End Using
                'Debug.WriteLine("请求体内容: " & requestBody)
                If requestBody.StartsWith("{") And requestBody.EndsWith("}") Then
                    WebViewEvents.SaveBase64ToFile(Newtonsoft.Json.Linq.JObject.Parse(requestBody).Item("data"), strFileName)
                    'Dim uriRequestFile As New Uri("http://strVBAFunctionName")
                    'MdlLoad.runVBAFunction(uriRequestToVBAFunctionName(uriRequestFile), filePath)
                    Return strFileName
                End If
            Else
                Return ""
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
    End Sub ' webview2控件处理blob URL的函数，通过让JavaScript获取blob数据，并将数据通过postMessage发送到webview2控件，webview2控件再通过webmessagereceived事件接收数据
    Public Sub SaveBase64ToFile(base64Data As String, filePath As String)
        Dim intStartPos As Integer, intStringLen As Integer
        intStartPos = base64Data.IndexOf(""":""") + 2 : intStringLen = base64Data.Length
        Dim bytes As Byte() = Convert.FromBase64String(base64Data.Substring(intStartPos, intStringLen - intStartPos).Replace("""", ""))
        File.WriteAllBytes(filePath, bytes)
        'Debug.WriteLine("文件已保存到: " & filePath)
    End Sub ' webview2控件保存base64数据到文件的函数

    Private Function uriRequestToVBAFunctionName(uriRequest As Uri) As String
        Dim strVBAFunctionName As String
        With uriRequest
            If .AbsolutePath <> "/" Then
                If .Query.Length > 1 Then
                    strVBAFunctionName = .AbsolutePath & .Query
                Else
                    strVBAFunctionName = .AbsolutePath
                End If
            Else
                strVBAFunctionName = .AbsoluteUri.Substring(.AbsoluteUri.LastIndexOf("://") + 3)
            End If '根据URI获取VBA函数名称和模块名称，如果URI中没有绝对路径，则使用URI的主机名称作为函数名称
        End With
        strVBAFunctionName = System.Web.HttpUtility.UrlEncode(Regex.Replace(strVBAFunctionName, "[^A-Z0-9\u4e00-\u9fa5a-zA-Z0-9]", "")）
        strVBAFunctionName = UCase(Regex.Replace(strVBAFunctionName, "[^a-zA-Z0-9]", ""))
        Return strVBAFunctionName
    End Function

    Public Async Sub GetWebView2Cookies()
        Try
            ' 获取 WebView2 的 CookieManager
            Dim cookieManager As CoreWebView2CookieManager = Globals.ThisAddIn.tpConstomWebVieTaskPanel.wvCoreWevview2.CookieManager

            ' 获取所有 Cookie
            Dim cookies As IList(Of CoreWebView2Cookie) = Await cookieManager.GetCookiesAsync("")

            ' 遍历并输出 Cookie 信息
            For Each cookie As CoreWebView2Cookie In cookies
                Debug.WriteLine($"Name: {cookie.Name}, Value: {cookie.Value}, Domain: {cookie.Domain}, Path: {cookie.Path}, Expires: {cookie.Expires}")
            Next
        Catch ex As Exception
            Debug.WriteLine("获取 Cookie 时出错: " & ex.Message)
        End Try
    End Sub
End Module
