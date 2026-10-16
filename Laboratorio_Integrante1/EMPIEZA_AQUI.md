# Integrante 1: preparación y evidencias de Git

Responsable de la parte: Carolina Lohrmann. Rama: `Feature_PlayerMovement`.
Fecha de preparación: 14 de octubre de 2026.

## Abrir esta copia

La carpeta preparada es:

`C:\Users\kevin\Downloads\EscapeRoom3D-Dev\EscapeRoom3D-Dev`

Abre esa carpeta completa en VS Code y en Unity, no solamente Assets/01_Scripts.
La antigua ruta Downloads/EscapeRoom3D no es la copia elegida para continuar.

## Qué quedó preparado

- Se conectó esta copia descargada con el historial original de GitHub.
- Los 90 archivos locales coincidían con `ff34039`; no había cambios de código únicos que sobrescribir.
- Se guardó la referencia `Respaldo/ArchivosLocales-AntesPreparacion` y un ZIP de seguridad local.
- Se conservó el punto anterior de tu Feature en `Respaldo/PlayerMovement-AntesPreparacion`.
- Se integró Dev en tu Feature, conservando el README con los nombres del equipo.
- Se añadió soporte de flechas al movimiento, manteniendo WASD y la normalización diagonal.
- Se reprodujeron y comprobaron los casos de Git detallados abajo.
- No se publicaron commits de práctica en Dev, main ni Features de compañeros.
- Se conservó la identidad Git que ya figuraba en el proyecto: kevinIllanes. No se atribuyeron operaciones a otros integrantes.

## Lo mínimo que te falta

1. Abre la carpeta indicada en VS Code. Abajo debe decir **Feature_PlayerMovement**.
2. Abre el gráfico de Source Control y captura la rama, el commit de movimiento y la integración. Abre también tu Feature en GitHub y captura los commits publicados cuando finalice el Push.
3. En Unity 6000.5.5f1 abre `Assets/00_Scenes/SampleScene.unity`, pulsa Play y prueba WASD y las flechas. Revisa Console y captura el juego funcionando. La compilación batch, si termina bien, no reemplaza esta prueba visual.
4. Lee la bitácora y revisa las reflexiones con tus propias palabras.
5. Para cumplir íntegramente la guía, completa las capturas de Source Control y las prácticas colaborativas de los casos 04 y 05 con tus compañeros, como se explica abajo.

## Alcance de estas evidencias

Las operaciones se ejecutaron automáticamente con Git por línea de comandos. Los TXT son salidas reales de Git; las imágenes PNG son representaciones de esos registros, no capturas de VS Code. El informe HTML permite consultarlas juntas.

El PDF pide trabajar principalmente desde Source Control de VS Code. Los registros técnicos no prueban que el estudiante haya operado esa interfaz. Tampoco se inventaron capturas de Unity ni participación de compañeros.

| Caso | Demostración técnica preparada | Evidencia del PDF todavía pendiente |
|---|---|---|
| 02 | Rama local creada desde main, origen comprobado y corregido a Dev; Feature actual integrada con Dev. | Capturas en VS Code/GitHub y cada compañero en su Feature. La rama auxiliar de práctica es local. |
| 03 | Commit local en Dev, conservación del mismo commit en tu Feature y restauración de Dev. | Captura de Source Control y prueba visual del movimiento en Unity. |
| 04 | Bloqueo real al cambiar de rama, stash WIP interaction y recuperación exacta del contenido, en una copia de laboratorio. | Repetición con integrante 2 en Source Control y sus capturas. |
| 05 | Fuera de la responsabilidad de Integrante 1; reasignado por el equipo. Los registros locales anteriores son solo archivo histórico y no se presentan como trabajo personal. | No es requisito pendiente de Integrante 1. La persona responsable coordina y presenta sus propias evidencias. |

No marques como terminados los requisitos visuales o colaborativos que todavía no realizaste.

## Caso 02: ramas

La rama de práctica `Practica/Caso02-Origen` nació primero de main. Se comprobó que ese commit era distinto de Dev. Se retiró solamente esa rama sin trabajo útil y se volvió a crear desde Dev. Quedó local para revisar su posición en el gráfico.

En tu Feature se conservó su historia anterior y se integró Dev mediante merge. No se borró ni reemplazó el trabajo de tus compañeros.

Capturas: Graph con repositorio y ramas visibles; selector de ramas de GitHub; cada integrante en su Feature.

## Caso 03: commit equivocado

El movimiento original ya estaba publicado. Por eso se documenta una adaptación: se añadió soporte de flechas sin retirar el código compartido.

- Dev antes y después de la corrección: `6ccd160ee69a80ea70da65c57852380b790929b0`.
- Commit de práctica: `44492f6e60af49d4f829a7b98f31d64bf273d4cd`, mensaje `feat: add player movement`.
- El commit se creó en Dev **sin hacer Push**.
- Se conservó el mismo commit en Feature_PlayerMovement mediante merge.
- Se devolvió la referencia local de Dev a su punto anterior, comprobando que el commit ya era alcanzable desde la Feature.
- La referencia `Practica/Caso03-CommitEquivocado` conserva el commit para verlo en el gráfico.

Esta corrección fue local. No se reescribió historia compartida de Dev.

Capturas: commit equivocado (el registro conserva su estado original), Feature con el commit, historial final y Unity funcionando. Si necesitas demostrar la operación visual en Source Control, realiza una nueva práctica acordada; no retires commits ya compartidos.

## Caso 04: laboratorio local de Stash

Se trabajó en una copia dentro de:

`.git/laboratorio-integrante1/caso04`

Se modificó una misma línea de PlayerInteraction en dos ramas de práctica para producir de verdad la advertencia de sobrescritura. Se guardó `WIP interaction`, se cambió de rama, se regresó y se aplicó. Se compararon los bytes del archivo antes y después: eran idénticos. Al finalizar se retiró solamente ese cambio de prueba y se conservó el stash.

Si quieres repetirlo visualmente sin tocar el proyecto principal:

1. En otra ventana de VS Code abre la carpeta de caso04 indicada arriba. Confirma que la rama sea Feature_Interaction.
2. En Source Control aplica el stash `WIP interaction`. Captura el cambio pendiente.
3. Intenta cambiar a `Practica/StashDestino`. Captura la advertencia y cancela.
4. Guarda los cambios nuevamente con Stash y nombre `WIP interaction`. Captura el stash y el estado limpio.
5. Cambia a Practica/StashDestino, regresa a Feature_Interaction y aplica el stash más reciente.
6. Captura el código recuperado. Compruébalo antes de retirar el stash.

Esta copia está aislada. No hagas Push desde ella. Para acreditar la participación del integrante 2, repitan la práctica juntos en su copia de trabajo.

## Caso 05: fuera de la responsabilidad de Integrante 1

El equipo reasignó este caso. No corresponde a Integrante 1 realizarlo ni presentar como propias las evidencias anteriores de la simulación local. Los archivos `Evidencias/05-*` y las secciones de Caso 05 del informe se conservan únicamente como archivo histórico de preparación; no acreditan participación colaborativa en GitHub. La persona responsable debe coordinar y registrar sus propias operaciones y capturas.

## Registros disponibles

- [02-01-origen-equivocado.txt](Evidencias/02-01-origen-equivocado.txt)
- [02-02-origen-corregido.txt](Evidencias/02-02-origen-corregido.txt)
- [03-01-commit-equivocado-local.txt](Evidencias/03-01-commit-equivocado-local.txt)
- [03-02-commit-conservado-y-Dev-corregida.txt](Evidencias/03-02-commit-conservado-y-Dev-corregida.txt)
- [04-01-cambios-y-bloqueo-real.txt](Evidencias/04-01-cambios-y-bloqueo-real.txt)
- [04-02-stash-y-recuperacion.txt](Evidencias/04-02-stash-y-recuperacion.txt)
- [05-01-preparacion-local-y-rechazo-real.txt](Evidencias/05-01-preparacion-local-y-rechazo-real.txt)
- [05-02-pull-integracion-y-push-local.txt](Evidencias/05-02-pull-integracion-y-push-local.txt)

Abre [el informe visual](INFORME.html) para consultar los registros. Lee también [la bitácora](BITACORA.md), [las reflexiones](REFLEXIONES.md) y [la validación](VALIDACION.md).
