@echo off
chcp 65001 > nul

:: Crear y cambiar a la nueva rama BETA
git checkout -b BETA

:: Agregar todos los archivos y hacer un commit inicial
git add .
git commit -m "Primer commit en la rama BETA: Correcciones y nuevas funcionalidades."

:: Subir la rama BETA al repositorio remoto
git push -u origin BETA

:: Pausa para mostrar mensajes en consola
pause
