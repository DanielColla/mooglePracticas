#!/bin/bash

# directorios
DIR_INFORME="informe"
DIR_PRESENTACION="presentacion"

# opciones
OPCIONES="run|clean|report|slides|show_report|show_slides"

# programas para visualizar los PDF (pueden variar según el sistema operativo)
VISOR_POR_DEFECTO="evince"    # Linux
VISOR_WINDOWS="start"         # Windows

# función para ejecutar el proyecto
run() {
  echo "Ejecutando el proyecto..."
  # comando para ejecutar el proyecto
}

# función para compilar el informe en LaTeX
report() {
  echo "Compilando el informe..."
  cd $DIR_INFORME
  pdflatex informe.tex
  cd ..
}

# función para compilar la presentación en LaTeX
slides() {
  echo "Compilando la presentación..."
  cd $DIR_PRESENTACION
  pdflatex presentacion.tex
  cd ..
}

# función para visualizar el informe en PDF
show_report() {
  echo "Visualizando el informe..."
  cd $DIR_INFORME
  if [ ! -f informe.pdf ]; then
    pdflatex informe.tex
  fi
  if command -v $VISOR_POR_DEFECTO >/dev/null 2>&1; then
    $VISOR_POR_DEFECTO informe.pdf
  elif command -v $VISOR_WINDOWS >/dev/null 2>&1; then
    $VISOR_WINDOWS informe.pdf
  else
    echo "No se pudo encontrar un visor de PDFs. Por favor, instale uno."
  fi
  cd ..
}

# función para visualizar la presentación en PDF
show_slides() {
  echo "Visualizando la presentación..."
  cd $DIR_PRESENTACION
  if [ ! -f presentacion.pdf ]; then
    pdflatex presentacion.tex
  fi
  if command -v $VISOR_POR_DEFECTO >/dev/null 2>&1; then
    $VISOR_POR_DEFECTO presentacion.pdf
  elif command -v $VISOR_WINDOWS >/dev/null 2>&1; then
    $VISOR_WINDOWS presentacion.pdf
  else
    echo "No se pudo encontrar un visor de PDFs. Por favor, instale uno."
  fi
  cd ..
}

# función para eliminar archivos auxiliares generados por la compilación o ejecución del proyecto
clean() {
  echo "Limpiando archivos auxiliares..."
  # comando para eliminar los archivos auxiliares
  find . -type f -name "*.aux" -delete
  find . -type f -name "*.log" -delete
  find . -type f -name "*.out" -delete
  find . -type f -name "*.toc" -delete
  find . -type f -name "*.nav" -delete
  find . -type f -name "*.snm" -delete
  find . -type f -name "*.vrb" -delete
  find . -type f -name "*.fdb_latexmk" -delete
  find . -type f -name "*.fls" -delete
}

# comprobar los argumentos del script y llamar a la función correspondiente
if [ $# -eq 0 ]; then
  echo "Uso: proyecto.sh [$OPCIONES]"
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
    echo "Uso: proyecto.sh [$OPCIONES]"
    exit 1
    ;;
esac