Imports System.Security.Policy
Imports System.IO
Imports System.Diagnostics
Imports Newtonsoft.Json
Imports System.Net
Imports System.Text
Module NasModel
    Public Function nasRequestWebFile(ByVal strUrl As String, ByVal strFileName As String) As Boolean
        Dim objWebClient As New System.Net.WebClient()
        Dim strFileFullName As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), strFileName)
        Try
            Try
                objWebClient.DownloadFile(strUrl, strFileFullName)
                Return True
            Catch ex1 As Exception
                Debug.WriteLine(ex1.Message)
                Return False
            End Try
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function nasRequestSid(ByVal strUrl As String) As String
        Dim objWebClient As New System.Net.WebClient()
        Dim strResponse As String
        Try
            strResponse = objWebClient.DownloadString(strUrl)
            Return strResponse
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function nasDownloadFile(strUrl As String, strFileName As String) As String
        Dim result As String
        Dim objWebClient As New System.Net.WebClient()
        Try
            objWebClient.DownloadFile(strUrl, strFileName)
            Dim fileContent As String = File.ReadAllText(strFileName, Encoding.UTF8)
            '以 ANSI 编码重新写入文件
            File.WriteAllText(strFileName, fileContent, Encoding.Default)
            result = strFileName
        Catch ex As Exception
            ' 处理异常
            Debug.WriteLine("下载失败: " & ex.Message)
            result = String.Empty
        End Try
        Return result
    End Function

    Public Function UploadFileToNas(fileBytes As Byte(), strHostName As String, strSessionId As String, Optional path As String = "/home/excelWeview2", Optional destFileName As String = "", Optional overwrite As String = "true") As String

        Dim boundary As String = "WebKitFormBoundarywwchungjeecomwwwhwakongcn"
        Dim postData As New StringBuilder()
        Dim Url As String = strHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Upload&version=2&method=upload&_sid=" & strSessionId

        ' 构建表单数据
        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""overwrite""")
        postData.AppendLine()
        postData.AppendLine(overwrite)

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""path""")
        postData.AppendLine()
        postData.AppendLine(path)

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""mtime""")
        postData.AppendLine()
        postData.AppendLine(DateToUnixTime(DateTime.Now).ToString())

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""size""")
        postData.AppendLine()
        postData.AppendLine(CStr(fileBytes.Length * 8))

        postData.AppendLine("--" & boundary)
        If String.IsNullOrEmpty(destFileName) Then
            destFileName = path
        End If
        postData.AppendLine($"Content-Disposition: form-data; name=""file""; filename=""{destFileName}""")
        postData.AppendLine("Content-Type: application/octet-stream")
        postData.AppendLine()

        Dim fileContent As String = Encoding.Default.GetString(fileBytes)
        postData.Append(fileContent)
        postData.AppendLine()
        postData.AppendLine("--" & boundary & "--")

        ' 转换为字节数组
        Dim postBytes As Byte() = Encoding.UTF8.GetBytes(postData.ToString())

        ' 创建 HTTP 请求
        Dim httpRequest As HttpWebRequest = CType(WebRequest.Create(Url), HttpWebRequest)
        httpRequest.Method = "POST"
        httpRequest.ContentType = "multipart/form-data; boundary=" & boundary
        httpRequest.ContentLength = postBytes.Length

        ' 发送数据
        Using requestStream As Stream = httpRequest.GetRequestStream()
            requestStream.Write(postBytes, 0, postBytes.Length)
        End Using

        ' 获取响应
        Try
            Using httpResponse As HttpWebResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = httpResponse.GetResponseStream()
                    Using reader As New StreamReader(responseStream, Encoding.UTF8)
                        Return reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As WebException
            Using responseStream As Stream = ex.Response.GetResponseStream()
                Using reader As New StreamReader(responseStream, Encoding.UTF8)
                    Return "Error: " & reader.ReadToEnd()
                End Using
            End Using
        End Try
    End Function
    Public Function UploadFileToNas(strFileName As String, strHostName As String, strSessionId As String, Optional path As String = "/home/excelWeview2", Optional destFileName As String = "", Optional overwrite As String = "true") As String

        Dim boundary As String = "WebKitFormBoundarywwchungjeecomwwwhwakongcn"
        Dim postData As New StringBuilder()
        'My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Upload&version=2&method=upload&_sid=" & My.ChungJee.Default.strNasSid
        Dim Url As String = strHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Upload&version=2&method=upload&_sid=" & strSessionId

        ' 构建表单数据
        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""overwrite""")
        postData.AppendLine()
        postData.AppendLine(overwrite)

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""path""")
        postData.AppendLine()
        postData.AppendLine(path)

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""mtime""")
        postData.AppendLine()
        postData.AppendLine(DateToUnixTime(DateTime.Now).ToString())

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""size""")
        postData.AppendLine()
        postData.AppendLine(New FileInfo(strFileName).Length.ToString())

        postData.AppendLine("--" & boundary)
        If String.IsNullOrEmpty(destFileName) Then
            destFileName = path
        End If
        postData.AppendLine($"Content-Disposition: form-data; name=""file""; filename=""{destFileName}""")
        postData.AppendLine("Content-Type: application/octet-stream")
        postData.AppendLine()

        ' 读取文件内容
        Dim fileBytes As Byte() = File.ReadAllBytes(strFileName)
        Dim fileContent As String = Encoding.UTF8.GetString(fileBytes)
        postData.Append(fileContent)
        postData.AppendLine()
        postData.AppendLine("--" & boundary & "--")

        ' 转换为字节数组
        Dim postBytes As Byte() = Encoding.UTF8.GetBytes(postData.ToString())

        ' 创建 HTTP 请求
        Dim httpRequest As HttpWebRequest = CType(WebRequest.Create(Url), HttpWebRequest)
        httpRequest.Method = "POST"
        httpRequest.ContentType = "multipart/form-data; boundary=" & boundary
        httpRequest.ContentLength = postBytes.Length

        ' 发送数据
        Using requestStream As Stream = httpRequest.GetRequestStream()
            requestStream.Write(postBytes, 0, postBytes.Length)
        End Using

        ' 获取响应
        Try
            Using httpResponse As HttpWebResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = httpResponse.GetResponseStream()
                    Using reader As New StreamReader(responseStream, Encoding.UTF8)
                        Return reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As WebException
            Using responseStream As Stream = ex.Response.GetResponseStream()
                Using reader As New StreamReader(responseStream, Encoding.UTF8)
                    Return "Error: " & reader.ReadToEnd()
                End Using
            End Using
        End Try
    End Function

    Private Function DateToUnixTime(dateTime As DateTime) As Long
        Dim unixStart As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        Return CLng((dateTime.ToUniversalTime() - unixStart).TotalSeconds)
    End Function
    Public Function nasGetSid(strHostName As String, strUserName As String, strPassword As String) As String
        Dim objWebClient As New System.Net.WebClient()
        Dim strResponse As String
        Dim strUrl As String = strHostName & "/webapi/auth.cgi?api=SYNO.API.Auth&version=3&method=login&account=" & strUserName & "&passwd=" & strPassword & "&session=FileStation&format=sid"
        Try
            strResponse = objWebClient.DownloadString(strUrl)
            Return strResponse
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Module
