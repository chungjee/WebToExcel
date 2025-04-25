Sub strVBAFunctionName(strFileName As String)
	Dim strRangeAddress As String, strListObjectName As String, wbDownload As Workbook, shDownload As Worksheet, shCurrent As Worksheet, xlApp As Application
    Dim rowItem As Range, intOffsetRow As Integer: intOffsetRow = 1
    Set xlApp = New Application: Set wbDownload = xlApp.Workbooks.Open(strFileName): Set shDownload = wbDownload.Sheets(1)
    If shDownload.UsedRange.Rows.Count = 1 and shDownload.UsedRange.Columns.Count < 2 Then
        MsgBox "没有数据！", vbExclamation, "提示": wbDownload.Close SaveChanges:=False: Kill strFileName: xlApp.Quit: Set xlApp = Nothing: Exit Sub
    End If'如果没有数据则退出

    Set shCurrent = ThisWorkbook.ActiveSheet
    with shCurrent
        .Cells.Clear:.Cells.NumberFormat = "@":.Range(shDownload.UsedRange.Address).Value = shDownload.UsedRange.Value
        wbDownload.Close SaveChanges:=False: Kill strFileName: xlApp.Quit: Set xlApp = Nothing: Set wbDownload = Nothing: Set shDownload = Nothing

        If .Columns(.UsedRange.Columns.Count).Cells(1, 1).Value = "" Then
            intOffsetRow = .UsedRange.Columns(.UsedRange.Columns.Count).Cells(1, 1).End(Excel.xlDown).Row
            If intOffsetRow = .UsedRange.Rows.Count Then intOffsetRow = 1
        End If
        Dim regex As Object, matachs As Object
        Set regex = CreateObject("VBScript.RegExp"): regex.Global = True: regex.pattern = "[\u4e00-\u9fa5]+":Set matches = regex.Execute(strFileName)
        For Each match In matches
            strListObjectName = strListObjectName & match.Value   ' 拼接所有匹配到的中文字符
        Next
        If strListObjectName = "" Then strListObjectName = "TEMP"'如果没有中文字符，则使用默认名称
        If intOffsetRow <= 1 Then rows.Item(1).EntireRow.insert shift:=xlDown:intOffsetRow=2'如果没有标题行，则插入一行
        
        .Cells(1, 1).Value = strListObjectName
        With .Range(.Cells(1, 1), .Cells(1, .UsedRange.Columns.Count))
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenterAcrossSelection:.Font.Size = 16
        End With
        strRangeAddress = shCurrent.UsedRange.Offset(intOffsetRow - 1).Resize(shCurrent.UsedRange.Rows.Count - intOffsetRow + 1).Address

        On Error Resume Next: .AutoFilterMode = False
        With .ListObjects.Add(xlSrcRange, .Range(strRangeAddress), , xlYes)
            .Name = strListObjectName: .DataBodyRange.Font.Size = 9: .DataBodyRange.HorizontalAlignment = xlCenter: .HeaderRowRange.HorizontalAlignment = xlCenter: .ShowAutoFilterDropDown = False
            .DataBodyRange.EntireColumn.AutoFit: .DataBodyRange.EntireRow.AutoFit
            strFieldName = "序号"
            With .ListColumns.Add(1)
                .DataBodyRange.NumberFormatLocal = "0"
                .Name = strFieldName: .DataBodyRange.Formula = "=ROW()-" & intOffsetRow
            End With
            .DataBodyRange.EntireColumn.AutoFit: .DataBodyRange.EntireRow.AutoFit
            With .Sort
                .SortFields.Clear
                .SortFields.Add2 .ListColumns("日期"), xlSortOnValues, xlAscending, xlSortNormal
                .Header = xlYes: .MatchCase = False: .Orientation = xlTopToBottom: .SortMethod = xlPinYin: .Apply
            end with
        End With
    End With
    MdlLoad.frmHidden
End Sub