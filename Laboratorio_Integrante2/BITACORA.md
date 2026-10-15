# Bitácora — Escape Room 3D
Laboratorio de control de versiones con Git, GitHub, Visual Studio Code y Unity
Docente: Erick Salomon Cespedes Gamarra

**Integrantes**
1. Carolina Lohrmann — Movimiento del jugador — `Feature_PlayerMovement`
2. Saúl Álvarez Alanes — Interacción con objetos — `Feature_Interaction`
3. Estefanía Laredo — Puzzle y desbloqueo del cofre — `Feature_Puzzle`
4. Kevin Illanes — Animaciones, llave y estado del juego — `Feature_Animation`

Repositorio: https://github.com/kevinillanes/EscapeRoom3D

---

## Caso 01 — Repositorio y primer commit
- Provocado: —
- Resuelto: sí
- Evidencia: pendiente
- Problema: —
- Resolución: —
- Reflexión: —

## Caso 02 — Dev y ramas Feature
- Provocado: sí
- Resuelto: sí
- Evidencia: `caso02_problema.png`, `caso02_borrada.png`, `caso02_corregido.png`

**Problema.** Las ramas `Feature_Interaction`, `Feature_Puzzle` y `Feature_Animation` se crearon desde el commit inicial del repositorio y no desde `Dev`. Como consecuencia no contenían el proyecto Unity y figuraban como dos commits por detrás, con cero commits propios.

**Resolución.** Se verificó en la pantalla de Branches de GitHub que la rama no tuviera trabajo propio (columna Ahead en 0), por lo que eliminarla no suponía pérdida de información. Se eliminó la rama remota desde GitHub, se creó de nuevo en local partiendo de `Dev` conservando el mismo nombre, y se publicó. Es la resolución que indica el propio Caso 02 para una Feature nacida del punto equivocado.

**Reflexión.** Una rama no es una copia de los archivos: es un puntero a un commit. El commit del que nace determina todo lo que contiene. Por eso crear una Feature desde `main` o desde el commit inicial, en vez de desde `Dev`, produce una rama que parece correcta por el nombre pero está basada en un estado del proyecto que ya quedó atrás.

## Caso 03 — Commit en la rama equivocada
- Provocado: parcialmente
- Resuelto: pendiente
- Evidencia: pendiente

**Observación.** `PlayerController.cs` quedó tanto en `Feature_PlayerMovement` como en `Dev`. El caso pide que `Dev` vuelva al estado anterior. Pendiente de cerrar con Carolina.

## Caso 04 — Cambios locales y Stash
- Provocado: sí
- Resuelto: sí
- Evidencia: `caso04_sin_commit.png`, `caso04_advertencia.png`, `caso04_limpio.png`, `caso04_recuperado.png`

**Problema.** Había cambios sin commitear en `Assets/01_Scripts/Interaction/PlayerInteraction.cs` y era necesario cambiar a la rama `Dev`. Git rechazó el cambio de rama con el mensaje *«Your local changes to the following files would be overwritten by checkout»*, porque el archivo no existe en `Dev` y cambiar de rama habría implicado borrarlo del disco junto con las modificaciones sin guardar.

**Resolución.** Se guardó el trabajo en curso con *Stash (Include Untracked)* bajo el nombre `WIP interaction`, lo que dejó el Working Tree limpio. Se cambió a `Dev`, se comprobó que el archivo no estaba presente en esa rama, se volvió a `Feature_Interaction` y se recuperó el trabajo con *Pop Stash*. El código volvió íntegro y solo entonces se hizo commit.

**Reflexión.** El enunciado plantea el caso creando el archivo y cambiando de rama sin commitear, pero en esa situación la advertencia no aparece: Git no protege archivos que todavía no están bajo seguimiento, de modo que permite el cambio de rama y los arrastra consigo. La advertencia solo se produce cuando el archivo ya forma parte del historial y tiene modificaciones sin guardar. Entender esa diferencia fue el aprendizaje principal del caso: Stash resuelve un conflicto entre el estado del disco y el estado de la rama, y ese conflicto solo existe para archivos que Git ya conoce.

**Commits generados** (rama `Feature_Interaction`, autor Saúl Álvarez)
- `8139e34` — `feat: agrega interacción del jugador` — crea `PlayerInteraction.cs` e `IInteractable.cs`
- `69fd015` — `feat: completa la interacción con raycast` — recupera y confirma el trabajo guardado en el stash

**Estado:** cerrado. Ambos commits publicados en GitHub, Working Tree limpio.

---

## Nota de configuración — Active Input Handling

El proyecto estaba configurado con **Active Input Handling: Input System Package (New)**, pero todo el código del Anexo C de la guía (incluido `PlayerInteraction.cs`) usa `Input.GetKeyDown`, que pertenece al sistema de entrada antiguo. Con esa configuración, pulsar la tecla E lanzaba `InvalidOperationException` en tiempo de ejecución.

Se cambió a **Both**, que habilita los dos sistemas a la vez. Es una modificación de `ProjectSettings/ProjectSettings.asset` de una sola línea (`activeInputHandler: 1` → `2`) y se comunicó al equipo antes de aplicarla. Se optó por esta vía en lugar de reescribir el código para no desviarse del código exacto que entrega la guía.

Commit: `chore: habilita Both en Active Input Handling`

---

## Integración a Dev — 11 de octubre

A pedido del equipo se integró `Feature_Interaction` en `Dev` para que los cuatro integrantes trabajaran sobre la misma versión del proyecto. El merge fue *fast-forward*: `Feature_Interaction` contenía a `Dev` más cinco commits, de modo que no hubo conflictos.

Con esa integración llegaron a `Dev` el sistema de interacción (`PlayerInteraction` e `IInteractable`), el componente ya asignado al Player en la escena, un objeto de prueba temporal y el cambio de Active Input Handling a Both.

Commit de cabecera de `Dev` tras el merge: `6ccd160`

---

## Caso 05 — Push rechazado porque Dev avanzó
- Provocado: no
- Resuelto: no
- Pendiente: requiere que Carolina (integrante A) suba primero su cambio al README. Saúl modifica otra sección del README sin hacer Pull y recibe el rechazo.

## Casos 06 al 20
Pendientes.
