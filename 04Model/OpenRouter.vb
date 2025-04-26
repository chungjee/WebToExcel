Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Diagnostics
Imports System.Web.UI.WebControls

' 定义请求和响应模型
Public Class OpenRouterRequest
    Public Property model As String
    Public Property messages As List(Of Message)
End Class

Public Class Message
    Public Property role As String
    Public Property content As String
End Class

Public Class OpenRouterResponse
    Public Property choices As List(Of Choice)
End Class

Public Class Choice
    Public Property message As Message
End Class

' OpenRouter客户端类
Public Class OpenRouterClient
    Private ReadOnly httpClient As HttpClient
    Private ReadOnly apiKey As String
    Private Const ApiUrl As String = "https://openrouter.ai/api/v1/chat/completions"

    Public Sub New(apiKey As String)
        Me.apiKey = apiKey
        httpClient = New HttpClient()
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}")
        httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "YOUR_WEBSITE_URL") ' 可选
        httpClient.DefaultRequestHeaders.Add("X-Title", "YOUR_APP_NAME") ' 可选
    End Sub

    Public Async Function GetChatResponseAsync(request As OpenRouterRequest) As Task(Of String)
        Try
            Dim jsonRequest = JsonConvert.SerializeObject(request)
            Dim content = New StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json")

            Dim response = Await httpClient.PostAsync(ApiUrl, content)
            response.EnsureSuccessStatusCode()

            Dim jsonResponse = Await response.Content.ReadAsStringAsync()
            Return ParseResponse(jsonResponse)
        Catch ex As HttpRequestException
            Return $"Error: {ex.InnerException} - {ex.Message}"
        Catch ex As Exception
            Return $"Error: {ex.Message}"
        End Try
    End Function

    Private Function ParseResponse(jsonResponse As String) As String
        Try
            Dim responseObj = JsonConvert.DeserializeObject(Of OpenRouterResponse)(jsonResponse)
            Return If(responseObj?.choices?.FirstOrDefault()?.message?.content, "No response")
        Catch
            Return "Failed to parse response"
        End Try
    End Function
End Class

' 使用示例
Module Program
    Sub jsonToVBA(strContent As String, strFunctionName As String)
        If strContent.Length <= 3 Then Return
        If strContent.Substring(0, 1) <> "{" Or strContent.Substring(strContent.Length - 1, 1) <> "}" Then Return
        Dim strOldFunctionName As String = "jsonToExcel"
        Dim strBigModelName As String = "microsoft/mai-ds-r1:free"
        Dim strSystemPrompt As String = "你是一个Json格式内容分析小能手"
        Dim strUserPrompt As String = "以下内容为JSON格式的文本，对JSON内容进行分析，将你认为有意义的内容，通过编写一个Visual Basic宏输出到excel当前工作簿,对VBA宏的要求是：宏名称为" & strOldFunctionName & "，宏的参数为strJson as string，将Json的内容以参数方式传递给宏;如果提取的字段为英文就将字段名翻译成中文名称；对Json内容解析请使用frmLoading_ChungJee.ParseJson(ByVal JsonString As String)这个函数；输出到Excel工作表时，要考虑到美观，如果有可能请使用超级表进行输出;在函数体的首行加入错误处理语句""On Error Resume Next""，以保证VBA宏能完整运行。对返回结果的要求是：因为需要将返回的内容直接写入vba的工程中，所以要求只给出VBA代码，不要有其他任何无关内容，也不要有任何注释,返回结果的开头和结尾也不要有注释，以保证返回的代码能够直接运行。"
        Dim apiKey = "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"
        Dim client = New OpenRouterClient(apiKey)
        Dim request = New OpenRouterRequest With {
            .model = strBigModelName,
            .messages = New List(Of Message) From {
                New Message With {.role = "system", .content = ""},
                New Message With {.role = "user", .content = strUserPrompt & Chr(13) & Chr(13) & strContent}
            }
        }
        Debug.WriteLine("请求内容: " & strUserPrompt & strContent)
        Dim vbaCode As String = client.GetChatResponseAsync(request).Result
        Debug.WriteLine("返回内容: " & vbaCode)
        If vbaCode.Contains(strOldFunctionName) = False Then MsgBox(vbaCode) : Return
        If vbaCode.Substring(0, vbaCode.IndexOfAny(New Char() {vbCr, vbLf})).Contains(strOldFunctionName) = False Then
            vbaCode = vbaCode.Substring(vbaCode.IndexOfAny(New Char() {vbCr, vbLf}) + 1)
            vbaCode = vbaCode.Substring(0, vbaCode.IndexOf("End Sub") + 7)
        End If
        vbaCode = vbaCode.Replace(strOldFunctionName, strFunctionName)
        ' 将大模型返回的VBA代码写入当前工作表的VBA工程中
        If Not String.IsNullOrEmpty(vbaCode) Then
            Dim xlApp As Excel.Application = Globals.ThisAddIn.Application
            Dim vbProject As Microsoft.Vbe.Interop.VBProject = xlApp.ActiveWorkbook.VBProject
            Dim vbComponent As Microsoft.Vbe.Interop.VBComponent
            Dim strVBModuleName As String
            If strFunctionName.Length > 30 Then
                strVBModuleName = strFunctionName.Substring(0, 30)
            Else
                strVBModuleName = strFunctionName
            End If
            Try
                vbComponent = vbProject.VBComponents.Item(strVBModuleName)
                If vbComponent.CodeModule.CountOfLines > 0 Then vbComponent.CodeModule.DeleteLines(1, vbComponent.CodeModule.CountOfLines)
                vbComponent.CodeModule.AddFromString(vbaCode)
                xlApp.Run(strVBModuleName & "." & strFunctionName, strContent)

            Catch ex As Exception
                Try
                    vbComponent = vbProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule)
                    vbComponent.Name = strVBModuleName
                    vbComponent.CodeModule.AddFromString(vbaCode)
                    xlApp.Run(strFunctionName, strContent)
                Catch ex1 As Exception
                    Debug.WriteLine("error: " & ex1.Message)
                End Try
            End Try
        End If
    End Sub
End Module
