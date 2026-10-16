# Bitácora y reflexiones — Carolina Lohrmann, integrante 1

Fecha del registro: 15 de octubre de 2026. Usuario Git de los aportes personales: `carolinalohrmann`.

Este registro documenta las acciones realizadas con asistencia durante la sesión y las capturas aportadas por Carolina. Las prácticas de los casos 02–04 se ejecutaron en copias locales separadas, sin publicar sus ramas ni integrar esos ejercicios en Dev. El script del caso 03 fue preparado con asistencia; las operaciones de Source Control y las capturas fueron realizadas por Carolina. No se atribuyen a Carolina commits anteriores de otros integrantes.

## Seguimiento

| Caso | Problema | ¿Provocado? | ¿Resuelto? | Evidencia | Pendiente |
|---|---|---|---|---|---|
| 01 | Selección de archivos del proyecto e inicio del repositorio. | No se reprodujo el inicio; el repositorio ya existía. | Revisión parcial de la base existente. | [Repositorio en GitHub](../Capturas/01-04-repositorio-GitHub.png). | Evidencia original anterior al .gitignore, captura del .gitignore y primer commit del equipo, estado limpio y validación de Unity. |
| 02 | Feature creada desde main en lugar de Dev. | Sí, en Caso02_Ramas. | Sí, localmente: recreada desde Dev. | [Origen equivocado](../Capturas/02-01-origen-equivocado.png), [corregido](../Capturas/02-02-origen-corregido.png), [ramas remotas](../Capturas/02-03-ramas-GitHub.png). | Capturas de los demás integrantes en sus Features. La captura remota no demuestra que se haya publicado la recreación local; conviene repetirla con URL visible. |
| 03 | Commit nuevo realizado deliberadamente en Dev. | Sí, en Caso03_Commit. | Sí, localmente: conservado en la Feature y retirado de Dev. | [Cambio](../Capturas/03-00-cambio-en-Dev.png), [commit equivocado](../Capturas/03-01-commit-en-Dev.png), [conservado](../Capturas/03-02-commit-conservado.png), [Dev corregida](../Capturas/03-03-Dev-corregida.png), [historial final](../Capturas/03-04-historial-final.png). | Publicación y validación de Unity de esta copia actual. La imagen de Unity aportada es anterior y no identifica esta ejecución. |
| 04 | Cambio local que impide cambiar de rama y conflicto al aplicar stash en destino. | Sí, en Caso04_Stash. | Sí, localmente: cambio recuperado en Feature_Interaction. | [Cambio](../Capturas/04-01-cambio-sin-commit.png), [advertencia](../Capturas/04-02-advertencia.png), [stash](../Capturas/04-03-stash-guardado.png), [recuperación](../Capturas/04-04-trabajo-recuperado.png), [conflicto adicional](../Capturas/04-extra-conflicto-stash-en-destino.png). | Verificación con el responsable de interacción si se exige el reparto de roles del PDF. |
| 05 | Push rechazado por avances remotos. | No acreditado con estas capturas. | Pendiente de comprobar con Kevin y el equipo. | No se aportaron capturas del caso 05 en esta carpeta. | Revisar la ejecución del equipo y registrar participación, hashes, rechazo, actualización, Push y resultado propios. |

## Caso 01 — Revisión del repositorio existente

La captura de GitHub muestra `kevinillanes/EscapeRoom3D`, la URL, main, Assets, Packages, ProjectSettings, .gitignore y README con los integrantes. El commit inicial existente es `c8a1e9b`, autor `kevinillanes`, mensaje `Initial commit`. No se presenta como un primer commit realizado por Carolina ni como el mensaje exacto requerido por el PDF.

La comprobación local de la preparación encontró reglas para Library, Temp, obj, Builds y Logs, y no encontró esas carpetas entre los archivos versionados. Esa comprobación no reemplaza la captura del problema original ni la prueba del editor.

**26. ¿Por qué no conviene subir Library/?** Unity puede reconstruir sus cachés; versionarlas aumenta el tamaño del repositorio y añade cambios innecesarios entre computadoras.

**27. ¿Archivo ignorado y archivo seguido?** .gitignore evita normalmente añadir archivos no versionados que coincidan con sus reglas. No deja de seguir automáticamente un archivo ya versionado ni borra su historia.

**28. ¿Qué representa un commit?** Una instantánea de los archivos versionados con mensaje, autor, fechas y relación con sus commits padres, identificada mediante un hash.

**29. ¿Para qué sirve el remoto?** Para compartir el historial y coordinar aportes y revisiones del equipo desde distintas computadoras.

## Caso 02 — Origen de la Feature

Se creó `Feature_PlayerMovement` desde main en `ba6e8a7`. Tras capturar el origen incorrecto y comprobar que no tenía commits propios, se cambió a Dev, se eliminó la Feature y se recreó desde Dev en `6ccd160`. Las capturas muestran ambos estados reales de la práctica local.

**45. ¿Desde qué commit nació tu Feature?** En esta práctica, primero desde `ba6e8a7` de main y, después de corregirla, desde `6ccd160` de Dev. Esto no cambia el origen histórico de la Feature publicada.

**46. ¿Por qué no desde main?** Dev contiene los avances de desarrollo que la Feature necesita como base. Partir de una main atrasada puede omitir trabajo e incrementar las dificultades de integración.

## Caso 03 — Conservar el commit y corregir Dev

La base ya contenía movimiento WASD. Se adaptó el ejercicio añadiendo flechas, sin presentar el script existente como creado desde cero. El cambio se confirmó en Dev mediante Source Control con el mensaje `feat: add player movement`, hash `65ee4d2` y autor `carolinalohrmann`.

Se creó `Feature_PlayerMovement` desde ese commit para conservarlo. Después se volvió a Dev, se usó Undo Last Commit, se quitó PlayerController de Stage y se descartó únicamente ese cambio en Dev. Dev volvió a `6ccd160`; la Feature conservó `65ee4d2`, con el área de cambios limpia.

[Imagen de Unity aportada](../Capturas/03-05-prueba-Unity.png): muestra el proyecto `EscapeRoom3D-Dev` en Play y el script con WASD/flechas. Es evidencia anterior de apoyo; aunque su nombre diga prueba, no demuestra que se ejecutara la copia Caso03_Commit después del commit `65ee4d2`, ni muestra Console abierta.

**63. ¿Qué significa que una rama apunte a un commit?** La rama es una referencia con nombre a ese commit. Al conservar esa referencia, el commit y su historia siguen accesibles aunque otra rama retroceda.

**64. ¿Por qué no copiar y borrar archivos entre ramas?** Eso no conserva ni traslada el commit original ni corrige las referencias. Es mejor preservar el commit en una rama y corregir la rama equivocada según si el trabajo ya fue compartido.

## Caso 04 — Stash y recuperación

En `Feature_Interaction` se cambió el aviso de PlayerInteraction por `CASO04 trabajo pendiente sin commit.` y se guardó sin commit. Al intentar cambiar a `Practica/StashDestino`, VS Code advirtió que se sobrescribirían cambios; se capturó y canceló el cambio.

Se guardó inicialmente un stash con nombre automático y se recuperó. Después se guardó nuevamente el cambio con el nombre requerido `WIP interaction`, conservando también el stash anterior.

Al aplicar el stash estando en `Practica/StashDestino` surgió un conflicto real en el mismo aviso: el destino contenía `CASO04 destino de practica.`. Se revisaron ambas versiones y se conservó el contenido del destino para cancelar esa aplicación, manteniendo el trabajo a salvo en el stash. Tras guardar y marcar la resolución, la rama destino quedó limpia sin crear un commit.

Finalmente se volvió a `Feature_Interaction` y se aplicó `WIP interaction`. El texto original del trabajo volvió a aparecer como cambio local, sin conflictos. Se conservan los stashes y el cambio recuperado sin commit.

**82. ¿Qué problema resuelve Stash?** Permite guardar cambios locales temporalmente y cambiar de contexto sin crear un commit incompleto.

**83. ¿Por qué no reemplaza a Commit?** El stash es temporal y local y no se publica mediante un Push normal de la rama. El commit forma parte del historial que se puede compartir e integrar.

**84. ¿Qué ocurre si la rama cambió?** Pueden surgir conflictos al recuperar el trabajo sobre contenido incompatible. En esta práctica ocurrió al aplicarlo en la rama destino; se resolvió sin borrar el stash y se recuperó después en la Feature correcta.

## Caso 05 — Pendiente de evidencia individual

Se recibió información de que el equipo cerró el caso 05, pero estas capturas no permiten verificar la participación individual ni los hashes de esa ejecución. No se considera completado por los registros automáticos antiguos incluidos en la preparación.

**100. ¿Fetch y Pull?** Fetch descarga información sin integrar cambios en la rama activa. Pull descarga e integra, mediante merge o rebase según la configuración.

**101. ¿Por qué se rechaza un Push no fast-forward?** Porque la nueva punta propuesta dejaría fuera commits que ya existen en la rama remota; primero se deben revisar e integrar los avances.

**102. ¿Riesgo de sobrescribir automáticamente?** Los aportes de otros podrían desaparecer de la historia visible de la rama y las copias del equipo quedarían descoordinadas.

## Relación con los documentos anteriores

Los archivos antiguos de este laboratorio se conservan como antecedentes de preparación. Sus commits y registros no sustituyen las actividades y evidencias personales descritas aquí. Este documento es el registro actualizado de la sesión; no certifica casos pendientes ni las actividades de compañeros.
