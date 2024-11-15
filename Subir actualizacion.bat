@echo off
chcp 65001 > nul
git add .
git commit -m "Se agrego menu de pausa, musica de fondo para cada escena, cada boton manda a donde debe y se añadio boton de regresar"
git push origin master
pause
