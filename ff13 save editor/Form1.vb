Imports System.IO
Imports System.Text
Public Class Form1

    Private Sub OpenSave_Click(sender As Object, e As EventArgs) Handles OpenSave.Click
        OpenFileDialog1.Filter = "DAT_DECRYPTED FILES | *.dat_decrypted"


        If OpenFileDialog1.ShowDialog = DialogResult.OK Then ' Opens File and if opened executes code



            Dim fileInfo As System.IO.FileInfo

            fileInfo = My.Computer.FileSystem.GetFileInfo(OpenFileDialog1.FileName)

            Dim NameOfSave As String = fileInfo.Name

            Me.Text = (Me.Text + " - " + NameOfSave)

            Dim filename As String = OpenFileDialog1.FileName

            Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming


            fs.Close() ' close the FileStream

            TabControl1.Visible = True

        Else
            MsgBox("Select a Save")
        End If

    End Sub

    Private Sub GilButton_Click(sender As Object, e As EventArgs) Handles GilButton.Click

        Dim MyAddress As Short = &H48BC ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        GilTextBox.Text = Hex(GilTextBox.Text) 'Converts textbox input to hex

        While GilTextBox.Text.Length < 8 ' Enures any values that would be unchanged become 0 to get exact gil value E.G 50000 returns C3 50 and it requires 00 00 to be added to fill all offsets
            GilTextBox.Text = GilTextBox.Text + "0"
        End While

        Dim bytes As Byte()

        If GilTextBox.Text.Length Mod 2 <> 0 Then GilTextBox.Text = GilTextBox.Text.Insert(0, "0")

        ReDim bytes((GilTextBox.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To GilTextBox.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(GilTextBox.Text.Substring(i, 2), 16)
            n += 1
        Next

        GilTextBox.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()

        GilSucessMessage.Visible = True ' Makes message label visible saying it has been updated

    End Sub

    Private Sub CrystariumLevelButton_Click(sender As Object, e As EventArgs) Handles CrystariumLevelButton.Click
        Dim MyAddress As Short = &H4937 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        CrystariumComboBox.Text = Hex(CrystariumComboBox.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If CrystariumComboBox.Text.Length Mod 2 <> 0 Then CrystariumComboBox.Text = CrystariumComboBox.Text.Insert(0, "0")

        ReDim bytes((CrystariumComboBox.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To CrystariumComboBox.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(CrystariumComboBox.Text.Substring(i, 2), 16)
            n += 1
        Next

        CrystariumComboBox.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()

        CrystariumSuccessMessage.Visible = True



    End Sub

    Private Sub TPButton_Click(sender As Object, e As EventArgs) Handles TPButton.Click
        Dim MyAddress As Long = &H1C059 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        TPValueBox.Text = Hex(TPValueBox.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If TPValueBox.Text.Length Mod 2 <> 0 Then TPValueBox.Text = TPValueBox.Text.Insert(0, "0")

        ReDim bytes((TPValueBox.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To TPValueBox.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(TPValueBox.Text.Substring(i, 2), 16)
            n += 1
        Next

        TPValueBox.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()

        TpSuccessMessage.visible = True
    End Sub

    Private Sub FangHPButton_Click(sender As Object, e As EventArgs) Handles FangHPButton.Click
        Dim MyAddress As Long = &H248D7 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangHP.Text = Hex(FangHP.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If FangHP.Text.Length Mod 2 <> 0 Then FangHP.Text = FangHP.Text.Insert(0, "0")

        ReDim bytes((FangHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To FangHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(FangHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        FangHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub FangCPButton_Click(sender As Object, e As EventArgs) Handles FangCPButton.Click
        Dim MyAddress As Long = &H248CA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangCP.Text = Hex(FangCP.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If FangCP.Text.Length Mod 2 <> 0 Then FangCP.Text = FangCP.Text.Insert(0, "0")

        ReDim bytes((FangCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To FangCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(FangCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        FangCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub FangStrButton_Click(sender As Object, e As EventArgs) Handles FangStrButton.Click
        Dim MyAddress As Long = &H248E7 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangStr.Text = Hex(FangStr.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If FangStr.Text.Length Mod 2 <> 0 Then FangStr.Text = FangStr.Text.Insert(0, "0")

        ReDim bytes((FangStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To FangStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(FangStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        FangStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub FangMagButton_Click(sender As Object, e As EventArgs) Handles FangMagButton.Click
        Dim MyAddress As Long = &H248EB ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangMag.Text = Hex(FangMag.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If FangMag.Text.Length Mod 2 <> 0 Then FangMag.Text = FangMag.Text.Insert(0, "0")

        ReDim bytes((FangMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To FangMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(FangMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        FangMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub FangATBButton_Click(sender As Object, e As EventArgs) Handles FangATBButton.Click
        Dim MyAddress As Long = &H248E0 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangATB.Text = (FangATB.Text * 10)
        FangATB.Text = Hex(FangATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If FangATB.Text.Length Mod 2 <> 0 Then FangATB.Text = FangATB.Text.Insert(0, "0")

        ReDim bytes((FangATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To FangATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(FangATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        FangATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

End Class
