Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Security.Policy
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Module MdlLoad
    Public xlApp As Excel.Application = Globals.ThisAddIn.Application
    Public strFileFullName As String

    Public Sub initializeLoadingForm()
        Dim Wb As Microsoft.Office.Interop.Excel.Workbook = xlApp.ActiveWorkbook
        'xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityLow
        Dim strFormName As String = My.ChungJee.Default.strLoadingFormName
        Dim objVBFormComponent As Microsoft.Vbe.Interop.VBComponent = Nothing
        Dim objVBModuleComponent As Microsoft.Vbe.Interop.VBComponent = Nothing
        Try
            objVBModuleComponent = xlApp.ActiveWorkbook.VBProject.VBComponents.Item(strFormName)
        Catch ex As Exception
            If ex.Message.Contains("不信任") Then Return
            If ex.Message.Contains("下标越界") Then
                With xlApp.ActiveWorkbook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule)
                    .Name = "MdlLoad"
                    .CodeModule.AddFromString(My.ChungJee.Default.strLoadFormFunction)
                End With
            End If 'VBA工程中没有MdlLoad模块，创建VBA的MdlLoad模块
        End Try '加载或创建VBA的MdlLoad模块‘检查VBA工程中是否有MdlLoad模块以及模块代码是否为空
        Try
            objVBFormComponent = xlApp.ActiveWorkbook.VBProject.VBComponents.Item(strFormName)
            'If objVBFormComponent.CodeModule.CountOfLines = 0 Then
            '    Try
            '        objVBFormComponent.CodeModule.AddFromFile(strFormName)
            '    Catch ex2 As Exception
            '        If ex2.Message.Contains("文件未找到") Then
            '            Dim strUrl As String, strSid As String
            '            If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
            '                strUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strFormName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
            '                strSid = NasModel.nasDownloadFile(strUrl, strFormName)
            '                If strSid = strFormName Then
            '                    objVBFormComponent.CodeModule.AddFromFile(strFormName)
            '                Else
            '                    MsgBox("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
            '                End If
            '            Else
            '                Dim frmLoginForm As New LoginForm
            '                frmLoginForm.UsernameTextBox.Text = My.ChungJee.Default.strNasUserName
            '                If My.ChungJee.Default.strNasPassWord <> "" Then frmLoginForm.PasswordTextBox.Text = My.ChungJee.Default.strNasPassWord
            '                xlApp.Visible = False
            '                frmLoginForm.ShowDialog()
            '                xlApp.Visible = True
            '                If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
            '                    strUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strFormName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
            '                    strSid = NasModel.nasDownloadFile(strUrl, strFormName)
            '                    If strSid = strFormName Then
            '                        objVBFormComponent.CodeModule.AddFromFile(strFormName)
            '                    Else
            '                        MsgBox("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
            '                    End If
            '                End If
            '            End If
            '        Else
            '            MsgBox("加载窗体失败，请检查文件路径或联系管理员！")
            '        End If
            '    End Try
            'Else
            '    Return
            'End If 'VBA工程中有frmloading窗体，但窗体代码为空，重新加载窗体代码'检查VBA工程中是否有frmloading窗体以及窗体代码是否为空
        Catch ex As Exception
            If ex.Message.Contains("不信任") Then MsgBox("请设置对VBA工程模型进行访问！") : Return
            If ex.Message.Contains("下标越界") Then
                With xlApp.ActiveWorkbook.VBProject.VBComponents
                    objVBFormComponent = .Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_MSForm)
                    'xlApp.VBE.ActiveWindow.Close()
                    With objVBFormComponent
                        .Name = strFormName
                        Try
                            .CodeModule.AddFromFile(strFormName)
                        Catch ex1 As Exception
                            If ex1.Message.Contains("文件未找到") Then
                                Dim strUrl As String, strSid As String
                                If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
                                    strUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strFormName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
                                    strSid = NasModel.nasDownloadFile(strUrl, strFormName)
                                    If strSid = strFormName Then
                                        objVBFormComponent.CodeModule.AddFromFile(strFormName)
                                    Else
                                        MsgBox("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
                                    End If
                                Else '没有令牌，提示重新登录
                                    Dim frmLoginForm As New LoginForm
                                    frmLoginForm.UsernameTextBox.Text = My.ChungJee.Default.strNasUserName
                                    If My.ChungJee.Default.strNasPassWord <> "" Then frmLoginForm.PasswordTextBox.Text = My.ChungJee.Default.strNasPassWord
                                    frmLoginForm.ShowDialog()
                                    If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
                                        strUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strFormName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
                                        strSid = NasModel.nasDownloadFile(strUrl, strFormName)
                                        If strSid = strFormName Then
                                            objVBFormComponent.CodeModule.AddFromFile(strFormName)
                                        Else
                                            MsgBox("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
                                        End If '文件下载成功，重新加载VBA模块代码
                                    Else
                                        MsgBox("令牌为空，登录网络存储系统失败！")
                                    End If '令牌不为空，重新下载代码文件
                                End If '令牌不为空，重新下载代码文件，如果令牌为空，提示重新登录网络存储系统,登录后，如果令牌不为空，重新下载代码文件
                            End If
                        End Try
                    End With
                End With
            Else
                MsgBox(ex.Message)
            End If 'VBA工程中没有frmloading窗体，创建VBA的frmLoading窗体
        End Try '检查VBA工程中是否有frmloading窗体以及窗体代码是否为空，如果没有frmloading窗体，创建VBA的frmLoading窗体，如果没有frmloading窗体代码，重新加载frmloading窗体代码，如果代码文件不存在，下载代码文件，并加载frmloading窗体代码，然后删除代码文件
    End Sub '初始化VBA模块中的frmLoading窗体

    Public Async Function getResponseContext(objResponse As Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseView) As Threading.Tasks.Task(Of String)
        Dim content As String
        Try
            Dim contentStream As Stream = Await objResponse.GetContentAsync()
            Using reader As New StreamReader(contentStream)
                content = Await reader.ReadToEndAsync()
                'Debug.WriteLine(content)
                Return content
            End Using
        Catch ex As Exception
            Debug.WriteLine("获取响应内容时出错: " & ex.Message)
            Return ""
        End Try
    End Function

    '根据URI获取VBA函数名称，模块名称，调用VBA函数，并传递参数,如果VBA函数不存在，下载VBA函数代码文件，并加载VBA函数代码，如果VBA函数代码文件不存在，提示下载失败；如果VBA函数代码文件存在，加载VBA函数代码，并调用VBA函数，如果加载失败，提示加载失败；如果加载成功，调用VBA函数，并传递参数，如果调用失败，提示调用失败；如果调用成功，返回True，否则返回False
    Public Function runVBAFunction(ByVal uriToVBAFuctionName As Uri, ByVal strParameters As String) As Boolean
        Dim strVBAFunctionName As String, strVBModuleName As String
        If uriToVBAFuctionName.AbsolutePath <> "/" Then
            If uriToVBAFuctionName.Query.Length > 1 Then
                strVBAFunctionName = uriToVBAFuctionName.AbsolutePath & uriToVBAFuctionName.Query
            Else
                strVBAFunctionName = uriToVBAFuctionName.AbsolutePath
            End If
        Else
            strVBAFunctionName = uriToVBAFuctionName.AbsoluteUri.Substring(uriToVBAFuctionName.AbsoluteUri.LastIndexOf("://") + 3)
        End If '根据URI获取VBA函数名称和模块名称，如果URI中没有绝对路径，则使用URI的主机名称作为函数名称

        strVBAFunctionName = System.Web.HttpUtility.UrlEncode(Regex.Replace(strVBAFunctionName, "[^A-Z0-9\u4e00-\u9fa5a-zA-Z0-9]", "")）
        strVBAFunctionName = UCase(Regex.Replace(strVBAFunctionName, "[^a-zA-Z0-9]", ""))

        If strVBAFunctionName.Length > 30 Then
            strVBModuleName = strVBAFunctionName.Substring(0, 30)
        Else
            strVBModuleName = strVBAFunctionName
        End If ' '根据URI获取VBA函数名称和模块名称，如果函数名称长度大于30，则使用前30个字符作为模块名称，否则使用函数名称作为模块名称
        'Debug.WriteLine("strVBAFunctionName: " & strVBAFunctionName & "长度：" & strVBAFunctionName.Length)
        'Debug.WriteLine("strResponse: " & strResponse)
        Try
            xlApp.Run(strVBModuleName & "." & strVBAFunctionName, strParameters)
            Return True
        Catch ex As Exception
            Debug.WriteLine("错误：" & ex.Message)
            Dim objVBModuleComponent As Microsoft.Vbe.Interop.VBComponent
            Try
                objVBModuleComponent = xlApp.ActiveWorkbook.VBProject.VBComponents.Item(strVBModuleName)
                If objVBModuleComponent.CodeModule.CountOfLines = 0 Then
                    Try
                        objVBModuleComponent.CodeModule.AddFromFile(strVBAFunctionName)
                        xlApp.Run(strVBModuleName & "." & strVBAFunctionName, strParameters)
                        System.IO.File.Delete(strVBAFunctionName)
                        Return True
                    Catch ex5 As Exception
                        If ex5.Message.Contains("文件未找到") Then
                            Dim strFileUrl As String, strSid As String
                            If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
                                strFileUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strVBAFunctionName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
                                strSid = NasModel.nasDownloadFile(strFileUrl, strVBAFunctionName)
                                If strSid = strVBAFunctionName Then
                                    Try
                                        objVBModuleComponent.CodeModule.AddFromFile(strVBAFunctionName)
                                        xlApp.Run(strVBModuleName & "." & strVBAFunctionName, strParameters)
                                        System.IO.File.Delete(strVBAFunctionName)
                                        Return True
                                    Catch ex6 As Exception
                                        Debug.WriteLine(ex6.Message)
                                    End Try '下载代码文件成功，重新加载VBA模块代码
                                Else
                                    If InStr(strParameters, ".xls") Then
                                        objVBModuleComponent.CodeModule.AddFromString(My.ChungJee.Default.strNoVBAFunction)
                                        xlApp.Run(strVBModuleName & ".strVBAFunctionName", strParameters)
                                        Return True
                                    Else

                                    End If
                                End If '如果代码文件下载成功，重新加载VBA模块代码，否则提示下载失败
                            Else
                                MsgBox("令牌为空，请重新登录网络存储系统！")
                            End If '网络存储系统令牌不为空，重新下载代码文件；如果令牌为空，提示重新登录网络存储系统
                        Else
                            Debug.WriteLine(ex5.Message)
                        End If 'VBA工程中已经存在该模块，该模块中的代码为空，重新加载VBA模块代码，代码文件不存在，下载代码文件
                    End Try 'VBA工程中已经存在该模块，重新加载VBA模块代码
                Else
                    objVBModuleComponent.CodeModule.DeleteLines(1, objVBModuleComponent.CodeModule.CountOfLines)
                    objVBModuleComponent.CodeModule.AddFromString(My.ChungJee.Default.strNoVBAFunction)
                    xlApp.Run(strVBModuleName & ".strVBAFunctionName", strParameters)
                End If 'VBA工程中已经存在该模块，该模块中的代码为空，重新加载VBA模块代码，如果代码文件不存在，下载代码文件
            Catch ex1 As Exception
                If ex1.Message.Contains("不信任") Then
                    Debug.WriteLine(ex1.Message)
                    Return False
                End If
                If ex1.Message.Contains("下标越界") Then
                    With xlApp.ActiveWorkbook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule)
                        Try
                            .Name = strVBModuleName
                            .CodeModule.AddFromFile(strVBAFunctionName)
                            xlApp.Run(strVBAFunctionName, strParameters)
                            System.IO.File.Delete(strVBAFunctionName)
                            Return True
                        Catch ex2 As Exception
                            If ex2.Message.Contains("文件未找到") Then
                                Dim strFileUrl As String, strSid As String
                                If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
                                    'strFileUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & System.Web.HttpUtility.UrlEncode(My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strVBAFunctionName) & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
                                    strFileUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strVBAFunctionName & "&mode=download&_sid=" & My.ChungJee.Default.strNasSid
                                    strSid = NasModel.nasDownloadFile(strFileUrl, strVBAFunctionName)
                                    If strSid = strVBAFunctionName Then
                                        Try
                                            .CodeModule.AddFromFile(strVBAFunctionName)
                                            xlApp.Run(strVBModuleName & "." & strVBAFunctionName, strParameters)
                                            System.IO.File.Delete(strVBAFunctionName)
                                            Return True
                                        Catch ex3 As Exception
                                            Debug.WriteLine(ex3.Message)
                                        End Try '下载代码文件成功，重新加载VBA模块代码，并删除代码文件
                                    Else
                                        If InStr(strParameters, ".xls") Then
                                            .CodeModule.AddFromString(My.ChungJee.Default.strNoVBAFunction.Replace("strVBAFunctionName", strVBAFunctionName))
                                            xlApp.Run(strVBAFunctionName, strParameters)
                                            Return True
                                        End If
                                    End If '代码文件下载成功，重新加载VBA模块代码
                                Else
                                    MsgBox("令牌为空， 请重新登录网络存储系统！")
                                End If '网络存储系统令牌不为空，重新下载代码文件；如果令牌为空，提示重新登录网络存储系统
                            Else
                                Debug.WriteLine(ex2.Message)
                            End If ' 'VBA工程中不存在该模块，创建VBA模块，加载VBA模块代码失败,下载代码文件
                        End Try 'VBA工程中不存在该模块，创建VBA模块，加载VBA模块代码，如果代码文件不存在，下载代码文件
                    End With 'VBA工程中不存在该模块，创建VBA模块，加载VBA模块代码，如果代码文件不存在，下载代码文件
                Else
                    Debug.WriteLine(ex1.Message)
                End If 'VBA工程中不存在该模块，创建VBA模块，加载VBA模块代码，如果代码文件不存在，下载代码文件
            End Try

            Return False
        End Try
    End Function

    Public Sub getXMLData(ByVal XMLStr As String)
        Dim objXmlDoc As New XmlDocument(), strFileName As String = "temp.xls", strRangeAdress As String, strListObjectName As String
        Dim wbDownload As Microsoft.Office.Interop.Excel.Workbook, shDownload As Microsoft.Office.Interop.Excel.Worksheet, shCurrent As Microsoft.Office.Interop.Excel.Worksheet
        objXmlDoc.LoadXml(XMLStr)
        objXmlDoc.Save(strFileName)
        Dim xlappDownload As New Microsoft.Office.Interop.Excel.Application With {.Visible = False, .DisplayAlerts = False}
        Dim strFieldName As String
        wbDownload = xlappDownload.Workbooks.Open(strFileName) : shDownload = wbDownload.Sheets.Item(1)
        strRangeAdress = shDownload.UsedRange.Offset(1).Address
        strListObjectName = Strings.Left(shDownload.Cells(1, 1).Value, InStr(shDownload.Cells(1, 1).Value, "[") - 1)
        shDownload.ListObjects.AddEx(, shDownload.UsedRange.Offset(1), , 1).Name = strListObjectName
        xlApp.ScreenUpdating = False
        shCurrent = xlApp.ActiveSheet
        shCurrent.Cells.Delete()
        With shCurrent.ListObjects.AddEx(, shCurrent.Range(strRangeAdress), , 1)
            .Name = strListObjectName
            .ListColumns(4).DataBodyRange.NumberFormatLocal = "@"
            .ListColumns(5).DataBodyRange.NumberFormatLocal = "￥#,##0.00;￥-#,##0.00"
            .HeaderRowRange.Value = shDownload.ListObjects(strListObjectName).HeaderRowRange.Value : .DataBodyRange.Value = shDownload.ListObjects(strListObjectName).DataBodyRange.Value
            .DataBodyRange.Font.Size = 9 : .DataBodyRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

            wbDownload.Close(SaveChanges:=False) : xlappDownload.Quit() '关闭临时文件和应用程序

            .ShowAutoFilter = False : .ListRows(.ListRows.Count).Delete()
            strFieldName = "序号"
            With .ListColumns.Add(1)
                .Name = strFieldName
                .DataBodyRange.Value = "=ROW()-2"
            End With '增加序号列
            strFieldName = .ListColumns(8).Name
            With .ListColumns.Add(2)
                .DataBodyRange.NumberFormatLocal = "yyyy/mm/dd hh:mm"
                .DataBodyRange.Value = shCurrent.ListObjects(strListObjectName).ListColumns(strFieldName).DataBodyRange.Value : shCurrent.ListObjects(strListObjectName).ListColumns(strFieldName).Delete() : .Name = strFieldName
            End With '移动时间列
            strFieldName = .ListColumns(10).Name
            With .ListColumns.Add(3)
                .DataBodyRange.NumberFormatLocal = "@"
                .DataBodyRange.Value = shCurrent.ListObjects(strListObjectName).ListColumns(strFieldName).DataBodyRange.Value
                shCurrent.ListObjects(strListObjectName).ListColumns(strFieldName).Delete() : .Name = strFieldName
            End With '移动门店列

            strFieldName = .ListColumns(4).Name
            Dim objArrayProduct As Object(,) = .ListColumns(strFieldName).DataBodyRange.Value : Dim objProductObjectItem As Object
            Dim objArrayProductPrice As Object(,) = .ListColumns(strFieldName).DataBodyRange.Value
            Dim objArrayShopActivity As Object(,) = .ListColumns("商家优惠金额").DataBodyRange.Value
            Dim i As Integer = 1, strArrayProductInfo As String()
            For Each objProductObjectItem In objArrayProduct
                strArrayProductInfo = ParseServiceString(objProductObjectItem)
                objArrayProduct(i， 1) = strArrayProductInfo(0)
                objArrayProductPrice(i， 1) = CInt(Mid(strArrayProductInfo(1), InStr(strArrayProductInfo(1), "[") + 1, InStr(strArrayProductInfo(1), "]") - 3)) - SumNumbersInString(objArrayShopActivity(i, 1))
                i += 1
            Next
            .ListColumns(strFieldName).DataBodyRange.Value = objArrayProduct '分析商品信息的内容，提取商品名称，提取商品价格

            strFieldName = "结算金额"
            With .ListColumns.Add(9)
                .Name = strFieldName : .DataBodyRange.NumberFormatLocal = "0.00" : .DataBodyRange.Value = objArrayProductPrice
                .TotalsCalculation = Microsoft.Office.Interop.Excel.XlTotalsCalculation.xlTotalsCalculationSum : .DataBodyRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            End With '增加结算金额列


            .ListColumns("验券帐号").Delete()
            .ListColumns("分店城市").Delete()
            .ListColumns("商品类型").Delete()
            .ListColumns("商家优惠金额").DataBodyRange.EntireColumn.Hidden = True
            .DataBodyRange.EntireColumn.AutoFit()
            .DataBodyRange.EntireRow.AutoFit()
            .ShowTotals = True '整理超级表

            '.Sort.SortFields.Clear()
            .Sort.SortFields.Add(.ListColumns(2).Range, Microsoft.Office.Interop.Excel.XlSortOn.xlSortOnValues, Microsoft.Office.Interop.Excel.XlSortOrder.xlAscending, Microsoft.Office.Interop.Excel.XlSortDataOption.xlSortNormal)

        End With

        Try
            File.Delete(strFileName)
            Debug.WriteLine("文件已成功删除。")
        Catch ex As Exception
            Debug.WriteLine("删除文件时出错: " & ex.Message)
        End Try
        xlApp.ScreenUpdating = True
        xlApp.Run("frmHidden")
    End Sub '处理XML数据，生成Excel表格
    Public Function ParseServiceString(inputString As String) As String()
        Dim pattern As String = "^(.*?)(\[.*?\])(\[.*?\])$"        ' 定义正则表达式模式
        ' 创建正则表达式对象
        Dim regex As New Regex(pattern, RegexOptions.IgnoreCase) : Dim match As Match = regex.Match(inputString.Trim()) : Dim result(2) As String
        If match.Success Then
            result(0) = match.Groups(1).Value.Trim() : result(1) = match.Groups(2).Value : result(2) = match.Groups(3).Value ' 提取ID
        Else
            result(0) = "未知服务" : result(1) = "0.00" : result(2) = "0"
        End If
        Return result
    End Function '处理服务项目字符串，返回数组
    Function SumNumbersInString(input As String) As Double
        ' 使用正则表达式匹配所有数字（包括小数）
        Dim matches As MatchCollection = Regex.Matches(input, "\d+(\.\d+)?")
        Dim sum As Double = 0
        ' 遍历所有匹配项并累加
        For Each match As Match In matches
            sum += Double.Parse(match.Value)
        Next
        Return sum
    End Function '处理商家活动字符串，返回活动金额合计数字


    Public Function getResponseFiltParserFile(strVBAFunctionName As String, objVBModuleComponent As Microsoft.Vbe.Interop.VBComponent) As String
        Dim strFileUrl As String, strFileName As String
        If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then '令牌不为空，重新下载代码文件；如果令牌为空，提示重新登录网络存储系统
            strFileUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strVBAFunctionName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
            strFileName = NasModel.nasDownloadFile(strFileUrl, strVBAFunctionName)
            If strFileName = strVBAFunctionName Then
                Try
                    objVBModuleComponent.CodeModule.AddFromFile(strVBAFunctionName)
                    System.IO.File.Delete(strVBAFunctionName)
                    Return True
                Catch ex3 As Exception
                    Debug.WriteLine(ex3.Message)
                End Try '下载代码文件成功，重新加载VBA模块代码，并删除代码文件
            Else
                Debug.WriteLine("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
            End If '代码文件下载成功，重新加载VBA模块代码
        Else  '令牌为空，提示重新登录网络存储系统,登录后，如果令牌不为空，重新下载代码文件,如果令牌为空，提示登录网络存储系统失败。
            Dim frmLoginForm As New LoginForm
            frmLoginForm.UsernameTextBox.Text = My.ChungJee.Default.strNasUserName
            If My.ChungJee.Default.strNasPassWord <> "" Then frmLoginForm.PasswordTextBox.Text = My.ChungJee.Default.strNasPassWord
            frmLoginForm.ShowDialog()
            If My.ChungJee.Default.strNasSid <> "" And My.ChungJee.Default.strNasSid <> "False" Then
                strFileUrl = My.ChungJee.Default.strNasHostName & "/webapi/entry.cgi?api=SYNO.FileStation.Download&version=2&method=download&path=" & My.ChungJee.Default.con_NAS_HOME_FOLDERNAME & strVBAFunctionName & "&mode=open&_sid=" & My.ChungJee.Default.strNasSid
                strFileName = NasModel.nasDownloadFile(strFileUrl, strVBAFunctionName)
                If strFileName = strVBAFunctionName Then
                    Try
                        objVBModuleComponent.CodeModule.AddFromFile(strVBAFunctionName)
                        System.IO.File.Delete(strVBAFunctionName)
                        Return True
                    Catch ex3 As Exception
                        Debug.WriteLine(ex3.Message)
                    End Try '下载代码文件成功，重新加载VBA模块代码，并删除代码文件
                Else
                    Debug.WriteLine("文件下载失败，请检查网络连接或文件路径。或者请联系管理员分配权限！")
                End If '代码文件下载成功，重新加载VBA模块代码
            Else
                MsgBox("令牌为空，登录网络存储系统失败！")
                Return ""
            End If '令牌不为空，重新下载代码文件，如果令牌为空，提示重新登录网络存储系统,登录后，如果令牌不为空，重新下载代码文件
        End If '网络存储系统令牌不为空，重新下载代码文件；如果令牌为空，提示重新登录网络存储系统

        Return ""
    End Function '获取响应过滤器文件

    Public Function jsonResponseUrlListToArrayList(objJToken As JToken) As Dictionary(Of String, System.Collections.Generic.List(Of String))
        Dim arrlsResponseUrlDictionary As New Dictionary(Of String, System.Collections.Generic.List(Of String))
        For Each key As JToken In objJToken
            Dim uriRequest As System.Uri
            Dim strSecondNodeName As String
            Try
                If key("enable") = "false" Then Continue For
                uriRequest = New Uri(key("url"))
                strSecondNodeName = uriRequest.Scheme & "://" & uriRequest.Host
            Catch ex As Exception
                Continue For
            End Try
            If arrlsResponseUrlDictionary.ContainsKey(strSecondNodeName) Then
                arrlsResponseUrlDictionary.Item(strSecondNodeName).Add(uriRequest.AbsolutePath & uriRequest.Query)
            Else
                arrlsResponseUrlDictionary.Add(strSecondNodeName, New System.Collections.Generic.List(Of String))
                arrlsResponseUrlDictionary.Item(strSecondNodeName).Add(uriRequest.AbsolutePath & uriRequest.Query)
            End If
        Next key
        Return arrlsResponseUrlDictionary
    End Function '将JSON格式的URL列表转换为字典格式的URL列表

    Public Async Sub initializeThisAddinWebview2Async()
        Dim options As New Microsoft.Web.WebView2.Core.CoreWebView2EnvironmentOptions()
        Dim userDataFolder As String = System.IO.Path.GetTempPath()
        Dim environment As Microsoft.Web.WebView2.Core.CoreWebView2Environment = Await Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync(Nothing, userDataFolder, options)
        With Globals.ThisAddIn
            Await .tpConstomWebVieTaskPanel.wbvTaskPan.EnsureCoreWebView2Async(environment)
            .tpConstomWebVieTaskPanel.wbvTaskPan.ZoomFactor = 0.8
            .tpConstomWebVieTaskPanel.wvCoreWevview2 = .tpConstomWebVieTaskPanel.wbvTaskPan.CoreWebView2

            .tpConstomWebVieTaskPanel.wvCoreWevview2.AddWebResourceRequestedFilter("*://e.dianping.com/*", CoreWebView2WebResourceContext.All)
            .tpConstomWebVieTaskPanel.wvCoreWevview2.AddWebResourceRequestedFilter("*://mss-shon.sankuai.com/*", CoreWebView2WebResourceContext.All)
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebResourceRequested, AddressOf WebViewEvents.EventWebResourceRequested
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebResourceResponseReceived, AddressOf WebViewEvents.EventWebResourceResponseReceived
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.DownloadStarting, AddressOf WebViewEvents.EventDownloadStarting

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.WebMessageReceived, AddressOf WebViewEvents.EventWebMessageReceived
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NavigationStarting, AddressOf WebViewEvents.EventNavigationStarting
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NavigationCompleted, AddressOf WebViewEvents.EventNavigationCompleted

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.DOMContentLoaded, AddressOf WebViewEvents.EventDOMContentLoaded
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.NewWindowRequested, AddressOf WebViewEvents.EventNewWindowRequested

            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.FrameNavigationStarting, AddressOf WebViewEvents.EventFrameNavigationStarting
            AddHandler .tpConstomWebVieTaskPanel.wvCoreWevview2.FrameNavigationCompleted, AddressOf WebViewEvents.EventFrameNavigationCompleted
            Try
                Dim objJsonUrlListTemp As JObject = Newtonsoft.Json.Linq.JObject.Parse(My.ChungJee.Default.jsonUrlList)
                .jsonResponseFilter = New Newtonsoft.Json.Linq.JObject()
                Dim jsonArray As New JArray
                For Each key As JToken In objJsonUrlListTemp.Item("data")
                    If key("enable") <> "false" Then
                        jsonArray.Add(key)
                    End If
                Next
                .jsonResponseFilter.Add("data", jsonArray)
            Catch ex As Exception
                MsgBox("请求过滤器列表加载失败！")
            End Try
        End With
    End Sub '初始化CoreWebview，进行事件绑定
End Module
