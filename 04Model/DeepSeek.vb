
Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Diagnostics
Imports System.Net
Imports System.Net.Http

Module DeepSeek

    Async Function callDeepSeek(strPrompt As String, Optional fileContent As String = "", Optional strSystemRole As String = "") As Task(Of String)
        ' 替换为你的实际API密钥
        Dim apiKey As String = "sk-2616b64ba296478fb67157d430314c3f"
        Dim apiUrl As String = "https://api.deepseek.com/chat/completions"
        ' SSL设置（仅开发环境）
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, errors) True ' 生产环境应移除

        ' 配置HttpClient
        Dim handler As New HttpClientHandler() With {
        .UseProxy = False,
        .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
    }
        ' 创建HttpClient实例
        Using client As New HttpClient(handler)
            ' 设置请求头
            client.Timeout = TimeSpan.FromSeconds(300)
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}")
            client.DefaultRequestHeaders.Accept.Add(New Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))

            ' 准备请求数据
            Dim requestData As New With {
                .model = "deepseek-chat",
                .messages = {
                    New With {.role = "system", .content = strSystemRole},
                    New With {
                        .role = "user",
                        .content = strPrompt & $"\n\n{fileContent}"
                    }
                },
                .stream = False
            }

            ' 序列化为JSON
            Dim jsonContent As String = System.Text.Json.JsonSerializer.Serialize(requestData)
            Dim content As New StringContent(jsonContent, Encoding.Default, "application/json")

            Try
                ' 发送POST请求
                Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

                ' 检查响应状态
                If response.IsSuccessStatusCode Then
                    ' 读取响应内容
                    Dim responseContent As String = Await response.Content.ReadAsStringAsync()

                    ' 解析JSON响应
                    Dim jsonDoc = System.Text.Json.JsonDocument.Parse(responseContent)
                    Dim summary = jsonDoc.RootElement.GetProperty("choices")(0).GetProperty("message").GetProperty("content").GetString()
                    Return summary
                    Debug.WriteLine(summary)
                Else
                    Debug.WriteLine($"请求失败，状态码: {response.StatusCode}")
                    Debug.WriteLine($"错误详情: {Await response.Content.ReadAsStringAsync()}")
                    Return ""
                End If
            Catch ex As Exception
                Debug.WriteLine($"发生异常: {ex.Message}")
                Return ""
            End Try
        End Using
    End Function

    Sub jsonToVBA（content As String）
        If content.Length <= 3 Then Return
        ' 调用大模型分析JSON内容
        If content.Substring(0, 1) <> "{" Or content.Substring(content.Length - 1, 1) <> "}" Then Return

        Dim prompt As String = "以下内容为Json格式的文本，将有意义的内容通过VBA宏输出到excel当前工作簿， _
                                对你提供的VBA宏的要求：宏名称为jsonToExcel，宏的参数为strJson as string，将Json的内容以参数方式传递给宏；
                                如果提取的字段为英文就将字段名翻译成中文名称；对Json内容解析请使用frmLoading_ChungJee.ParseJson(ByVal JsonString As String)这个函数；_
                                输出到Excel工作表时，要考虑到美观，如果有可能请使用超级表进行输出。" & Chr(13) &
                                "对你的返回结果要求如下：因为我需要将你返回的内容直接写入vba的工程中，所以你需要只给出vba代码，不要有其他任何无关内容，也不要有任何注释,包括返回结果的开头和结尾也不要使用注释，以保证返回代码能够直接运行。"

        Dim vbaCode As String = DeepSeek.callDeepSeek(prompt, content, "你是一个Json格式内容分析小能手").GetAwaiter().GetResult()

        If Not String.IsNullOrEmpty(vbaCode) Then
            Dim xlApp As Excel.Application = Globals.ThisAddIn.Application
            Dim vbProject As Microsoft.Vbe.Interop.VBProject = xlApp.ActiveWorkbook.VBProject
            Dim vbComponent As Microsoft.Vbe.Interop.VBComponent
            Try
                vbComponent = vbProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule)
                vbComponent.Name = "GeneratedCode"
                vbComponent.CodeModule.AddFromString(vbaCode)

                ' 执行大模型生成的VBA代码
                xlApp.Run("GeneratedCode.jsonToExcel", content)
            Catch ex As Exception
                Debug.WriteLine("写入或执行VBA代码时出错: " & ex.Message)
            End Try
        End If

    End Sub
End Module