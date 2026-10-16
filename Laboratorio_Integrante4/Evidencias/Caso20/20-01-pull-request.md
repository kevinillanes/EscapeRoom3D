# Caso 20 — Pull Request

## Feature_Animation hacia Dev

- Estado: aprobado, fusionado y cerrado.
- Rama origen: `Feature_Animation`
- Rama destino: `Dev`
- Pull Request #1: https://github.com/kevinillanes/EscapeRoom3D/pull/1

## Revisión colaborativa completada

Carolina Lohrmann realizó la revisión desde su cuenta, solicitó una corrección, comprobó el ajuste y aprobó el Pull Request.

## Primera revisión de Carolina

Carolina Lohrmann señaló que `ChestController` activaba el canvas y llamaba a `BeginPuzzle` incluso si no encontraba un `PuzzleManager`. Solicitó validar esa referencia antes de mostrar la interfaz para evitar un canvas abierto sin un puzzle disponible.

Comentario: https://github.com/kevinillanes/EscapeRoom3D/pull/1#discussion_r4295724666

La corrección busca primero el `PuzzleManager`, comprueba nuevamente si sigue siendo nulo y, en ese caso, registra una advertencia, muestra el mensaje al jugador y termina la interacción. El canvas solo se activa después de superar esa validación.

- Commit de corrección: `3c01b17` — `fix: validate puzzle manager before opening UI`
- Revisión final: aprobada por `carolinalohrmann`.
- Merge en `Dev`: `41e80fd` — `Merge pull request #1 from kevinillanes/Feature_Animation`
- Prueba final: `Capturas/Caso20/20-07-prueba-final-unity.mp4`

## Dev hacia main

El primer Pull Request y la prueba final ya están completados. El siguiente paso es crear el Pull Request de `Dev` hacia `main`.
