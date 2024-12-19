Imports System.Diagnostics.CodeAnalysis
Imports System.Diagnostics.Eventing.Reader
Imports System.Diagnostics.Tracing
Imports System.IO
Imports System.Net
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
    ''' 
    ''' 
    Shared Function StringToHex(ByVal text As String) As String
        Dim hex As String
        For i As Integer = 0 To text.Length - 1
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
            hex &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
#Enable Warning BC42104 ' Variable is used before it has been assigned a value
        Next
        Return hex
    End Function

    Shared Function HexToString(ByVal hex As String) As String
        Dim text As New System.Text.StringBuilder(hex.Length \ 2)
        For i As Integer = 0 To hex.Length - 2 Step 2
            text.Append(Chr(Convert.ToByte(hex.Substring(i, 2), 16)))
        Next
        Return text.ToString
    End Function
    Function NumberGrabber(Address As Long, arraySize As Int16)
        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File


        fs.Seek(Address, SeekOrigin.Begin)

        Dim Buffer() As Byte = New Byte(arraySize) {}

        fs.Read(Buffer, 0, Buffer.Length)

        Dim ItemAmount = Convert.ToHexString(Buffer)
        Dim ItemAmountString As Integer = Convert.ToInt32(ItemAmount, 16)

        fs.Close() : fs.Dispose()

        Return ItemAmountString


    End Function
    Function NumberWriter(Address As Long, text As String, Length As Int16)

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        br.BaseStream.Seek(Address, SeekOrigin.Begin) ' moves to the address you want

        text = Hex(text) 'Converts textbox input to hex

        While text.Length < Length ' Enures any values that would be unchanged become 0 to get exact HP value E.G 999999 returns F423F and it requires 0 before 0F for 999999 to work.
            text = "0" + text
        End While

        Dim bytes As Byte()


        If text.Length Mod 2 <> 0 Then text = text.Insert(0, "0")

        ReDim bytes((text.Length \ 2) - 1)

        Dim n As Integer = 0

        For i As Integer = 0 To text.Length - 1 Step 2
            bytes(n) = Convert.ToByte(text.Substring(i, 2), 16)
            n += 1
        Next

        fs.Write(bytes, 0, bytes.Length) ' Writes to file
        fs.Close() : fs.Dispose()
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function

    Function PartyMemberPuller(Address As Long)
        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim FlippedCharacterDictionary As New Dictionary(Of Int64, String) From {
            {0, "None"},
            {13, "Lightning"},
            {14, "Sazh"},
            {9, "Fang"},
            {17, "Vanille"},
            {11, "Hope"},
            {16, "Snow"},
            {255, "Slot Empty"}
        }

        br.BaseStream.Seek(Address, SeekOrigin.Begin) ' moves to the address you want
        Dim bytes = fs.ReadByte()
        Dim endValue = FlippedCharacterDictionary.GetValueOrDefault(bytes)



        fs.Close() : fs.Dispose()

        Return endValue
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths






    Private Sub OpenSave_Click(sender As Object, e As EventArgs) Handles OpenSave.Click
        OpenFileDialog1.Filter = "DAT_DECRYPTED FILES | *.dat_decrypted"


        If OpenFileDialog1.ShowDialog = DialogResult.OK Then ' Opens File and if opened executes code



            Dim fileInfo As System.IO.FileInfo

            fileInfo = My.Computer.FileSystem.GetFileInfo(OpenFileDialog1.FileName)

            Dim NameOfSave As String = fileInfo.Name

            Me.Text = (Me.Text + " - " + NameOfSave)

            TabControl1.Visible = True

            OpenSave.Visible = False

        End If


        If TabPage2.Visible() = True Then
            '' Area for preloading Party Editor config + Setting the defaults for it so when user clicks the tab its preloaded automatically and shows current party configuration.
            TabPage2.Show()

            Dim CharacterDictionary As New Dictionary(Of String, Int64) From {
                {"None", 0},
                {"Lightning", 13},
                {"Sazh", 14},
                {"Fang", 9},
                {"Vanille", 17},
                {"Hope", 11},
                {"Snow", 16},
                {"Slot Empty", 255}
            }

            PartyMember1.DataSource = New BindingSource(CharacterDictionary, Nothing)
            PartyMember1.ValueMember = "Value"
            PartyMember1.DisplayMember = "Key"


            PartyMember2.DataSource = New BindingSource(CharacterDictionary, Nothing)
            PartyMember2.ValueMember = "Value"
            PartyMember2.DisplayMember = "Key"


            PartyMember3.DataSource = New BindingSource(CharacterDictionary, Nothing)
            PartyMember3.ValueMember = "Value"
            PartyMember3.DisplayMember = "Key"


            PartyMember4.DataSource = New BindingSource(CharacterDictionary, Nothing)
            PartyMember4.ValueMember = "Value"
            PartyMember4.DisplayMember = "Key"


            PartyMember5.DataSource = New BindingSource(CharacterDictionary, Nothing)
            PartyMember5.ValueMember = "Value"
            PartyMember5.DisplayMember = "Key"


            PartyMember6.DataSource = New BindingSource(CharacterDictionary, Nothing)
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

            PartyMember1.Text = PartyMemberPuller(PartyMemberOriginal1)
            PartyMember2.Text = PartyMemberPuller(PartyMemberOriginal2)
            PartyMember3.Text = PartyMemberPuller(PartyMemberOriginal3)
            PartyMember4.Text = PartyMemberPuller(PartyMemberOriginal4)
            PartyMember5.Text = PartyMemberPuller(PartyMemberOriginal5)
            PartyMember6.Text = PartyMemberPuller(PartyMemberOriginal6)

            '''
            '''
            ''' automatically pulls Party Member information.
            '''
            '''

            Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

            Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

            Dim br As New BinaryReader(fs) ' BinaryReader accesses File


            Dim Dictionary As New Dictionary(Of String, String) From {
                {"material_m000", "Begrimed Claw"},
                {"material_m001", "Bestail Claw"},
                {"material_m002", "Gargantuan Claw"},
                {"material_m003", "Hellish Tallon"},
                {"material_m004", "Shattered Bone"},
                {"material_m005", "Sturdy Bone"},
                {"material_m006", "Otherworldly Bone"},
                {"material_m007", "Ancient Bone"},
                {"material_m008", "Unknown"},
                {"material_m009", "Moistened Scale"},
                {"material_m010", "Seapetal Scale"},
                {"material_m011", "Abyssal Scale"},
                {"material_m012", "Seaking'S Beard"},
                {"material_m013", "Segmented Carapace"},
                {"material_m014", "Iron Shell"},
                {"material_m015", "Armored Shell"},
                {"material_m016", "Regenerating Carapace"},
                {"material_m017", "Chipped Fang"},
                {"material_m018", "Wicked Fang"},
                {"material_m019", "Monstrous Fang"},
                {"material_m020", "Sinister Fang"},
                {"material_m021", "Severed Wing"},
                {"material_m022", "Scaled Wing"},
                {"material_m023", "Abonimable Wing"},
                {"material_m024", "Menacing Wings"},
                {"material_m025", "Moited Tail"},
                {"material_m026", "Barbed Tail"},
                {"material_m027", "Diabolical Tail"},
                {"material_m028", "Entrancing Tail"},
                {"material_m029", "Torn Leather"},
                {"material_m030", "Thickend Hide"},
                {"material_m031", "Smooth Hide"},
                {"material_m032", "Supple Leather"},
                {"material_m033", "Gummy Oil"},
                {"material_m034", "Fragrant Oil"},
                {"material_m035", "Medicanal Oil"},
                {"material_m036", "Esteric Oil"},
                {"material_m037", "Scraggly Wool"},
                {"material_m038", "Rough Wool"},
                {"material_m039", "Thick Wool"},
                {"material_m040", "Fluffy Wool"},
                {"material_m041", "Bomb Ashes"},
                {"material_m042", "Bomb Fragment"},
                {"material_m043", "Bomb Sheel"},
                {"material_m044", "Bomb Core"},
                {"material_m045", "Murky Ooze"},
                {"material_m046", "Vibrant Ooze"},
                {"material_m047", "Transperent Ooze"},
                {"material_m048", "Wonder Gel"},
                {"material_m049", "Fractured Horn"},
                {"material_m050", "Spined Horn"},
                {"material_m051", "Fiendish Horn"},
                {"material_m052", "Infernal Horn"},
                {"material_m053", "Strange Fluid"},
                {"material_m054", "Enigamatic Fluid"},
                {"material_m055", "Mysterous Fluid"},
                {"material_m056", "Ineffable Fluid"},
                {"material_m057", "Cie'Th Tear"},
                {"material_m058", "Tear Of Frustratoin"},
                {"material_m059", "Tear Of Remorce"},
                {"material_m060", "Tear Or Woe"},
                {"material_m061", "Chocoblo Plume"},
                {"material_m062", "Chocobo Tail Feather"},
                {"material_m063", "Green Needle"},
                {"material_m064", "Dawnlight Dew"},
                {"material_m065", "Dusklight Dew"},
                {"material_m066", "Gloom Stalk"},
                {"material_m067", "Sunpetal"},
                {"material_m068", "Red Mycelium"},
                {"material_m069", "Blue Mycelium"},
                {"material_m070", "White Mycelium"},
                {"material_m071", "Black Mycelium"},
                {"material_m072", "Succulent Fruit"},
                {"material_m073", "Malodouros fruit"},
                {"material_m074", "Moonblossom Seed"},
                {"material_m075", "Sunblossom Seed"},
                {"material_m076", "Perfume"},
                {"material_j000", "Insulated Cabling"},
                {"material_j001", "Fiber,Optic Cable"},
                {"material_j002", "Liquid Crystal Lens"},
                {"material_j003", "Ring Joint"},
                {"material_j004", "Epicyclic Gear"},
                {"material_j005", "Crankshaft"},
                {"material_j006", "Electrolytic Capacitor"},
                {"material_j007", "Flywheel"},
                {"material_j008", "Sprocket"},
                {"material_j009", "Actuator"},
                {"material_j010", "Spark Plug"},
                {"material_j011", "Iridium Plug"},
                {"material_j012", "Needle Valve"},
                {"material_j013", "Butterfly Valve"},
                {"material_j014", "Analog Circet"},
                {"material_j015", "Digital Circut"},
                {"material_j016", "Gyroscope"},
                {"material_j017", "Electrode"},
                {"material_j018", "Ceramic Armor"},
                {"material_j019", "Chobham Armor"},
                {"material_j020", "Radial Bearing"},
                {"material_j021", "Thrust Bearing"},
                {"material_j022", "Solenoid"},
                {"material_j023", "Mobius Coil"},
                {"material_j024", "Tungesten Tube"},
                {"material_j025", "Titanium Tube"},
                {"material_j026", "Passive Detector"},
                {"material_j027", "Active Detector"},
                {"material_j028", "Transformer"},
                {"material_j029", "Amplifer"},
                {"material_j030", "Carburetor"},
                {"material_j031", "Supercharger"},
                {"material_j032", "Piezoelectric Element"},
                {"material_j033", "Cystal Oscillator"},
                {"material_j034", "Paraffin Oil"},
                {"material_j035", "Silicone Oil"},
                {"material_j036", "Synthetic Muscle"},
                {"material_j037", "Turboprop"},
                {"material_j038", "Turbo Jet"},
                {"material_j039", "Tesla Turbine"},
                {"material_j040", "Polymer Emulsion"},
                {"material_j041", "Ferroelectric Film"},
                {"material_j042", "Super Conductor"},
                {"material_j043", "Perfect Conductor"},
                {"material_j044", "Particle Accelerator"},
                {"material_j045", "Ulracompact Reactor"},
                {"material_j046", "Credit Chip"},
                {"material_j047", "Incentive Chip"},
                {"material_j048", "Cactar Doll"},
                {"material_j049", "Moogle Puppet"},
                {"material_j050", "Tonberry Figure"},
                {"material_j051", "Plush Chocobo"},
                {"material_o000", "Millerite"},
                {"material_o001", "Rhodochrosite"},
                {"material_o002", "Cobaltie"},
                {"material_o003", "Persovskite"},
                {"material_o004", "Uraninite"},
                {"material_o005", "Minar Stone"},
                {"material_o006", "Scarletite"},
                {"material_o007", "Adamantite"},
                {"material_o008", "Dark Matter"},
                {"material_o009", "Trapezohedron"},
                {"material_o010", "Gold Dust"},
                {"material_o011", "Gold Nugget"},
                {"material_o012", "Platinum Ingot"}
            }


            Dim MyAddress As Long = &H3A83C ' Sets Offset Address 

            Dim ItemAmountOffset As Long = 239693

            fs.Seek(ItemAmountOffset, SeekOrigin.Begin)


            Dim x = 0
            Dim y = 0

            While x < 240
                fs.Seek(ItemAmountOffset, SeekOrigin.Begin)

                Dim arraySize = 2

                Dim Buffer() As Byte = New Byte(arraySize) {}

                ItemAmountOffset += 20

                fs.Read(Buffer, 0, Buffer.Length)

                Dim ItemAmount = Convert.ToHexString(Buffer)
                Dim ItemAmountString As Integer = Convert.ToInt32(ItemAmount, 16)


                If ItemAmountString > 0 Then
                    ListBox2.Items.Add(ItemAmountString & Environment.NewLine)
                End If

                x += 1

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

                y += 1

            End While

            '''
            '''
            '''   Item Inventory Code - Pulls Item names and amounts and displays them.
            '''
            '''


            Dim ItemDictionary As New Dictionary(Of String, String) From {
                {"it_potion", "Potion"},
                {"it_phenxtal", "Pheonix Down"},
                {"it_powersmoke", "Fortisol"},
                {"it_barsmoke", "Aegisol"},
                {"it_sneaksmoke", "Deceptisol"},
                {"it_tpsmoke", "Ethersol"},
                {"it_libra", "Librascope"},
                {"it_antidote", "Antidote"},
                {"it_holywater", "Holy Water"},
                {"it_stinkwater", “Foul Liquid”},
                {“it_tonkati”, “Mallet”},
                {“it_sedative”, “Painkiller”},
                {“it_wax”, “Wax”},
                {"it_elixir", "Elixir"}
            }

            Dim ItemListOffset As Long = &H374A0
            Dim ItemListAmount As Long = &H374B1



            br.BaseStream.Seek(ItemListOffset, SeekOrigin.Begin)

            Dim a = 0
            Dim b = 0


            While a < 60

                Dim ItemListArraySize = 12
                Dim ItemListNameArray() As Byte = New Byte(ItemListArraySize) {}

                fs.Read(ItemListNameArray, 0, ItemListNameArray.Length)

                Dim text = Convert.ToHexString(ItemListNameArray)
                Dim Convertedtext = HexToString(text)


                Dim CurrentPos = fs.Position()
                Dim Newoffset = CurrentPos + 7

                br.BaseStream.Seek(Newoffset, SeekOrigin.Begin)

                If text <> "00000000000000000000000000" Then
                    Convertedtext = HexToString(text)
                    Convertedtext = Convertedtext.Trim(vbNullChar)

                    ListBox8.Items.Add(ItemDictionary.Item(Convertedtext))
                End If

                a += 1
            End While




            While b < 60

                fs.Seek(ItemListAmount, SeekOrigin.Begin)

                Dim arraySize = 2

                Dim Buffer() As Byte = New Byte(arraySize) {}

                ItemListAmount += 20

                fs.Read(Buffer, 0, Buffer.Length)

                Dim ItemAmount = Convert.ToHexString(Buffer)
                Dim ItemAmountString As Integer = Convert.ToInt32(ItemAmount, 16)


                If ItemAmountString > 0 Then
                    ListBox7.Items.Add(ItemAmountString & Environment.NewLine)
                End If

                b += 1

            End While

            '''
            '''
            ''' Automatically pulls character stats from file and displays them accordingly.
            '''
            '''

            fs.Close() : fs.Dispose()

            '''
            ''' Lightning Stats
            '''
            Dim LightHPAddress As Long = &H27ED6
            Dim LightCPAddress As Long = &H27EC9
            Dim LightStrAddress As Long = &H27EE6
            Dim LightMagAddress As Long = &H27EEA
            Dim LightATBAddress As Long = &H27EE0



            LightHPValue.Value = NumberGrabber(LightHPAddress, 2)
            LightCPValue.Value = NumberGrabber(LightCPAddress, 3)
            LightStrValue.Value = NumberGrabber(LightStrAddress, 2)
            LightMagValue.Value = NumberGrabber(LightMagAddress, 2)
            LightATBValue.Value = NumberGrabber(LightATBAddress, 0) / 10

            '''
            ''' Sazh Stats
            '''
            Dim SazhHPAddress As Long = &H28C56
            Dim SazhCPAddress As Long = &H28C49
            Dim SazhStrAddress As Long = &H28C66
            Dim SazhMagAddress As Long = &H28C6A
            Dim SazhATBAddress As Long = &H28C60

            SazhHPValue.Value = NumberGrabber(SazhHPAddress, 2)
            SazhCPValue.Value = NumberGrabber(SazhCPAddress, 3)
            SazhStrValue.Value = NumberGrabber(SazhStrAddress, 2)
            SazhMagValue.Value = NumberGrabber(SazhMagAddress, 2)
            SazhATBValue.Value = NumberGrabber(SazhATBAddress, 0) / 10

            '''
            ''' Vanille Stats
            '''
            Dim VanHPAddress As Long = &H2B4D6
            Dim VanCPAddress As Long = &H2B4C9
            Dim VanStrAddress As Long = &H2B4E6
            Dim VanMagAddress As Long = &H2B4EA
            Dim VanATBAddress As Long = &H2B4E0

            VanHPValue.Value = NumberGrabber(VanHPAddress, 2)
            VanCPValue.Value = NumberGrabber(VanCPAddress, 3)
            VanStrValue.Value = NumberGrabber(VanStrAddress, 2)
            VanMagValue.Value = NumberGrabber(VanMagAddress, 2)
            VanATBValue.Value = NumberGrabber(VanATBAddress, 0) / 10

            '''
            ''' Hope Stats
            '''
            Dim HopeHPAddress As Long = &H263D6
            Dim HopeCPAddress As Long = &H263C9
            Dim HopeStrAddress As Long = &H263E6
            Dim HopeMagAddress As Long = &H263EA
            Dim HopeATBAddress As Long = &H263E0

            HopeHPValue.Value = NumberGrabber(HopeHPAddress, 2)
            HopeCPValue.Value = NumberGrabber(HopeCPAddress, 3)
            HopeStrValue.Value = NumberGrabber(HopeStrAddress, 2)
            HopeMagValue.Value = NumberGrabber(HopeMagAddress, 2)
            HopeATBValue.Value = NumberGrabber(HopeATBAddress, 0) / 10


            '''
            ''' Fang Stats
            '''
            Dim FangHPAddress As Long = &H248D6
            Dim FangCPAddress As Long = &H248C9
            Dim FangStrAddress As Long = &H248E6
            Dim FangMagAddress As Long = &H248EA
            Dim FangATBAddress As Long = &H248E0

            FangHPValue.Value = NumberGrabber(FangHPAddress, 2)
            FangCPValue.Value = NumberGrabber(FangCPAddress, 3)
            FangStrValue.Value = NumberGrabber(FangStrAddress, 2)
            FangMagValue.Value = NumberGrabber(FangMagAddress, 2)
            FangATBValue.Value = NumberGrabber(FangATBAddress, 0) / 10

            '''
            ''' Snow Stats
            '''
            Dim SnowHPAddress As Long = &H2A756
            Dim SnowCPAddress As Long = &H2A749
            Dim SnowStrAddress As Long = &H2A766
            Dim SnowMagAddress As Long = &H2A76A
            Dim SnowATBAddress As Long = &H2A760

            SnowHPvalue.Value = NumberGrabber(SnowHPAddress, 2)
            SnowCPValue.Value = NumberGrabber(SnowCPAddress, 3)
            SnowStrValue.Value = NumberGrabber(SnowStrAddress, 2)
            SnowMagValue.Value = NumberGrabber(SnowMagAddress, 2)
            SnowATBValue.Value = NumberGrabber(SnowATBAddress, 0) / 10


            '''
            '''
            ''' Party Stats - Gil, Crystarium Level + TP (TP will be changed once borders are known)
            '''
            '''

            Dim CrystariumLevelAddress As Long = &H4937

            CrystariumComboBox.SelectedIndex = NumberGrabber(CrystariumLevelAddress, 0) - 1

            Dim GilAddress As Long = &H48BC

            GilTextValue.Value = NumberGrabber(GilAddress, 3)


        End If


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

        Dim GilAddress As Short = &H48BC ' Sets Offset Address 


        If GilTextValue.Value > 999999999 Then
            GilTextValue.Value = 999999999
        End If

        GilTextBox.Text = GilTextValue.Value

        NumberWriter(GilAddress, GilTextBox.Text, 8)

        GilSucessMessage.Visible = True ' Makes message label visible saying it has been updated

    End Sub


    ''' 
    ''' 
    ''' Party Crystarium Level Segment
    ''' 
    ''' 


    Private Sub CrystariumLevelButton_Click(sender As Object, e As EventArgs) Handles CrystariumLevelButton.Click
        Dim CrystariumAddress As Short = &H4937 ' Sets Offset Address 

        NumberWriter(CrystariumAddress, CrystariumComboBox.Text, CrystariumComboBox.SelectionLength)

        CrystariumSuccessMessage.Visible = True



    End Sub


    ''' 
    ''' 
    ''' TP Value Segment
    ''' 
    ''' 


    Private Sub TPButton_Click(sender As Object, e As EventArgs) Handles TPButton.Click
        Dim TPAddress As Long = &H1C059 ' Sets Offset Address 

        NumberWriter(TPAddress, TPValueBox.Text, 4)

        TpSuccessMessage.Visible = True
    End Sub


    '''
    '''
    ''' Omni Kit Segment
    '''
    '''

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            Label44.Visible = True

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


    Private Sub FangSaveButton_Click(sender As Object, e As EventArgs) Handles FangSaveButton.Click

        Dim FangHPAddress As Long = &H248D6
        Dim FangCPAddress As Long = &H248C9
        Dim FangStrAddress As Long = &H248E6
        Dim FangMagAddress As Long = &H248EA
        Dim FangATBAddress As Long = &H248E0

        FangHP.Text = FangHPValue.Value
        FangCP.Text = FangCPValue.Value
        FangStr.Text = FangStrValue.Value
        FangMag.Text = FangMagValue.Value
        FangATB.Text = FangATBValue.Value * 10

        NumberWriter(FangHPAddress, FangHP.Text, 6)
        NumberWriter(FangCPAddress, FangCP.Text, 8)
        NumberWriter(FangStrAddress, FangStr.Text, 6)
        NumberWriter(FangMagAddress, FangMag.Text, 6)
        NumberWriter(FangATBAddress, FangATB.Text, 2)

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

    Private Sub LightSaveButton_Click(sender As Object, e As EventArgs) Handles LightSaveButton.Click

        Dim LightHPAddress As Long = &H27ED6
        Dim LightATBAddress As Long = &H27EE0
        Dim LightCPAddress As Long = &H27EC9
        Dim LightStrAddress As Long = &H27EE6
        Dim LightMagAddress As Long = &H27EEA

        LightHP.Text = LightHPValue.Value
        LightCP.Text = LightCPValue.Value
        LightStr.Text = LightStrValue.Value
        LightMag.Text = LightMagValue.Value
        LightATB.Text = LightATBValue.Value * 10

        NumberWriter(LightHPAddress, LightHP.Text, 6)
        NumberWriter(LightCPAddress, LightCP.Text, 8)
        NumberWriter(LightStrAddress, LightStr.Text, 6)
        NumberWriter(LightMagAddress, LightMag.Text, 6)
        NumberWriter(LightATBAddress, LightATB.Text, 2)
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

    Private Sub SazhSaveButton_Click(sender As Object, e As EventArgs) Handles SazhSaveButton.Click

        Dim SazhHPAddress As Long = &H28C56
        Dim SazhCPAddress As Long = &H28C49
        Dim SazhStrAddress As Long = &H28C66
        Dim SazhMagAddress As Long = &H28C6A
        Dim SazhATBAddress As Long = &H28C60

        SazhHP.Text = SazhHPValue.Value
        SazhCP.Text = SazhCPValue.Value
        SazhStr.Text = SazhStrValue.Value
        SazhMag.Text = SazhMagValue.Value
        SazhATB.Text = SazhATBValue.Value * 10

        NumberWriter(SazhHPAddress, SazhHP.Text, 6)
        NumberWriter(SazhCPAddress, SazhCP.Text, 8)
        NumberWriter(SazhStrAddress, SazhStr.Text, 6)
        NumberWriter(SazhMagAddress, SazhMag.Text, 6)
        NumberWriter(SazhATBAddress, SazhATB.Text, 2)

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

    Private Sub VanSaveButton_Click(sender As Object, e As EventArgs) Handles VanSaveButton.Click

        Dim VanHPAddress As Long = &H2B4D6
        Dim VanCPAddress As Long = &H2B4C9
        Dim VanStrAddress As Long = &H2B4E6
        Dim VanMagAddress As Long = &H2B4EA
        Dim VanATBAddress As Long = &H2B4E0

        VanHP.Text = VanHPValue.Value
        VanCP.Text = VanCPValue.Value
        VanStr.Text = VanStrValue.Value
        VanMag.Text = VanMagValue.Value
        VanATB.Text = VanATBValue.Value * 10

        NumberWriter(VanHPAddress, VanHP.Text, 6)
        NumberWriter(VanCPAddress, VanCP.Text, 8)
        NumberWriter(VanStrAddress, VanStr.Text, 6)
        NumberWriter(VanMagAddress, VanMag.Text, 6)
        NumberWriter(VanATBAddress, VanATB.Text, 2)

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

    Private Sub HopeSaveButton_Click(sender As Object, e As EventArgs) Handles HopeSaveButton.Click

        Dim HopeHPAddress As Long = &H263D6
        Dim HopeCPAddress As Long = &H263C9
        Dim HopeStrAddress As Long = &H263E6
        Dim HopeMagAddress As Long = &H263EA
        Dim HopeATBAddress As Long = &H263E0

        HopeHP.Text = HopeHPValue.Value
        HopeCP.Text = HopeCPValue.Value
        HopeStr.Text = HopeStrValue.Value
        HopeMag.Text = HopeMagValue.Value
        HopeATB.Text = HopeATBValue.Value * 10

        NumberWriter(HopeHPAddress, HopeHP.Text, 6)
        NumberWriter(HopeCPAddress, HopeCP.Text, 8)
        NumberWriter(HopeStrAddress, HopeStr.Text, 6)
        NumberWriter(HopeMagAddress, HopeMag.Text, 6)
        NumberWriter(HopeATBAddress, HopeATB.Text, 2)


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

    Private Sub SnowSaveButton_Click(sender As Object, e As EventArgs) Handles SnowSaveButton.Click

        Dim SnowHPAddress As Long = &H2A756
        Dim SnowCPAddress As Long = &H2A749
        Dim SnowStrAddress As Long = &H2A766
        Dim SnowMagAddress As Long = &H2A76A
        Dim SnowATBAddress As Long = &H2A760

        SnowHP.Text = SnowHPvalue.Value
        SnowCP.Text = SnowCPValue.Value
        SnowStr.Text = SnowStrValue.Value
        SnowMag.Text = SnowMagValue.Value
        SnowATB.Text = SnowATBValue.Value * 10

        NumberWriter(SnowHPAddress, SnowHP.Text, 6)
        NumberWriter(SnowCPAddress, SnowCP.Text, 8)
        NumberWriter(SnowStrAddress, SnowStr.Text, 6)
        NumberWriter(SnowMagAddress, SnowMag.Text, 6)
        NumberWriter(SnowATBAddress, SnowATB.Text, 2)

    End Sub

    ''' 
    ''' 
    ''' Relates to Inventory Editing - Populating listboxes for data retrieval + adding. And simultanious scrolling of the two boxes to track Item name : amount.
    ''' 
    ''' 

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox1.TopIndex <> ListBox2.TopIndex Then
            ListBox1.TopIndex = ListBox2.TopIndex
        End If

        If ListBox1.SelectedIndex <> ListBox2.SelectedIndex Then
            ListBox1.SelectedIndex = ListBox2.SelectedIndex
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
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
        Dim Dictionary As New Dictionary(Of String, String) From {
            {"Begrimed Claw", "material_m000"},
            {"Bestail Claw", "material_m001"},
            {"Gargantuan Claw", "material_m002"},
            {"Hellish Tallon", "material_m003"},
            {"Shattered Bone", "material_m004"},
            {"Sturdy Bone", "material_m005"},
            {"Otherworldly Bone", "material_m006"},
            {"Ancient Bone", "material_m007"},
            {"Unknown", "material_m008"},
            {"Moistened Scale", "material_m009"},
            {"Seapetal Scale", "material_m010"},
            {"Abyssal Scale", "material_m011"},
            {"Seaking'S Beard", "material_m012"},
            {"Segmented Carapace", "material_m013"},
            {"Iron Shell", "material_m014"},
            {"Armored Shell", "material_m015"},
            {"Regenerating Carapace", "material_m016"},
            {"Chipped Fang", "material_m017"},
            {"Wicked Fang", "material_m018"},
            {"Monstrous Fang", "material_m019"},
            {"Sinister Fang", "material_m020"},
            {"Severed Wing", "material_m021"},
            {"Scaled Wing", "material_m022"},
            {"Abonimable Wing", "material_m023"},
            {"Menacing Wings", "material_m024"},
            {"Moited Tail", "material_m025"},
            {"Barbed Tail", "material_m026"},
            {"Diabolical Tail", "material_m027"},
            {"Entrancing Tail", "material_m028"},
            {"Torn Leather", "material_m029"},
            {"Thickend Hide", "material_m030"},
            {"Smooth Hide", "material_m031"},
            {"Supple Leather", "material_m032"},
            {"Gummy Oil", "material_m033"},
            {"Fragrant Oil", "material_m034"},
            {"Medicanal Oil", "material_m035"},
            {"Esteric Oil", "material_m036"},
            {"Scraggly Wool", "material_m037"},
            {"Rough Wool", "material_m038"},
            {"Thick Wool", "material_m039"},
            {"Fluffy Wool", "material_m040"},
            {"Bomb Ashes", "material_m041"},
            {"Bomb Fragment", "material_m042"},
            {"Bomb Sheel", "material_m043"},
            {"Bomb Core", "material_m044"},
            {"Murky Ooze", "material_m045"},
            {"Vibrant Ooze", "material_m046"},
            {"Transperent Ooze", "material_m047"},
            {"Wonder Gel", "material_m048"},
            {"Fractured Horn", "material_m049"},
            {"Spined Horn", "material_m050"},
            {"Fiendish Horn", "material_m051"},
            {"Infernal Horn", "material_m052"},
            {"Strange Fluid", "material_m053"},
            {"Enigamatic Fluid", "material_m054"},
            {"Mysterous Fluid", "material_m055"},
            {"Ineffable Fluid", "material_m056"},
            {"Cie'Th Tear", "material_m057"},
            {"Tear Of Frustratoin", "material_m058"},
            {"Tear Of Remorce", "material_m059"},
            {"Tear Or Woe", "material_m060"},
            {"Chocoblo Plume", "material_m061"},
            {"Chocobo Tail Feather", "material_m062"},
            {"Green Needle", "material_m063"},
            {"Dawnlight Dew", "material_m064"},
            {"Dusklight Dew", "material_m065"},
            {"Gloom Stalk", "material_m066"},
            {"Sunpetal", "material_m067"},
            {"Red Mycelium", "material_m068"},
            {"Blue Mycelium", "material_m069"},
            {"White Mycelium", "material_m070"},
            {"Black Mycelium", "material_m071"},
            {"Succulent Fruit", "material_m072"},
            {"Malodouros fruit", "material_m073"},
            {"Moonblossom Seed", "material_m074"},
            {"Sunblossom Seed", "material_m075"},
            {"Perfume", "material_m076"},
            {"Insulated Cabling", "material_j000"},
            {"Fiber,Optic Cable", "material_j001"},
            {"Liquid Crystal Lens", "material_j002"},
            {"Ring Joint", "material_j003"},
            {"Epicyclic Gear", "material_j004"},
            {"Crankshaft", "material_j005"},
            {"Electrolytic Capacitor", "material_j006"},
            {"Flywheel", "material_j007"},
            {"Sprocket", "material_j008"},
            {"Actuator", "material_j009"},
            {"Spark Plug", "material_j010"},
            {"Iridium Plug", "material_j011"},
            {"Needle Valve", "material_j012"},
            {"Butterfly Valve", "material_j013"},
            {"Analog Circet", "material_j014"},
            {"Digital Circut", "material_j015"},
            {"Gyroscope", "material_j016"},
            {"Electrode", "material_j017"},
            {"Ceramic Armor", "material_j018"},
            {"Chobham Armor", "material_j019"},
            {"Radial Bearing", "material_j020"},
            {"Thrust Bearing", "material_j021"},
            {"Solenoid", "material_j022"},
            {"Mobius Coil", "material_j023"},
            {"Tungesten Tube", "material_j024"},
            {"Titanium Tube", "material_j025"},
            {"Passive Detector", "material_j026"},
            {"Active Detector", "material_j027"},
            {"Transformer", "material_j028"},
            {"Amplifer", "material_j029"},
            {"Carburetor", "material_j030"},
            {"Supercharger", "material_j031"},
            {"Piezoelectric Element", "material_j032"},
            {"Cystal Oscillator", "material_j033"},
            {"Paraffin Oil", "material_j034"},
            {"Silicone Oil", "material_j035"},
            {"Synthetic Muscle", "material_j036"},
            {"Turboprop", "material_j037"},
            {"Turbo Jet", "material_j038"},
            {"Tesla Turbine", "material_j039"},
            {"Polymer Emulsion", "material_j040"},
            {"Ferroelectric Film", "material_j041"},
            {"Super Conductor", "material_j042"},
            {"Perfect Conductor", "material_j043"},
            {"Particle Accelerator", "material_j044"},
            {"Ulracompact Reactor", "material_j045"},
            {"Credit Chip", "material_j046"},
            {"Incentive Chip", "material_j047"},
            {"Cactar Doll", "material_j048"},
            {"Moogle Puppet", "material_j049"},
            {"Tonberry Figure", "material_j050"},
            {"Plush Chocobo", "material_j051"},
            {"Millerite", "material_o000"},
            {"Rhodochrosite", "material_o001"},
            {"Cobaltie", "material_o002"},
            {"Persovskite", "material_o003"},
            {"Uraninite", "material_o004"},
            {"Minar Stone", "material_o005"},
            {"Scarletite", "material_o006"},
            {"Adamantite", "material_o007"},
            {"Dark Matter", "material_o008"},
            {"Trapezohedron", "material_o009"},
            {"Gold Dust", "material_o010"},
            {"Gold Nugget", "material_o011"},
            {"Platinum Ingot", "material_o012"}
        }


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

            y += 1

        End While


        Dim x = 0

        While x < ListBox1.Items.Count

            Dim s As String = ListBox1.Items.Item(x).ToString
            Dim sDict = Dictionary.Item(s)
            Dim sHexString = StringToHex(sDict)

            ListBox3.Items.Add(sHexString)

            x += 1

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
        Label43.Visible = True
    End Sub

    Private Sub ItemsSaveButton_Click(sender As Object, e As EventArgs) Handles ItemsSaveButton.Click
        Dim FlippedItemDictionary As New Dictionary(Of String, String) From {
            {"Potion", "it_potion"},
            {"Pheonix Down", "it_phenxtal"},
            {"Fortisol", "it_powersmoke"},
            {"Aegisol", "it_barsmoke"},
            {"Deceptisol", "it_sneaksmoke"},
            {"Ethersol", "it_tpsmoke"},
            {"Librascope", "it_libra"},
            {"Antidote", "it_antidote"},
            {"Holy Water", "it_holywater"},
            {"Foul Liquid", "it_stinkwater"},
            {"Mallet", "it_tonkati"},
            {"Painkiller", "it_sedative"},
            {"Wax", "it_wax"},
            {"Elixir", "it_elixir"}
        }

        Dim MyAddress As Long = &H374A0

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim ItemAmountOffset As Long = &H374B2

        Dim Bytes As Byte()

        fs.Seek(MyAddress, SeekOrigin.Begin)


        Dim y = 0


        '' Currently Displays Items as Hex into text box, and numbers as hex into text box.

        While y < ListBox7.Items.Count

            Dim d As String = Hex(ListBox7.Items.Item(y))

            ListBox5.Items.Add(d)

            y += 1

        End While


        Dim x = 0

        While x < ListBox8.Items.Count

            Dim s As String = ListBox8.Items.Item(x).ToString
            Dim sDict = FlippedItemDictionary.Item(s)
            Dim sHexString = StringToHex(sDict)

            While sHexString.Length < 26
                sHexString += "0"
            End While

            ListBox6.Items.Add(sHexString)

            x += 1

        End While

        Dim zx = 0

        While zx < ListBox6.Items.Count

            Dim hexstring As String = ListBox6.Items.Item(zx)
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

        While xz < ListBox7.Items.Count
            Dim numberValue = Hex(ListBox7.Items.Item(xz))

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

        While zx And xz < 60

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

        Button2.Enabled = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ListBox8.Items.Add(ComboBox2.Text)
        ListBox7.Items.Add(NumericUpDown2.Value)

    End Sub

    Private Sub SavePointGrabber_Click(sender As Object, e As EventArgs) Handles SavePointGrabber.Click

        ' This is a WIP - Code may not be functional or work correctly at this time
        '
        ' To Create a save teleporter the following needs to be done:
        '
        ' Read the bytes between offsets Ox3028 and 0x3036 - This gives the save name e.g. S_Point_xxx_01 with xxx denoting the area.
        ' Read the value at 40B3 as this seems to be crucial to making it work. 
        '
        ' Use a dictionary with a list of hex save names, and their corrosponding english name such as "Grapa Whitewood - Chapter 5 "
        ' Have a dictionary to go with the hex save name, detailing what value is found at 40B3 to allow for that value to be correct.
        ' Have a flipped dictionary to allow reversing of the process.

        ' All of this combined should allow for a functioning save teleporter where a used can select from a dropdown box, and when the button is clicked it edits the save point you spawn in at. 

    End Sub

    Private Sub WepAccButton_Click(sender As Object, e As EventArgs) Handles WepAccButton.Click
        ' Length of Weapon/Accessory Strings are 28 bytes total. Contains prefix - Wea or Acc.
        ' Example of entry: wep_lig_001 or acc_000_001 followed by levels, exp, exp multiplier*
        '* Not figured out order/Structure as of yet, however from testing this is in there. 

        Dim weaponDictionary As New Dictionary(Of String, String) From {
            {"wea_fan_001", "Bladed Lance"},
            {"wea_fan_002", "Glaive"},
            {"wea_fan_003", "Dragoon Lance"},
            {"wea_fan_004", "Dragonhorn"},
            {"wea_fan_005", "Partisan"},
            {"wea_fan_006", "Rhomphaia"},
            {"wea_fan_007", "Shamanic Spear"},
            {"wea_fan_008", "Heretic's Halberd"},
            {"wea_fan_009", "Punisher"},
            {"wea_fan_010", "Banescissor Spear"},
            {"wea_fan_011", "Pandoran Spear"},
            {"wea_fan_012", "Calamity Spear"},
            {"wea_fan_013", "Taming Pole"},
            {"wea_fan_014", "Venus Gospel"},
            {"wea_fan_015", "Gae Bolg"},
            {"wea_fan_016", "Gungnir"},
            {"wea_fan_017", "Kain's Lance(Bladed Lance)"},
            {"wea_fan_018", "Kain's Lance(Dragoon Lance)"},
            {"wea_fan_019", "Kain's Lance(Partisan)"},
            {"wea_fan_020", "Kain's Lance(Shamanic Spear)"},
            {"wea_fan_021", "Kain's Lance(Punisher)"},
            {"wea_fan_022", "Kain's Lance(Pandoran Spear)"},
            {"wea_fan_023", "Kain's Lance(Taming Pole)"},
            {"wea_fan_024", "Kain's Lance(Gae Bolg)"},
            {"wea_hop_001", "Airwing"},
            {"wea_hop_002", "Skycutter"},
            {"wea_hop_003", "Hawkeye"},
            {"wea_hop_004", "Eagletalon"},
            {"wea_hop_005", "Otshirvani"},
            {"wea_hop_006", "Urubutsin"},
            {"wea_hop_007", "Ninurta"},
            {"wea_hop_008", "Jatayu"},
            {"wea_hop_009", "Vidofnir"},
            {"wea_hop_010", "Hresvelgr"},
            {"wea_hop_011", "Simurgh"},
            {"wea_hop_012", "Tezcatlipoca"},
            {"wea_hop_013", "Malphas"},
            {"wea_hop_014", "Naberius"},
            {"wea_hop_015", "Alicanto"},
            {"wea_hop_016", "Caladrius"},
            {"wea_hop_017", "Nue(Airwing)"},
            {"wea_hop_018", "Nue(Hawkeye)"},
            {"wea_hop_019", "Nue(Otshirvani)"},
            {"wea_hop_020", "Nue(Ninurta)"},
            {"wea_hop_021", "Nue(Vidofnir)"},
            {"wea_hop_022", "Nue(Simurgh)"},
            {"wea_hop_023", "Nue(Malphas)"},
            {"wea_hop_024", "Nue(Alicanto)"},
            {"wea_lig_001", "Blazefire Saber"},
            {"wea_lig_002", "Flamberge"},
            {"wea_lig_003", "Axis Blade"},
            {"wea_lig_004", "Enkindler"},
            {"wea_lig_005", "Edged Carbine"},
            {"wea_lig_006", "Razor Carbine"},
            {"wea_lig_007", "Lifesaber"},
            {"wea_lig_008", "Peacemaker"},
            {"wea_lig_009", "Gladius"},
            {"wea_lig_010", "Helter-skelter"},
            {"wea_lig_011", "Organyx"},
            {"wea_lig_012", "Apocalypse"},
            {"wea_lig_013", "Hauteclaire"},
            {"wea_lig_014", "Durandal"},
            {"wea_lig_015", "Lionheart"},
            {"wea_lig_016", "Ultima Weapon"},
            {"wea_lig_017", "Omega Weapon(Blazefire Saber)"},
            {"wea_lig_018", "Omega Weapon(Axis Blade)"},
            {"wea_lig_019", "Omega Weapon(Edged Carbine)"},
            {"wea_lig_020", "Omega Weapon(Lifesaber)"},
            {"wea_lig_021", "Omega Weapon(Gladius)"},
            {"wea_lig_022", "Omega Weapon(Organyx)"},
            {"wea_lig_023", "Omega Weapon(Hauteclaire)"},
            {"wea_lig_024", "Omega Weapon(Lionheart)"},
            {"wea_saz_001", "Vega 42s"},
            {"wea_saz_002", "Altairs"},
            {"wea_saz_003", "Spica Defenders"},
            {"wea_saz_004", "Sirius Sidearms"},
            {"wea_saz_005", "Deneb Duellers"},
            {"wea_saz_006", "Canopus AMPs"},
            {"wea_saz_007", "Rigels"},
            {"wea_saz_008", "Polaris Specials"},
            {"wea_saz_009", "Aldebarabs"},
            {"wea_saz_010", "Sadalmeliks"},
            {"wea_saz_011", "Pleiades Hi-Powers"},
            {"wea_saz_012", "Hyades Magnums"},
            {"wea_saz_013", "Antares Deluxes"},
            {"wea_saz_014", "Fomalhaut Elites"},
            {"wea_saz_015", "Procyons"},
            {"wea_saz_016", "Betelgeuse Customs"},
            {"wea_saz_017", "Total Eclipse(Vega 42s)"},
            {"wea_saz_018", "Total Eclipse(Spica Defenders)"},
            {"wea_saz_019", "Total Eclipse(Deneb Duellers)"},
            {"wea_saz_020", "Total Eclipse(Rigels)"},
            {"wea_saz_021", "Total Eclipse(Aldebarabs)"},
            {"wea_saz_022", "Total Eclipse(Pleiades Hi-Powers)"},
            {"wea_saz_023", "Total Eclipse(Antares Deluxes)"},
            {"wea_saz_024", "Total Eclipse(Procyons)"},
            {"wea_sno_001", "Wild Bear"},
            {"wea_sno_002", "Feral Pride"},
            {"wea_sno_003", "Paladin"},
            {"wea_sno_004", "Winged Saint"},
            {"wea_sno_005", "Rebel Heart"},
            {"wea_sno_006", "Warrior's Emblem"},
            {"wea_sno_007", "Power Circle"},
            {"wea_sno_008", "Battle Standard"},
            {"wea_sno_009", "Feymark"},
            {"wea_sno_010", "Soul Blazer"},
            {"wea_sno_011", "Sacrificial Circle"},
            {"wea_sno_012", "Indomitus"},
            {"wea_sno_013", "Unsetting Sun"},
            {"wea_sno_014", "Midnight Sun"},
            {"wea_sno_015", "Umbra"},
            {"wea_sno_016", "Solaris"},
            {"wea_sno_017", "Save the Queen(Wild Bear)"},
            {"wea_sno_018", "Save the Queen(Paladin)"},
            {"wea_sno_019", "Save the Queen(Rebel Heart)"},
            {"wea_sno_020", "Save the Queen(Power Circle)"},
            {"wea_sno_021", "Save the Queen(Feymark)"},
            {"wea_sno_022", "Save the Queen(Sacrificial Circle)"},
            {"wea_sno_023", "Save the Queen(Unsetting Sun)"},
            {"wea_sno_024", "Save the Queen(Umbra)"},
            {"wea_van_001", "Binding Rod"},
            {"wea_van_002", "Hunter's Rod"},
            {"wea_van_003", "Tigerclaw"},
            {"wea_van_004", "Wyrmfang"},
            {"wea_van_005", "Healer's Staff"},
            {"wea_van_006", "Physician's Staff"},
            {"wea_van_007", "Pearlwing Staff"},
            {"wea_van_008", "Brightwing Staff"},
            {"wea_van_009", "Rod of Thorns"},
            {"wea_van_010", "Orochi Rod"},
            {"wea_van_011", "Mistilteinn"},
            {"wea_van_012", "Erinye's Cane"},
            {"wea_van_013", "Belladonna Wand"},
            {"wea_van_014", "Malboro Wand"},
            {"wea_van_015", "Heavenly Axis"},
            {"wea_van_016", "Abraxas"},
            {"wea_van_017", "Nirvana(Binding Rod)"},
            {"wea_van_018", "Nirvana(Tigerclaw)"},
            {"wea_van_019", "Nirvana(Healer's Staff)"},
            {"wea_van_020", "Nirvana(Pearlwing Staff)"},
            {"wea_van_021", "Nirvana(Rod of Thorns)"},
            {"wea_van_022", "Nirvana(Mistilteinn)"},
            {"wea_van_023", "Nirvana(Belladonna Wand)"},
            {"wea_van_024", "Nirvana(Heavenly Axis)"}
            }

        Dim accessoryDictionary As New Dictionary(Of String, String) From {
            {"acc_000_000", "Iron Bangle"},
            {"acc_000_001", "Silver Bangle"},
            {"acc_000_002", "Tungsten Bangle"},
            {"acc_000_003", "Titanium Bangle"},
            {"acc_000_004", "Gold Bangle"},
            {"acc_000_005", "Mythril Bangle"},
            {"acc_000_006", "Platinum Bangle"},
            {"acc_000_007", "Diamond Bangle"},
            {"acc_000_008", "Adamant Bangle"},
            {"acc_000_009", "Wurtzite Bangle"},
            {"acc_000_100", "Power Wristband"},
            {"acc_000_101", "Brawler's Wristband"},
            {"acc_000_102", "Warrior's Wristband"},
            {"acc_000_103", "Power Glove"},
            {"acc_000_104", "Kaiser Knuckles"},
            {"acc_000_200", "Magician's Mark"},
            {"acc_000_201", "Shaman's Mark"},
            {"acc_000_202", "Sorcerer's Mark"},
            {"acc_000_203", "Weirding Glyph"},
            {"acc_000_204", "Magistral Crest"},
            {"acc_000_300", "Black Belt"},
            {"acc_000_301", "General's Belt"},
            {"acc_000_302", "Champion's Belt"},
            {"acc_000_400", "Rune Bracelet"},
            {"acc_000_401", "Witch's Bracelet"},
            {"acc_000_402", "Magus's Bracelet"},
            {"acc_000_500", "Royal Armlet"},
            {"acc_000_501", "Imperial Armlet"},
            {"acc_001_000", "Ember Ring"},
            {"acc_001_001", "Blaze Ring"},
            {"acc_001_002", "Salamandrine Ring"},
            {"acc_002_000", "Frost Ring"},
            {"acc_002_001", "Icicle Ring"},
            {"acc_002_002", "Boreal Ring"},
            {"acc_003_000", "Spark Ring"},
            {"acc_003_001", "Fulmen Ring"},
            {"acc_003_002", "Raijin Ring"},
            {"acc_004_000", "Aqua Ring"},
            {"acc_004_001", "Riptide Ring"},
            {"acc_004_002", "Nereid Ring"},
            {"acc_005_000", "Zephyr Ring"},
            {"acc_005_001", "Gale Ring"},
            {"acc_005_002", "Sylphid Ring"},
            {"acc_006_000", "Clay Ring"},
            {"acc_006_001", "Siltstone Ring"},
            {"acc_006_002", "Gaian Ring"},
            {"acc_007_000", "Entite Ring"},
            {"acc_009_000", "Giant's Glove"},
            {"acc_009_001", "Warlord's Glove"},
            {"acc_010_000", "Glass Buckle"},
            {"acc_010_001", "Tektite Buckle"},
            {"acc_011_000", "Metal Armband"},
            {"acc_011_001", "Ceramic Armband"},
            {"acc_012_000", "Serenity Sachet"},
            {"acc_012_001", "Safeguard Sachet"},
            {"acc_013_000", "Glass Orb"},
            {"acc_013_001", "Dragonfly Orb"},
            {"acc_014_000", "Star Pendant"},
            {"acc_014_001", "Starfall Pendant"},
            {"acc_015_000", "Pearl Necklace"},
            {"acc_015_001", "Gemstone Necklace"},
            {"acc_016_000", "Warding Talisman"},
            {"acc_016_001", "Hexbane Talisman"},
            {"acc_017_000", "Pain Dampener"},
            {"acc_017_001", "Pain Deflector"},
            {"acc_018_000", "White Cape"},
            {"acc_018_001", "Effulgent Cape"},
            {"acc_019_000", "Rainbow Anklet"},
            {"acc_019_001", "Moonbow Anklet"},
            {"acc_020_000", "Cherub's Crown"},
            {"acc_020_001", "Seraph's Crown"},
            {"acc_023_000", "Ribbon"},
            {"acc_023_001", "Supper Ribbon"},
            {"acc_025_000", "Guardian Amulet"},
            {"acc_025_001", "Shield Talisman"},
            {"acc_026_000", "Auric Amulet"},
            {"acc_026_001", "Soulfont Talisman"},
            {"acc_027_000", "Watchman's Amulet"},
            {"acc_027_001", "Shrouding Talisman"},
            {"acc_028_000", "Hero's Amulet"},
            {"acc_028_001", "Morale Talisman"},
            {"acc_029_000", "Saint's Amulet"},
            {"acc_029_001", "Blessed Talisman"},
            {"acc_030_000", "Hermes Sandals"},
            {"acc_030_001", "Sprint Shoes"},
            {"acc_031_000", "Flamebane Brooch"},
            {"acc_031_001", "Flameshield Talisman"},
            {"acc_032_000", "Frostbane Brooch"},
            {"acc_032_001", "Frostshield Talisman"},
            {"acc_033_000", "Sparkbane Brooch"},
            {"acc_033_001", "Sparkshield Talisman"},
            {"acc_034_000", "Aquabane Brooch"},
            {"acc_034_001", "Aquashield Talisman"},
            {"acc_035_000", "Zealot's Amulet"},
            {"acc_035_001", "Battle Talisman"},
            {"acc_036_000", "Tetradic Crown"},
            {"acc_036_001", "Tetradic Tiara"},
            {"acc_037_000", "Whistlewind Scarf"},
            {"acc_037_001", "Aurora Scarf"},
            {"acc_038_000", "Nimbletoe Boots"},
            {"acc_039_000", "Collector Catalog"},
            {"acc_039_001", "Connoisseur Catalog"},
            {"acc_040_000", "Gold Watch"},
            {"acc_040_001", "Champion's Badge"},
            {"acc_040_002", "Survivalist Catalog"},
            {"acc_041_000", "Hunter's Friend"},
            {"acc_041_001", "Speed Sash"},
            {"acc_041_002", "Energy Sash"},
            {"acc_042_001", "Genji Glove"},
            {"acc_045_000", "Growth Egg"},
            {"acc_046_000", "Twenty-sided Die"},
            {"acc_047_000", "Fire Charm"},
            {"acc_048_000", "Ice Charm"},
            {"acc_049_000", "Lightning Charm"},
            {"acc_050_000", "Water Charm"},
            {"acc_051_000", "Wind Charm"},
            {"acc_052_000", "Earth Charm"},
            {"acc_053_000", "Doctor's Code"},
            {"acc_054_000", "Goddess's Favor"}
            }


        'Total Length = 14360 / 28 = ~ 512 entries of Weapons/Accessories. (+1 when moving to next entry so it starts at the next entry vs end of previous one.)

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim MyAddress As Long = 284176 ' Sets Offset Address 

        fs.Seek(MyAddress, SeekOrigin.Begin)


        Dim TotalEntries = 0

        ' While Loop that pulls all of the data per entry. - Commented out incase other 

        While TotalEntries < 512

            fs.Seek(MyAddress, SeekOrigin.Begin)

            Dim initialArraySize = 27

            Dim Buffer() As Byte = New Byte(initialArraySize) {}

            fs.Read(Buffer, 0, Buffer.Length)

            Dim ItemString = Convert.ToHexString(Buffer)
            If ItemString.Contains("776561") Then
                WepBox.Items.Add(ItemString)
            ElseIf ItemString.Contains("616363") Then
                AccBox.Items.Add(ItemString)
            End If

            MyAddress += 28

            TotalEntries += 1

        End While

        Dim weaponCounter = 0
        Dim accessoryCounter = 0

        While weaponCounter < WepBox.Items.Count()
            Dim weaponIDItem = WepBox.Items.Item(weaponCounter)

            Dim weaponIDText = weaponIDItem.ToString.Remove(22)
            Dim weaponInformation = weaponIDItem.ToString.Remove(0, 22)

            WepInformation.Items.Add(weaponInformation)

            WepName.Items.Add(weaponDictionary.Item(HexToString(weaponIDText)))

            weaponCounter += 1
        End While

        While accessoryCounter < AccBox.Items.Count()
            Dim accIDItem = AccBox.Items.Item(accessoryCounter)

            Dim accIDText = accIDItem.ToString.Remove(22)
            Dim accessoryInformation = accIDItem.ToString.Remove(0, 22)

            AccInformation.Items.Add(accessoryInformation)

            AccName.Items.Add(accessoryDictionary.Item(HexToString(accIDText)))

            accessoryCounter += 1
        End While


        ' Weapon/Accessory IDs have a length of 11. Will split the Identifier from the Weapon Information (Levels, Exp etc) and Display the name in a box, and hide the extra information.
        ' Will create a default string that gives level 1 with 0 exp to any new added weapons until I add a level/exp changer.

        ' While loop that Splits Identifier and Information.



        fs.Close() : fs.Dispose()

    End Sub

    Private Sub AddWeapon_Click(sender As Object, e As EventArgs) Handles addWeapon.Click


        Dim flippedWeaponDictionary As New Dictionary(Of String, String) From {
            {"Bladed Lance", "wea_fan_001"},
            {"Glaive", "wea_fan_002"},
            {"Dragoon Lance", "wea_fan_003"},
            {"Dragonhorn", "wea_fan_004"},
            {"Partisan", "wea_fan_005"},
            {"Rhomphaia", "wea_fan_006"},
            {"Shamanic Spear", "wea_fan_007"},
            {"Heretic's Halberd", "wea_fan_008"},
            {"Punisher", "wea_fan_009"},
            {"Banescissor Spear", "wea_fan_010"},
            {"Pandoran Spear", "wea_fan_011"},
            {"Calamity Spear", "wea_fan_012"},
            {"Taming Pole", "wea_fan_013"},
            {"Venus Gospel", "wea_fan_014"},
            {"Gae Bolg", "wea_fan_015"},
            {"Gungnir", "wea_fan_016"},
            {"Kain's Lance(Bladed Lance)", "wea_fan_017"},
            {"Kain's Lance(Dragoon Lance)", "wea_fan_018"},
            {"Kain's Lance(Partisan)", "wea_fan_019"},
            {"Kain's Lance(Shamanic Spear)", "wea_fan_020"},
            {"Kain's Lance(Punisher)", "wea_fan_021"},
            {"Kain's Lance(Pandoran Spear)", "wea_fan_022"},
            {"Kain's Lance(Taming Pole)", "wea_fan_023"},
            {"Kain's Lance(Gae Bolg)", "wea_fan_024"},
            {"Airwing", "wea_hop_001"},
            {"Skycutter", "wea_hop_002"},
            {"Hawkeye", "wea_hop_003"},
            {"Eagletalon", "wea_hop_004"},
            {"Otshirvani", "wea_hop_005"},
            {"Urubutsin", "wea_hop_006"},
            {"Ninurta", "wea_hop_007"},
            {"Jatayu", "wea_hop_008"},
            {"Vidofnir", "wea_hop_009"},
            {"Hresvelgr", "wea_hop_010"},
            {"Simurgh", "wea_hop_011"},
            {"Tezcatlipoca", "wea_hop_012"},
            {"Malphas", "wea_hop_013"},
            {"Naberius", "wea_hop_014"},
            {"Alicanto", "wea_hop_015"},
            {"Caladrius", "wea_hop_016"},
            {"Nue(Airwing)", "wea_hop_017"},
            {"Nue(Hawkeye)", "wea_hop_018"},
            {"Nue(Otshirvani)", "wea_hop_019"},
            {"Nue(Ninurta)", "wea_hop_020"},
            {"Nue(Vidofnir)", "wea_hop_021"},
            {"Nue(Simurgh)", "wea_hop_022"},
            {"Nue(Malphas)", "wea_hop_023"},
            {"Nue(Alicanto)", "wea_hop_024"},
            {"Blazefire Saber", "wea_lig_001"},
            {"Flamberge", "wea_lig_002"},
            {"Axis Blade", "wea_lig_003"},
            {"Enkindler", "wea_lig_004"},
            {"Edged Carbine", "wea_lig_005"},
            {"Razor Carbine", "wea_lig_006"},
            {"Lifesaber", "wea_lig_007"},
            {"Peacemaker", "wea_lig_008"},
            {"Gladius", "wea_lig_009"},
            {"Helter-skelter", "wea_lig_010"},
            {"Organyx", "wea_lig_011"},
            {"Apocalypse", "wea_lig_012"},
            {"Hauteclaire", "wea_lig_013"},
            {"Durandal", "wea_lig_014"},
            {"Lionheart", "wea_lig_015"},
            {"Ultima Weapon", "wea_lig_016"},
            {"Omega Weapon(Blazefire Saber)", "wea_lig_017"},
            {"Omega Weapon(Axis Blade)", "wea_lig_018"},
            {"Omega Weapon(Edged Carbine)", "wea_lig_019"},
            {"Omega Weapon(Lifesaber)", "wea_lig_020"},
            {"Omega Weapon(Gladius)", "wea_lig_021"},
            {"Omega Weapon(Organyx)", "wea_lig_022"},
            {"Omega Weapon(Hauteclaire)", "wea_lig_023"},
            {"Omega Weapon(Lionheart)", "wea_lig_024"},
            {"Vega 42s", "wea_saz_001"},
            {"Altairs", "wea_saz_002"},
            {"Spica Defenders", "wea_saz_003"},
            {"Sirius Sidearms", "wea_saz_004"},
            {"Deneb Duellers", "wea_saz_005"},
            {"Canopus AMPs", "wea_saz_006"},
            {"Rigels", "wea_saz_007"},
            {"Polaris Specials", "wea_saz_008"},
            {"Aldebarabs", "wea_saz_009"},
            {"Sadalmeliks", "wea_saz_010"},
            {"Pleiades Hi-Powers", "wea_saz_011"},
            {"Hyades Magnums", "wea_saz_012"},
            {"Antares Deluxes", "wea_saz_013"},
            {"Fomalhaut Elites", "wea_saz_014"},
            {"Procyons", "wea_saz_015"},
            {"Betelgeuse Customs", "wea_saz_016"},
            {"Total Eclipse(Vega 42s)", "wea_saz_017"},
            {"Total Eclipse(Spica Defenders)", "wea_saz_018"},
            {"Total Eclipse(Deneb Duellers)", "wea_saz_019"},
            {"Total Eclipse(Rigels)", "wea_saz_020"},
            {"Total Eclipse(Aldebarabs)", "wea_saz_021"},
            {"Total Eclipse(Pleiades Hi-Powers)", "wea_saz_022"},
            {"Total Eclipse(Antares Deluxes)", "wea_saz_023"},
            {"Total Eclipse(Procyons)", "wea_saz_024"},
            {"Wild Bear", "wea_sno_001"},
            {"Feral Pride", "wea_sno_002"},
            {"Paladin", "wea_sno_003"},
            {"Winged Saint", "wea_sno_004"},
            {"Rebel Heart", "wea_sno_005"},
            {"Warrior's Emblem", "wea_sno_006"},
            {"Power Circle", "wea_sno_007"},
            {"Battle Standard", "wea_sno_008"},
            {"Feymark", "wea_sno_009"},
            {"Soul Blazer", "wea_sno_010"},
            {"Sacrificial Circle", "wea_sno_011"},
            {"Indomitus", "wea_sno_012"},
            {"Unsetting Sun", "wea_sno_013"},
            {"Midnight Sun", "wea_sno_014"},
            {"Umbra", "wea_sno_015"},
            {"Solaris", "wea_sno_016"},
            {"Save the Queen(Wild Bear)", "wea_sno_017"},
            {"Save the Queen(Paladin)", "wea_sno_018"},
            {"Save the Queen(Rebel Heart)", "wea_sno_019"},
            {"Save the Queen(Power Circle)", "wea_sno_020"},
            {"Save the Queen(Feymark)", "wea_sno_021"},
            {"Save the Queen(Sacrificial Circle)", "wea_sno_022"},
            {"Save the Queen(Unsetting Sun)", "wea_sno_023"},
            {"Save the Queen(Umbra)", "wea_sno_024"},
            {"Binding Rod", "wea_van_001"},
            {"Hunter's Rod", "wea_van_002"},
            {"Tigerclaw", "wea_van_003"},
            {"Wyrmfang", "wea_van_004"},
            {"Healer's Staff", "wea_van_005"},
            {"Physician's Staff", "wea_van_006"},
            {"Pearlwing Staff", "wea_van_007"},
            {"Brightwing Staff", "wea_van_008"},
            {"Rod of Thorns", "wea_van_009"},
            {"Orochi Rod", "wea_van_010"},
            {"Mistilteinn", "wea_van_011"},
            {"Erinye's Cane", "wea_van_012"},
            {"Belladonna Wand", "wea_van_013"},
            {"Malboro Wand", "wea_van_014"},
            {"Heavenly Axis", "wea_van_015"},
            {"Abraxas", "wea_van_016"},
            {"Nirvana(Binding Rod)", "wea_van_017"},
            {"Nirvana(Tigerclaw)", "wea_van_018"},
            {"Nirvana(Healer's Staff)", "wea_van_019"},
            {"Nirvana(Pearlwing Staff)", "wea_van_020"},
            {"Nirvana(Rod of Thorns)", "wea_van_021"},
            {"Nirvana(Mistilteinn)", "wea_van_022"},
            {"Nirvana(Belladonna Wand)", "wea_van_023"},
            {"Nirvana(Heavenly Axis)", "wea_van_024"}
        }

        WepName.Items.Add(weaponComboBox.Text)

        Dim s As String

        s = flippedWeaponDictionary.Item(weaponComboBox.Text)

        Dim wephex = StringToHex(s)

        WepBox.Items.Add(wephex)
        WepInformation.Items.Add("0000000000000100000000000000000000")

    End Sub

    Private Sub AddAccessory_Click(sender As Object, e As EventArgs) Handles addAccessory.Click
        Dim flippedAccessoryDictionary As New Dictionary(Of String, String) From {
            {"Iron Bangle", "acc_000_000"},
            {"Silver Bangle", "acc_000_001"},
            {"Tungsten Bangle", "acc_000_002"},
            {"Titanium Bangle", "acc_000_003"},
            {"Gold Bangle", "acc_000_004"},
            {"Mythril Bangle", "acc_000_005"},
            {"Platinum Bangle", "acc_000_006"},
            {"Diamond Bangle", "acc_000_007"},
            {"Adamant Bangle", "acc_000_008"},
            {"Wurtzite Bangle", "acc_000_009"},
            {"Power Wristband", "acc_000_100"},
            {"Brawler's Wristband", "acc_000_101"},
            {"Warrior's Wristband", "acc_000_102"},
            {"Power Glove", "acc_000_103"},
            {"Kaiser Knuckles", "acc_000_104"},
            {"Magician's Mark", "acc_000_200"},
            {"Shaman's Mark", "acc_000_201"},
            {"Sorcerer's Mark", "acc_000_202"},
            {"Weirding Glyph", "acc_000_203"},
            {"Magistral Crest", "acc_000_204"},
            {"Black Belt", "acc_000_300"},
            {"General's Belt", "acc_000_301"},
            {"Champion's Belt", "acc_000_302"},
            {"Rune Bracelet", "acc_000_400"},
            {"Witch's Bracelet", "acc_000_401"},
            {"Magus's Bracelet", "acc_000_402"},
            {"Royal Armlet", "acc_000_500"},
            {"Imperial Armlet", "acc_000_501"},
            {"Ember Ring", "acc_001_000"},
            {"Blaze Ring", "acc_001_001"},
            {"Salamandrine Ring", "acc_001_002"},
            {"Frost Ring", "acc_002_000"},
            {"Icicle Ring", "acc_002_001"},
            {"Boreal Ring", "acc_002_002"},
            {"Spark Ring", "acc_003_000"},
            {"Fulmen Ring", "acc_003_001"},
            {"Raijin Ring", "acc_003_002"},
            {"Aqua Ring", "acc_004_000"},
            {"Riptide Ring", "acc_004_001"},
            {"Nereid Ring", "acc_004_002"},
            {"Zephyr Ring", "acc_005_000"},
            {"Gale Ring", "acc_005_001"},
            {"Sylphid Ring", "acc_005_002"},
            {"Clay Ring", "acc_006_000"},
            {"Siltstone Ring", "acc_006_001"},
            {"Gaian Ring", "acc_006_002"},
            {"Entite Ring", "acc_007_000"},
            {"Giant's Glove", "acc_009_000"},
            {"Warlord's Glove", "acc_009_001"},
            {"Glass Buckle", "acc_010_000"},
            {"Tektite Buckle", "acc_010_001"},
            {"Metal Armband", "acc_011_000"},
            {"Ceramic Armband", "acc_011_001"},
            {"Serenity Sachet", "acc_012_000"},
            {"Safeguard Sachet", "acc_012_001"},
            {"Glass Orb", "acc_013_000"},
            {"Dragonfly Orb", "acc_013_001"},
            {"Star Pendant", "acc_014_000"},
            {"Starfall Pendant", "acc_014_001"},
            {"Pearl Necklace", "acc_015_000"},
            {"Gemstone Necklace", "acc_015_001"},
            {"Warding Talisman", "acc_016_000"},
            {"Hexbane Talisman", "acc_016_001"},
            {"Pain Dampener", "acc_017_000"},
            {"Pain Deflector", "acc_017_001"},
            {"White Cape", "acc_018_000"},
            {"Effulgent Cape", "acc_018_001"},
            {"Rainbow Anklet", "acc_019_000"},
            {"Moonbow Anklet", "acc_019_001"},
            {"Cherub's Crown", "acc_020_000"},
            {"Seraph's Crown", "acc_020_001"},
            {"Ribbon", "acc_023_000"},
            {"Supper Ribbon", "acc_023_001"},
            {"Guardian Amulet", "acc_025_000"},
            {"Shield Talisman", "acc_025_001"},
            {"Auric Amulet", "acc_026_000"},
            {"Soulfont Talisman", "acc_026_001"},
            {"Watchman's Amulet", "acc_027_000"},
            {"Shrouding Talisman", "acc_027_001"},
            {"Hero's Amulet", "acc_028_000"},
            {"Morale Talisman", "acc_028_001"},
            {"Saint's Amulet", "acc_029_000"},
            {"Blessed Talisman", "acc_029_001"},
            {"Hermes Sandals", "acc_030_000"},
            {"Sprint Shoes", "acc_030_001"},
            {"Flamebane Brooch", "acc_031_000"},
            {"Flameshield Talisman", "acc_031_001"},
            {"Frostbane Brooch", "acc_032_000"},
            {"Frostshield Talisman", "acc_032_001"},
            {"Sparkbane Brooch", "acc_033_000"},
            {"Sparkshield Talisman", "acc_033_001"},
            {"Aquabane Brooch", "acc_034_000"},
            {"Aquashield Talisman", "acc_034_001"},
            {"Zealot's Amulet", "acc_035_000"},
            {"Battle Talisman", "acc_035_001"},
            {"Tetradic Crown", "acc_036_000"},
            {"Tetradic Tiara", "acc_036_001"},
            {"Whistlewind Scarf", "acc_037_000"},
            {"Aurora Scarf", "acc_037_001"},
            {"Nimbletoe Boots", "acc_038_000"},
            {"Collector Catalog", "acc_039_000"},
            {"Connoisseur Catalog", "acc_039_001"},
            {"Gold Watch", "acc_040_000"},
            {"Champion's Badge", "acc_040_001"},
            {"Survivalist Catalog", "acc_040_002"},
            {"Hunter's Friend", "acc_041_000"},
            {"Speed Sash", "acc_041_001"},
            {"Energy Sash", "acc_041_002"},
            {"Genji Glove", "acc_042_001"},
            {"Growth Egg", "acc_045_000"},
            {"Twenty-sided Die", "acc_046_000"},
            {"Fire Charm", "acc_047_000"},
            {"Ice Charm", "acc_048_000"},
            {"Lightning Charm", "acc_049_000"},
            {"Water Charm", "acc_050_000"},
            {"Wind Charm", "acc_051_000"},
            {"Earth Charm", "acc_052_000"},
            {"Doctor's Code", "acc_053_000"},
            {"Goddess's Favour", "acc_054_000"}
        }


        AccName.Items.Add(accessoryComboBox.Text)

        Dim s As String

        s = flippedAccessoryDictionary.Item(accessoryComboBox.Text)

        Dim accHex = StringToHex(s)

        AccBox.Items.Add(accHex)
        AccInformation.Items.Add("0000000000000100000000000000000000")
    End Sub

    Private Sub WepAccSave_Click(sender As Object, e As EventArgs) Handles wepAccSave.Click


        Dim inventoryOffset As Long = 284176 ' Sets offset for the overall item inventory
        Dim weaponInventoryOffset As Long = 227668 ' Sets offset where it points to weapon locations in the inventory
        Dim accessoryInventoryOffset As Long = 232472 ' Sets offset where it points to weapon locations in the inventory

        Dim filename As String = OpenFileDialog1.FileName ' Sets filename as string

        Dim fs As New FileStream(filename, FileMode.Open) ' Opens the file and begins streaming

        Dim br As New BinaryReader(fs) ' BinaryReader accesses File

        Dim Bytes As Byte()





        Dim weaponCounter = 0
        Dim itemNumber = 0

        While weaponCounter < WepBox.Items.Count

            fs.Seek(inventoryOffset, SeekOrigin.Begin)



            Dim weaponString = WepBox.Items.Item(weaponCounter) + WepInformation.Items.Item(weaponCounter)

            Dim Length As Integer = weaponString.Length
            Dim upperBound As Integer = Length \ 2
            If Length Mod 2 = 0 Then
                upperBound -= 1
            Else
                weaponString = "0" & weaponString

            End If

            ReDim Bytes(upperBound)
            For i As Integer = 0 To upperBound
                Bytes(i) = Convert.ToByte(weaponString.Substring(i * 2, 2), 16)
            Next

            fs.Write(Bytes, 0, Bytes.Length)

            inventoryOffset += 28

            '''
            ''' CODE FOR POINTING TO WEAPONS GOES HERE
            '''

            Dim pointerString1 = "0000000_00000"
            Dim pointerMiddle = Hex(itemNumber + 1)
            If pointerMiddle.Length < 4 Then
                pointerMiddle = "00" & pointerMiddle
            End If
            Dim pointerString2 = "000000000001"


            Dim completePointerString As String = (StringToHex(pointerString1) & pointerMiddle & pointerString2)

            weaponCounter += 1
            itemNumber += 1

        End While

        Dim accessoryCounter = 0

        While accessoryCounter < WepBox.Items.Count

            fs.Seek(inventoryOffset, SeekOrigin.Begin)


            Dim accessoryString = AccBox.Items.Item(accessoryCounter) + AccInformation.Items.Item(accessoryCounter)

            Dim Length As Integer = accessoryString.Length
            Dim upperBound As Integer = Length \ 2
            If Length Mod 2 = 0 Then
                upperBound -= 1
            Else
                accessoryString = "0" & accessoryString

            End If

            ReDim Bytes(upperBound)
            For i As Integer = 0 To upperBound
                Bytes(i) = Convert.ToByte(accessoryString.Substring(i * 2, 2), 16)
            Next

            fs.Write(Bytes, 0, Bytes.Length)

            inventoryOffset += 28

            '''
            ''' CODE FOR POINTING TO ACCESSORIES GOES HERE
            '''

            Dim pointerString1 = "303030303030305F3030303030"
            Dim pointerMiddle = Hex(itemNumber + 1)
            If pointerMiddle.Length < 4 Then
                pointerMiddle = "0" & pointerMiddle
            End If
            Dim pointerString2 = "0000000001"

            Dim completePointerString = pointerString1 & pointerMiddle & pointerString2

            accessoryCounter += 1
            itemNumber += 1

        End While


    End Sub
End Class
