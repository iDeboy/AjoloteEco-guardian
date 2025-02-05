@echo off
chcp 65001 > nul

:: Crear y cambiar a la nueva rama BETA
git checkout -b Movil

:: Agregar todos los archivos y hacer un commit inicial
git add .
git commit -m "Optimizacion en dispositivos mobiles asi como el añadir controles basicos de movimiento, falta pausa y mejorar diseño de los componentes"

:: Subir la rama BETA al repositorio remoto
git push -u origin 

:: Pausa para mostrar mensajes en consola
pause
