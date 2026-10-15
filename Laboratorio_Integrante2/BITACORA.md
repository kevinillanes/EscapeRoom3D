# Bitácora — Escape Room 3D

**Integrante 2:** Saúl Álvarez Alanes
**Parte del juego:** interacción con objetos
**Rama:** Feature_Interaction
**Repositorio:** https://github.com/kevinillanes/EscapeRoom3D

Equipo: Carolina Lohrmann (movimiento), Saúl Álvarez (interacción), Estefanía Laredo (puzzle y cofre), Kevin Illanes (animaciones, llave y estado del juego).

---

## Caso 02 — Dev y las ramas Feature

**¿Provocado?** Sí · **¿Resuelto?** Sí
**Evidencias:** `caso02_problema.png`, `caso02_borrada.png`, `caso02_corregido.png`

Cuando fui a trabajar en mi rama me di cuenta de que estaba vacía. No tenía ni los scripts ni la escena, solo el README y el .gitignore. En GitHub, en la pantalla de Branches, decía que estaba 2 commits por detrás y 0 por delante.

Lo que pasó es que mi rama se había creado desde el commit inicial del repositorio y no desde Dev. Las de Estefanía y Kevin estaban igual.

Antes de tocar nada revisé la columna Ahead: estaba en 0, o sea que no había ningún trabajo mío ahí que pudiera perder. Avisé al grupo, borré la rama remota desde GitHub, la volví a crear en local partiendo de Dev con el mismo nombre y la publiqué. Es lo que dice la propia resolución del Caso 02 para una Feature que nació del punto equivocado.

**Qué aprendí:** una rama no es una copia de los archivos, es un puntero a un commit. De donde nace decide todo lo que contiene. Por eso una rama puede tener el nombre correcto y aun así estar basada en un estado del proyecto que ya quedó atrás.

---

## Caso 03 — Commit en la rama equivocada

**¿Provocado?** Sí (por el integrante 1) · **¿Resuelto?** Parcialmente

Este caso no me tocaba a mí, pero lo anoto porque afecta a Dev: `PlayerController.cs` quedó tanto en Feature_PlayerMovement como en Dev, y el caso pide que Dev vuelva al estado anterior. Queda pendiente de cerrar con Carolina.

---

## Caso 04 — Cambios locales y Stash

**¿Provocado?** Sí · **¿Resuelto?** Sí
**Evidencias:** `caso04_sin_commit.png`, `caso04_advertencia.png`, `caso04_limpio.png`, `caso04_recuperado.png`

Primero creé `PlayerInteraction.cs` e `IInteractable.cs` y los subí. Después empecé a mejorar el script para que lanzara un rayo desde la cámara, y a mitad de ese trabajo intenté cambiarme a la rama Dev sin haber hecho commit.

Git no me dejó. Salió el aviso *"Your local changes to the following files would be overwritten by checkout"*. Es lógico: el archivo no existe en Dev, así que para cambiarme de rama Git tendría que borrarlo del disco, y tenía cambios míos sin guardar.

Ahí entra el Stash. Guardé el trabajo con *Stash (Include Untracked)* con el nombre `WIP interaction`, y el Working Tree quedó limpio. Me cambié a Dev, comprobé que el archivo no estaba, volví a mi rama e hice *Pop Stash*. El código volvió entero y recién entonces hice commit.

**Un problema que tuve:** el enunciado dice que creas el archivo, lo guardas sin commitear y al cambiar de rama sale la advertencia. A mí no me salía. Estuve un rato pensando que lo estaba haciendo mal. Resulta que con un archivo **recién creado** la advertencia no aparece nunca, porque Git no protege archivos que todavía no conoce: te deja cambiar de rama y se los lleva consigo. La advertencia solo sale cuando el archivo ya está en el historial y tiene cambios sin guardar. Por eso tuve que hacer primero el commit y modificarlo después.

**Otro detalle:** ejecuté el comando de Stash dos veces sin darme cuenta y me quedaron dos cajones guardados. La segunda vez salió *"There are no changes to stash"*, y al principio pensé que había fallado, cuando en realidad era que el primero ya se había llevado todo. Después borré el duplicado.

**Qué aprendí:** Stash resuelve un choque entre lo que tienes en el disco y lo que la rama espera tener. Y ese choque solo existe para los archivos que Git ya está siguiendo.

**Commits:**
- `8139e34` — feat: agrega interacción del jugador
- `69fd015` — feat: completa la interacción con raycast

---

## Nota de configuración — Active Input Handling

El proyecto estaba puesto en **Input System Package (New)**, pero todo el código del Anexo C de la guía usa `Input.GetKeyDown`, que es del sistema de entrada antiguo. Con esa configuración, al pulsar E saltaba una excepción en tiempo de ejecución.

Lo hablé con el grupo y lo cambiamos a **Both**, que deja los dos sistemas activos. Es una sola línea en ProjectSettings. Preferimos eso antes que reescribir el código, porque la guía pide usar su código exacto y además habría habido que tocar archivos de tres personas distintas.

Commit: `d94cf7d` — chore: habilita Both en Active Input Handling

---

## Integración a Dev

Kevin pidió juntar mi rama con Dev para que los cuatro trabajáramos sobre la misma versión. El merge no dio ningún conflicto porque mi rama era Dev más mis cinco commits, así que Git solo tuvo que adelantar el puntero.

Con eso llegaron a Dev el sistema de interacción, el componente ya puesto en el Player, un objeto de prueba y el cambio de configuración.

Commit de Dev después del merge: `6ccd160`

---

## Prueba de integración con el trabajo de otra persona

**Evidencia:** `caso04_cofre_funciona.png`

Cuando Estefanía subió su cofre lo probé desde su rama. Pulsé E delante del cofre y en la consola salió *"El cofre está cerrado. Necesitas resolver el puzzle."*, y apareció el panel del código.

Ese mensaje solo puede salir si mi rayo encontró el cofre y llamó a su método `Interact()`. O sea que su script y el mío se conectaron sin que ninguno de los dos tuviera que tocar el código del otro: a ella le bastó con poner `: IInteractable` en su clase.

Antes de eso tuvimos un ida y vuelta porque el commit que ella había subido no coincidía con lo que me había descrito (el cofre estaba en otra posición y la cámara se había movido). Se arregló cuando volvió a guardar la escena y subió de nuevo. Lección: en Unity hay que guardar la escena **antes** de hacer el commit, si no subes la versión anterior sin enterarte.

---

## Casos 05 al 20

Pendientes. El 05 y los del 06 al 10 necesitan a dos personas trabajando desde la misma base, así que dependen de coordinar con el resto del equipo.
