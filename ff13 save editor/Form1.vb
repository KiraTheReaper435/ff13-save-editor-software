﻿Imports System.Diagnostics.Eventing.Reader
Imports System.Diagnostics.Tracing
Imports System.IO
Imports System.Reflection.Metadata.Ecma335
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.Devices

Public Class Form1


    ''' 
    ''' 
    ''' Functions Here: 
    ''' 
    '''  <summary>
    ''' 
    Function StringToHex(ByVal text As String) As String
        Dim hex As String
        For i As Integer = 0 To text.Length - 1
            hex &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
        Next
        Return hex
    End Function

    Function HexToString(ByVal hex As String) As String
        Dim text As New System.Text.StringBuilder(hex.Length \ 2)
        For i As Integer = 0 To hex.Length - 2 Step 2
            text.Append(Chr(Convert.ToByte(hex.Substring(i, 2), 16)))
        Next
        Return text.ToString
    End Function







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
            Me.OpenSave.PerformClick()
        End If



        '' Area for preloading Party Editor config + Setting the defaults for it so when user clicks the tab its preloaded automatically and shows current party configuration.
        TabPage10.Show()

        Dim Dictionary1 As New Dictionary(Of String, Int64)

        Dictionary1.Add("None", 0)
        Dictionary1.Add("Lightning", 13)
        Dictionary1.Add("Sazh", 14)
        Dictionary1.Add("Fang", 9)
        Dictionary1.Add("Vanille", 17)
        Dictionary1.Add("Hope", 11)
        Dictionary1.Add("Snow", 16)
        Dictionary1.Add("Slot Empty", 255)

        Dim FlippedDictionary As New Dictionary(Of Int64, String)

        FlippedDictionary.Add(0, "None")
        FlippedDictionary.Add(13, "Lightning")
        FlippedDictionary.Add(14, "Sazh")
        FlippedDictionary.Add(9, "Fang")
        FlippedDictionary.Add(17, "Vanille")
        FlippedDictionary.Add(11, "Hope")
        FlippedDictionary.Add(16, "Snow")
        FlippedDictionary.Add(255, "Slot Empty")



        PartyMember1.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember1.ValueMember = "Value"
        PartyMember1.DisplayMember = "Key"


        PartyMember2.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember2.ValueMember = "Value"
        PartyMember2.DisplayMember = "Key"


        PartyMember3.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember3.ValueMember = "Value"
        PartyMember3.DisplayMember = "Key"


        PartyMember4.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember4.ValueMember = "Value"
        PartyMember4.DisplayMember = "Key"


        PartyMember5.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember5.ValueMember = "Value"
        PartyMember5.DisplayMember = "Key"


        PartyMember6.DataSource = New BindingSource(Dictionary1, Nothing)
        PartyMember6.ValueMember = "Value"
        PartyMember6.DisplayMember = "Key"


        '' How to get value from selected option: Dim value = PartyMemberX.SelectedValue
        '' PartyMember2.Text = value shows correct value meaning above works correctly.

        Dim PartyMemberOriginal1 As Long = &H1C029
        Dim PartyMemberOriginal2 As Long = &H1C02D
        Dim PartyMemberOriginal3 As Long = &H1C031
        Dim PartyMemberOriginal4 As Long = &H1C035
        Dim PartyMemberOriginal5 As Long = &H1C039
        Dim PartyMemberOriginal6 As Long = &H1C03D

        '''
        '''
        ''' automatically pulls inventory information.
        '''
        '''



        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(PartyMemberOriginal1, SeekOrigin.Begin) ' moves to the address you want
        Dim Value1member = fs.ReadByte()
        PartyMember1.Text = FlippedDictionary.GetValueOrDefault(Value1member)

        fs.Seek(PartyMemberOriginal2, SeekOrigin.Begin)
        Dim Value2member = fs.ReadByte()
        PartyMember2.Text = FlippedDictionary.GetValueOrDefault(Value2member)

        fs.Seek(PartyMemberOriginal3, SeekOrigin.Begin)
        Dim Value3member = fs.ReadByte()
        PartyMember3.Text = FlippedDictionary.GetValueOrDefault(Value3member)

        fs.Seek(PartyMemberOriginal4, SeekOrigin.Begin)
        Dim Value4member = fs.ReadByte()
        PartyMember4.Text = FlippedDictionary.GetValueOrDefault(Value4member)

        fs.Seek(PartyMemberOriginal5, SeekOrigin.Begin)
        Dim Value5member = fs.ReadByte()
        PartyMember5.Text = FlippedDictionary.GetValueOrDefault(Value5member)

        fs.Seek(PartyMemberOriginal6, SeekOrigin.Begin)
        Dim Value6member = fs.ReadByte()
        PartyMember6.Text = FlippedDictionary.GetValueOrDefault(Value6member)





        Dim Dictionary As New Dictionary(Of String, String)

        Dictionary.Add("material_m000", "Begrimed Claw")
        Dictionary.Add("material_m001", "Bestail Claw")
        Dictionary.Add("material_m002", "Gargantuan Claw")
        Dictionary.Add("material_m003", "Hellish Tallon")
        Dictionary.Add("material_m004", "Shattered Bone")
        Dictionary.Add("material_m005", "Sturdy Bone")
        Dictionary.Add("material_m006", "Otherworldly Bone")
        Dictionary.Add("material_m007", "Ancient Bone")
        Dictionary.Add("material_m008", "Unknown")
        Dictionary.Add("material_m009", "Moistened Scale")
        Dictionary.Add("material_m010", "Seapetal Scale")
        Dictionary.Add("material_m011", "Abyssal Scale")
        Dictionary.Add("material_m012", "Seaking'S Beard")
        Dictionary.Add("material_m013", "Segmented Carapace")
        Dictionary.Add("material_m014", "Iron Shell")
        Dictionary.Add("material_m015", "Armored Shell")
        Dictionary.Add("material_m016", "Regenerating Carapace")
        Dictionary.Add("material_m017", "Chipped Fang")
        Dictionary.Add("material_m018", "Wicked Fang")
        Dictionary.Add("material_m019", "Monstrous Fang")
        Dictionary.Add("material_m020", "Sinister Fang")
        Dictionary.Add("material_m021", "Severed Wing")
        Dictionary.Add("material_m022", "Scaled Wing")
        Dictionary.Add("material_m023", "Abonimable Wing")
        Dictionary.Add("material_m024", "Menacing Wings")
        Dictionary.Add("material_m025", "Moited Tail")
        Dictionary.Add("material_m026", "Barbed Tail")
        Dictionary.Add("material_m027", "Diabolical Tail")
        Dictionary.Add("material_m028", "Entrancing Tail")
        Dictionary.Add("material_m029", "Torn Leather")
        Dictionary.Add("material_m030", "Thickend Hide")
        Dictionary.Add("material_m031", "Smooth Hide")
        Dictionary.Add("material_m032", "Supple Leather")
        Dictionary.Add("material_m033", "Gummy Oil")
        Dictionary.Add("material_m034", "Fragrant Oil")
        Dictionary.Add("material_m035", "Medicanal Oil")
        Dictionary.Add("material_m036", "Esteric Oil")
        Dictionary.Add("material_m037", "Scraggly Wool")
        Dictionary.Add("material_m038", "Rough Wool")
        Dictionary.Add("material_m039", "Thick Wool")
        Dictionary.Add("material_m040", "Fluffy Wool")
        Dictionary.Add("material_m041", "Bomb Ashes")
        Dictionary.Add("material_m042", "Bomb Fragment")
        Dictionary.Add("material_m043", "Bomb Sheel")
        Dictionary.Add("material_m044", "Bomb Core")
        Dictionary.Add("material_m045", "Murky Ooze")
        Dictionary.Add("material_m046", "Vibrant Ooze")
        Dictionary.Add("material_m047", "Transperent Ooze")
        Dictionary.Add("material_m048", "Wonder Gel")
        Dictionary.Add("material_m049", "Fractured Horn")
        Dictionary.Add("material_m050", "Spined Horn")
        Dictionary.Add("material_m052", "Infernal Horn")
        Dictionary.Add("material_m053", "Strange Fluid")
        Dictionary.Add("material_m054", "Enigamatic Fluid")
        Dictionary.Add("material_m055", "Mysterous Fluid")
        Dictionary.Add("material_m056", "Ineffable Fluid")
        Dictionary.Add("material_m057", "Cie'Th Tear")
        Dictionary.Add("material_m058", "Tear Of Frustratoin")
        Dictionary.Add("material_m059", "Tear Of Remorce")
        Dictionary.Add("material_m060", "Tear Or Woe")
        Dictionary.Add("material_m061", "Chocoblo Plume")
        Dictionary.Add("material_m062", "Chocobo Tail Feather")
        Dictionary.Add("material_m063", "Green Needle")
        Dictionary.Add("material_m064", "Dawnlight Dew")
        Dictionary.Add("material_m065", "Dusklight Dew")
        Dictionary.Add("material_m066", "Gloom Stalk")
        Dictionary.Add("material_m067", "Sunpetal")
        Dictionary.Add("material_m068", "Red Mycelium")
        Dictionary.Add("material_m069", "Blue Mycelium")
        Dictionary.Add("material_m070", "White Mycelium")
        Dictionary.Add("material_m071", "Black Mycelium")
        Dictionary.Add("material_m072", "Succulent Fruit")
        Dictionary.Add("material_m073", "Malodouros fruit")
        Dictionary.Add("material_m074", "Moonblossom Seed")
        Dictionary.Add("material_m075", "Sunblossom Seed")
        Dictionary.Add("material_m076", "Perfume")
        Dictionary.Add("material_j000", "Insulated Cabling")
        Dictionary.Add("material_j001", "Fiber,Optic Cable")
        Dictionary.Add("material_j002", "Liquid Crystal Lens")
        Dictionary.Add("material_j003", "Ring Joint")
        Dictionary.Add("material_j004", "Epicyclic Gear")
        Dictionary.Add("material_j005", "Crankshaft")
        Dictionary.Add("material_j006", "Electrolytic Capacitor")
        Dictionary.Add("material_j007", "Flywheel")
        Dictionary.Add("material_j008", "Sprocket")
        Dictionary.Add("material_j009", "Actuator")
        Dictionary.Add("material_j010", "Spark Plug")
        Dictionary.Add("material_j011", "Iridium Plug")
        Dictionary.Add("material_j012", "Needle Valve")
        Dictionary.Add("material_j013", "Butterfly Valve")
        Dictionary.Add("material_j014", "Analog Circet")
        Dictionary.Add("material_j015", "Digital Circut")
        Dictionary.Add("material_j016", "Gyroscope")
        Dictionary.Add("material_j017", "Electrode")
        Dictionary.Add("material_j018", "Ceramic Armor")
        Dictionary.Add("material_j019", "Chobham Armor")
        Dictionary.Add("material_j020", "Radial Bearing")
        Dictionary.Add("material_j021", "Thrust Bearing")
        Dictionary.Add("material_j022", "Solenoid")
        Dictionary.Add("material_j023", "Mobius Coil")
        Dictionary.Add("material_j024", "Tungesten Tube")
        Dictionary.Add("material_j025", "Titanium Tube")
        Dictionary.Add("material_j026", "Passive Detector")
        Dictionary.Add("material_j027", "Active Detector")
        Dictionary.Add("material_j028", "Transformer")
        Dictionary.Add("material_j029", "Amplifer")
        Dictionary.Add("material_j030", "Carburetor")
        Dictionary.Add("material_j031", "Supercharger")
        Dictionary.Add("material_j032", "Piezoelectric Element")
        Dictionary.Add("material_j033", "Cystal Oscillator")
        Dictionary.Add("material_j034", "Paraffin Oil")
        Dictionary.Add("material_j035", "Silicone Oil")
        Dictionary.Add("material_j036", "Synthetic Muscle")
        Dictionary.Add("material_j037", "Turboprop")
        Dictionary.Add("material_j038", "Turbo Jet")
        Dictionary.Add("material_j039", "Tesla Turbine")
        Dictionary.Add("material_j040", "Polymer Emulsion")
        Dictionary.Add("material_j041", "Ferroelectric Film")
        Dictionary.Add("material_j042", "Super Conductor")
        Dictionary.Add("material_j043", "Perfect Conductor")
        Dictionary.Add("material_j044", "Particle Accelerator")
        Dictionary.Add("material_j045", "Ulracompact Reactor")
        Dictionary.Add("material_j046", "Credit Chip")
        Dictionary.Add("material_j047", "Incentive Chip")
        Dictionary.Add("material_j048", "Cactar Doll")
        Dictionary.Add("material_j049", "Moogle Puppet")
        Dictionary.Add("material_j050", "Tonberry Figure")
        Dictionary.Add("material_j051", "Plush Chocobo")
        Dictionary.Add("material_o000", "Millerite")
        Dictionary.Add("material_o001", "Rhodochrosite")
        Dictionary.Add("material_o002", "Cobaltie")
        Dictionary.Add("material_o003", "Persovskite")
        Dictionary.Add("material_o004", "Uraninite")
        Dictionary.Add("material_o005", "Minar Stone")
        Dictionary.Add("material_o006", "Scarletite")
        Dictionary.Add("material_o007", "Adamantite")
        Dictionary.Add("material_o008", "Dark Matter")
        Dictionary.Add("material_o009", "Trapezohedron")
        Dictionary.Add("material_o010", "Gold Dust")
        Dictionary.Add("material_o011", "Gold Nugget")
        Dictionary.Add("material_o012", "Platinum Ingot")


        Dim MyAddress As Long = &H3A83C ' Sets Offset Address 

        Dim ItemAmountOffset As Long = 239693

        fs.Seek(ItemAmountOffset, SeekOrigin.Begin)









        Dim x = 0
        Dim y = 0

        While x < 240
            fs.Seek(ItemAmountOffset, SeekOrigin.Begin)

            Dim arraySize = 2

            Dim Buffer() As Byte = New Byte(arraySize) {}

            ItemAmountOffset = ItemAmountOffset + 20

            fs.Read(Buffer, 0, Buffer.Length)

            Dim ItemAmount = Convert.ToHexString(Buffer)
            Dim ItemAmountString As Integer = Convert.ToInt32(ItemAmount, 16)


            If ItemAmountString > 0 Then
                ListBox2.Items.Add(ItemAmountString & Environment.NewLine)
            End If

            x = x + 1

        End While




        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin)

        '''
        '''
        ''' Inventory Name Display - Pulls out the name of any items in the inventory.
        '''
        '''

        While y < 240

            Dim nameArraySize = 12
            Dim nameArray() As Byte = New Byte(nameArraySize) {}

            fs.Read(nameArray, 0, nameArray.Length)


            Dim testText = Convert.ToHexString(nameArray)
            Dim testTextConvert = HexToString(testText)

            ' TextBox2.AppendText(testTextConvert & Environment.NewLine)

            Dim CurrentPos = fs.Position()
            Dim NewOffset = CurrentPos + 7

            br.BaseStream.Seek(NewOffset, SeekOrigin.Begin)

            ' TextBox3.AppendText(testText & Environment.NewLine)

            If testText <> "00000000000000000000000000" Then
                testTextConvert = HexToString(testText)
                'ListBox1.Items.Add(testTextConvert & Environment.NewLine)

                ListBox1.Items.Add(Dictionary.Item(testTextConvert))


            End If






            y = y + 1

        End While





        fs.Close() : fs.Dispose()


    End Sub



    '''
    '''
    '''
    ''' Menu Stats - Crystarium Level, Gil, TP, Omni Kit
    '''
    '''
    '''



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
            GilTextBox.Text = "0" + GilTextBox.Text
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

        TpSuccessMessage.Visible = True
    End Sub


    '''
    '''
    ''' Omni Kit Segment
    '''
    '''

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then

            Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string
            Dim hex As String = "FF" ' Declares a string as hex (in this case the value FF)
            Dim MyAddress As Long = &H410DF ' Sets Offset Address
            Using fs As New FileStream(filename, FileMode.Open)  ' Opens file, converts hex string to bytes, then writes at offset address.
                For Each byteHex As String In hex.Split()
                    fs.Seek(MyAddress, SeekOrigin.Begin)
                    fs.WriteByte(Convert.ToByte(byteHex, 16))
                Next
            End Using

        End If
    End Sub



    '''
    '''
    ''' Party Characters - Stats
    '''
    '''

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
            FangHP.Text = "0" + FangHP.Text
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
        Dim MyAddress As Long = &H248C9 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        FangCP.Text = Hex(FangCP.Text) 'Converts textbox input to hex

        While FangCP.Text.Length < 8
            FangCP.Text = "0" + FangCP.Text
        End While

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
            FangStr.Text = "0" + FangStr.Text
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
            FangMag.Text = "0" + FangMag.Text
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
            LightHP.Text = "0" + LightHP.Text
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
        Dim MyAddress As Long = &H27EC9 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' you don't neccessarily need this, I use this in my app. toset the size an Array.

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        LightCP.Text = Hex(LightCP.Text) 'Converts textbox input to hex


        While LightCP.Text.Length < 8
            LightCP.Text = "0" + LightCP.Text
        End While


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
            LightStr.Text = "0" + LightStr.Text
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
            LightMag.Text = "0" + LightMag.Text
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
            SazhHP.Text = "0" + SazhHP.Text
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
        Dim MyAddress As Long = &H28C49 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SazhCP.Text = Hex(SazhCP.Text) 'Converts textbox input to hex


        While SazhCP.Text.Length < 8
            SazhCP.Text = "0" + SazhCP.Text
        End While


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
            SazhStr.Text = "0" + SazhStr.Text
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
            SazhMag.Text = "0" + SazhMag.Text
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
            VanHP.Text = "0" + VanHP.Text
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
        Dim MyAddress As Long = &H2B4C9 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        VanCP.Text = Hex(VanCP.Text) 'Converts textbox input to hex

        While VanCP.Text.Length < 8
            VanCP.Text = "0" + VanCP.Text
        End While

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
            VanStr.Text = "0" + VanStr.Text
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
            VanMag.Text = "0" + VanMag.Text
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
            HopeHP.Text = "0" + HopeHP.Text
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
        Dim MyAddress As Long = &H263C9 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        HopeCP.Text = Hex(HopeCP.Text) 'Converts textbox input to hex

        While HopeCP.Text.Length < 8
            HopeCP.Text = "0" + HopeCP.Text
        End While

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
            HopeStr.Text = "0" + HopeStr.Text
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
            HopeMag.Text = "0" + HopeMag.Text
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
            SnowHP.Text = "0" + SnowHP.Text
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
        Dim MyAddress As Long = &H2A749 ' Sets Offset Address 

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim sz As Integer = fs.Length ' Used for sizing an array if required

        br.BaseStream.Seek(MyAddress, SeekOrigin.Begin) ' moves to the address you want

        SnowCP.Text = Hex(SnowCP.Text) 'Converts textbox input to hex

        While SnowCP.Text.Length < 8
            SnowCP.Text = "0" + SnowCP.Text
        End While

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
            SnowStr.Text = "0" + SnowStr.Text
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
            SnowMag.Text = "0" + SnowMag.Text
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
    ''' 
    ''' 
    ''' Relates to Inventory Editing - Populating listboxes for data retrieval + adding. And simultanious scrolling of the two boxes to track Item name : amount.
    ''' 
    ''' 

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox1.TopIndex <> ListBox2.TopIndex Then
            ListBox1.TopIndex = ListBox2.TopIndex
        End If

        If ListBox1.SelectedIndex <> ListBox2.SelectedIndex Then
            ListBox1.SelectedIndex = ListBox2.SelectedIndex
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox2.TopIndex <> ListBox1.TopIndex Then
            ListBox2.TopIndex = ListBox1.TopIndex
        End If

        If ListBox2.SelectedIndex <> ListBox1.SelectedIndex Then
            ListBox2.SelectedIndex = ListBox1.SelectedIndex
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox1.Items.Add(ComboBox1.Text)
        ListBox2.Items.Add(NumericUpDown1.Value)



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Dictionary As New Dictionary(Of String, String)

        Dictionary.Add("Begrimed Claw", "material_m000")
        Dictionary.Add("Bestail Claw", "material_m001")
        Dictionary.Add("Gargantuan Claw", "material_m002")
        Dictionary.Add("Hellish Tallon", "material_m003")
        Dictionary.Add("Shattered Bone", "material_m004")
        Dictionary.Add("Sturdy Bone", "material_m005")
        Dictionary.Add("Otherworldly Bone", "material_m006")
        Dictionary.Add("Ancient Bone", "material_m007")
        Dictionary.Add("Unknown", "material_m008")
        Dictionary.Add("Moistened Scale", "material_m009")
        Dictionary.Add("Seapetal Scale", "material_m010")
        Dictionary.Add("Abyssal Scale", "material_m011")
        Dictionary.Add("Seaking'S Beard", "material_m012")
        Dictionary.Add("Segmented Carapace", "material_m013")
        Dictionary.Add("Iron Shell", "material_m014")
        Dictionary.Add("Armored Shell", "material_m015")
        Dictionary.Add("Regenerating Carapace", "material_m016")
        Dictionary.Add("Chipped Fang", "material_m017")
        Dictionary.Add("Wicked Fang", "material_m018")
        Dictionary.Add("Monstrous Fang", "material_m019")
        Dictionary.Add("Sinister Fang", "material_m020")
        Dictionary.Add("Severed Wing", "material_m021")
        Dictionary.Add("Scaled Wing", "material_m022")
        Dictionary.Add("Abonimable Wing", "material_m023")
        Dictionary.Add("Menacing Wings", "material_m024")
        Dictionary.Add("Moited Tail", "material_m025")
        Dictionary.Add("Barbed Tail", "material_m026")
        Dictionary.Add("Diabolical Tail", "material_m027")
        Dictionary.Add("Entrancing Tail", "material_m028")
        Dictionary.Add("Torn Leather", "material_m029")
        Dictionary.Add("Thickend Hide", "material_m030")
        Dictionary.Add("Smooth Hide", "material_m031")
        Dictionary.Add("Supple Leather", "material_m032")
        Dictionary.Add("Gummy Oil", "material_m033")
        Dictionary.Add("Fragrant Oil", "material_m034")
        Dictionary.Add("Medicanal Oil", "material_m035")
        Dictionary.Add("Esteric Oil", "material_m036")
        Dictionary.Add("Scraggly Wool", "material_m037")
        Dictionary.Add("Rough Wool", "material_m038")
        Dictionary.Add("Thick Wool", "material_m039")
        Dictionary.Add("Fluffy Wool", "material_m040")
        Dictionary.Add("Bomb Ashes", "material_m041")
        Dictionary.Add("Bomb Fragment", "material_m042")
        Dictionary.Add("Bomb Sheel", "material_m043")
        Dictionary.Add("Bomb Core", "material_m044")
        Dictionary.Add("Murky Ooze", "material_m045")
        Dictionary.Add("Vibrant Ooze", "material_m046")
        Dictionary.Add("Transperent Ooze", "material_m047")
        Dictionary.Add("Wonder Gel", "material_m048")
        Dictionary.Add("Fractured Horn", "material_m049")
        Dictionary.Add("Spined Horn", "material_m050")
        Dictionary.Add("Fiendish Horn", "material_m051")
        Dictionary.Add("Infernal Horn", "material_m052")
        Dictionary.Add("Strange Fluid", "material_m053")
        Dictionary.Add("Enigamatic Fluid", "material_m054")
        Dictionary.Add("Mysterous Fluid", "material_m055")
        Dictionary.Add("Ineffable Fluid", "material_m056")
        Dictionary.Add("Cie'Th Tear", "material_m057")
        Dictionary.Add("Tear Of Frustratoin", "material_m058")
        Dictionary.Add("Tear Of Remorce", "material_m059")
        Dictionary.Add("Tear Or Woe", "material_m060")
        Dictionary.Add("Chocoblo Plume", "material_m061")
        Dictionary.Add("Chocobo Tail Feather", "material_m062")
        Dictionary.Add("Green Needle", "material_m063")
        Dictionary.Add("Dawnlight Dew", "material_m064")
        Dictionary.Add("Dusklight Dew", "material_m065")
        Dictionary.Add("Gloom Stalk", "material_m066")
        Dictionary.Add("Sunpetal", "material_m067")
        Dictionary.Add("Red Mycelium", "material_m068")
        Dictionary.Add("Blue Mycelium", "material_m069")
        Dictionary.Add("White Mycelium", "material_m070")
        Dictionary.Add("Black Mycelium", "material_m071")
        Dictionary.Add("Succulent Fruit", "material_m072")
        Dictionary.Add("Malodouros fruit", "material_m073")
        Dictionary.Add("Moonblossom Seed", "material_m074")
        Dictionary.Add("Sunblossom Seed", "material_m075")
        Dictionary.Add("Perfume", "material_m076")
        Dictionary.Add("Insulated Cabling", "material_j000")
        Dictionary.Add("Fiber,Optic Cable", "material_j001")
        Dictionary.Add("Liquid Crystal Lens", "material_j002")
        Dictionary.Add("Ring Joint", "material_j003")
        Dictionary.Add("Epicyclic Gear", "material_j004")
        Dictionary.Add("Crankshaft", "material_j005")
        Dictionary.Add("Electrolytic Capacitor", "material_j006")
        Dictionary.Add("Flywheel", "material_j007")
        Dictionary.Add("Sprocket", "material_j008")
        Dictionary.Add("Actuator", "material_j009")
        Dictionary.Add("Spark Plug", "material_j010")
        Dictionary.Add("Iridium Plug", "material_j011")
        Dictionary.Add("Needle Valve", "material_j012")
        Dictionary.Add("Butterfly Valve", "material_j013")
        Dictionary.Add("Analog Circet", "material_j014")
        Dictionary.Add("Digital Circut", "material_j015")
        Dictionary.Add("Gyroscope", "material_j016")
        Dictionary.Add("Electrode", "material_j017")
        Dictionary.Add("Ceramic Armor", "material_j018")
        Dictionary.Add("Chobham Armor", "material_j019")
        Dictionary.Add("Radial Bearing", "material_j020")
        Dictionary.Add("Thrust Bearing", "material_j021")
        Dictionary.Add("Solenoid", "material_j022")
        Dictionary.Add("Mobius Coil", "material_j023")
        Dictionary.Add("Tungesten Tube", "material_j024")
        Dictionary.Add("Titanium Tube", "material_j025")
        Dictionary.Add("Passive Detector", "material_j026")
        Dictionary.Add("Active Detector", "material_j027")
        Dictionary.Add("Transformer", "material_j028")
        Dictionary.Add("Amplifer", "material_j029")
        Dictionary.Add("Carburetor", "material_j030")
        Dictionary.Add("Supercharger", "material_j031")
        Dictionary.Add("Piezoelectric Element", "material_j032")
        Dictionary.Add("Cystal Oscillator", "material_j033")
        Dictionary.Add("Paraffin Oil", "material_j034")
        Dictionary.Add("Silicone Oil", "material_j035")
        Dictionary.Add("Synthetic Muscle", "material_j036")
        Dictionary.Add("Turboprop", "material_j037")
        Dictionary.Add("Turbo Jet", "material_j038")
        Dictionary.Add("Tesla Turbine", "material_j039")
        Dictionary.Add("Polymer Emulsion", "material_j040")
        Dictionary.Add("Ferroelectric Film", "material_j041")
        Dictionary.Add("Super Conductor", "material_j042")
        Dictionary.Add("Perfect Conductor", "material_j043")
        Dictionary.Add("Particle Accelerator", "material_j044")
        Dictionary.Add("Ulracompact Reactor", "material_j045")
        Dictionary.Add("Credit Chip", "material_j046")
        Dictionary.Add("Incentive Chip", "material_j047")
        Dictionary.Add("Cactar Doll", "material_j048")
        Dictionary.Add("Moogle Puppet", "material_j049")
        Dictionary.Add("Tonberry Figure", "material_j050")
        Dictionary.Add("Plush Chocobo", "material_j051")
        Dictionary.Add("Millerite", "material_o000")
        Dictionary.Add("Rhodochrosite", "material_o001")
        Dictionary.Add("Cobaltie", "material_o002")
        Dictionary.Add("Persovskite", "material_o003")
        Dictionary.Add("Uraninite", "material_o004")
        Dictionary.Add("Minar Stone", "material_o005")
        Dictionary.Add("Scarletite", "material_o006")
        Dictionary.Add("Adamantite", "material_o007")
        Dictionary.Add("Dark Matter", "material_o008")
        Dictionary.Add("Trapezohedron", "material_o009")
        Dictionary.Add("Gold Dust", "material_o010")
        Dictionary.Add("Gold Nugget", "material_o011")
        Dictionary.Add("Platinum Ingot", "material_o012")


        ' Dim MyAddress As Long = &H3A83B ' Sets Offset Address 
        Dim MyAddress As Long = &H3A83C

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim ItemAmountOffset As Long = &H3A84E

        Dim Bytes As Byte()

        fs.Seek(MyAddress, SeekOrigin.Begin)


        Dim y = 0


        '' Currently Displays Items as Hex into text box, and numbers as hex into text box.

        While y < ListBox2.Items.Count

            Dim d As String = Hex(ListBox2.Items.Item(y))

            ListBox4.Items.Add(d)



            y = y + 1



        End While




        Dim x = 0

        While x < ListBox1.Items.Count

            Dim s As String = ListBox1.Items.Item(x).ToString
            Dim sDict = Dictionary.Item(s)
            Dim sHexString = StringToHex(sDict)

            ListBox3.Items.Add(sHexString)




            x = x + 1

        End While

        Dim zx = 0

        While zx < ListBox3.Items.Count

            Dim hexstring As String = ListBox3.Items.Item(zx)
            Dim Length As Integer = hexstring.Length
            Dim upperBound As Integer = Length \ 2
            If Length Mod 2 = 0 Then
                upperBound -= 1
            Else
                hexstring = "0" & hexstring

            End If

            ReDim Bytes(upperBound)
            For i As Integer = 0 To upperBound
                Bytes(i) = Convert.ToByte(hexstring.Substring(i * 2, 2), 16)
            Next

            fs.Write(Bytes, 0, Bytes.Length)

            Dim CurrentPos = fs.Position
            Dim newPos = CurrentPos + 7

            fs.Seek(newPos, SeekOrigin.Begin)

            zx += 1
        End While

        Dim xz = 0

        fs.Seek(ItemAmountOffset, SeekOrigin.Begin)

        While xz < ListBox2.Items.Count
            Dim numberValue = Hex(ListBox2.Items.Item(xz))

            While numberValue.Length < 4
                numberValue = "0" + numberValue
            End While

            If numberValue.Length Mod 2 <> 0 Then numberValue = numberValue.Insert(0, "0")

            ReDim Bytes((numberValue.Length \ 2) - 1)

            Dim a As Integer = 0

            For i As Integer = 0 To numberValue.Length - 1 Step 2
                Bytes(a) = Convert.ToByte(numberValue.Substring(i, 2), 16)
                a += 1
            Next

            fs.Write(Bytes, 0, Bytes.Length)

            Dim CurrentPos = fs.Position
            Dim newPos = CurrentPos + 18
            fs.Seek(newPos, SeekOrigin.Begin)

            xz += 1

        End While

        While zx And xz < 240

            Dim hexstring = "00000000000000000000"
            Dim Length As Integer = hexstring.Length
            Dim upperBound As Integer = Length \ 2
            If Length Mod 2 = 0 Then
                upperBound -= 1
            Else
                hexstring = "0" & hexstring

            End If

            ReDim Bytes(upperBound)
            For i As Integer = 0 To upperBound
                Bytes(i) = Convert.ToByte(hexstring.Substring(i * 2, 2), 16)
            Next

            fs.Write(Bytes, 0, Bytes.Length)

            zx += 1
            xz += 1


        End While

        fs.Close() : fs.Dispose()

        Button4.Enabled = False










    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim PartyMember1A As Long = &H1C029 ' Sets Offset Address 
        Dim PartyMember2A As Long = &H1C02D
        Dim PartyMember3A As Long = &H1C031
        Dim PartyMember4A As Long = &H1C035
        Dim PartyMember5A As Long = &H1C039
        Dim PartyMember6A As Long = &H1C03D
        Dim BattleMember1A As Long = &H1C041
        Dim BattleMember2A As Long = &H1C045
        Dim BattleMember3A As Long = &H1C049
        Dim PartyLeaderA As Long = &H1C04D

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim Bytes As Byte()


        '''
        '''
        ''' Party Member 1
        '''
        '''

        fs.Seek(PartyMember1A, SeekOrigin.Begin)

        Dim partymember1value = Hex(PartyMember1.SelectedValue)

        If partymember1value.Length Mod 2 <> 0 Then partymember1value = partymember1value.Insert(0, "0")

        ReDim Bytes((partymember1value.Length \ 2) - 1)

        Dim a As Integer = 0

        For i As Integer = 0 To partymember1value.Length - 1 Step 2
            Bytes(a) = Convert.ToByte(partymember1value.Substring(i, 2), 16)
            a += 1
        Next

        fs.Write(Bytes, 0, Bytes.Length)

        fs.Seek(BattleMember1A, SeekOrigin.Begin)
        fs.Write(Bytes, 0, Bytes.Length)
        fs.Seek(PartyLeaderA, SeekOrigin.Begin)
        fs.Write(Bytes, 0, Bytes.Length)



        '''
        '''
        ''' Party Member 2
        '''
        '''


        fs.Seek(PartyMember2A, SeekOrigin.Begin)

        Dim partymember2value = Hex(PartyMember2.SelectedValue)

        If partymember2value.Length Mod 2 <> 0 Then partymember2value = partymember2value.Insert(0, "0")

        ReDim Bytes((partymember2value.Length \ 2) - 1)

        Dim b As Integer = 0

        For i As Integer = 0 To partymember2value.Length - 1 Step 2
            Bytes(b) = Convert.ToByte(partymember2value, 16)
            b += 1
        Next

        fs.Write(Bytes, 0, Bytes.Length)
        fs.Seek(BattleMember2A, SeekOrigin.Begin)
        fs.Write(Bytes, 0, Bytes.Length)



        '''
        '''
        ''' Party Member 3
        '''
        '''

        fs.Seek(PartyMember3A, SeekOrigin.Begin)

        Dim Partymember3value = Hex(PartyMember3.SelectedValue)

        If Partymember3value.Length Mod 2 <> 0 Then Partymember3value = Partymember3value.Insert(0, "0")

        ReDim Bytes((Partymember3value.Length \ 2) - 1)

        Dim c As Integer = 0

        For i As Integer = 0 To Partymember3value.Length - 1 Step 2
            Bytes(c) = Convert.ToByte(Partymember3value, 16)
        Next

        fs.Write(Bytes, 0, Bytes.Length)
        fs.Seek(BattleMember3A, SeekOrigin.Begin)
        fs.Write(Bytes, 0, Bytes.Length)






        '''
        '''
        ''' Party Member 4
        '''
        '''

        fs.Seek(PartyMember4A, SeekOrigin.Begin)

        Dim Partymember4value = Hex(PartyMember4.SelectedValue)

        If Partymember4value.Length Mod 2 <> 0 Then Partymember4value = Partymember4value.Insert(0, "0")

        ReDim Bytes((Partymember4value.Length \ 2) - 1)

        Dim d As Integer = 0

        For i As Integer = 0 To Partymember4value.Length - 1 Step 2
            Bytes(d) = Convert.ToByte(Partymember4value, 16)
        Next

        fs.Write(Bytes, 0, Bytes.Length)


        '''
        '''
        ''' Party Member 5
        '''
        '''


        fs.Seek(PartyMember5A, SeekOrigin.Begin)

        Dim Partymember5value = Hex(PartyMember5.SelectedValue)

        If Partymember5value.Length Mod 2 <> 0 Then Partymember5value = Partymember5value.Insert(0, "0")

        ReDim Bytes((Partymember5value.Length \ 2) - 1)

        Dim z As Integer = 0

        For i As Integer = 0 To Partymember5value.Length - 1 Step 2
            Bytes(z) = Convert.ToByte(Partymember5value, 16)
        Next

        fs.Write(Bytes, 0, Bytes.Length)

        '''
        '''
        ''' Party Member 6
        '''
        '''


        fs.Seek(PartyMember6A, SeekOrigin.Begin)

        Dim Partymember6value = Hex(PartyMember6.SelectedValue)

        If Partymember6value.Length Mod 2 <> 0 Then Partymember6value = Partymember6value.Insert(0, "0")

        ReDim Bytes((Partymember6value.Length \ 2) - 1)

        Dim f As Integer = 0

        For i As Integer = 0 To Partymember6value.Length - 1 Step 2
            Bytes(f) = Convert.ToByte(Partymember6value, 16)
        Next

        fs.Write(Bytes, 0, Bytes.Length)

        fs.Close() : fs.Dispose()

    End Sub


End Class
