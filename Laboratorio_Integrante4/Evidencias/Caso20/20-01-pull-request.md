# Caso 20 — Pull Request

## Feature_Animation hacia Dev

- Estado: abierto, esperando revisión de otro integrante.
- Rama origen: `Feature_Animation`
- Rama destino: `Dev`
- Pull Request #1: https://github.com/kevinillanes/EscapeRoom3D/pull/1

## Revisión colaborativa pendiente

Otro integrante debe dejar un comentario de revisión real, solicitar al menos una corrección verificable y aprobar después del ajuste. Kevin no debe aprobar su propio trabajo.

## Primera revisión de Carolina

Carolina Lohrmann señaló que `ChestController` activaba el canvas y llamaba a `BeginPuzzle` incluso si no encontraba un `PuzzleManager`. Solicitó validar esa referencia antes de mostrar la interfaz para evitar un canvas abierto sin un puzzle disponible.

Comentario: https://github.com/kevinillanes/EscapeRoom3D/pull/1#discussion_r4295724666

La corrección busca primero el `PuzzleManager`, comprueba nuevamente si sigue siendo nulo y, en ese caso, registra una advertencia, muestra el mensaje al jugador y termina la interacción. El canvas solo se activa después de superar esa validación.

## Dev hacia main

Este segundo Pull Request se crea después de integrar el primero y completar la prueba final de Unity.
