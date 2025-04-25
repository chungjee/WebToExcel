Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel

' 必须标记为 COM 可见
<ComVisible(True)>
<Guid("ADB77660-E910-4C1F-AE15-0BC2D7476FA8")>
<ClassInterface(ClassInterfaceType.None)>
Public Class DataAggregate
    Implements IVSTOFunctions
    Public Sub DoSomething(Optional strURL As String = "") Implements IVSTOFunctions.DoSomething
        ' 实际业务逻辑
        Dim frmLoginNas As New LoginForm
        frmLoginNas.ShowDialog()
    End Sub
    Public Function GetMessage(input As String) As String Implements IVSTOFunctions.GetMessage
        Return $"来自 VSTO 的回复: {input}"
    End Function
End Class

' 定义 COM 接口
<ComVisible(True)>
<Guid("B0E177C2-0812-48D8-96B4-3E2F7CEE17E4")>
<InterfaceType(ComInterfaceType.InterfaceIsIDispatch)>
Public Interface IVSTOFunctions
    Sub DoSomething(Optional strURL As String = "")
    Function GetMessage(input As String) As String
End Interface