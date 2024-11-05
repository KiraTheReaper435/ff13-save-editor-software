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
End Class
