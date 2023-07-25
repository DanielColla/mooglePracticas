#!/bin/bash

run() {
  # comando para ejecutar el proyecto
  echo "Ejecutando el proyecto..."
}

report() {
  # comando para compilar el informe en LaTeX
  echo "Compilando el informe..."
}

slides() {
  # comando para compilar la presentación en LaTeX
  echo "Compilando la presentación..."
}

show_report() {
  # comando para visualizar el informe en PDF
  # si el archivo PDF no existe, compilarlo primero
  # el comando 'evince' es para visualizar el PDF en Linux, puedes usar otro en tu sistema operativo
  echo "Visualizando el informe..."
  evince informe/informe.pdf
}

show_slides() {
  # comando para visualizar la presentación en PDF
  # si el archivo PDF no existe, compilarlo primero
  # el comando 'evince' es para visualizar el PDF en Linux, puedes usar otro en tu sistema operativo
  echo "Visualizando la presentación..."
  evince presentación/presentación.pdf
}

clean() {
  # comando para eliminar archivos auxiliares generados por la compilación o ejecución del proyecto
  echo "Limpiando archivos auxiliares..."
}

# comprobar los argumentos del script y llamar a la función correspondiente
if [ $# -eq 0 ]; then
  echo "Uso: proyecto.sh [run|report|slides|show_report|show_slides|clean]"
  exit 1
fi

case "$1" in
  run)
    run
    ;;
  report)
    report
    ;;
  slides)
    slides
    ;;
  show_report)
    show_report
    ;;
  show_slides)
    show_slides
    ;;
  clean)
    clean
    ;;
  *)
    echo "Opción inválida: $1"
    echo "Uso: proyecto.sh [run|report|slides|show_report|show_slides|clean]"
    exit 1
    ;;
esac