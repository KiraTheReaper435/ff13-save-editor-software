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

            TabControl1.Visible = True

        Else
            MsgBox("Select a Save")
        End If

    End Sub


    ''' 
    ''' 
    ''' Gil Editing Segment
    ''' 
    ''' 


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


    ''' 
    ''' 
    ''' Party Crystarium Level Segment
    ''' 
    ''' 


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


    ''' 
    ''' 
    ''' TP Value Segment
    ''' 
    ''' 


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


    ''' 
    ''' 
    ''' Fang Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub FangHPButton_Click(sender As Object, e As EventArgs) Handles FangHPButton.Click
        Dim MyAddress As Long = &H248D6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangHP.Text = Hex(FangHP.Text) 'Converts textbox input to hex

        While FangHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            FangHP.Text = FangHP.Text + "0"
        End While



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
        Dim MyAddress As Long = &H248E6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangStr.Text = Hex(FangStr.Text) 'Converts textbox input to hex

        While FangStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact Str value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            FangStr.Text = FangStr.Text + "0"
        End While

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
        Dim MyAddress As Long = &H248EA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangMag.Text = Hex(FangMag.Text) 'Converts textbox input to hex

        While FangMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact Mag value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            FangMag.Text = FangMag.Text + "0"
        End While

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


    ''' 
    ''' 
    ''' Lightning Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub LightHPButton_Click(sender As Object, e As EventArgs) Handles LightHPButton.Click
        Dim MyAddress As Long = &H27ED6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightHP.Text = Hex(LightHP.Text) 'Converts textbox input to hex

        While LightHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            LightHP.Text = LightHP.Text + "0"
        End While

        Dim bytes As Byte()


        If LightHP.Text.Length Mod 2 <> 0 Then LightHP.Text = LightHP.Text.Insert(0, "0")

        ReDim bytes((LightHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To LightHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(LightHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        LightHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub LightCPButton_Click(sender As Object, e As EventArgs) Handles LightCPButton.Click
        Dim MyAddress As Long = &H27ECA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightCP.Text = Hex(LightCP.Text) 'Converts textbox input to hex


        Dim bytes As Byte()

        If LightCP.Text.Length Mod 2 <> 0 Then LightCP.Text = LightCP.Text.Insert(0, "0")

        ReDim bytes((LightCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To LightCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(LightCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        LightCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub LightStrButton_Click(sender As Object, e As EventArgs) Handles LightStrButton.Click
        Dim MyAddress As Long = &H27EE6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightStr.Text = Hex(LightStr.Text) 'Converts textbox input to hex

        While LightStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact STR value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            LightStr.Text = LightStr.Text + "0"
        End While

        Dim bytes As Byte()

        If LightStr.Text.Length Mod 2 <> 0 Then LightStr.Text = LightStr.Text.Insert(0, "0")

        ReDim bytes((LightStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To LightStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(LightStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        LightStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub LightMagButton_Click(sender As Object, e As EventArgs) Handles LightMagButton.Click
        Dim MyAddress As Long = &H27EEA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightMag.Text = Hex(LightMag.Text) 'Converts textbox input to hex

        While LightMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact MAG value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            LightMag.Text = LightMag.Text + "0"
        End While

        Dim bytes As Byte()

        If LightMag.Text.Length Mod 2 <> 0 Then LightMag.Text = LightMag.Text.Insert(0, "0")

        ReDim bytes((LightMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To LightMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(LightMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        LightMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub LightATBButton_Click(sender As Object, e As EventArgs) Handles LightATBButton.Click
        Dim MyAddress As Long = &H27EE0 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightATB.Text = (LightATB.Text * 10)
        LightATB.Text = Hex(LightATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If LightATB.Text.Length Mod 2 <> 0 Then LightATB.Text = LightATB.Text.Insert(0, "0")

        ReDim bytes((LightATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To LightATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(LightATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        LightATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub


    ''' 
    ''' 
    ''' Sazh Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub SazhHPButton_Click(sender As Object, e As EventArgs) Handles SazhHPButton.Click
        Dim MyAddress As Long = &H28C56 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhHP.Text = Hex(SazhHP.Text) 'Converts textbox input to hex


        While SazhHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SazhHP.Text = SazhHP.Text + "0"
        End While

        Dim bytes As Byte()

        If SazhHP.Text.Length Mod 2 <> 0 Then SazhHP.Text = SazhHP.Text.Insert(0, "0")

        ReDim bytes((SazhHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SazhHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SazhHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        SazhHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SazhCPButton_Click(sender As Object, e As EventArgs) Handles SazhCPButton.Click
        Dim MyAddress As Long = &H28C4A ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhCP.Text = Hex(SazhCP.Text) 'Converts textbox input to hex

        Dim bytes As Byte()

        If SazhCP.Text.Length Mod 2 <> 0 Then SazhCP.Text = SazhCP.Text.Insert(0, "0")

        ReDim bytes((SazhCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SazhCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SazhCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        SazhCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SazhStrButton_Click(sender As Object, e As EventArgs) Handles SazhStrButton.Click
        Dim MyAddress As Long = &H28C66 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhStr.Text = Hex(SazhStr.Text) 'Converts textbox input to hex

        While SazhStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact STR value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SazhStr.Text = SazhStr.Text + "0"
        End While

        Dim bytes As Byte()

        If SazhStr.Text.Length Mod 2 <> 0 Then SazhStr.Text = SazhStr.Text.Insert(0, "0")

        ReDim bytes((SazhStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SazhStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SazhStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        SazhStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SazhMagButton_Click(sender As Object, e As EventArgs) Handles SazhMagButton.Click
        Dim MyAddress As Long = &H28C6A ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhMag.Text = Hex(SazhMag.Text) 'Converts textbox input to hex

        While SazhMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact MAG value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SazhMag.Text = SazhMag.Text + "0"
        End While

        Dim bytes As Byte()

        If SazhMag.Text.Length Mod 2 <> 0 Then SazhMag.Text = SazhMag.Text.Insert(0, "0")

        ReDim bytes((SazhMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SazhMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SazhMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        SazhMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SazhATBButton_Click(sender As Object, e As EventArgs) Handles SazhATBButton.Click
        Dim MyAddress As Long = &H28C60 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhATB.Text = (SazhATB.Text * 10)
        SazhATB.Text = Hex(SazhATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If SazhATB.Text.Length Mod 2 <> 0 Then SazhATB.Text = SazhATB.Text.Insert(0, "0")

        ReDim bytes((SazhATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SazhATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SazhATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        SazhATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub


    ''' 
    ''' 
    ''' Vanille Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub VanHPButton_Click(sender As Object, e As EventArgs) Handles VanHPButton.Click
        Dim MyAddress As Long = &H2B4D6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanHP.Text = Hex(VanHP.Text) 'Converts textbox input to hex

        While VanHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            VanHP.Text = VanHP.Text + "0"
        End While

        Dim bytes As Byte()

        If VanHP.Text.Length Mod 2 <> 0 Then VanHP.Text = VanHP.Text.Insert(0, "0")

        ReDim bytes((VanHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To VanHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(VanHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        VanHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub VanCPButton_Click(sender As Object, e As EventArgs) Handles VanCPButton.Click
        Dim MyAddress As Long = &H2B4CA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanCP.Text = Hex(VanCP.Text) 'Converts textbox input to hex

        Dim bytes As Byte()

        If VanCP.Text.Length Mod 2 <> 0 Then VanCP.Text = VanCP.Text.Insert(0, "0")

        ReDim bytes((VanCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To VanCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(VanCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        VanCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub VanStrButton_Click(sender As Object, e As EventArgs) Handles VanStrButton.Click
        Dim MyAddress As Long = &H2B4E6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanStr.Text = Hex(VanStr.Text) 'Converts textbox input to hex

        While VanStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact STR value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            VanStr.Text = VanStr.Text + "0"
        End While

        Dim bytes As Byte()

        If VanStr.Text.Length Mod 2 <> 0 Then VanStr.Text = VanStr.Text.Insert(0, "0")

        ReDim bytes((VanStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To VanStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(VanStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        VanStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub VanMagButton_Click(sender As Object, e As EventArgs) Handles VanMagButton.Click
        Dim MyAddress As Long = &H2B4EA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanMag.Text = Hex(VanMag.Text) 'Converts textbox input to hex

        While VanMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact MAG value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            VanMag.Text = VanMag.Text + "0"
        End While

        Dim bytes As Byte()

        If VanMag.Text.Length Mod 2 <> 0 Then VanMag.Text = VanMag.Text.Insert(0, "0")

        ReDim bytes((VanMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To VanMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(VanMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        VanMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub VanATBButton_Click(sender As Object, e As EventArgs) Handles VanATBButton.Click
        Dim MyAddress As Long = &H2B4E0 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanATB.Text = (VanATB.Text * 10)
        VanATB.Text = Hex(VanATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If VanATB.Text.Length Mod 2 <> 0 Then VanATB.Text = VanATB.Text.Insert(0, "0")

        ReDim bytes((VanATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To VanATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(VanATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        VanATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub


    ''' 
    ''' 
    ''' Hope Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub HopeHPButton_Click(sender As Object, e As EventArgs) Handles HopeHPButton.Click
        Dim MyAddress As Long = &H263D6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeHP.Text = Hex(HopeHP.Text) 'Converts textbox input to hex

        While HopeHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            HopeHP.Text = HopeHP.Text + "0"
        End While

        Dim bytes As Byte()

        If HopeHP.Text.Length Mod 2 <> 0 Then HopeHP.Text = HopeHP.Text.Insert(0, "0")

        ReDim bytes((HopeHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To HopeHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(HopeHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        HopeHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub HopeCPButton_Click(sender As Object, e As EventArgs) Handles HopeCPButton.Click
        Dim MyAddress As Long = &H263CA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeCP.Text = Hex(HopeCP.Text) 'Converts textbox input to hex

        Dim bytes As Byte()

        If HopeCP.Text.Length Mod 2 <> 0 Then HopeCP.Text = HopeCP.Text.Insert(0, "0")

        ReDim bytes((HopeCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To HopeCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(HopeCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        HopeCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub HopeStrButton_Click(sender As Object, e As EventArgs) Handles HopeStrButton.Click
        Dim MyAddress As Long = &H263E6 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeStr.Text = Hex(HopeStr.Text) 'Converts textbox input to hex

        While HopeStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact STR value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            HopeStr.Text = HopeStr.Text + "0"
        End While

        Dim bytes As Byte()

        If HopeStr.Text.Length Mod 2 <> 0 Then HopeStr.Text = HopeStr.Text.Insert(0, "0")

        ReDim bytes((HopeStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To HopeStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(HopeStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        HopeStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub HopeMagButton_Click(sender As Object, e As EventArgs) Handles HopeMagButton.Click
        Dim MyAddress As Long = &H263EA ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeMag.Text = Hex(HopeMag.Text) 'Converts textbox input to hex

        While HopeMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact MAG value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            HopeMag.Text = HopeMag.Text + "0"
        End While

        Dim bytes As Byte()

        If HopeMag.Text.Length Mod 2 <> 0 Then HopeMag.Text = HopeMag.Text.Insert(0, "0")

        ReDim bytes((HopeMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To HopeMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(HopeMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        HopeMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub HopeATBButton_Click(sender As Object, e As EventArgs) Handles HopeATBButton.Click
        Dim MyAddress As Long = &H263E0 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeATB.Text = (HopeATB.Text * 10)
        HopeATB.Text = Hex(HopeATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If HopeATB.Text.Length Mod 2 <> 0 Then HopeATB.Text = HopeATB.Text.Insert(0, "0")

        ReDim bytes((HopeATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To HopeATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(HopeATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        HopeATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub


    ''' 
    ''' 
    ''' Snow Character Stat Segment:
    ''' 
    ''' HP
    ''' CP
    ''' STR
    ''' MAG
    ''' ATB
    ''' 


    Private Sub SnowHPButton_Click(sender As Object, e As EventArgs) Handles SnowHPButton.Click
        Dim MyAddress As Long = &H2A756 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowHP.Text = Hex(SnowHP.Text) 'Converts textbox input to hex

        While SnowHP.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SnowHP.Text = SnowHP.Text + "0"
        End While

        Dim bytes As Byte()

        If SnowHP.Text.Length Mod 2 <> 0 Then SnowHP.Text = SnowHP.Text.Insert(0, "0")

        ReDim bytes((SnowHP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SnowHP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SnowHP.Text.Substring(i, 2), 16)
            n += 1
        Next

        SnowHP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SnowCPButton_Click(sender As Object, e As EventArgs) Handles SnowCPButton.Click
        Dim MyAddress As Long = &H2A74A ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowCP.Text = Hex(SnowCP.Text) 'Converts textbox input to hex

        Dim bytes As Byte()

        If SnowCP.Text.Length Mod 2 <> 0 Then SnowCP.Text = SnowCP.Text.Insert(0, "0")

        ReDim bytes((SnowCP.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SnowCP.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SnowCP.Text.Substring(i, 2), 16)
            n += 1
        Next

        SnowCP.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SnowStrButton_Click(sender As Object, e As EventArgs) Handles SnowStrButton.Click
        Dim MyAddress As Long = &H2A766 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowStr.Text = Hex(SnowStr.Text) 'Converts textbox input to hex

        While SnowStr.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact STR value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SnowStr.Text = SnowStr.Text + "0"
        End While

        Dim bytes As Byte()

        If SnowStr.Text.Length Mod 2 <> 0 Then SnowStr.Text = SnowStr.Text.Insert(0, "0")

        ReDim bytes((SnowStr.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SnowStr.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SnowStr.Text.Substring(i, 2), 16)
            n += 1
        Next

        SnowStr.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SnowMagButton_Click(sender As Object, e As EventArgs) Handles SnowMagButton.Click
        Dim MyAddress As Long = &H2A76A ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowMag.Text = Hex(SnowMag.Text) 'Converts textbox input to hex

        While SnowMag.Text.Length < 6 ' Enures any values that would be unchanged become 0 to get exact MAG value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            SnowMag.Text = SnowMag.Text + "0"
        End While

        Dim bytes As Byte()

        If SnowMag.Text.Length Mod 2 <> 0 Then SnowMag.Text = SnowMag.Text.Insert(0, "0")

        ReDim bytes((SnowMag.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SnowMag.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SnowMag.Text.Substring(i, 2), 16)
            n += 1
        Next

        SnowMag.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub

    Private Sub SnowATBButton_Click(sender As Object, e As EventArgs) Handles SnowATBButton.Click
        Dim MyAddress As Long = &H2A760 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowATB.Text = (SnowATB.Text * 10)
        SnowATB.Text = Hex(SnowATB.Text) 'Converts textbox input to hex



        Dim bytes As Byte()

        If SnowATB.Text.Length Mod 2 <> 0 Then SnowATB.Text = SnowATB.Text.Insert(0, "0")

        ReDim bytes((SnowATB.Text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To SnowATB.Text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(SnowATB.Text.Substring(i, 2), 16)
            n += 1
        Next

        SnowATB.Text = "" ' Blanks text box to prevent it showing hex
        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
    End Sub
End Class
