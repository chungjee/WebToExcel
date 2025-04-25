Imports Newtonsoft.Json.Linq

Public Class frmSystemSetup
    Private Sub JsonBottomButtonScriptTextBox_TextChanged(sender As Object, e As EventArgs) Handles JsonBottomButtonScriptTextBox.TextChanged

    End Sub
    Private Sub frmSystemSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.ChungJee.Default
            Me.StrNasHostNameTextBox.Text = .strNasHostName
            Me.StrNasUserNameTextBox.Text = .strNasUserName
            Me.Con_BASE_DATATextBox.Text = .con_BASE_DATA
            Me.Con_COMPANY_INFOTextBox.Text = .con_COMPANY_INFO
            Me.Con_NAS_HOME_FOLDERNAMETextBox.Text = .con_NAS_HOME_FOLDERNAME
            Me.Con_START_DATETextBox.Text = .con_START_DATE
            Me.Con_END_DATETextBox.Text = .con_END_DATE
            Me.StrNasSidTextBox.Text = .strNasSid
            Me.JsonUrlListTextBox.Text = Chr(13) & .jsonUrlList
            Me.JsonBottomButtonScriptTextBox.Text = Chr(13) & .jsonBottomButtonScript
            Me.StrNoVBAFunctionTextBox.Text = .strNoVBAFunction
        End With
    End Sub

    Private Sub frmSystemSetup_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        Me.StrNasSidTextBox.Text = ""
        My.ChungJee.Default.strNasSid = ""
        My.ChungJee.Default.Save()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        With My.ChungJee.Default
            .strNasHostName = Me.StrNasHostNameTextBox.Text
            .strNasUserName = Me.StrNasUserNameTextBox.Text
            .con_BASE_DATA = Me.Con_BASE_DATATextBox.Text
            .con_COMPANY_INFO = Me.Con_COMPANY_INFOTextBox.Text
            .con_NAS_HOME_FOLDERNAME = Me.Con_NAS_HOME_FOLDERNAMETextBox.Text
            .con_START_DATE = Me.Con_START_DATETextBox.Text
            .con_END_DATE = Me.Con_END_DATETextBox.Text
            .jsonUrlList = Me.JsonUrlListTextBox.Text.Replace(" ", "").Replace(Chr(13), "")
            .jsonBottomButtonScript = Me.JsonBottomButtonScriptTextBox.Text.Replace("  ", "").Replace(Chr(13) & " ", "")
            .strNoVBAFunction = Me.StrNoVBAFunctionTextBox.Text
            .Save()
            Try
                Dim objJsonUrlListTemp As JObject = Newtonsoft.Json.Linq.JObject.Parse(My.ChungJee.Default.jsonUrlList)
                Globals.ThisAddIn.jsonResponseFilter = New Newtonsoft.Json.Linq.JObject()
                Dim jsonArray As New JArray
                For Each key As JToken In objJsonUrlListTemp.Item("data")
                    If key("enable") <> "false" Then
                        jsonArray.Add(key)
                    End If
                Next
                Globals.ThisAddIn.jsonResponseFilter.Add("data", jsonArray)
            Catch ex As Exception
                MsgBox("请求过滤器列表加载失败！")
            End Try
        End With
        Me.Close()
    End Sub

    Private Sub btnFontSize_Click(sender As Object, e As EventArgs) Handles btnFontSize.Click
        If fntDialogSystemSetup.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.JsonBottomButtonScriptTextBox.Font = fntDialogSystemSetup.Font
            Me.JsonUrlListTextBox.Font = fntDialogSystemSetup.Font
        End If
    End Sub

    Private Sub fntDialogSystemSetup_Apply(sender As Object, e As EventArgs) Handles fntDialogSystemSetup.Apply
        Me.JsonBottomButtonScriptTextBox.Font = fntDialogSystemSetup.Font
        Me.JsonUrlListTextBox.Font = fntDialogSystemSetup.Font
    End Sub

    Private Sub btnClear_Click_1(sender As Object, e As EventArgs) Handles btnClear.Click
        Me.StrNasSidTextBox.Text = ""
        My.ChungJee.Default.strNasSid = ""
        My.ChungJee.Default.Save()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class