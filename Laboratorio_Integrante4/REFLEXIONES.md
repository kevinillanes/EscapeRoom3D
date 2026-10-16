# Reflexiones — Casos 16 a 20

## Caso 16

**¿Por qué Accept Both puede romper el juego aunque el archivo compile?**  
Porque Git combina texto, no entiende la intención del programa. Dos líneas válidas pueden ejecutar dos veces una operación que solo debía ocurrir una vez.

**¿Qué se debe hacer después de resolver un conflicto?**  
Leer el resultado completo y probar el comportamiento en Unity. Quitar los marcadores de conflicto no garantiza que la lógica sea correcta.

## Caso 17

**¿Por qué se usó Revert?**  
El commit roto ya estaba compartido. Revert conserva ese hecho histórico y añade otro commit que invierte sus cambios, sin alterar la historia que otros compañeros pudieron descargar.

**¿Cuándo sería peligroso usar Reset?**  
Cuando el commit ya se publicó, porque mover la rama hacia atrás obligaría a reescribir el remoto y podría separar o perder trabajo de otros integrantes.

## Caso 18

**¿Por qué Reflog pudo recuperar el commit?**  
Aunque ninguna rama lo señalaba después del reset, el repositorio local todavía registraba que `HEAD` había pasado por ese hash. Reflog permitió encontrarlo y volver a crear una referencia.

**¿Qué límite tiene Reflog?**  
Es un registro local y temporal. No sustituye a una rama publicada ni a una copia remota del trabajo.

## Caso 19

**¿Qué hace Cherry-pick?**  
Aplica el cambio de un commit específico sobre la rama actual y crea un commit nuevo. Por eso el mensaje puede coincidir, pero el hash de origen y el de destino son distintos.

**¿Por qué no se hizo Merge?**  
El objetivo era trasladar solo la corrección de validación. Un merge también habría introducido las dos notas de prueba de la rama fuente.

## Caso 20

**¿Qué aporta un Pull Request?**  
Permite comparar ramas, conversar sobre líneas concretas, pedir una corrección y registrar quién revisó y aprobó el cambio antes de integrarlo.

**¿Por qué la aprobación debe ser de otra persona?**  
Una autoaprobación no demuestra revisión del equipo. El laboratorio busca evidencia de colaboración real y una segunda persona puede detectar errores que el autor pasó por alto.

