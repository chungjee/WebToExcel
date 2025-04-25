Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq

Public Class frmAnalysiserSetup

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialog1.FileOk
        Me.txtFileName.Text = sender.FileName
    End Sub


    Private Sub frmAnalysiserSetup_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dicResponseUrlList As Dictionary(Of String, System.Collections.Generic.List(Of String))
        dicResponseUrlList = MdlLoad.jsonResponseUrlListToArrayList(Globals.ThisAddIn.jsonResponseFilter.Item("data"))

        Dim objTreeNode As Windows.Forms.TreeNode
        For Each key As String In dicResponseUrlList.Keys
            objTreeNode = Me.trvResponseFilter.Nodes(0).Nodes.Add(Regex.Replace(System.Web.HttpUtility.UrlDecode(key), "[^a-zA-Z0-9]", ""), key)
            objTreeNode.Name = Regex.Replace(key, "[^a-zA-Z0-9]", "")
            For Each item As String In dicResponseUrlList(key)
                objTreeNode.Nodes.Add(Regex.Replace(item, "[^a-zA-Z0-9]", ""), System.Web.HttpUtility.UrlDecode(item)).Name = Regex.Replace(item, "[^a-zA-Z0-9]", "")
            Next item
        Next key
    End Sub



    Private Sub UncheckNodeAndChildren(ByVal node As TreeNode)
        node.Checked = False ' 假设需要取消选中所有子节点
        For Each childNode As TreeNode In node.Nodes
            UncheckNodeAndChildren(childNode)
        Next
    End Sub


    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click
        Try
            Dim objFilterUrl As New System.Uri(Me.txtFilterUrl.Text)
            Dim strFilterUrl As String = objFilterUrl.AbsolutePath & objFilterUrl.Query
            Dim objSecondNodeName As String = Regex.Replace(objFilterUrl.Scheme & "://" & objFilterUrl.Host, "[^a-zA-Z0-9]", "")
            If Me.trvResponseFilter.Nodes(0).Nodes.ContainsKey(objSecondNodeName) Then
                For Each key As JToken In Globals.ThisAddIn.jsonResponseFilter.Item("data")
                    If key.Item("url").Value(Of String) = Me.txtFilterUrl.Text Then
                        MsgBox("此URL已存在") : Return
                    End If
                Next key
                Me.trvResponseFilter.Nodes(0).Nodes.Item(objSecondNodeName).Nodes.Add(System.Web.HttpUtility.UrlDecode(strFilterUrl)).Name = Regex.Replace(Me.txtFilterUrl.Text, "[^a-zA-Z0-9]", "")
            Else
                Dim objTreeNode As Windows.Forms.TreeNode
                objTreeNode = Me.trvResponseFilter.Nodes(0).Nodes.Add(objFilterUrl.Scheme & "://" & objFilterUrl.Host)
                objTreeNode.Nodes.Add(System.Web.HttpUtility.UrlDecode(strFilterUrl)).Name = Regex.Replace(strFilterUrl, "[^a-zA-Z0-9]", "")
            End If
            Dim objJsonUrlListItem As New JObject
            Me.txtFilterUrl.Text = Me.txtFilterUrl.Text.Replace("\r", "").Replace("\n", "").Replace(" ", "")
            objJsonUrlListItem.Add("url", Me.txtFilterUrl.Text)
            CType（Globals.ThisAddIn.jsonResponseFilter.Item("data"), JArray）.Add(objJsonUrlListItem)
            My.ChungJee.Default.jsonUrlList = My.ChungJee.Default.jsonUrlList.Replace("""}]}", """},{""url"":""" & Me.txtFilterUrl.Text & """}]}")
            My.ChungJee.Default.Save()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub trvResponseFilter_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvResponseFilter.AfterSelect
        Me.txtContentEdit.Text = ""
        If e.Node.Nodes.Count = 0 Then
            Dim strVBAFunctionName As String = e.Node.Text
            Dim strModuleName As String

            ' 使用正则表达式保留字母、数字和汉字
            strVBAFunctionName = System.Web.HttpUtility.UrlEncode(Regex.Replace(strVBAFunctionName, "[^\u4e00-\u9fa5a-zA-Z0-9]", "")）
            strVBAFunctionName = UCase(Regex.Replace(strVBAFunctionName, "[^a-zA-Z0-9]", ""))
            If strVBAFunctionName.Length > 30 Then
                strModuleName = strVBAFunctionName.Substring(0, 30)
            Else
                strModuleName = strVBAFunctionName
            End If
            Dim objVBModuleComponent As Microsoft.Vbe.Interop.VBComponent
            Dim objVBModuleComponentLinesCount As Integer
            Try
                objVBModuleComponent = xlApp.ActiveWorkbook.VBProject.VBComponents.Item(strModuleName)
                objVBModuleComponentLinesCount = objVBModuleComponent.CodeModule.CountOfLines
            Catch ex As Exception
                objVBModuleComponent = xlApp.ActiveWorkbook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule)
                objVBModuleComponent.Name = strModuleName
            End Try
            If objVBModuleComponentLinesCount = 0 Then
                If MdlLoad.getResponseFiltParserFile(strVBAFunctionName, objVBModuleComponent) <> "" Then
                    objVBModuleComponentLinesCount = objVBModuleComponent.CodeModule.CountOfLines
                    Me.txtContentEdit.Text = objVBModuleComponent.CodeModule.Lines(1, objVBModuleComponentLinesCount)
                Else
                    objVBModuleComponent.CodeModule.AddFromString("sub " & strVBAFunctionName & "()" & Chr(13) & "end sub")
                End If
            Else
                Me.txtContentEdit.Text = objVBModuleComponent.CodeModule.Lines(1, objVBModuleComponentLinesCount)
            End If
        End If
    End Sub

    Private Sub btnBrowseFile_Click(sender As Object, e As EventArgs) Handles btnBrowseFile.Click
        Me.OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        Dim strDestFileName As String = Me.trvResponseFilter.SelectedNode.Text
        strDestFileName = System.Web.HttpUtility.UrlEncode(Regex.Replace(strDestFileName, "[^\u4e00-\u9fa5a-zA-Z0-9]", "")）
        strDestFileName = UCase(Regex.Replace(strDestFileName, "[^a-zA-Z0-9]", ""))

        If Me.trvResponseFilter.SelectedNode Is Nothing Then
            MsgBox("请先选择一个过滤器节点")
            Return
        ElseIf Me.txtFileName.Text <> "" Then
            ' 调用上传函数
            Dim result As String = UploadFileToNas(Me.txtFileName.Text, My.ChungJee.Default.strNasHostName, My.ChungJee.Default.strNasSid, "/home/excelWeview2/", strDestFileName)
            MsgBox("上传结果: " & result)
            Return
        ElseIf Me.txtContentEdit.Text <> "" Then
            Try
                ' 创建临时文件
                ' 调用上传函数
                Dim contentBytes As Byte() = System.Text.Encoding.Default.GetBytes(Me.txtContentEdit.Text)
                Dim result As String = UploadFileToNas(contentBytes, My.ChungJee.Default.strNasHostName, My.ChungJee.Default.strNasSid,, strDestFileName)
                ' 显示上传结果
                If result.Contains(strDestFileName) Then
                    MsgBox("节点:""" & Me.trvResponseFilter.SelectedNode.Text & """的过滤器函数上传成功！",, "成功")
                Else
                    MsgBox("上传结果: " & result)
                End If
            Catch ex As Exception
                MsgBox("上传失败: " & ex.Message)
            End Try
        Else
            ' 其他逻辑
        End If
    End Sub

    Private Sub txtFilterUrl_TextChanged(sender As Object, e As EventArgs) Handles txtFilterUrl.TextChanged

    End Sub
End Class