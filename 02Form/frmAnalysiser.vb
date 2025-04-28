Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmAnalysiserSetup
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialogSelectFile.FileOk
        If sender.name = "导入节点集合" Then
            '将选择的文件内容反序列化到jsonResponseFilter
            Dim filePath As String = sender.FileName
            Dim jsonContent As String = File.ReadAllText(filePath)
            Dim jsonResponseFilter As JObject = JsonConvert.DeserializeObject(Of JObject)(jsonContent)
            If jsonResponseFilter IsNot Nothing Then Globals.ThisAddIn.jsonResponseFilter = jsonResponseFilter
            Dim dicResponseUrlList As Dictionary(Of String, System.Collections.Generic.List(Of String))
            dicResponseUrlList = MdlLoad.jsonResponseUrlListToArrayList(Globals.ThisAddIn.jsonResponseFilter.Item("data"))
            Dim objTreeNode As Windows.Forms.TreeNode
            Me.trvResponseFilter.Nodes.Clear()
            objTreeNode = Me.trvResponseFilter.Nodes.Add("ResponseFilter")
            objTreeNode.Name = "ResponseFilter"
            For Each key As String In dicResponseUrlList.Keys
                objTreeNode = Me.trvResponseFilter.Nodes(0).Nodes.Add(Regex.Replace(System.Web.HttpUtility.UrlDecode(key), "[^a-zA-Z0-9]", ""), key)
                objTreeNode.Name = Regex.Replace(key, "[^a-zA-Z0-9]", "")
                For Each item As String In dicResponseUrlList(key)
                    objTreeNode.Nodes.Add(Regex.Replace(item, "[^a-zA-Z0-9]", ""), System.Web.HttpUtility.UrlDecode(item)).Name = Regex.Replace(item, "[^a-zA-Z0-9]", "")
                Next item
            Next key
        Else
            Me.txtFileName.Text = sender.FileName
        End If
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
                    objVBModuleComponent.CodeModule.AddFromString("Sub " & strVBAFunctionName & "(strFileName As String)" & Chr(13) & "End Sub")
                End If
            Else
                Me.txtContentEdit.Text = objVBModuleComponent.CodeModule.Lines(1, objVBModuleComponentLinesCount)
            End If
        End If
    End Sub

    Private Sub btnBrowseFile_Click(sender As Object, e As EventArgs) Handles btnBrowseFile.Click
        Me.OpenFileDialogSelectFile.ShowDialog()
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

    Private Sub trvResponseFilter_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles trvResponseFilter.AfterCheck
        If e.Node.Nodes.Count > 0 Then
            If e.Node.Checked Then
                For Each childNode As TreeNode In e.Node.Nodes
                    childNode.Checked = True
                Next childNode
            Else
                For Each childNode As TreeNode In e.Node.Nodes
                    UncheckNodeAndChildren(childNode)
                Next childNode
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItemDeleteNode_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemDeleteNode.Click
        If MsgBox(Me.trvResponseFilter.SelectedNode.Text & "节点删除后不可恢复，是否继续？", MsgBoxStyle.YesNo, "删除节点") = MsgBoxResult.Yes Then
            Dim objTreeNode As Windows.Forms.TreeNode
            objTreeNode = Me.trvResponseFilter.SelectedNode
            Dim strUrl As String = objTreeNode.FullPath.Replace("\", "").Replace(Me.trvResponseFilter.Nodes(0).Text, "")
            Dim strUrlName As String = objTreeNode.Name
            RemoveHandler trvResponseFilter.AfterSelect, AddressOf trvResponseFilter_AfterSelect
            If objTreeNode.Parent IsNot Nothing Then
                objTreeNode.Parent.Nodes.Remove(objTreeNode)
            Else
                objTreeNode.Remove()
            End If
            AddHandler trvResponseFilter.AfterSelect, AddressOf trvResponseFilter_AfterSelect
            For Each key As JToken In Globals.ThisAddIn.jsonResponseFilter.Item("data")
                If System.Web.HttpUtility.UrlDecode(key.Item("url").Value(Of String)).StartsWith(System.Web.HttpUtility.UrlDecode(strUrl)) Then
                    key.Remove()
                    Exit For
                End If
            Next key
            My.ChungJee.Default.jsonUrlList = JsonConvert.SerializeObject(Globals.ThisAddIn.jsonResponseFilter)
            My.ChungJee.Default.Save()
        End If
    End Sub

    Private Sub ToolStripMenuItemExportNodes_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemExportNodes.Click
        With Me.SaveFileDialogExportNodes
            .FileName = Me.trvResponseFilter.Nodes(0).Text & CLng((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) & ".json"
            .Tag = sender.name
            .Filter = "Json文件(*.json)|*.json"
            .Title = "导出节点"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            .ShowDialog()
        End With

    End Sub

    Private Sub ToolStripMenuItemImportNodes_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemImportNodes.Click
        With Me.OpenFileDialogSelectFile
            .FileName = Me.trvResponseFilter.Nodes(0).Text
            .Tag = sender.name
            .Filter = "Json文件(*.json)|*.json"
            .Title = "导出节点"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            .ShowDialog()
        End With
    End Sub

    Private Sub SaveFileDialogExportNodes_FileOk(sender As Object, e As CancelEventArgs) Handles SaveFileDialogExportNodes.FileOk
        Dim strResponseFilter As String = JsonConvert.SerializeObject(Globals.ThisAddIn.jsonResponseFilter)
        ' 将JSON字符串写入文件
        Dim filePath As String = Me.SaveFileDialogExportNodes.FileName
        Using writer As New StreamWriter(filePath, False, System.Text.Encoding.UTF8)
            writer.Write(strResponseFilter)
        End Using
        MsgBox("导出成功！",, "成功")
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
    End Sub '添加过滤器点击事件处理

End Class