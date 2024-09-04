# Final Fantasy XIII Save Editor and Save Decryptor tools.

A save editor for the Steam and Xbox App/Windows Store version of Final Fantasy XIII.

This can only be used in combination with Final Fantasy XIII Save Decryptor, found in the Nova Crystallis Discord in #general channel. (Created by Joschka)

https://discord.gg/pc9PdpKsRc - Discord Invite
https://discord.com/channels/824468795275608136/824488395937677333/941761558537723954 - Should link to XIIIPCSaveDecryptor

DISCLAIMER - This is not tested fully and may not work as intended. I am not proficient at coding, so if you find any bugs, found that something broke or have any questions, please contact me on Discord @Kirathereaperuwu


## How to use FF13 Save Editor and XIIIDecryptor tools.

1 - Make a folder on your desktop and place the `XIIIPCSaveDecryptor.exe` and  `ff13.save.editor.exe` tools in that folder. Acquire your save from one of these locations and copy them to this folder on your desktop:

### Steam:

`$SteamFolder\userdata\<user-id>\292120\remote\` (Cloud saves enabled are stored here)
`%LOCALAPPDATA%\SquareEnix\FinalFantasyXIII\save\` (Cloud Saves disabled are stored here)
Note – You must disable cloud saves in order to have the game pick up the files. You can get the saves from either location and use them with the editor.

The save will have a file name `ff13-##.dat`, with ## being the save slot number. The following instructions will assume you are working with `ff13-01.dat` - ammend this according to the save slot you're choosing to use.

### Xbox App/Windows Store
`%LOCALAPPDATA%\Packages\39EA002F.FFXIII_n746a19ndrrjg\SystemAppData\wgs\$RandomNumbers\$RandomNumbers\`

In here you should see various files with random numbers and letters, depending on how many save slots you have used in game.
Two of the files will be either 1 or 2kb in size. The files which are are +150kb are the saves. The only way to determine which slot is which is by checking the "Date Modified".

**1. Backup**

Make another copy of your save in that same folder and add `.bak` to the end of the file name. Keep this around as a backup in case the save corrupts or causes unexpected behaviour.

*If using a save from the Xbox/Windows Store version, rename the save you're planning to mod to `ff13-01.dat` - the number doesn't matter in this case.*

**2. Decrypt**

Drag the `ff13-01.dat` file onto the XIIIPCSaveDecryptor.exe (or in Terminal, run `.\XIIIPCSaveDecryptor.exe ff13-01.dat`). This should create a `ff13-01.dat_decrypted` version of the save file.

**3. Editing**

Open `FF13-Save-Editor.exe` and open the `ff13-01.dat_decrypted` file. Edit what you want, click Save then close the editor.

**4. Re-encrypt**

Drag and drop the `ff13-01.dat_decrypted` file onto the `XIIIPCSaveDecryptor.exe` (or in Terminal, run `.\XIIIPCSaveDecryptor.exe ff13-01.dat_decrypted`). This will create a `ff13-01.dat_decrypted_encrypted` file.

**5. Use**

Rename the file `ff13-01.dat_decrypted_encrypted` to `ff13-##.dat` (Steam - with ## being the number it was. Xbox/Windows Store - rename this back to the random numbers/letters that it used to be). Place the save back inside of the folder you copied from in Step #1, and then load it in the game.
