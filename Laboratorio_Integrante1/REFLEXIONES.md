# Reflexiones base para revisar

Estas respuestas explican los conceptos. Revísalas y exprésalas con tus palabras; no describen experiencias personales que no hayas realizado.

## Caso 02

**¿Desde qué commit nació la Feature de práctica?** Primero nació del commit de main. Después se recreó desde Dev, en 6ccd160. La Feature principal existente se conservó y recibió Dev mediante merge.

**¿Por qué una Feature debe partir de Dev?** Porque Dev reúne los avances de integración del equipo. Partir de main cuando está atrasada deja fuera esa base de trabajo.

## Caso 03

**¿Qué significa que una rama apunte a un commit?** La rama guarda una referencia a ese commit. Sus padres permiten recorrer la historia que lo precede. Cambiar la referencia local no elimina automáticamente los commits que otra rama conserva.

**¿Por qué copiar el archivo y borrarlo no corrige el commit?** Porque mover archivos cambia contenido, pero no traslada ni conserva por sí mismo el commit en la rama correcta. Hay que revisar las referencias y la historia, además de los archivos.

**¿Qué se hizo en esta preparación?** El commit local 44492f6 se conservó en Feature_PlayerMovement y se devolvió Dev a su commit previo. Nunca se publicó ese commit en Dev. Si ya hubiera sido compartido, se debería corregir sin reescribir la historia compartida, por ejemplo con Revert.

## Caso 04

**¿Qué resuelve Stash?** Guarda temporalmente cambios pendientes para dejar limpio el árbol de trabajo y poder cambiar de contexto.

**¿Por qué no reemplaza a Commit?** Porque es un guardado temporal local. No registra por sí mismo un avance en la historia de una rama para compartirlo mediante Push.

**¿Qué puede pasar al aplicar un stash sobre una rama que cambió?** Los cambios pueden entrar limpiamente o generar conflictos que se deben revisar y resolver. Antes de borrar el stash hay que comprobar que el trabajo se recuperó.

## Caso 05

**¿Cuál es la diferencia entre Fetch y Pull?** Fetch descarga objetos y actualiza referencias del remoto sin integrarlos en los archivos actuales. Pull descarga e integra el avance en la rama actual, según su configuración de merge o rebase.

**¿Por qué se rechaza un Push no fast-forward?** Porque el remoto contiene commits que la historia local que se intenta publicar todavía no incluye. Primero se debe integrar ese trabajo.

**¿Qué riesgo habría al sobrescribir automáticamente el remoto?** Podrían retirarse aportes de compañeros de la historia visible de la rama. El rechazo protege frente a esa pérdida accidental de integración.
