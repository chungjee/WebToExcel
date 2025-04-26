Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks

' �����������Ӧģ��
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

' OpenRouter�ͻ�����
Public Class OpenRouterClient
    Private ReadOnly httpClient As HttpClient
    Private ReadOnly apiKey As String
    Private Const ApiUrl As String = "https://openrouter.ai/api/v1/chat/completions"

    Public Sub New(apiKey As String)
        Me.apiKey = apiKey
        httpClient = New HttpClient()
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}")
        httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "YOUR_WEBSITE_URL") ' ��ѡ
        httpClient.DefaultRequestHeaders.Add("X-Title", "YOUR_APP_NAME") ' ��ѡ
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
            If responseObj IsNot Nothing AndAlso responseObj.choices IsNot Nothing AndAlso responseObj.choices.Any() Then
                Return responseObj.choices.First().message.content
            Else
                Return "No response"
            End If
        Catch
            Return "Failed to parse response"
        End Try
    End Function
End Class

' ʹ��ʾ��
Module Program
    Sub Main()
        Dim apiKey = "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"
        Dim client = New OpenRouterClient(apiKey)

        Dim request = New OpenRouterRequest With {
            .model = "gpt-3.5-turbo",
            .messages = New List(Of Message) From {
                New Message With {.role = "user", .content = "дһ�׹��������ʫ"}
            }
        }

        Dim response = client.GetChatResponseAsync(request).Result
        Console.WriteLine("OpenRouter��Ӧ��")
        Console.WriteLine(response)
        Console.ReadLine()
    End Sub
End Module
