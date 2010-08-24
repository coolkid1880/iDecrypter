
Public Class Main

    Dim InputSelected As Boolean = False, _
           OutputSelected As Boolean = False

    ' A subroutine that will execute a .EXE and wait til its done.
    Sub DoCMD(ByVal file As String, ByVal arg As String)
        Dim procNlite As New Process
        winstyle = 1
        procNlite.StartInfo.FileName = file
        procNlite.StartInfo.Arguments = " " & arg
        procNlite.StartInfo.WindowStyle = winstyle
        Application.DoEvents()
        procNlite.Start()
        Do Until procNlite.HasExited
            Application.DoEvents()
            For i = 0 To 5000000
                Application.DoEvents()
            Next
        Loop
        procNlite.WaitForExit()
    End Sub

    Private Sub buttonDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonDecrypt.Click

        tmrChecker.Enabled = False
        buttonDecrypt.Enabled = False
        buttonDecrypt.Text = "Decrypting..."

        DoCMD("vfdecrypt.exe", _
                                                    " -i " & Chr(34) & TextBox1.Text & Chr(34) & _
                                                    " -k " & TextBox3.Text & " " & _
                                                    " -o " & Chr(34) & TextBox2.Text & Chr(34))

        buttonDecrypt.Text = "Decryption is done!"

    End Sub

    Private Sub buttonSelectInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSelectInput.Click
        dlgInput.ShowDialog()
        If dlgInput.FileName = "" Then Exit Sub
        InputSelected = True
        TextBox1.Text = dlgInput.FileName
        auto = System.IO.Path.GetDirectoryName(TextBox1.Text) & "\" & _
                   System.IO.Path.GetFileName(TextBox1.Text) & _
                    "_decrypt.dmg"
        TextBox2.Text = auto
    End Sub


    Private Sub buttonSelectOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSelectOutput.Click
        dlgOutput.ShowDialog()
        If dlgOutput.FileName = "" Then Exit Sub
        OutputSelected = True
        TextBox2Text = dlgInput.FileName
    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Command() <> "" Then
            TextBox1.Text = Command()
            InputSelected = True
            auto = System.IO.Path.GetDirectoryName(TextBox1.Text) & "\" & _
                       System.IO.Path.GetFileNameWithoutExtension(TextBox1.Text) & _
                       "_decrypted.dmg"
            TextBox2.Text = auto
        End If

    End Sub

    Private Sub tmrChecker_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrChecker.Tick
        If InputSelected = True And OutputSelected = True Then
            buttonDecrypt.Enabled = True
        Else
            buttonDecrypt.Enabled = False
        End If
    End Sub

End Class
