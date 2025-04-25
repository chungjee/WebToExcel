Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks

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
            Return responseObj?.choices?.FirstOrDefault()?.message?.content ?? "No response"
        Catch
            Return "Failed to parse response"
        End Try
    End Function
End Class

' 使用示例
Module Program
    Sub Main()
        Dim apiKey = "your_api_key_here"
        Dim client = New OpenRouterClient(apiKey)

        Dim request = New OpenRouterRequest With {
            .model = "gpt-3.5-turbo",
            .messages = New List(Of Message) From {
                New Message With {.role = "user", .content = "写一首关于秋天的诗"}
            }
        }

        Dim response = client.GetChatResponseAsync(request).Result
        Console.WriteLine("OpenRouter响应：")
        Console.WriteLine(response)
        Console.ReadLine()
    End Sub
End Module
