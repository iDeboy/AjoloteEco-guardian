@echo off
chcp 65001 > nul

:: Crear y cambiar a la nueva rama BETA
::git checkout -b master

:: Agregar todos los archivos y hacer un commit inicial
git add .
git commit -m "Actualizacion de niveles y  puntaje final"

:: Subir la rama BETA al repositorio remoto
git push -u origin master

:: Pausa para mostrar mensajes en consola
pause
