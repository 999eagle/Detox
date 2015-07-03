@echo off
echo Copying skins

set CONTENT_DIR="%1\Detox\bin\x86\%2\DetoxContent\Skins"
set SKIN_DIR="%1\NeoforceControls\Skins"
set SKINS=Default,Green,Blue,Magenta,Purple

for %%A in (%SKINS%) do copy "%SKIN_DIR%\%%A.skin" "%CONTENT_DIR%\%%A.skin"