Imports System.Diagnostics

Public Class LoginForm

    ' TODO: 插入代码，以使用提供的用户名和密码执行自定义的身份验证
    ' (请参阅 https://go.microsoft.com/fwlink/?LinkId=35339)。  
    ' 随后自定义主体可附加到当前线程的主体，如下所示: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' 其中 CustomPrincipal 是用于执行身份验证的 IPrincipal 实现。
    ' 随后，My.User 将返回 CustomPrincipal 对象中封装的标识信息
    ' 如用户名、显示名等

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Me.UsernameTextBox.Text = "" Then
            MsgBox("请输入用户名")
            Return
        Else
            My.ChungJee.Default.strNasUserName = Me.UsernameTextBox.Text
            If Me.chkRemember.Checked = True Then My.ChungJee.Default.strNasPassWord = Me.PasswordTextBox.Text
            My.ChungJee.Default.Save()
        End If
        Dim strResponseJson As String = NasModel.nasGetSid(My.ChungJee.Default.strNasHostName, My.ChungJee.Default.strNasUserName, Me.PasswordTextBox.Text)
        Dim objResponseJson As Newtonsoft.Json.Linq.JObject
        If strResponseJson.Contains("""success"":false") Then
            MsgBox("用户名或密码错误")
            Return
        Else
            objResponseJson = Newtonsoft.Json.Linq.JObject.Parse(strResponseJson)
            Try
                'If objResponseJson("data")("account") = My.ChungJee.Default.strNasUserName Then
                My.ChungJee.Default.strNasSid = objResponseJson("data")("sid")
                    My.ChungJee.Default.Save()
                ' Else
                'Return
                'End If
            Catch ex1 As Exception
                Return
            End Try
        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class
