; List Setup.iss

; (See 'Help - Inno Setup Documentation' for Script format.)

;     Constants
; {src} - The directory in which the Setup files are located
; {app} - Application destination location. (e.g. 'C:\Program Files\progName')
; {pf} - Program Files directory.
; {group} - The path to the Start Menu folder

; Setup section
; SourceDir - Location of source (.exe, Readme) files. (relative to .iss location.)
; OutputBaseFilename - Name of setup (.exe) file.
[Setup]
AppName=List
AppVersion=1.0
DefaultDirName={pf}\List
DefaultGroupName=List
SourceDir="..\List\bin\Release"
; If you set 'SourceDir', you must force 'OutputDir' to be where you want it.
OutputDir="..\..\..\Setup\Output"
OutputBaseFilename="List_Setup"

; [Types]  Type of installation. (e.g. "full", "custom", etc.)

; Files section (i.e. the .exe file)
[Files]
Source: "List.exe"; DestDir: "{app}"

; Icons section - Defines shortcuts to be created.
[Icons]
; (NO shortcuts are created by default. You need this.)
; 'Comment:' parameter is tooltip for icon.
; ({userdesktop} doesn't work!)
;Name: "{userdesktop}\List"; Filename: "{app}\List.exe"; Tasks: "desktopicon"; Comment: "Comment Comment Comment."
Name: "{commondesktop}\List"; Filename: "{app}\List.exe"; Tasks: "desktopicon"; Comment: "List content of any type of file."
Name: "{group}\List"; Filename: "{app}\List.exe"; Comment: "List content of any type of file."
Name: "{group}\Uninstall List"; Filename: "{uninstallexe}"

; Tasks section - Defines user-customizable tasks
[Tasks]
Name: "desktopicon"; Description: "Create a desktop icon"

; Run section
[Run]
;Filename: "notepad.exe"; Parameters: "{app}\List.exe.config"; Flags: shellexec postinstall; Description: "Edit config file."

; End of Script