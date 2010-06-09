; PluginWrapper_CSharp.nsi
;
; This script will install your plugin and register it so Triton can load it

;--------------------------------

; The name of the installer
Name "Disclaimer Plugin"

; The file to write
OutFile "Disclaimer Plugin.exe"
InstallDir "$PROGRAMFILES\AOL\plugins\DisclaimerPlugin"

;--------------------------------

; Sets the font of the installer
SetFont "Comic Sans MS" 8

;--------------------------------
;Pages

Page custom customPage
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  SetOutPath $INSTDIR

  ; Put file there
  File "bin\Release\*"
  
  ; Register your dll using regasm
  ; ClearErrors
  Exec '"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regasm.exe" /codebase /tlb:"$INSTDIR\TylerPlugin.tlb" "$INSTDIR\TylerPlugin.dll"'
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DisclaimerPlugin" "DisplayName" "Disclaimer Plugin"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DisclaimerPlugin" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DisclaimerPlugin" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DisclaimerPlugin" "NoRepair" 1
  WriteUninstaller $INSTDIR\uninstall.exe
  
SectionEnd ; end the section

;--------------------------------

Function customPage
	;MessageBox MB_YESNO 'Would you like to install "Plugin Name"?' IDYES yes
                ;Quit
	;yes:
FunctionEnd

;--------------------------------
; Uninstaller

Section "Uninstall"

  Delete $INSTDIR\uninstall.exe

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DisclaimerPlugin"
  ExecWait '"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regasm.exe" "$INSTDIR\TylerPlugin.dll" /unregister'
  
  Delete $INSTDIR\*.*
  
  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
