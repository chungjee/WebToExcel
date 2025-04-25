Imports System.Diagnostics
Imports Microsoft.Office.Tools.Ribbon

Public Class Ribbon
    Private Sub Ribbon1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    End Sub
    Private Sub btnSystemSetup_Click(sender As Object, e As RibbonControlEventArgs)
        Dim frmSystemSetup1 As New frmSystemSetup
        frmSystemSetup1.Show()
    End Sub

    Private Sub btnTaskPanel_Click(sender As Object, e As RibbonControlEventArgs) Handles btnTaskPanel.Click
        If Globals.ThisAddIn.tpConstomWebVieTaskPanel.wvCoreWevview2.Source = "about:blank" Then
            Me.btnTaskPanel.ScreenTip = My.ChungJee.Default.strHomePage
        Else
            Me.btnTaskPanel.ScreenTip = ""
        End If
        btnClick(sender)
    End Sub

    Private Sub chkRequestFilterSwitch_Click(sender As Object, e As RibbonControlEventArgs) Handles chkRequestFilterSwitch.Click
        Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestSwitch.Checked = sender.checked
    End Sub

    Private Sub chkResponseFilterSwitch_Click(sender As Object, e As RibbonControlEventArgs) Handles chkResponseFilterSwitch.Click
        Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemResponseSwitch.Checked = sender.checked
    End Sub

    Private Sub btnLoginNas_Click(sender As Object, e As RibbonControlEventArgs) Handles btnLoginNas.Click
        Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemLoginNas.PerformClick()
    End Sub

    Private Sub btnSystemSetup_Click_1(sender As Object, e As RibbonControlEventArgs) Handles btnSystemSetup.Click
        Dim frmSystemSetup1 As New frmSystemSetup
        frmSystemSetup1.Show()
    End Sub

    Private Sub btnTaxation_Click(sender As Object, e As RibbonControlEventArgs) Handles btnTaxation.Click
        btnClick(sender)
    End Sub
    Private Sub btnMeiTuan_Click(sender As Object, e As RibbonControlEventArgs) Handles btnMeiTuan.Click
        btnClick(sender)
    End Sub

    Sub btnClick(sender As Object)
        If Globals.ThisAddIn.tpAddinWebviewTaskPanel IsNot Nothing Then
            With Globals.ThisAddIn
                With .tpAddinWebviewTaskPanel
                    .Visible = True
                    Try
                        .Width = 450
                    Catch ex As Exception
                        .DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
                        .Width = 450
                    End Try

                End With
                MdlLoad.initializeLoadingForm()
                Try
                    If xlApp.VBE.ActiveWindow IsNot Nothing Then xlApp.VBE.ActiveWindow.Close()
                Catch ex As Exception
                    Debug.Print(ex.Message)
                End Try
                If sender.ScreenTip <> "" Then .tpConstomWebVieTaskPanel.wvCoreWevview2.Navigate(sender.ScreenTip)
            End With
        Else
            MsgBox("任务窗格系统错误！")
        End If
    End Sub

    Private Sub btnRequestFilter_Click(sender As Object, e As RibbonControlEventArgs) Handles btnRequestFilter.Click
        Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemRequestFilter.PerformClick()
    End Sub

    Private Sub btnResponseFilter_Click(sender As Object, e As RibbonControlEventArgs) Handles btnResponseFilter.Click
        Globals.ThisAddIn.tpConstomWebVieTaskPanel.MenuItemResponseFilter.PerformClick()
    End Sub
End Class
