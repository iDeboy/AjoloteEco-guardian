@echo off
chcp 65001 > nul

:: Crear y cambiar a la nueva rama BETA
::git checkout -b master

:: Agregar todos los archivos y hacer un commit inicial
git add .
git commit -m "Actualizacion de niveles, se completo nivel 2, se añadio coco gracias a chat gpt funciona con el, falta estableer el tiempo por nivel y la cantidad de objetos a generar en cada uno"

:: Subir la rama BETA al repositorio remoto
git push -u origin master

:: Pausa para mostrar mensajes en consola
pause
