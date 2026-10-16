# Bitácora — Integrante 4

**Estudiante:** Kevin Illanes  
**Parte del juego:** animaciones del cofre y puerta, llave y estado del juego  
**Rama:** `Feature_Animation`  
**Casos coordinados:** 16, 17, 18, 19 y 20  
**Repositorio:** https://github.com/kevinillanes/EscapeRoom3D

## Funcionalidad desarrollada

El juego cuenta con cámara fija, dinosaurio visible con animación procedural al caminar, cofre animado, llave recogible, control del estado de la partida y puerta de salida. El jugador resuelve el código `1234`, abre el cofre, recoge la llave con `E` y finalmente abre la puerta con `E`.

Commits principales:

- `3d445cb` — `feat: add procedural dinosaur movement animation`
- `5d6e095` — `feat: add animated chest key and escape flow`

## Caso 16 — Accept Both rompe el juego

**Estado:** provocado, comprobado y corregido.

Se preparó una base común y dos ramas que modificaban la misma línea de inicialización:

- `Caso16/Initialize-A`: `32c63ad` — llama a `Initialize()`.
- `Caso16/Initialize-B`: `1ff6f79` — llama a `InitializeGameState()`.

Al unirlas en `Caso16/Integracion-Kevin` apareció un conflicto real. Para reproducir el error se conservaron ambas llamadas, equivalente a elegir **Accept Both**. El merge quedó en `2c3a811`.

Unity ejecutó la inicialización dos veces y dejó estos mensajes:

```text
[CASO 16] Initialize ejecutado 1 vez/veces.
[CASO 16] Initialize ejecutado 2 vez/veces.
```

El texto se podía combinar, pero la lógica no: `InitializeGameState()` terminaba llamando también a `Initialize()`. La solución fue dejar una sola llamada. El arreglo está en `a680f06` — `fix: remove duplicate game initialization`.

## Caso 17 — Revert de un cambio compartido

**Estado:** provocado y revertido sin borrar la historia.

En `Caso17/Revert-Kevin` se cambió intencionalmente la condición de `DoorController` para que la puerta rechazara al jugador cuando sí tenía la llave. El cambio roto se publicó como:

- `7e3af48` — `test: break door key validation for case 17`

Como ya estaba compartido, no se reescribió la rama. Se aplicó `git revert` y se creó:

- `6845a2e` — `Revert "test: break door key validation for case 17"`

La condición correcta volvió y los dos commits permanecen visibles en el historial.

## Caso 18 — Recuperación con Reflog

**Estado:** commit perdido de la rama y recuperado con su hash exacto.

Se creó `RECOVERY_TEST.md` y se guardó en:

- `426fd14eb5186f5e1a7449aba0a06b9aa09d2853` — `test: recovery point`

Después se movió la rama un commit hacia atrás. El archivo desapareció del árbol de trabajo, pero `git reflog` mostró:

```text
5d6e095 HEAD@{0}: reset: moving to HEAD~1
426fd14 HEAD@{1}: commit: test: recovery point
```

El commit se recuperó creando `Caso18/Recovered-Kevin` desde el hash completo. Se comprobó que `RECOVERY_TEST.md` volvió y que la rama apunta exactamente a `426fd14eb5186f5e1a7449aba0a06b9aa09d2853`.

## Caso 19 — Cherry-pick selectivo

**Estado:** tres commits creados; solo la corrección fue integrada en `Dev`.

La rama `Caso19/Feature-Puzzle-Source` contiene, en orden:

1. `18e42f4` — `docs: add puzzle ui note`
2. `50ead2b` — `fix: correct puzzle validation`
3. `9f5b72f` — `docs: add future puzzle hint note`

En `Dev` se aplicó únicamente `50ead2b` mediante `git cherry-pick`. Como el hash cambia al copiar el commit a otra historia, el resultado en `Dev` es:

- `36b5750` — `fix: correct puzzle validation`

La verificación dio `False` para los dos archivos de notas en `Dev`; por lo tanto, los commits 1 y 3 no entraron. La corrección elimina una marca temporal y normaliza el texto con `Trim()` antes de compararlo con `1234`.

## Caso 20 — Pull Requests finales

`Feature_Animation` fue actualizada con la versión más reciente de `Dev`. El conflicto en `PuzzleManager.cs` se resolvió conservando tanto `Configure(...)`, necesario para construir el juego, como la validación normalizada del caso 19.

El Pull Request #1 recibió una revisión real de Carolina Lohrmann. Ella solicitó validar `PuzzleManager` antes de activar el canvas del puzzle. La corrección se realizó en `3c01b17`, Carolina volvió a revisar y aprobó el cambio, y el PR fue integrado en `Dev` mediante `41e80fd`.

La prueba final quedó registrada en `Capturas/Caso20/20-07-prueba-final-unity.mp4`. El video muestra el movimiento y la animación del dinosaurio, el código `1234`, el cofre, la llave y la puerta hasta completar el escape.

