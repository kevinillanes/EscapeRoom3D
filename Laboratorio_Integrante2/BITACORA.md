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

## Antes del merge — las tres carpetas .vscode

Antes de poder integrar nada me topé con algo que no está en ningún caso de la guía. Al intentar el merge desde VS Code me salía un cuadro que decía *"Git: Aborting"* y el merge no se hacía. Lo intenté dos veces y las dos veces igual, sin ningún mensaje que explicara nada.

Al final lo corrí desde la terminal para ver el error completo:

```
error: The following untracked working tree files would be overwritten by merge:
	.vscode/extensions.json
	.vscode/launch.json
	.vscode/settings.json
Please move or remove them before you merge.
Aborting
```

Lo que pasaba es que Estefanía había subido la carpeta `.vscode` en su rama, y yo tenía esos mismos tres archivos en mi disco pero sin commitear. Git no los iba a sobrescribir porque son míos y no los conoce, así que prefería cancelar el merge antes que borrarme algo. Le cambié el nombre a mi carpeta local (`.vscode_respaldo_local`) y el merge pasó sin problema.

**Qué aprendí:** VS Code no siempre te muestra el motivo real. Cuando algo falla sin explicación, el mismo comando en la terminal te lo dice completo.

---

## Caso 06 — Conflicto en PlayerController.cs

**¿Provocado?** Sí · **¿Resuelto?** Sí
**Evidencias:** `caso06_conflicto.png`, `caso06_merge_editor.png`, `caso06_resultado.png`, `caso06_unity.png`

Después de integrar la rama del puzzle (que entró limpia, sin conflictos, porque Git solo tuvo que adelantar el puntero), integré la rama del movimiento. Ahí sí salió el conflicto:

```
Auto-merging Assets/01_Scripts/Player/PlayerController.cs
CONFLICT (add/add): Merge conflict in Assets/01_Scripts/Player/PlayerController.cs
```

Dice *add/add* y no *content* porque las dos ramas crearon el archivo por separado en vez de partir del mismo. Para Git son dos archivos distintos que casualmente se llaman igual, así que no tiene ninguna versión común con la que comparar y marca todo el archivo como conflicto.

Lo abrí en el Merge Editor y leí los dos lados. **Current** era la versión que estaba en Dev y **Incoming** la de Feature_PlayerMovement. Comparándolas vi que la de Carolina tenía exactamente las mismas teclas WASD que la de Dev y además las flechas del teclado. O sea que una contenía a la otra. Construí el Result tomando su versión completa y revisé que no quedara nada afuera.

**Criterio:** se conservó la versión de Feature_PlayerMovement porque incluye las mismas teclas WASD que la versión de Dev y además añade las flechas del teclado. No se perdió ninguna funcionalidad de ninguna de las dos ramas.

Guardé, el archivo dejó de aparecer en Merge Changes y lo pasé a Staged.

**Qué aprendí:** resolver texto es decidir qué letras quedan; resolver software es decidir qué comportamiento queda. Aquí los dos lados se veían parecidos, pero lo que había que mirar no era cuál tenía más líneas sino qué teclas dejaba de responder cada opción. Por eso se prueba en Unity después y no se confía en que el archivo compile.

---

## Caso 08 — Conflicto en la escena

**¿Provocado?** Sí · **¿Resuelto?** Sí
**Evidencias:** `caso08_conflicto.png`, `caso08_hierarchy.png`, `caso08_escena.png`, `caso08_playmode.png`

En el mismo merge, junto con el script, entró en conflicto la escena:

```
Auto-merging Assets/00_Scenes/SampleScene.unity
CONFLICT (content): Merge conflict in Assets/00_Scenes/SampleScene.unity
```

Este fue el más delicado de todos. La escena tenía 3767 líneas y Git marcó 14 zonas en conflicto. Un archivo `.unity` no es código, es una lista de objetos serializados con identificadores internos, y si se resuelve mal el proyecto abre igual pero la escena queda rota, y eso no se nota hasta darle Play.

Antes de tocar nada saqué las tres versiones por separado (la base común, la de Dev y la de Feature_PlayerMovement) y las comparé objeto por objeto en vez de línea por línea. Eso cambió todo:

| | Dev | Feature_PlayerMovement |
|---|---|---|
| Objetos en la escena | 27 | 5 |
| Componentes en el Player | Transform, PlayerController, **PlayerInteraction**, Collider, Rigidbody | Transform, PlayerController, Collider, Rigidbody |
| Rigidbody del Player | `m_UseGravity: 0` | `m_UseGravity: 1` |

Lo primero que revisé fue si los objetos que estaban en las dos ramas tenían el mismo `fileID`, porque si no coinciden quedan duplicados al juntarlos. Coincidían: el Player es el `437900929` y el piso el `2031225365` en las dos. O sea que eran el mismo objeto y no dos copias.

Lo segundo fue que la rama del movimiento no agregaba ningún objeto nuevo que Dev no tuviera ya. De los 14 conflictos, 12 eran espacios en blanco al final de líneas `m_Name:`, diferencia de cómo guarda el archivo cada editor, nada funcional.

**Criterio:** se conservó la versión de Dev, que contenía todos los objetos del cofre, el PuzzleManager, el canvas y las pistas, además de mi componente PlayerInteraction en el Player. De la rama Feature_PlayerMovement se tomó el único cambio funcional que aportaba: `m_UseGravity: 1` en el Rigidbody del Player, que es lo que hace que el jugador se apoye en el piso en vez de flotar. Antes de completar el merge verifiqué que la lista `SceneRoots` tuviera los 15 objetos raíz y que cada objeto apareciera una sola vez.

Después abrí la escena en Unity, revisé el Hierarchy objeto por objeto y probé el Play.

**Por qué conviene no editar la misma escena a la vez:** porque una escena no se puede partir en pedazos. Dos personas pueden editar dos scripts distintos y Git los junta solo; en una escena cualquier cosa que toque cada una queda escrita en el mismo archivo, revuelta con todo lo demás, y Git no tiene forma de saber que un bloque de texto es un cofre y otro es una puerta.

**Cómo lo organizaría:** que una sola persona sea dueña de la escena por turnos, y que lo demás se haga en Prefabs. Cada uno arma su parte en su propio Prefab, lo sube, y el dueño de la escena lo arrastra adentro. Así el archivo de la escena lo toca uno solo y los conflictos se mueven a archivos que sí se pueden separar.

---

## Casos pendientes

- **Caso 05** (push rechazado) y **Casos 07, 09 y 10** los voy a hacer con Kevin, porque necesitan a dos personas trabajando desde la misma base.
- **Caso 10:** no se puede hacer con `PuzzleManager.cs` como dice el enunciado, porque ese archivo ya existe en el proyecto desde la rama del puzzle. Lo vamos a hacer con `GameManager.cs`, que nadie ha creado todavía, para que el Add/Add sea real.
- **Caso 18** (recuperación de trabajo perdido): la carpeta `Laboratorio_Integrante1` desapareció de todas las ramas después de un push forzado. El commit todavía existe como objeto en el repositorio, así que se puede recuperar. Eso es exactamente lo que pide el caso 18.
