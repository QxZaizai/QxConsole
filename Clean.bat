@ECHO OFF

ECHO QxConsole Build Directories Clear (By QxZaizai)
ECHO Press any key to clean build directories
PAUSE>NUL
CLS

FOR /D %%I IN (QxConsole.*) DO (
	ECHO Deleting %%I\bin
	RMDIR /S /Q "%%I\bin"
	ECHO Deleting %%I\obj
	RMDIR /S /Q "%%I\obj"
)
PAUSE